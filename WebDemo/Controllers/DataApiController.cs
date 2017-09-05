﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENV.Labs;

namespace WebDemo.Controllers
{
    public class DataApiController : Controller
    {
        static DataApiController()
        {
            ViewModelHelper.RegisterEntity("Products",typeof(Northwind.Models.Products));
        }
        // GET: DataApi
        public void Index(string name, string id = null)
        {
            ViewModelHelper.ProcessRequest(name, id);
        }
    }
}