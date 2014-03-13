using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartPortal.Web.Controllers_API
{
    public class Test2Controller : ApiController
    {
        public class TestClass
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        [HttpGet]
        public TestClass TestMethod()
        {
            return new TestClass
            {
                Name = "Baba",
                Surname = "Zazaza"
            };
        }
    }
}
