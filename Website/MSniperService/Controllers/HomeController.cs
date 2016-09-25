using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using MSniperService.Cache;
using MSniperService.Enums;
using MSniperService.Models;

namespace MSniperService.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.EnableSearch = true;
            return View(CacheManager<EncounterInfo>.Instance.GetAll().OrderBy(p => p.Iv).ToList());
        }

        [Route("Home/Panel")]
        public ActionResult AdminPanel()
        {
            var xRightList = CacheManager<RareList>.Instance.GetCache("RareList");
            if (xRightList == null)
            {
                xRightList = msniperHub.defaultRareList;
            }
            return View(xRightList);
        }



    }
}