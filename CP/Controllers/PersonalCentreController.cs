using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CP.Controllers
{
    public class PersonalCentreController : Controller
    {
        List<SelectListItem> yearList = new List<SelectListItem>();
        List<SelectListItem> mouthList = new List<SelectListItem>();
        List<SelectListItem> dayList = new List<SelectListItem>();
        // GET: PersonalCentre
        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 个人主页
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalHome()
        {
            return View();
        }
        /// <summary>
        /// 个人资料
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalData() 
        {
           
            int years = DateTime.Now.Year;
            int mouths = 12;
            int days=30;
            for (int i = years; i >=1900; i--)
            {
                yearList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = false });
            }
            for (int i = 1; i <= mouths; i++)
            {
                mouthList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = false });
            }
            for (int i = 1; i <= days; i++)
            {
                dayList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = false });
            }
            ViewData["year"] = yearList;
            ViewData["month"] = mouthList;
            ViewData["day"] = dayList;
            return View();
        }
        public JsonResult GetDay(string year, string month)
        {
            if (year != "" && month != "")
            {
                int date = 0;
                int m = Convert.ToInt32(month);
                int y = Convert.ToInt32(year);
                if (m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 11 || m == 12)
                {
                    date = 31;
                }
                else if (m == 4 || m == 6 || m == 9)
                {
                    date = 30;
                }
                else if (m == 2)
                {
                    if ((y % 4 == 0 && y % 100 != 0) || y % 400 == 0)
                    {
                        date = 29;
                    }
                    else
                    {
                        date = 28;
                    }
                }
                List<int> l = new List<int>();
                for (int i = 1; i <= date; i++)
                {
                    l.Add(i);
                }
                return Json(l);
            }
            else {
                return Json(0);
            
            }

        }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <returns></returns>
        public ActionResult IdentifiManage()
        {
            return View();
        }
        /// <summary>
        /// 密码设置
        /// </summary>
        /// <returns></returns>
        public ActionResult PasswordInstall() 
        {
            return View();
        }
        /// <summary>
        /// 推荐有奖
        /// </summary>
        /// <returns></returns>
        public ActionResult RecommmendUserful() 
        {
            return View();
        }
        /// <summary>
        /// 我的消息
        /// </summary>
        /// <returns></returns>
        public ActionResult MyNews()
        {
            return View();
        }
        /// <summary>
        /// 我的选报
        /// </summary>
        /// <returns></returns>
        public ActionResult MySignUp()
        {
            return View();

        }
        /// <summary>
        /// 我的申请
        /// </summary>
        /// <returns></returns>
        public ActionResult MyApply()
        {
            return View();
        }
    }
}