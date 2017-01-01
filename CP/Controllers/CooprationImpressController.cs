using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using CP.Model;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Data;
namespace CP.Controllers
{
    public class CooprationImpressController : Controller
    {
        #region  httpClient 连接Server
        public static HttpClient _httpClient;
        public const string url = "http://localhost:37094/";
        public class Token
        {
            public Token() { }
            [JsonProperty("Access_Token")]
            private string _accesstoken;
            public string AccessToken
            {
                set { _accesstoken = value; }
                get { return _accesstoken; }
            }
        }
        public HttpClient getHttpClient(string url)
        {
            _httpClient = new HttpClient();
            var dict = new SortedDictionary<string, string>();
            dict.Add("client_id", "irving");
            dict.Add("client_secret", "123456");
            dict.Add("grant_type", "client_credentials");
            //var rst = await(@"http://" + Request.RequestUri.Authority + @"/token").PostUrlEncodedAsync(dict).ReceiveJson<Token>();
            // Console.WriteLine(_httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters)).Result.Content.ReadAsStringAsync().Result);
            var rst = _httpClient.PostAsync(url + "token", new FormUrlEncodedContent(dict)).Result.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<Token>(rst);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", obj.AccessToken);
            return _httpClient;
        }
        #endregion
        // GET: CooprationImpress
        /// <summary>
        /// 社团印象
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 社团介绍
        /// </summary>
        /// <returns></returns>
        public ActionResult CooprationIntroduction() 
        {
            return View();
        }
        /// <summary>
        /// 社团风采
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> EliteCoopration() 
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 8;//每一页显示8条数据
            IList<cpActivityInfo> list =await getPageList(pageIndex,pageSize);
            int recordCount = await getRecordCount(pageSize);
            ViewData["pageCount"] = recordCount;
            ViewData["cpActivityInfo"] = list;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageSize"] = pageSize;
            return View();
        }
        //// 从一个Json串生成对象信息
        //public static object JsonToObject(string jsonString, object obj)
        //{
        //    DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
        //    using (MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
        //    {
        //        return serializer.ReadObject(mStream);
        //    }

        //}
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public async Task<List<cpActivityInfo>> getPageList(int pageIndex, int pageSize)
        {
            string pagelist = await (await getHttpClient(url).GetAsync(url + "api/cpActivityInfo?pageIndex=" + pageIndex + "&pageSize=" + pageSize)).Content.ReadAsStringAsync();
            //设置转化JSON格式时字段长度
            List<cpActivityInfo> pagelistDs = JsonConvert.DeserializeObject<List<cpActivityInfo>>(pagelist);
            return pagelistDs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize">页容量</param>
        /// <returns>页数</returns>
        public async Task<int> getRecordCount(int pageSize)
        {
            var cpActivityInfoList = await (await getHttpClient(url).GetAsync(url + "api/cpActivityInfo/GETRecordCount")).Content.ReadAsStringAsync();
            int count = JsonConvert.DeserializeObject<int>(cpActivityInfoList);
            int page = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
            return page;
        }
    }
}