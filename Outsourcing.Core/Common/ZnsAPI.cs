using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Core.Common
{
    public class ZnsAPI
    {
        private string accesstoken { get; set; }

        public ZnsAPI(string accesstoken)
        {
            this.accesstoken = accesstoken;
        }

        public async Task<OTPCallBack> SendZnsOTP(string phone,string templateid,string otp,string trackingid = "tracking_id")
        {
            using (HttpClient client = new HttpClient())
            {               
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("access_token", this.accesstoken);

                string url = "https://business.openapi.zalo.me/message/template";

                var body = new
                {
                    //mode = "development",
                    phone = String.Concat("84", phone.Substring(1)),
                    template_id = templateid,
                    template_data = new
                    {
                        otp = otp,
                    },
                    tracking_id = "tracking_id"
                };

                //var templatedict = new Dictionary<string, string>();
                //templatedict.Add("otp", otp);

                //var dict = new Dictionary<string, string>();
                ////dict.Add("mode", "development");
                //dict.Add("phone", String.Concat("84", phone.Substring(1)));
                //dict.Add("template_id", templateid); // 7895417a7d3f9461cd2e
                //dict.Add("template_data", JsonConvert.SerializeObject(templatedict)); // JsonConvert.SerializeObject(templatedict)
                //dict.Add("tracking_id", "tracking_id");
                //HttpResponseMessage response = await client.PostAsync(
                //    "https://business.openapi.zalo.me/message/template", dict);

                var myContent = JsonConvert.SerializeObject(body);

                var stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, stringContent);
                

                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsStringAsync();
                    OTPCallBack call = JsonConvert.DeserializeObject<OTPCallBack>(await res);
                    return call;
                }
            }
            return null;
        }
    }
    public class OTPCallBack
    {
        public int error { get; set; }
        public string message { get; set; }
        public Data data { get; set; }

        public class Quota
        {
            public string remainingQuota { get; set; }
            public string dailyQuota { get; set; }
        }

        public class Data
        {
            public string sent_time { get; set; }
            public Quota quota { get; set; }
            public string msg_id { get; set; }
        }
    }
}
