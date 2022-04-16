using AN.infrastructure;
using ANOMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ANOMS.Controllers.Web
{
    public class ReportController : Controller
    {
        // GET: Report
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ANOMSBDConnectionString"].ConnectionString);
        public ActionResult Report()
        {

            return View();
        }

        public ActionResult ReportDaily()
        {

            listFor();

            return View();

        }
        private ANOMSBDContext db = new ANOMSBDContext();
        string pdID;
        string quantity;
        [HttpPost]
        public DailyInventoryVM ReportDaily_config(Productsvm pv)
        {
            SqlParameter prmCategoryId = new SqlParameter("@pc_ID", pv.pc_ID);
            SqlParameter prmProductId = new SqlParameter("@prdt_ID", pv.prdt_Name);
            SqlParameter prmProductDate = new SqlParameter("@prdt_Date", pv.entryDate);
            List<DailyInventoryVM> ListOfType = db.Database.SqlQuery<DailyInventoryVM>("sp_GetDateInventory @pc_ID , @prdt_ID,@prdt_Date", prmCategoryId, prmProductId, prmProductDate).ToList();

            DailyInventoryVM details = new DailyInventoryVM(); ;
            
            foreach (var it in ListOfType)
            {
                
                SqlDataAdapter _ta = new SqlDataAdapter("Select * From Prdt_Package where prdt_ID = '" + it.prdt_ID + "'", con);
                DataTable _dta = new DataTable();
                _ta.Fill(_dta);
                foreach (DataRow rows in _dta.Rows)
                {
                     pdID = rows["pkg_ID"].ToString();
                }
                int PID = Convert.ToInt32(pdID);
                SqlDataAdapter _taa = new SqlDataAdapter("Select * From Product_Order where pkg_ID = '" + PID + "'", con);
                DataTable _dtaa = new DataTable();
                _taa.Fill(_dtaa);
                foreach (DataRow rowss in _dtaa.Rows)
                {
                    quantity = rowss["quantity"].ToString();
                }
                int quant = Convert.ToInt32(quantity);
                int inventory = quant - it.total;
                List<DailyInventoryVM> ListOfVM = new List<DailyInventoryVM>();
                
                details.prdt_ID = it.prdt_ID;
                details.prdt_Name = it.prdt_Name;
                details.quantity = quant;
                details.total = it.total;
                details.inventory = inventory;
                ListOfVM.Add(details);


                ViewBag.list = ListOfType;
               

            }
            return details;
            //return ListOfType;
            //if (ListOfType.Count() > 0)
            //{

            //        return v;

            //}
            //else
            //{
            //    return null;
            //}
        }

        public ActionResult ReportMonthly()
        {
            listFor();
            return View();
        }
        public ActionResult ReportWeakly()
        {
            listFor();
            return View();
        }

        public ActionResult ReportYearly()
        {
            listFor();
            return View();
        }



        private void listFor()
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            SqlDataAdapter _pa = new SqlDataAdapter("Select * From Product", con);
            DataTable _dtpa = new DataTable();
            _pa.Fill(_dtpa);
            ViewBag.ProductList = ToSelectList(_dtpa, "prdt_ID", "prdt_Name");



            SqlDataAdapter sda = new SqlDataAdapter("select pc_ID from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["pc_ID"].ToString();

            SqlDataAdapter sdaa = new SqlDataAdapter("select t_ID from Usersd where id = '" + uid + "'", con);
            DataTable dtt = new DataTable();
            sdaa.Fill(dtt);
            string usertypes = dtt.Rows[0]["t_ID"].ToString();

            if (usertypes != "1")
            {
                SqlDataAdapter _ta = new SqlDataAdapter("Select pc_ID,pc_Name From P_Category where pc_ID = '" + types + "'", con);
                DataTable _dta = new DataTable();
                _ta.Fill(_dta);
                ViewBag.CategoryList = ToSelectList1(_dta, "pc_ID", "pc_Name");
            }
            else
            {
                SqlDataAdapter _ta = new SqlDataAdapter("Select * From P_Category ", con);
                DataTable _dta = new DataTable();
                _ta.Fill(_dta);
                ViewBag.CategoryList = ToSelectList1(_dta, "pc_ID", "pc_Name");
            }
        }



        [NonAction]
        public SelectList ToSelectList1(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list1 = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list1.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list1, "Value", "Text");
        }



        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }
    }
}