using Labixa.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.Service;
using Outsourcing.Data.Models;
using Outsourcing.Core.Common;
using Outsourcing.Core.Extensions;
using WebGrease.Css.Extensions;
using Outsourcing.Core.Framework.Controllers;
using PagedList;
using Labixa.ViewModels;
using System.Configuration;
using System.IO;

namespace Labixa.Controllers
{
    public class HomeController : Controller
    {

        readonly IProductService _productService;
        readonly IBlogService _blogService;
        readonly IWebsiteAttributeService _websiteAttributeService;
        readonly IProductCategoryService _productCategoryService;
        readonly IOrderService _orderService;
        readonly IMomoService _momoService;
        private string _accessKey;
        private string _endpoint;
        private string _partnerCode;
        private string _serectkey;
        private string _redirectUrl;
        private string _ipnUrl;
        private string _requestType;
        private string _partnerName;
        private string _storeId;
        private string _lang;


        public HomeController(IProductService productService, IBlogService blogService, IWebsiteAttributeService websiteAttributeService,
            IProductCategoryService productCategoryService, IMomoService momoService, IOrderService orderService)
        {
            this._productCategoryService = productCategoryService;
            this._websiteAttributeService = websiteAttributeService;
            this._blogService = blogService; ;
            this._productService = productService;
            this._momoService = momoService;
            this._orderService = orderService;
            this._accessKey = ConfigurationManager.AppSettings["accessKey"];
            this._endpoint = ConfigurationManager.AppSettings["endpoint"];
            this._partnerCode = ConfigurationManager.AppSettings["partnerCode"];
            this._serectkey = ConfigurationManager.AppSettings["serectkey"];
            this._redirectUrl = ConfigurationManager.AppSettings["redirectUrl"];
            this._ipnUrl = ConfigurationManager.AppSettings["ipnUrl"];
            this._requestType = ConfigurationManager.AppSettings["requestType"];
            this._partnerName = ConfigurationManager.AppSettings["partnerName"];
            this._storeId = ConfigurationManager.AppSettings["storeId"];
            this._lang = ConfigurationManager.AppSettings["lang"];
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {

            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.websiteAttributes = _websiteAttributeService.GetWebsiteAttributesByType("Home").ToList();

            var products = _productService.GetAllProducts().OrderByDescending(p => p.DateCreated).Take(8);

            ViewBag.indexViewModel = indexViewModel;
            return View(products);
        }

        public ActionResult Detail(string slug = "", string tokenid = "")
        {
            if (tokenid != "")
            {
                var order = _orderService.GetOrders().Where(p => p.signature.Equals(tokenid) && !p.Deleted && p.Count > 0).FirstOrDefault();
                if (order != null)
                {
                    ViewBag.TokenId = tokenid;
                }
            }
            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.websiteAttributes = _websiteAttributeService.GetWebsiteAttributesByType("Home").ToList();

            Product product = _productService.GetProductBySlug(slug);
            _productService.EditProduct(product);

            if (product != null)
            {
                foreach (var item in indexViewModel.websiteAttributes)
                {
                    if (item.Description == "title")
                    {
                        item.Value = product.MetaTitle;
                    }

                    if (item.Description == "keyword")
                    {
                        item.Value = product.MetaKeywords;
                    }

                    if (item.Description == "description")
                    {
                        item.Value = product.MetaDescription;
                    }

                    if (item.Description == "image")
                    {
                        item.Value = product.ProductImage;
                    }
                }
            }

            var productRelated = _productService.GetProductsByCategoryId(product.ProductCategoryId).Where(p => p.Id != product.Id).OrderByDescending(p => p.DateCreated).Take(8);
            indexViewModel.productRelated = productRelated;

            ViewBag.indexViewModel = indexViewModel;
            return View(product);
        }

        [HttpGet]
        public ActionResult TinTuc(int? page)
        {
            int pageSize = 6;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Blog> blgPageList = null;

            var blogs = _blogService.GetBlogs().Where(p => p.BlogCategoryId != 2).OrderByDescending(p => p.DateCreated).ToList();
            blgPageList = blogs.ToPagedList(pageIndex, pageSize);

            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.websiteAttributes = _websiteAttributeService.GetWebsiteAttributesByType("News").ToList();

            indexViewModel.blogReadMore = GetFourBlogs();
            //indexViewModel.blog = GetFourBlogs().FirstOrDefault();

            ViewBag.indexViewModel = indexViewModel;
            return View(blgPageList);
        }

        public ActionResult GioiThieu()
        {
            Blog blog = _blogService.GetBlogById(16);

            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.websiteAttributes = _websiteAttributeService.GetWebsiteAttributesByType("About").ToList();
            indexViewModel.blogReadMore = GetFourBlogs();
            //indexViewModel.blog = GetFourBlogs().FirstOrDefault();

            ViewBag.indexViewModel = indexViewModel;
            return View(blog);
        }

        public ActionResult ChiTietTinTuc(string slug)
        {
            IndexViewModel indexViewModel = new IndexViewModel();

            Blog blog = _blogService.GetBlogBySlug(slug);
            var blogsRelated = _blogService.GetBlogs().Where(p => p.Id != blog.Id).OrderByDescending(p => p.DateCreated).ToList().Take(10);
            indexViewModel.blogRelated = blogsRelated;


            indexViewModel.websiteAttributes = _websiteAttributeService.GetWebsiteAttributesByType("News").ToList();
            if (blog != null)
            {
                foreach (var item in indexViewModel.websiteAttributes)
                {
                    if (item.Description == "title")
                    {
                        item.Value = blog.MetaTitle;
                    }

                    if (item.Description == "keyword")
                    {
                        item.Value = blog.MetaKeywords;
                    }

                    if (item.Description == "description")
                    {
                        item.Value = blog.MetaDescription;
                    }

                    if (item.Description == "image")
                    {
                        item.Value = blog.ImageUrl;
                    }
                }
            }

            indexViewModel.blogReadMore = GetFourBlogs();
            //indexViewModel.blog = GetFourBlogs().FirstOrDefault();

            ViewBag.indexViewModel = indexViewModel;
            //ViewBag.blogsRelated = blogsRelated;
            return View(blog);
        }

        public ActionResult IndexCategory(string slug, int? page)
        {
            int pageSize = 6;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Product> prdPageList = null;

            IndexViewModel indexViewModel = new IndexViewModel();
            //Lấy meta web
            indexViewModel.websiteAttributes = _websiteAttributeService.GetWebsiteAttributesByType("Document").ToList();

            //Lấy thể loại tài liệu theo slug
            ProductCategory productCategory = _productCategoryService.GetProductCategoryBySlug(slug);
            indexViewModel.productCategory = productCategory;

            //Lấy list tài liệu theo thể loại
            var products = _productService.GetProductsByCategoryId(productCategory.Id).OrderByDescending(p => p.DateCreated);
            prdPageList = products.ToPagedList(pageIndex, pageSize);

            indexViewModel.blogReadMore = GetFourBlogs();
            //indexViewModel.blog = GetFourBlogs().FirstOrDefault();

            ViewBag.indexViewModel = indexViewModel;
            return View(prdPageList);
        }

        [HttpGet]
        public ActionResult Search(string search, int? page)
        {
            int pageSize = 6;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Blog> blgPageList = null;

            var blogs = _blogService.GetBlogs().Where(p => p.Title.Contains(search) && p.BlogCategoryId != 2 || p.Description.Contains(search) && p.BlogCategoryId != 2 || p.Content.Contains(search) && p.BlogCategoryId != 2).OrderByDescending(p => p.DateCreated).ToList();
            blgPageList = blogs.ToPagedList(pageIndex, pageSize);

            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.websiteAttributes = _websiteAttributeService.GetWebsiteAttributesByType("Home").ToList();
            indexViewModel.keySearch = search;
            indexViewModel.blogReadMore = GetFourBlogs();

            ViewBag.indexViewModel = indexViewModel;

            return View(blgPageList);
        }

        public IEnumerable<Blog> GetFourBlogs()
        {
            var blogs = _blogService.GetBlogs().Where(p => p.BlogCategoryId != 2).OrderByDescending(p => p.DateCreated).Take(5);
            return blogs;
        }
        public ActionResult ThanhToanMomo(string slug)
        {
            var _document = _productService.GetProductBySlug(slug);
            var momorequest = new ModelMomoRequest();
            var ord = new Order();
            if (_document != null)
            {
                ord.OrderTotal = _document.Price;
                ord.ProfileId = _document.Id;//id tai lieu
                ord.DateCreated = DateTime.Now;
                ord.Deleted = false;
                ord.ProductName = _document.Name;
                ord.Product_Slug = slug;
                ord.Status = "Waiting";
                ord.Note = _document.DescEng;
                ord.RequestId = momorequest.requestId = Guid.NewGuid().ToString();//ma don hang
                ord.Path_Download = _document.Tags;
                ord = _orderService.CreateOrder(ord);

            }

            momorequest.orderId = ord.Id.ToString();
            momorequest.amount = _document.Price.ToString();
            momorequest.partnerName = _partnerName;
            momorequest.orderInfo = _document.Name;
            momorequest.ipnUrl = _ipnUrl;
            momorequest.lang = _lang;
            momorequest.storeId = _storeId;
            momorequest.partnerCode = _partnerCode;
            momorequest.redirectUrl = _redirectUrl;
            momorequest.requestType = _requestType;
            momorequest.extraData = "";
            momorequest.signature = HMACMOMO(momorequest.amount, momorequest.extraData, momorequest.orderId, momorequest.orderInfo, momorequest.requestId);
            momorequest.storeId = _storeId;
            var url = _momoService.PurchaseMomo(momorequest, _endpoint, _accessKey, _serectkey);
            ViewBag.url = url;
            return Redirect(url);
        }
        public ActionResult RedirectMomo(string partnerCode, string orderId, string requestId, string amount, string orderInfo, string orderType, string transId, string resultCode, string message,
                                            string payType, string responseTime, string extraData, string signature)
        {
            var _signature = HMACMOMO_RESPONSE(amount, extraData, orderId, orderInfo, requestId, message, orderType, payType, responseTime, resultCode, transId);
            if (signature.Equals(_signature) && message.ToLower().Contains("successful"))
            {
                var order = _orderService.GetOrders().Where(p => p.RequestId.Equals(requestId) && p.Status.ToLower().Equals("waiting") && !p.Deleted).FirstOrDefault();
                if (order != null)
                {
                    //ok
                    order.message = message;

                    order.partnerCode = partnerCode;
                    order.payType = payType;
                    order.RequestId = requestId;
                    order.responseTime = responseTime;
                    order.resultCode = resultCode;
                    order.signature = signature;
                    order.Status = "Done";
                    order.transId = transId;
                    _orderService.EditOrder(order);
                    return Redirect("/Home/Detail?slug=" + order.Product_Slug + "&tokenid=" + order.signature);
                }
            }
            return View();
        }
        public ActionResult Download(string tokenId)
        {
            var order = _orderService.GetOrders().Where(p => p.signature.Equals(tokenId) && !p.Deleted && p.Count >= 1).FirstOrDefault();
            if (order != null)
            {
                order.Count -= 1;
                _orderService.EditOrder(order);
                string path = Server.MapPath(order.Path_Download);
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, order.Note);
            }
            return RedirectToAction("Index");
        }
        public ActionResult PSB(string partnerCode, string orderId, string requestId, string amount, string orderInfo, string orderType, string transId, string resultCode, string message,
                                            string payType, string responseTime, string extraData, string signature)
        {
            return View();
        }
        //public ActionResult OrderSuccess(string token)
        //{
        //    var result = Nganluong.GetTransaction(token, "51174", "e87f465fc53fc82ee0eb390d9132537a");
        //    return View();
        //}
        //public ActionResult CancelOrder(string token)
        //{
        //    return View();
        //}
        //public ActionResult GetNganluong()
        //{
        //    Guid guid = Guid.NewGuid();
        //    var nganluongURL = Nganluong.GetCheckout_URL("ATM_ONLINE", "BIDV", "51174", "e87f465fc53fc82ee0eb390d9132537a", "truonglongkt2021@gmail.com", "vnd", guid.ToString(), "10000", "0",
        //                                "0", "Test don hang LongT", "http://localhost:59560/Home/OrderSuccess", "http://localhost:59560/Home/CancelOrder", "Long Test 3",
        //                                "longt@lilotech.com", "0969394936");
        //    return Redirect(nganluongURL);

