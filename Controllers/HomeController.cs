using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cflora.Models;
using cflora.BLL;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace cflora.Controllers
{
    public class HomeController : Controller
    {
        DataSet _objDataSet = new DataSet();
        CommonBLL _objCommonBLL = new CommonBLL();

        public ActionResult Default()
        {
            _objDataSet = _objCommonBLL.MasterSelectDataSet("HomeMetaData", 1);
            if (_objDataSet.Tables[0].Rows.Count > 0)
            {
                ViewBag.Title = _objDataSet.Tables[0].Rows[0]["META_TITLE"].ToString();
                ViewBag.MetaKeywords = _objDataSet.Tables[0].Rows[0]["META_KEYWORD"].ToString();
                ViewBag.MetaDescription = _objDataSet.Tables[0].Rows[0]["META_DESCRIPTION"].ToString();
            }

            //List<MetaModels> _objMeta = _objCommonBLL.FetchMetaData("MetaData", 1);
            //ViewData["MetaData"] = _objMeta;

            List<ProductModels> _objMarquee = _objCommonBLL.FetchProductList("MarqueeItem");
            ViewData["MarqueeItem"] = _objMarquee;

            _objDataSet = _objCommonBLL.MasterSelectDataSet("WelcomePage");
            ViewData["WelcomePage"] = _objDataSet;

            return View("Default");
        }

        public ActionResult ProductDetail()
        {
            return View();
        }

        //[Route("students/category/{category=general}")]
        public ActionResult SubCategory(int id)
        {
            _objDataSet = _objCommonBLL.MasterSelectDataSet("GetSubCategory", id);
            if (_objDataSet.Tables[0].Rows.Count > 0)
            {
                ViewBag.Title = _objDataSet.Tables[0].Rows[0]["FT_PAGE_TITLE"].ToString();
                ViewBag.MetaKeywords = _objDataSet.Tables[0].Rows[0]["FT_META_KEYWORD"].ToString();
                ViewBag.MetaDescription = _objDataSet.Tables[0].Rows[0]["FT_META_DESCRIPTION"].ToString();
            }

            List<ProductModels> _objMarquee = _objCommonBLL.FetchProductList("MarqueeItem");
            ViewData["MarqueeItem"] = _objMarquee;

            ViewData["SubCategory"] = _objDataSet.Tables[1];
            return View("SubCategory");
        }
    }
}
