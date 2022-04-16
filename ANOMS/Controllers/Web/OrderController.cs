using AN.Entities.CommonModel;
using AN.Entities.Entities;
using AN.Entities.ViewModel;
using AN.Service;
using ANOMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ANOMS.Controllers.Web
{
    public class OrderController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ANOMSBDConnectionString"].ConnectionString);
        private readonly UserService _userService;
        public OrderController(UserService userService) : base()
        {
            this._userService = userService;
        }
        private GetOrderListVMs Orderstatus = new GetOrderListVMs();
        private string currentStatus;


        // GET: Order
        public ActionResult Promotion()
        {

            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];

            SqlDataAdapter sda = new SqlDataAdapter("select pc_ID from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["pc_ID"].ToString();


            SqlDataAdapter sdaa = new SqlDataAdapter("select t_ID from Usersd where id = '" + uid + "'", con);
            DataTable dtt = new DataTable();
            sdaa.Fill(dtt);
            string usertypes = dtt.Rows[0]["t_ID"].ToString();
           
            if (usertypes == "1")
            {
                SqlDataAdapter _da = new SqlDataAdapter("Select n_ID,n_Type From N_Category where n_ID= '" + types + "'", con);
                DataTable _dt = new DataTable();
                _da.Fill(_dt);
                ViewBag.typeList = ToSelectList(_dt, "n_ID", "n_Type");
            }
            else
            {
                SqlDataAdapter _da = new SqlDataAdapter("Select * From N_Category", con);
                DataTable _dt = new DataTable();
                _da.Fill(_dt);
                ViewBag.typeList = ToSelectList(_dt, "n_ID", "n_Type");
            }

            if (usertypes == "1")
            {
                List<GetAllNotificationVM> GetAllNotificationVMList = _userService.NotificationLists(types);
                ViewBag.list = _userService.NotificationLists(types);
            }
            else if (usertypes == "2")
            {
                List<GetAllNotificationVM> GetAllNotificationVMList = _userService.NotificationList();
                ViewBag.list = _userService.NotificationList();
            }
            else
            {

            }
           
            return View();
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
        private double notificationId()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*)from Notifications ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double pId = 1000 + counter;
            return pId;
        }
        public ActionResult Promotions_Config(Promotion promotion)
        {

            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            SqlDataAdapter sda = new SqlDataAdapter("select id from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["id"].ToString();
            double id = notificationId();

            if (dt.Rows[0][0].ToString() != "1")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "insert into Notifications values('" + id + "','" + 1 + "','" + promotion.n_msg + "','" + promotion.msg_bang + "',GETDATE(),'" + promotion.validity + "','" + promotion.n_ID + "',GETDATE(),GETDATE(),'" + types + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    TempData["AlertMessage"] = " Save Successfully ";
                    return RedirectToAction("Promotion", "Order");
                }
                catch (Exception)
                {
                    TempData["AlertMessage"] = "id Already Exist";
                    return RedirectToAction("Promotion", "Order");
                }

            }
            else
            {
                TempData["AlertMessage"] = " id Already Exist";
                return RedirectToAction("Promotion", "Order");

            }



        }

        public ActionResult Promotion_Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Promotion_Delete(string id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete Notifications where ntfn_ID = '" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            TempData["AlertMessage"] = " Delete Completed";

            return RedirectToAction("Promotion", "Order");
        }

        public ActionResult OrderList()
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            SqlDataAdapter sda = new SqlDataAdapter("select pc_ID from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["pc_ID"].ToString();
            SqlDataAdapter sdaa = new SqlDataAdapter("select t_ID from Usersd where id = '" + uid + "'", con);
            DataTable dtt = new DataTable();
            sdaa.Fill(dtt);
            string Usertypes = dtt.Rows[0]["t_ID"].ToString();
            if (Usertypes == "1")
            {
                List<GetOrderListVM> OrderList = _userService.OrderList(types);
                if (OrderList.Count() > 0)
                {
                    return View(OrderList);
                }
                else
                {
                    TempData["AlertMessage"] = "";
                    return RedirectToAction("EmptyPage", "Order");


                }
            }

            else
            {
                List<GetOrderListVM> OrderList = _userService.OrderListss();
                if (OrderList.Count() > 0)
                {
                    return View(OrderList);
                }
                else
                {
                    TempData["AlertMessage"] = "";
                    return RedirectToAction("EmptyPage", "Order");


                }
            }

        }

        public ActionResult EmptyPage()
        {

            return View();
        }
        public ActionResult ProductDetails(GetOrderListVM id)
        {
            GetOrderListVM DetailsOrder = _userService.DetailsOrderlists(id.prdt_ID);
            return View(DetailsOrder);
        }
        [HttpPost]
        public ActionResult ProductDetailsnew(GetOrderListVM id)
        {
            GetOrderListVM DetailsOrder = _userService.DetailsOrderlist(id.o_ID);
            return PartialView("ProductDetailsnew", DetailsOrder);
        }



        [HttpGet]
        public ActionResult OrderStatusnew(GetOrderListVMs id)
        {
            Orderstatus = _userService.OrderStatus(id.o_ID);

            return View(Orderstatus);

        }
        [HttpGet]
        public String OrderStatuModel(string id)
        {
            int oid = Int16.Parse(id);
            Orderstatus = _userService.OrderStatus(oid);
            //var jsondata = new JavaScriptSerializer().Serialize(Orderstatus);
            //string path = Server.MapPath("~/Json/");
            //System.IO.File.WriteAllText(path + "customer.json", jsondata);
            return Orderstatus.paymentSta;

        }

        [HttpPost]
        public ActionResult OrderStatusnew(string currentstatus, string orderId)
        {

            int oid = Int16.Parse(orderId);

            currentStatus = currentstatus;
            if (currentstatus != null && oid != 0)
            {

                if (currentStatus == "0")
                {
                    currentStatus = "In_production";
                }
                else if (currentStatus == "1")
                {
                    currentStatus = "Inshipment";
                }
                else if (currentStatus == "2")
                {
                    currentStatus = "Packaging";
                }
                else if (currentStatus == "3")
                {
                    currentStatus = "Deliver";
                }
                HttpCookie cookieObj = Request.Cookies["anmos"];
                string uid = cookieObj["id"];
                ResponseResult responseResult = new ResponseResult();
                Product product = new Product();
                SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                string types = dt.Rows[0]["userBy"].ToString();
                Notificationinsert(orderId, currentStatus);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update Product_Order set o_ID = '" + oid + "',paymentSta = '" + currentStatus + "',userBy='" + types + "',modifyDate='" + DateTime.Now+"' where o_ID = '" + oid + "'";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
            }

            return Json(currentStatus, JsonRequestBehavior.AllowGet);
        }

        private double NotificationId()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Notifications ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double sId = 1000 + counter;
            return sId;
        }
        private void Notificationinsert(string orderId, string currentstatus)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sdaaa = new SqlDataAdapter("select id from Usersd where id = '" + uid + "'", con);
            DataTable dttt = new DataTable();
            sdaaa.Fill(dttt);
            string userss = dttt.Rows[0]["id"].ToString();

            SqlDataAdapter sda = new SqlDataAdapter("select prdt_ID from Product_Order where o_ID = '" + orderId + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string prdtid = dt.Rows[0]["prdt_ID"].ToString();

            SqlDataAdapter sdas = new SqlDataAdapter("select pc_ID from Prdt_Package where prdt_ID = '" + prdtid + "'", con);
            DataTable dtt = new DataTable();
            sdas.Fill(dtt);
            string pc_ID = dtt.Rows[0]["pc_ID"].ToString();
            int pcid = Convert.ToInt32(pc_ID);
            //string userss = "Ripon";

            double n_ID = NotificationId();
            string msg = "Order Status Update:-  OrderID: " + orderId + " CurrentStatus: " + currentstatus ;
            DateTime addedDate = DateTime.Now.AddDays(10);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Notifications values('" + n_ID + "','" + 3 + "','" + msg + "','" + pc_ID + "',GETDATE(),'" + addedDate + "','" + pcid + "',GETDATE(),GETDATE(),'"+ userss + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public ActionResult NotificationLIst()
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];

            SqlDataAdapter sda = new SqlDataAdapter("select pc_ID from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["pc_ID"].ToString();

            SqlDataAdapter sdaa = new SqlDataAdapter("select t_ID from Usersd where id = '" + uid + "'", con);
            DataTable dtt = new DataTable();
            sdaa.Fill(dtt);
            string usertypes = dtt.Rows[0]["t_ID"].ToString();

            if (usertypes == "1")
            {
                List<GetAllNotificationVM> GetAllNotificationVMList = _userService.NotificationLists(types);
                return View(GetAllNotificationVMList);
            }
            else if (usertypes == "2")
            {
                List<GetAllNotificationVM> GetAllNotificationVMList = _userService.NotificationList();
                return View(GetAllNotificationVMList);
            }
            else
            {

            }
            return View();
        }

    }
}