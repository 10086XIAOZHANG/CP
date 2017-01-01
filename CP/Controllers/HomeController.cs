
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CP.Model;
namespace CP.Controllers
{
    public class HomeController : Controller
    {
       private List<SelectListItem> grade = new List<SelectListItem>();
       private List<SelectListItem> byActionClass = new List<SelectListItem>();//按功能分类
       //private static List<string> xskjl = new List<string>();//学术科技类
       //private static List<string> tyxxl = new List<string>();//体育休闲类
       //private static List<string> jnpxl = new List<string>();//技能培养类
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
       public class IP
       {
           [JsonProperty("IP")]
           public string ip { get; set; }
           public string address { get; set; }
       }
    
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult Reg()
        {
            return View();
        }
      
        public async  Task<JsonResult> StudentLogin(string userNum, string userPsd) 
        {
            GetIP();
            var rst_login = await (await getHttpClient(url).GetAsync(url + "api/LoginInfo?userId=" + userNum)).Content.ReadAsStringAsync();
            var login = JsonConvert.DeserializeObject<LoginInfo>(rst_login);
            if (login == null)
            {
                return null;
            }
            if (userNum == login.UserId || userNum == login.phoneNumber && userPsd == login.userPwd)
            {
                var rst_userinfo = await (await getHttpClient(url).GetAsync(url + "/api/UserInfo?userId=" + userNum)).Content.ReadAsStringAsync();
                var userinfo = JsonConvert.DeserializeObject<UserInfo>(rst_userinfo);
                Session["userinfo"] = userinfo;
                Session["stuName"] = userNum;
                Session["stuPwd"] = userPsd;
                Session["loginIdentify"] = 3;
                var rst_loginstate = await (await getHttpClient(url).GetAsync(url + "api/UserLoginState?userId=" + login.UserId)).Content.ReadAsStringAsync();
                var loginstate = JsonConvert.DeserializeObject<UserLoginState>(rst_loginstate);
                Session["loginPlace"] = loginstate.loginPlace;
                Session["lastLoginTime"] = loginstate.lastLoginTime;
                Session["loginNum"] = Convert.ToInt32(loginstate.loginNum) + 1 + "";
                if (loginstate == null)
                {
                    //添加userloginState数据
                    UserLoginState uls = new UserLoginState() { ipNum = GetIP().ip, userId = userinfo.userId, lastLoginTime = DateTime.Now.ToString(), loginNum = 1 + "", loginPlace = GetIP().address };
                    var ulsval = uls;
                    var requestJson = JsonConvert.SerializeObject(ulsval);
                    HttpContent httpContent = new StringContent(requestJson);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await (await getHttpClient(url).PostAsync(url + "api/UserLoginState", httpContent)).Content.ReadAsStringAsync();
                }
                else 
                {
                    //修改userloginState数据
                    UserLoginState uls = new UserLoginState() { ipNum = GetIP().ip, userId = userinfo.userId, lastLoginTime = DateTime.Now.ToString(), loginNum = Convert.ToInt32(loginstate.loginNum)+1+"", loginPlace = GetIP().address };
                    var ulsval = uls;
                    var requestJson = JsonConvert.SerializeObject(ulsval);
                    HttpContent httpContent = new StringContent(requestJson);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var udloginstate = await (await getHttpClient(url).PutAsync(url + "api/UserLoginState", httpContent)).Content.ReadAsStringAsync();
                }
                return Json(userinfo,JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        public JsonResult GetUserinfoModel() 
        {
            var userinfo = Session["userinfo"];
            return Json(userinfo,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout() 
        {
            Session["userinfo"]=null;
            Session["loginstate"] = 0;
            var loginIdentify=Session["loginIdentify"];
            if ( loginIdentify.ToString()== "3")
                Session["loginIdentify"] = 0;
            else
                Session["loginIdentify"] = 1;
            return Content("ok");
        }
        public ActionResult ShowValidateCode() 
        {

            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);//获取验证码.
            Session["code"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        /// <summary>
        /// 获取本机IP,位置定位
        /// </summary>
        /// <returns></returns>
        public static IP GetIP()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.Encoding = System.Text.Encoding.GetEncoding("GB2312"); 
                    var temp =Encoding.UTF8.GetString(webClient.DownloadData("http://ip.chinaz.com/getip.aspx"));
                    var ip = JsonConvert.DeserializeObject<IP>(temp);
                    return ip;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Search(string checks)
        {
            var listChecked ="" ;
            switch(checks){
                case "1000":
                    listChecked = "alll";
                    break;
            case "1001":
                    listChecked = "xskjl";
                    break;
                case "1002":
                    listChecked = "tyxxl";
                    break;
                case "1003":
                    listChecked = "jnpxl";
                    break;
                case "1004":
                    listChecked = "llyjl";
                    break;
                case "1005":
                    listChecked = "shsjl";
                    break;
                case "1006":
                    listChecked = "wxysl";
                    break;
            }
            return Content(listChecked);
        }
        public ActionResult SignUp()
        {
            
                #region 按功能分类
                byActionClass.Add(new SelectListItem { Text = "全部", Value = "1000" });
                byActionClass.Add(new SelectListItem { Text = "学术科技类", Value = "1001" });
                byActionClass.Add(new SelectListItem { Text = "体育休闲类", Value = "1002" });
                byActionClass.Add(new SelectListItem { Text = "技能培养类", Value = "1003" });
                byActionClass.Add(new SelectListItem { Text = "理论研究类", Value = "1004" });
                byActionClass.Add(new SelectListItem { Text = "社会实践类", Value = "1005" });
                byActionClass.Add(new SelectListItem { Text = "文学艺术类", Value = "1006" });
                
                #endregion
                //if (count == 0)
                //{
                //    #region 学术科技类社团添加
                //    xskjl.Add("科幻协会");
                //    xskjl.Add("电脑爱好者协会");
                //    xskjl.Add("电子科技协会");
                //    xskjl.Add("电子商务协会");
                //    xskjl.Add("材料科学协会");
                //    xskjl.Add("数学建模协会");
                //    xskjl.Add("IT爱好者协会");
                //    #endregion
                //    #region 体育休闲类社团添加
                //    tyxxl.Add("魔术爱好者协会");
                //    tyxxl.Add("足球协会");
                //    tyxxl.Add("乒乓球协会");
                //    tyxxl.Add("羽毛球协会");
                //    tyxxl.Add("电子竞技协会");
                //    tyxxl.Add("棋牌协会");
                //    tyxxl.Add("旅行者社团");
                //    tyxxl.Add("幻影轮滑社");
                //    tyxxl.Add("健美操协会");
                //    tyxxl.Add("VDS震舞糖街舞社");
                //    #endregion
                //    #region 技能培养类社团添加
                //    jnpxl.Add("微博协会");
                //    #endregion
                    //count += 1;
                //}
            #region 选择年级(如14级)
            try
            {
                
                string year = DateTime.Now.Year.ToString();
                string newyear = year.Substring(2, 2);
                for (int i = Convert.ToInt32(newyear); i > Convert.ToInt32(newyear) - 5; i--)
                {
                        grade.Add(new SelectListItem { Text = i + "级",Value=i+"", Selected = false });
                }
                ViewData["grade"] = grade;
            }
            catch {
                ViewData["grade"] = null;
            }
            #endregion
            ViewData["byactionclass"] = byActionClass;
            return View();
        }
      
    }
}