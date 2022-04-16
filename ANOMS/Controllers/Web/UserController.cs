using AN.infrastructure;
using AN.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AN.Entities.ViewModel;
using AN.Entities.CommonModel;
using AN.Service;
using System.Dynamic;
using ANOMS.Models;

namespace ANOMS.Controllers.Web
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        SqlDataAdapter _da;
        public UserController(UserService userService) : base()
        {
            this._userService = userService;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ANOMSBDConnectionString"].ConnectionString);
        // GET: User
        [HttpGet]
        public ActionResult Registration()
        {
            HttpCookie cookieObj = Request.Cookies["type"];
            string utype = cookieObj["type"];
            if (utype == "Admin")
            {
                _da = new SqlDataAdapter("Select *  From User_Type where t_ID!=2 and t_ID!=1", con);
                List<GetAllRegistrationVM> GetAllRegistrationVMList = _userService.RegistrationListForAdmin();
                ViewBag.list = _userService.RegistrationListForAdmin();
            }
            else
            {
                _da = new SqlDataAdapter("Select * From User_Type", con);
                List<GetAllRegistrationVM> GetAllRegistrationVMList = _userService.RegistrationList();
                ViewBag.list = _userService.RegistrationList();
            }

            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            ViewBag.typeList = ToSelectList(_dt, "t_ID", "t_Name");

            SqlDataAdapter _ta = new SqlDataAdapter("Select * From P_Category", con);
            DataTable _dta = new DataTable();
            _ta.Fill(_dta);
            ViewBag.CategoryList = ToSelectList1(_dta, "pc_ID", "pc_Name");





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

        [HttpPost]
        public ActionResult Registration_config(UsersdVM _user)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["userBy"].ToString();
            Usersd users = new Usersd();

            if (dt.Rows[0][0].ToString() != "1")
            {
                if (_user.s_Name == null)
                {
                    try
                    {
                        string ud = _user.id;
                        int sid = _user.s_ID;
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "insert into Usersd values('" + _user.id + "','" + _user.name + "','" + _user.t_ID + "','" + _user.pc_ID + "','" + _user.s_ID + "','" + _user.contact + "','" + _user.address + "','" + 0 + "',GETDATE(),GETDATE(),'" + types + "','" + _user.s_Name + "','" + _user.password + "','" + _user.email + "')";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        TempData["AlertMessage"] = " User Successfully Created";
                        UserDetailsinsert(ud, sid);
                        return RedirectToAction("Registration", "User");
                    }
                    catch
                    {
                        TempData["AlertMessage"] = "id Already Exist";
                        return RedirectToAction("Registration", "User");
                    }

                }
                else
                {
                    try
                    {
                        string ud = _user.id;
                        double sId = newId();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "insert into Usersd values('" + _user.id + "','" + _user.name + "','" + _user.t_ID + "','" + _user.pc_ID + "','" + sId + "','" + _user.contact + "','" + _user.address + "','" + 0 + "',GETDATE(),GETDATE(),'" + types + "','" + _user.s_Name + "','" + _user.password + "','" + _user.email + "')";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        TempData["AlertMessage"] = " User Successfully Created";
                        UserDetailsinserts(ud, sId);
                        return RedirectToAction("Registration", "User");
                    }
                    catch
                    {
                        TempData["AlertMessage"] = " id Already Exist";
                        return RedirectToAction("Registration", "User");
                    }

                }
            }

            else
            {
                TempData["AlertMessage"] = " id Already Exist";
                return RedirectToAction("Registration", "User");
            }

        }
        private void UserDetailsinsert(string ud, int sid)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];

            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["userBy"].ToString();
            int total_Time_Ordered = 0;
            double total_Around_Order = 0;
            string due_Status = "0";


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into UserDetails values('" + ud + "','" + sid + "',GETDATE(),'" + total_Time_Ordered + "','" + total_Around_Order + "','" + due_Status + "',GETDATE(),GETDATE(),'" + types + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            
        }

        private void UserDetailsinserts(string ud, double sid)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];

            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["userBy"].ToString();
            int total_Time_Ordered = 0;
            double total_Around_Order = 0;
            string due_Status = "0";


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into UserDetails values('" + ud + "','" + sid + "',GETDATE(),'" + total_Time_Ordered + "','" + total_Around_Order + "','" + due_Status + "',GETDATE(),GETDATE(),'" + types + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


        }
        private double newId()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Usersd ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double pId = 1000 + counter;
            return pId;
        }
        public enum MessageCode
        {
            Y,
            N
        }

        public ActionResult Userlist()
        {
            HttpCookie cookieObj = Request.Cookies["type"];
            string utype = cookieObj["type"];
            if (utype == "Admin")
            {
                _da = new SqlDataAdapter("Select *  From User_Type where t_ID!=2 and t_ID!=1", con);
                List<GetAllRegistrationVM> GetAllRegistrationVMList = _userService.RegistrationListForAdmin();
                return View(GetAllRegistrationVMList);
            }
            else
            {
                _da = new SqlDataAdapter("Select * From User_Type", con);
                List<GetAllRegistrationVM> GetAllRegistrationVMList = _userService.RegistrationList();
                return View(GetAllRegistrationVMList);
            }

        }

        [HttpGet]
        public ActionResult UserOrderDetails(string id)
        {

            UserDetailsVM UserDetails = _userService.userdetails(id);
            return View(UserDetails);

            //string id = Request["id"];
            //SqlDataAdapter sda = new SqlDataAdapter("select  name, contact, address, s_Name,email from Usersd where id = '" + id + "'", con);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            //r.name = dt.Rows[0]["name"].ToString();
            //r.s_Name = dt.Rows[0]["s_Name"].ToString();
            //r.address = dt.Rows[0]["address"].ToString();
            //r.contact = dt.Rows[0]["contact"].ToString();
            //r.email = dt.Rows[0]["email"].ToString();
            //return View(r);

        }

        public ActionResult User_Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult User_Delete(string id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete Usersd where id = '" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            TempData["AlertMessage"] = " Delete Completed";
            //Response.Redirect("User/Registration?msg=Delete Completed");
            return RedirectToAction("Registration", "User");
        }


        [HttpGet]
        public ActionResult Activity(string id)
        {
            ActivityupdateVM activity = _userService.activityUpdate(id)
;
            return View(activity);

        }

        [HttpPost]
        public ActionResult ActivityEdit_config(ActivityupdateVM ac)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            string types = dt.Rows[0]["userBy"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update Usersd set active = '" + ac.active + "',userBy='" + types + "' where id = '" + ac.id + "'";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            TempData["AlertMessage"] = " User Activity Successfully ";
            return RedirectToAction("Registration", "User");
        }
    }
}
