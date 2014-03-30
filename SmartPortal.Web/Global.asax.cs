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
            var databaseConfiguration = new DatabaseConfiguration("localhost", 8081, "smartportal");
            var activitySystem = new ActivitySystem(databaseConfiguration) { Device = device };

            Portal.Instance().ActivitySystem = activitySystem;

            //  HANDLERS
            activitySystem.UserAdded += activitySystem_UserAdded;
            activitySystem.UserChanged += activitySystem_userChanged;

            activitySystem.StartLocationTracker();
            activitySystem.Tracker.TagEnter += Tracker_TagEnter;

            
            /*
            
            //Start a activityservice which wraps an activity system into a REST service
            var activityService = new ActivityService(activitySystem, "127.0.0.1", 8060);
            activityService.Start();

            //make the system discoverable on the LAN
             activityService.StartBroadcast(DiscoveryType.Zeroconf, "smartPortalActivitySystem", "smartPortal", "1234");
              */
        }
     
        private void Tracker_TagEnter(Detector detector, TagEventArgs e)
        {
            Portal.Instance().HandleTagEnter(e.Tag.Id, e.Tag.Name, detector.Name);
        }

        private void activitySystem_userChanged(object sender, UserEventArgs e)
        {
            var user = e.User as Patient;
            if (user != null)
                PatientsManager.Instance.BroadcastUserUpdated(PatientViewModel.CreateFromPatient(user));
        }

        
        static void activitySystem_UserAdded(object sender, UserEventArgs e)
        {
            var user = e.User as Patient;
            if (user != null)
                PatientsManager.Instance.BroadcastUserAdded(PatientViewModel.CreateFromPatient(user));
        }

        
    }
}