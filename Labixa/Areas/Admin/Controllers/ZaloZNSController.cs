using Newtonsoft.Json;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Service;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    public class ZaloZNSController : Controller
    {
        private readonly ILogger _logger;
        private readonly IZaloTokenService zaloTokenService;

        public ZaloZNSController(IZaloTokenService zaloTokenService)
        {
            this.zaloTokenService = zaloTokenService;
            this._logger = Log.Logger;
        }

        //
        // GET: /Admin/ZaloZNS/
        public async Task<ActionResult> Index()
        {
            var current = new DateTimeOffset(DateTime.Now);
            ZaloToken token = zaloTokenService.GetAll().FirstOrDefault();

            //if >= 0 : token expired
            var comp = DateTimeOffset.Compare(current.AddHours(-1), token.DateCreated);

            if (comp >= 0)
            {
                await new ZaloOAController(zaloTokenService).GetToken();
                //await GetToken();
                token = zaloTokenService.GetAll().FirstOrDefault();
            }
            ZnsAPI znsAPI = new ZnsAPI(token.access_token);
            //4795450247322104425
            var result = await znsAPI.SendZnsOTP("0786321015", "217580", "CuongTest");

            _logger.Write(Serilog.Events.LogEventLevel.Information, JsonConvert.SerializeObject(result));

            return View();
        }
    }
}