using Microsoft.AspNet.SignalR;
using MSniperService.Enums;
using MSniperService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MSniperService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.EnableSearch = true;
            return View(/*new List<EncounterInfo>()*/);
        }

        [Route("Home/Panel")]
        public ActionResult AdminPanel()
        {
            var xRightList = msniperData.Instance.GetRareList();

            return View(xRightList);
        }
    }
}