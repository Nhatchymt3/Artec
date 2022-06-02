using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaloDotNetSDK;
using ZaloDotNetSDK.entities.oa;

namespace Outsourcing.Core.Common
{
    public class OaAPI
    {
        private string accesstoken { get; set; }
        public OaAPI(string accesstoken)
        {
            this.accesstoken = accesstoken;
        }
        
        //Get list of FollowerId
        public GetFollowerIds GetFollowerIDs()
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);
               
                JObject result = client.getListFollower(0, 20);

                GetFollowerIds response = JsonConvert.DeserializeObject<GetFollowerIds>(
                    result.ToString(Formatting.None));

                 

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send message for user via userid "7598536841305925270"
        public SendMessage SendMessage(string userid, string message)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendTextMessageToUserId(userid, message);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));                 

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send message with image by link
        public SendMessage SendMessageWithImageViaLink(string userid, string message, string url)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendImageMessageToUserIdByUrl(userid, message, url);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send message with image by attachment id
        public SendMessage SendMessageWithImageViaAttachmentId(string userid, string message, string attachment_id)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendImageMessageToUserIdByAttachmentId(userid, message, attachment_id);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send message with gif by attachment id
        public SendMessage SendMessageWithGifViaAttachmentId(string userid, string message, string attachment_id, int image_width = 120, int image_height = 120)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendGifMessageToUserIdByAttachmentId(userid, message, attachment_id, image_width, image_height);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send list element
        public SendMessage SendListElement(string userid, string message, List<Element> elements)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendListElementMessagetoUserId(userid, message, elements);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send list button
        public SendMessage SendListButton(string userid, string message, List<Button> buttons)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendListButtonMessagetoUserId(userid, message, buttons);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send list element and button
        public SendMessage SendListElementButton(string userid, string message, List<Element> elements, List<Button> buttons)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendListButtonAndElementMessagetoUserId(userid, message, elements, buttons);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send request to user to update info
        public SendMessage SendRequestUpdateInfo(string userid, string title, string subtitle, string url)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendRequestUserProfileToUserId(userid, title, subtitle, url);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send file to user
        public SendMessage SendFile(string userid, string fileattatchementId)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendFileToUserId(userid, fileattatchementId);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send reply message to message
        public SendMessage SendReplyMessageToMessage(string messageid, string message)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendTextMessageToMessageId(messageid, message);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send reply image to message by url
        public SendMessage SendReplyImageToMessageByUrl(string messageid, string message, string url)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendImageMessageToMessageIdByUrl(messageid, message, url);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send reply image to message by attatchement_id
        public SendMessage SendReplyImageToMessageByAttatchementId(string messageid, string message, string attatchement_id)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendImageMessageToMessageIdByAttachmentId(messageid, message, attatchement_id);


                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Send list button to message
        public SendMessage SendListButtonToMessage(string messageid, string message, List<Button> buttons)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.sendListButtonMessageToMessageId(messageid, message, buttons);

                SendMessage response = JsonConvert.DeserializeObject<SendMessage>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Get tag list
        public GetTag GetTagList()
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.getAllTagOfOfficialAccount();

                GetTag response = JsonConvert.DeserializeObject<GetTag>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Add tag to user
        public ZaloResponse AddTagToUser(string userid, string tagname)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.tagFollower(userid, tagname);

                ZaloResponse response = JsonConvert.DeserializeObject<ZaloResponse>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Remove tag from user
        public ZaloResponse RemoveTagFromUser(string userid, string tagname)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.removeTagFromFollower(userid, tagname);

                ZaloResponse response = JsonConvert.DeserializeObject<ZaloResponse>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Delete tag
        public ZaloResponse DeleteTag(string tagname)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.deleteTag(tagname);

                ZaloResponse response = JsonConvert.DeserializeObject<ZaloResponse>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //UploadImage from file
        public UploadImageResponse UploadImage(string imgpath)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.uploadImageForOfficialAccountAPI(new ZaloFile(imgpath));

                UploadImageResponse response = JsonConvert.DeserializeObject<UploadImageResponse>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Upload gif from file
        public UploadImageResponse UploadGif(string imgpath)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.uploadGifForOfficialAccountAPI(new ZaloFile(imgpath));

                UploadImageResponse response = JsonConvert.DeserializeObject<UploadImageResponse>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Upload gif from file
        public UploadFileResponse UploadFile(string filepath, string filetype)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                ZaloFile file = new ZaloFile(filepath);
                file.setMediaTypeHeader(filetype);//"application/pdf"
                JObject result = client.uploadFileForOfficialAccountAPI(file);

                UploadFileResponse response = JsonConvert.DeserializeObject<UploadFileResponse>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Get offical account
        public OAInfo GetOAInfo()
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.getProfileOfOfficialAccount();

                OAInfo response = JsonConvert.DeserializeObject<OAInfo>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Get message list
        public GetMessageList GetMessageList(int offset, int count)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.getListRecentChat(offset, count);

                GetMessageList response = JsonConvert.DeserializeObject<GetMessageList>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Get message list by user id
        public GetMessageList GetMessageListByUserid(long userid, int offset, int count)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.getListConversationWithUser(userid, offset, count);

                GetMessageList response = JsonConvert.DeserializeObject<GetMessageList>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Register IP
        public string RegisterIP(string ip, string name)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.registerIP(ip, name);

                //GetMessageList response = JsonConvert.DeserializeObject<GetMessageList>(
                //    result.ToString(Formatting.None));
                return result.ToString(Formatting.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Remove IP
        public string RemoveIP(string ip, string name)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.removeIP(ip, name);

                //GetMessageList response = JsonConvert.DeserializeObject<GetMessageList>(
                //    result.ToString(Formatting.None));
                return result.ToString(Formatting.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Update follower info
        public ZaloResponse UpdateUserInfo(string userid, string username, string phonenumn, string address, int cityid = 0, int districtid = 0)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.updateFollowerInfo(userid, username, phonenumn, address, cityid, districtid);

                ZaloResponse response = JsonConvert.DeserializeObject<ZaloResponse>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Zalo boardcast
        public ZaloResponse BoardCast(List<Element> elements, TargetBroadcastGender gender, List<TargetBroadcastAges> ages = null, List<TargetBroadcastLocations> locations = null, List<TargetBroadcastCities> cities = null, List<TargetBroadcastPlatforms> platforms = null, List<TargetBroadcastTelcos> telcos = null)
        {
            try
            {
                ZaloClient client = new ZaloClient(this.accesstoken);

                JObject result = client.broadcastLinks(elements, gender, ages, locations, cities, platforms, telcos);

                ZaloResponse response = JsonConvert.DeserializeObject<ZaloResponse>(
                    result.ToString(Formatting.None));

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }                      
    }
    
    public class GetFollowerIds : ZaloResponse
    {
        public Data data { get; set; }

        public class Follower
        {
            public string user_id { get; set; }
        }

        public class Data
        {
            public int total { get; set; }
            public List<Follower> followers { get; set; }
        }
    }
    public class SendMessage : ZaloResponse
    {
        public Data data { get; set; }

        public class Data
        {
            public string message_id { get; set; }
            public string user_id { get; set; }
        }
    }
    public class GetTag : ZaloResponse
    {
        public List<string> data { get; set; }
    }
    public class ZaloResponse
    {
        public int error { get; set; }
        public string message { get; set; }
    }
    public class UploadImageResponse : ZaloResponse
    {
        public Data data { get; set; }

        public class Data
        {
            public string attachment_id { get; set; }
        }
    }
    public class UploadFileResponse : ZaloResponse
    {
        public Data data { get; set; }

        public class Data
        {
            public string token { get; set; }
        }
    }
    public class OAInfo : ZaloResponse
    {
        public Data data { get; set; }

        public class Data
        {
            public string description { get; set; }
            public string name { get; set; }
            public string avatar { get; set; }
            public string cover { get; set; }
            public string oa_id { get; set; }
            public bool is_verified { get; set; }
        }
    }
    public class GetMessageList : ZaloResponse
    {
        public List<Data> data { get; set; }

        public class Data
        {
            public int src { get; set; }
            public object time { get; set; }
            public string type { get; set; }
            public string message { get; set; }
            public string message_id { get; set; }
            public string from_id { get; set; }
            public string to_id { get; set; }
            public string from_display_name { get; set; }
            public string from_avatar { get; set; }
            public string to_display_name { get; set; }
            public string to_avatar { get; set; }
            public List<Link> links { get; set; }
            public string thumb { get; set; }
            public string url { get; set; }
            public string description { get; set; }
        }
        public class Link
        {
            public string title { get; set; }
            public string url { get; set; }
            public string thumb { get; set; }
            public string description { get; set; }
        }
    }
}