        //}
        public string HMACMOMO(string amount, string extraData, string orderId, string orderInfo, string requestId)
        {
            //Before sign HMAC SHA256 signature
            string rawHash = "accessKey=" + _accessKey +
                "&amount=" + amount +
                "&extraData=" + extraData +
                "&ipnUrl=" + _ipnUrl +
                "&orderId=" + orderId +
                "&orderInfo=" + orderInfo +
                "&partnerCode=" + _partnerCode +
                "&redirectUrl=" + _redirectUrl +
                "&requestId=" + requestId +
                "&requestType=" + _requestType
                ;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string _signature = crypto.signSHA256(rawHash, _serectkey);
            return _signature;
        }
        public string HMACMOMO_RESPONSE(string amount, string extraData, string orderId, string orderInfo, string requestId, string message, string orderType, string payType, string responseTime, string resultCode, string transId)
        {
            //Before sign HMAC SHA256 signature
            string rawHash = "accessKey=" + _accessKey +
                "&amount=" + amount +
                "&extraData=" + extraData +
                 "&message=" + message +
                "&orderId=" + orderId +
                "&orderInfo=" + orderInfo +
                "&orderType=" + orderType +
                "&partnerCode=" + _partnerCode +
                "&payType=" + payType +
                "&requestId=" + requestId +
                "&responseTime=" + responseTime +
                "&resultCode=" + resultCode +
                "&transId=" + transId
                ;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string _signature = crypto.signSHA256(rawHash, _serectkey);
            return _signature;
        }
    }
}