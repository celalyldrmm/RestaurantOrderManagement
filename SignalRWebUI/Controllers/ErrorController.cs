﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public IActionResult NotFound404Page()
        {
            return View();
        }
    }
}
