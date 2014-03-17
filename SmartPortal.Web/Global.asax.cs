using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NooSphere.Infrastructure.ActivityBase;
using NooSphere.Infrastructure.Context.Location;
using NooSphere.Infrastructure.Discovery;
using NooSphere.Infrastructure.Helpers;
using NooSphere.Model.Device;
using NooSphere.Model.Users;
using Owin;
using SmartPortal.Model;
using SmartPortal.Web.Hubs;
using SmartPortal.Web.Infrastructure;
using SmartPortal.Web.ViewModels;

namespace SmartPortal.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //RouteTable.Routes.MapHubs();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            //AreaRegistration.RegisterAllAreas(); 
            //Create a user
            var user = new User
            {
                Name = "SmartPortalServer"
            };
            //Create a device
            var device = new Device
            {
                DeviceType = DeviceType.Desktop,
                DevicePortability = DevicePortability.Stationary,
                Owner = user
            };
            //create databaseconfiguration
            var databaseConfiguration = new DatabaseConfiguration("max", 8081, "smartportal");
            var activitySystem = new ActivitySystem(databaseConfiguration)
            {
                Device = device,
                Tracker = new LocationTracker("10.242.2.10") //vpn
            
            };
            //activitySystem.

            //var userActivitySystem = new ActivitySystem()

            Portal.Instance().ActivitySystem = activitySystem;

            activitySystem.StartLocationTracker();


            //  HANDLERS
            activitySystem.ActivityAdded += activitySystem_ActivityAdded;
            activitySystem.DeviceAdded += activitySystem_DeviceAdded;
            activitySystem.UserAdded += activitySystem_UserAdded;
            activitySystem.UserChanged += activitySystem_userChanged;
            activitySystem.SubscribeToTagMoved(activity_HandleTagMoved);

            /*
            //Start a activityservice which wraps an activity system into a REST service
            var activityService = new ActivityService(activitySystem, "127.0.0.1", 8060);
            activityService.Start();

            //make the system discoverable on the LAN
             activityService.StartBroadcast(DiscoveryType.Zeroconf, "smartPortalActivitySystem", "smartPortal", "1234");
             */ 


        }

        private void activity_HandleTagMoved(Detector detector, TagEventArgs e)
        {
            PatientsManager.Instance.BroadcastRecordLoactionChange("bf4cca52-19b0-4532-b367-f914c09e1c96", e.Tag.Detector.Name);
        }


        
        private void activitySystem_userChanged(object sender, UserEventArgs e)
        {


            var user = e.User as Patient;
            if (user != null)
                PatientsManager.Instance.BroadcastUserUpdated(PatientViewModel.CreateFromPatient(user));


        }

        static void activityClient_DeviceAdded(object sender, DeviceEventArgs e)
        {
            Console.WriteLine("Device {0} received from activityclient over http", e.Device.Name);

            Console.WriteLine("Associated user is {0}", e.Device.Owner.Name);
        }

        static void activityClient_UserAdded(object sender, UserEventArgs e)
        {
            Console.WriteLine("User {0} received from activityclient over http", e.User.Name);
        }

        static void activityClient_ActivityAdded(object sender, NooSphere.Infrastructure.ActivityEventArgs e)
        {
            Console.WriteLine("Activity {0} received from activityclient over http", e.Activity.Name);
        }

        static void activitySystem_UserAdded(object sender, UserEventArgs e)
        {
            //if (typeof(Patient) != e.User.GetType()) return;

            var user = e.User as Patient;
            if (user != null)
                PatientsManager.Instance.BroadcastUserAdded(PatientViewModel.CreateFromPatient(user));

            Console.WriteLine("User {0} received directly from activitysystem", e.User.Name);
        }

        static void activitySystem_DeviceAdded(object sender, DeviceEventArgs e)
        {
            Console.WriteLine("Device {0} received directly from activitysystem", e.Device.Name);
        }

        static void activitySystem_ActivityAdded(object sender, NooSphere.Infrastructure.ActivityEventArgs e)
        {
            Console.WriteLine("Activity {0} received directly from activitysystem", e.Activity.Name);
        }
    }
}