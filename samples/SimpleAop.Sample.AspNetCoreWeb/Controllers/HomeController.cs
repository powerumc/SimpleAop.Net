﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleAop.Sample.AspNetCoreWeb.Models;
using SimpleAop.Sample.AspNetCoreWeb.Services;

namespace SimpleAop.Sample.AspNetCoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService _testService;

        public HomeController(ITestService testService)
        {
            _testService = testService;
        }

        public IActionResult Index()
        {
            _testService.PrintLog();
            
            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = await _testService.GetGoogleHtmlAsync();

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}