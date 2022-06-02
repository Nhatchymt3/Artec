using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Service;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZaloDotNetSDK;
using ZaloDotNetSDK.entities.oa;

namespace Labixa.Areas.Admin.Controllers
{
    public class ZaloOAController : Controller
    {
        private readonly ILogger _logger;
        private readonly IZaloTokenService zaloTokenService;

        public ZaloOAController(IZaloTokenService zaloTokenService)
        {
            this.zaloTokenService = zaloTokenService;
            this._logger = Log.Logger;
        }
        //Callback url for zalo
        public void GetAccessTokenCallBack(string access_token,string refresh_token,string expires_in)
        {
            var callback = zaloTokenService.GetAll().FirstOrDefault();

            callback.access_token = access_token;
            callback.refresh_token = refresh_token;
            callback.expires_in = expires_in;

            zaloTokenService.UpdateToken(callback);
        }

        public async Task GetToken()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Add("secret_key", "ZVbvHSj73N732MGGXIUX");

                var token = zaloTokenService.GetAll().FirstOrDefault();

                var dict = new Dictionary<string, string>();
                dict.Add("refresh_token", token.refresh_token);
                dict.Add("app_id", "632622750545250005");
                dict.Add("grant_type", "refresh_token");

                HttpResponseMessage response = await client.PostAsync(
                    "https://oauth.zaloapp.com/v4/oa/access_token", new FormUrlEncodedContent(dict));

                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsStringAsync();
                    ZaloToken call = JsonConvert.DeserializeObject<ZaloToken>(await res);
                    _logger.Write(Serilog.Events.LogEventLevel.Information, await res);
                    if (call.refresh_token != null)
                    {
                        token.refresh_token = call.refresh_token;
                        token.access_token = call.access_token;
                        token.expires_in = call.expires_in;
                        token.DateCreated = new DateTimeOffset(DateTime.Now);
                        zaloTokenService.UpdateToken(token);
                    }
                }
            }
            _logger.Write(Serilog.Events.LogEventLevel.Information, "Fail call");
        }
        //
        // GET: /Admin/ZaloOA/
        public async Task<ActionResult> Index()
        {
            var current = new DateTimeOffset(DateTime.Now);
            ZaloToken token = zaloTokenService.GetAll().FirstOrDefault();

            //if >= 0 : token expired
            var comp = DateTimeOffset.Compare(current.AddHours(-1), token.DateCreated);

            if(comp >= 0)
            {
                await GetToken();
                token = zaloTokenService.GetAll().FirstOrDefault();
            }
            OaAPI oaAPI = new OaAPI(token.access_token);
            //4795450247322104425
            var result = oaAPI.SendMessage("7598536841305925270", "Hello");

            _logger.Write(Serilog.Events.LogEventLevel.Information, JsonConvert.SerializeObject(result));

            return View();
        }       
    }
    public class ErrorCall
    {
        public string error_name { get; set; }
        public string error_reason { get; set; }
        public string ref_doc { get; set; }
        public string error_description { get; set; }
        public int error { get; set; }
    }
}