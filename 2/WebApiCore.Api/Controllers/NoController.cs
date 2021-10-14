using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiCore.Data.Models;
using WebApiCore.Data.Repository;

namespace WebApiCore.Api.Controllers
{
    [NonController]
    public class NoController : ControllerBase
    {
        private readonly ILogger<NoController> logger;

        public NoController(
            ILogger<NoController> logger
            )
        {
            this.logger = logger;
        }
    }
}