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
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ANOMS.Controllers.Web
{
    public class ProductAddController : Controller
    {
        private readonly ProductService _ProductService;
        public ProductAddController(ProductService ProductService) : base()
        {
            this._ProductService  = ProductService;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ANOMSBDConnectionString"].ConnectionString);
        // GET: Product
        public ActionResult ProductAdd()
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
            if (usertypes =="1")
            {
                List<GetAllProductVM> GetAllProductVMList = _ProductService.ProductList(types);
                ViewBag.list = _ProductService.ProductList(types);
            }
            else if(usertypes == "2")
            {
                List<GetAllProductVM> GetAllProductVMList = _ProductService.ProductList();
                ViewBag.list = _ProductService.ProductList();
            }
            else
            {

            }

            return View();


            
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
        public ActionResult ProductAdd_config(Productsvm productvm, string n )
        {
            string pc_ID, prdt_Name, brnd_ID, pkg_grade, pkg_Name, min_Quantity, in_Price, sell_Price, image_bution;
            
            pc_ID = Request["pc_ID"];
            prdt_Name = Request["prdt_Name"];
            brnd_ID = Request["brnd_ID"];
            pkg_grade = Request["pkg_grade"];
            pkg_Name = Request["pkg_Name"];
            min_Quantity = Request["min_Quantity"];
            in_Price = Request["in_Price"];
            sell_Price = Request["sell_Price"];
            image_bution = Request["image_bution"];

            Productinsert(prdt_Name, pc_ID, productvm);
            TempData["AlertMessage"] = " Product Successfully Added";
            return RedirectToAction("ProductAdd", "ProductAdd");
        }
        private double newId()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Product ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double pId = 1000 + counter;
            return pId;
        }



        private void Productinsert(string prdt_Name,string pc_ID, Productsvm productvm )
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];

            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["userBy"].ToString();

          
            double prdt_ID = newId();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Product values('" + pc_ID + "','" + prdt_ID + "','" + prdt_Name + "',GETDATE(),GETDATE(),'" + types + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            ProductBandinsert(prdt_ID, pc_ID,productvm);
        }


        private double brandId()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Brand ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double sId = 1000 + counter;
            return sId;
        }

        private void ProductBandinsert(double prdt_ID, string pc_ID, Productsvm productvm)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["userBy"].ToString();

            string brnd_Name;
            double brnd_ID = brandId();
            brnd_Name = Request["brnd_Name"];
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Brand values('" + pc_ID + "','" + prdt_ID + "','" + brnd_ID + "','" + brnd_Name + "',GETDATE(),GETDATE(),'" + types + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

             Pakageinsert(pc_ID, prdt_ID, brnd_ID, productvm);
        }


        private double PakageId()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Prdt_Package ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double pId = 1000 + counter;
            return pId;
        }
        private double ImageId()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Prdt_Package ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double pId = 1000 + counter;
            return pId;
        }
        private void Pakageinsert(string pc_ID,double prdt_ID, double brnd_ID, Productsvm productvm)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["userBy"].ToString();
            
            string pkg_Grade, pkg_Name, min_Quantity, inn;

            pkg_Grade = Request["pkg_grade"];
            pkg_Name = Request["pkg_Name"];
            min_Quantity = Request["sellQuantity"];
            inn= Request["file"];
            double pkg_ID = PakageId();
            double image_ID = ImageId();


            string _FileName = Path.GetFileName(productvm.image_bution.FileName);
            string _path = Path.Combine(Server.MapPath("~/Content/Product/AddProduct/"), _FileName);
            string path = "~/Content/Product/AddProduct/" + _FileName;
            productvm.image_bution.SaveAs(_path);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Prdt_Package values('" + pc_ID + "','" + prdt_ID + "','" + brnd_ID + "','" + pkg_ID + "','" + pkg_Name + "','" + pkg_Grade + "','" + min_Quantity + "','" + image_ID + "','" + path + "',GETDATE(),GETDATE(),'" + types + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Priceinsert(pc_ID, prdt_ID, brnd_ID, pkg_ID, productvm);
        }

        private double priceId()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Price ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double sId = 1000 + counter;
            return sId;
        }


        private void Priceinsert(string pc_ID,double prdt_ID,double brnd_ID,double pkg_ID, Productsvm productvm)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["userBy"].ToString();

            double PriceID = priceId();
            
            string in_Price, sell_Price, prevPrice;
            in_Price = Request["in_Price"];
            sell_Price = Request["sell_Price"];
            prevPrice = Request[""];
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Price values('" + pc_ID + "','" + prdt_ID + "','" + brnd_ID + "','" + pkg_ID + "','" + in_Price + "','" + sell_Price + "','" + prevPrice + "',GETDATE(),GETDATE(),'" + types + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Inventoryinsert(pc_ID, prdt_ID, brnd_ID, pkg_ID,  productvm);
        }

        private void Inventoryinsert(string pc_ID, double prdt_ID, double brnd_ID, double pkg_ID, Productsvm productvm)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["userBy"].ToString();

            
            string total_Qunty, exp_Date, o_ID;
            total_Qunty = Request[""];
            exp_Date = Request[""];
            o_ID = Request[""];
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Inventory values('" + pc_ID + "','" + prdt_ID + "','" + brnd_ID + "','" + pkg_ID + "','" + total_Qunty + "','" + exp_Date + "',GETDATE(),GETDATE(),'" + types + "','" + o_ID + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Orderinsert(pc_ID, prdt_ID, brnd_ID, pkg_ID, productvm);
        }

        private double OrderId()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Product_Order ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double sId = 1000 + counter;
            return sId;
        }
        private void Orderinsert(string pc_ID, double prdt_ID, double brnd_ID, double pkg_ID, Productsvm productvm)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["userBy"].ToString();

            double o_ID  = OrderId();
            string orderBy, orderDate, orderNo, paymentSta, amounts;
            orderBy = Request[""];
            orderDate = Request[""];
            orderNo = Request[""];
            paymentSta = Request["0"];
            amounts = Request["0"];
            

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Product_Order values('" + o_ID + "','" + pc_ID + "','" + prdt_ID + "','" + brnd_ID + "','" + pkg_ID + "','" + orderBy + "',GETDATE(),'" + orderNo + "','" + paymentSta + "',GETDATE(),GETDATE(),'" + types + "','" + amounts + "','" +productvm.min_Quantity + "','" + productvm.sellQuantity + "','" + productvm.miniQuantity + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
           // Notificationinsert(o_ID, pc_ID, prdt_ID, brnd_ID, pkg_ID);
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
        //private void Notificationinsert(double o_ID, string pc_ID, double prdt_ID, double brnd_ID, double pkg_ID)
        //{
        //    HttpCookie cookieObj = Request.Cookies["anmos"];
        //    string uid = cookieObj["id"];
        //    ResponseResult responseResult = new ResponseResult();
        //    Product product = new Product();
        //    SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    string types = dt.Rows[0]["userBy"].ToString();

        //    double n_ID = NotificationId();
        //    string msg = o_ID.ToString() + pc_ID + prdt_ID.ToString() + brnd_ID.ToString() + pkg_ID.ToString();

        //    string msg_bang, validity, orderNo, paymentSta, amounts;
        //    msg_bang = Request[""];
        //    validity = Request[""];
        //    orderNo = Request[""];
        //    paymentSta = Request["0"];
        //    amounts = Request["0"];


        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "insert into Notifications values('" + n_ID + "','" + pc_ID + "','" + msg + "','" + msg_bang + "',GETDATE(),'" + validity + "','" + pc_ID + "',GETDATE(),GETDATE(),'" + types + "')";
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = con;
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //}


        //insert end
        //Delete start


        public ActionResult Product_Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Product_Delete(string id)
        {

            Productname_Delete(id);
            productbrandDelete(id);
            PakageDelete(id);
            priceDelete(id);
            InventoryDelete(id);
            OrderDelete(id);
            TempData["AlertMessage"] = " Delete Completed";
            return RedirectToAction("ProductAdd", "ProductAdd");
        }
        private void Productname_Delete(string id)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete Product where prdt_ID = '" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void productbrandDelete(string id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete Brand where prdt_ID = '" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        private void PakageDelete(string id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete Prdt_Package where prdt_ID = '" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        private void priceDelete(string id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete Price where prdt_ID = '" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        private void InventoryDelete(string id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete Inventory where pkg_ID = '" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        private void OrderDelete(string id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete Product_Order where prdt_ID = '" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        //Delete End


        //update start
        public ActionResult ProductUpdate(GetAllProductVM r)
        {

            return View();

        }

        public ActionResult ProductEntryEdit(GetAllProductVM id)
        {
            ProductEntryEditView editProductEntry = _ProductService.EditProductEntry(id.prdt_ID);

            return View(editProductEntry);
        }

        public ActionResult ProductEntryEdit_config()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ProductEntryEdit_config(ProductEntryEditViews pv)
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
            cmd.CommandText = "update Product set prdt_ID = '" + pv.prdt_ID + "', prdt_Name = '" + pv.prdt_Name + "', modifyDate = GETDATE(),userBy='" + types + "' where prdt_ID = '" + pv.prdt_ID + "'";
            
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
           BandEdit(pv);
            TempData["AlertMessage"] = " Product update Successfully ";
            return RedirectToAction("ProductAdd", "ProductAdd");
        }
        private void BandEdit(ProductEntryEditViews pv)
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
            cmd.CommandText = "update Brand set prdt_ID = '" + pv.prdt_ID + "', brnd_Name = '" + pv.brnd_Name + "', modifyDate = GETDATE(),userBy='" + types + "' where prdt_ID = '" + pv.prdt_ID + "'";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            PakageEdit(pv);

        }
        private void PakageEdit(ProductEntryEditViews pv)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            string types = dt.Rows[0]["userBy"].ToString();

            //string _FileName = Path.GetFileName(pv.image_bution.FileName);
            //string _path = Path.Combine(Server.MapPath("~/Content/Product/AddProduct/"), _FileName);
            //string path = "~/Content/Product/AddProduct/" + _FileName;
            //pv.image_bution.SaveAs(_path);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update Prdt_Package set prdt_ID = '" + pv.prdt_ID + "', pkg_Name = '" + pv.pkg_Name + "',pkg_Grade = '" + pv.pkg_Grade + "',min_Quantity = '" + pv.min_Quantity + "', modifyDate = GETDATE(),userBy='" + types + "' where prdt_ID = '" + pv.prdt_ID + "'";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            PriceEdit(pv);
            P_OrderEdit(pv);

        }
        private void P_OrderEdit(ProductEntryEditViews pv)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select userBy from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            string types = dt.Rows[0]["userBy"].ToString();

            //string _FileName = Path.GetFileName(pv.image_bution.FileName);
            //string _path = Path.Combine(Server.MapPath("~/Content/Product/AddProduct/"), _FileName);
            //string path = "~/Content/Product/AddProduct/" + _FileName;
            //pv.image_bution.SaveAs(_path);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update Product_Order set o_ID = '" + pv.o_ID + "', quantity = '" + pv.min_Quantity + "',sellQuantity = '" + pv.sellQuantity + "',miniQuantity = '" + pv.miniQuantity + "', modifyDate = GETDATE(),userBy='" + types + "' where o_ID = '" + pv.o_ID + "'";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            PriceEdit(pv);

        }
        private void PriceEdit(ProductEntryEditViews pv)
        {
            HttpCookie cookieObj = Request.Cookies["anmos"];
            string uid = cookieObj["id"];
            ResponseResult responseResult = new ResponseResult();
            Product product = new Product();
            SqlDataAdapter sda = new SqlDataAdapter("select name from Usersd where id = '" + uid + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string types = dt.Rows[0]["name"].ToString();

            SqlDataAdapter sdaa = new SqlDataAdapter("select sell_Price from Price where prdt_ID = '" + pv.prdt_ID + "'", con);
            DataTable dtt = new DataTable();
            sdaa.Fill(dtt);
            string sellPrice = dtt.Rows[0]["sell_Price"].ToString();
            SqlDataAdapter sdaaa = new SqlDataAdapter("select brnd_ID from Brand where prdt_ID = '" + pv.prdt_ID + "'", con);
            DataTable dttt = new DataTable();
            sdaaa.Fill(dttt);
            string brnd_ID = dttt.Rows[0]["brnd_ID"].ToString();
            SqlDataAdapter sd = new SqlDataAdapter("select pkg_ID from Prdt_Package where prdt_ID = '" + pv.prdt_ID + "'", con);
            DataTable d = new DataTable();
            sd.Fill(d);
            string pkg_ID = d.Rows[0]["pkg_ID"].ToString();

            double sellpri = Convert.ToDouble(sellPrice);
            int band = Convert.ToInt32(brnd_ID);
            int pkgid = Convert.ToInt32(pkg_ID);

            if(sellpri > pv.sell_Price)
            {
                double curPrice = pv.sell_Price;
                double dec = sellpri - pv.sell_Price;
                Notificationinsert(sellpri, uid, types, band, pkgid, dec, curPrice);
            }
            else if(sellpri < pv.sell_Price)
            {
                double curPrice = pv.sell_Price;
                double dec = pv.sell_Price - sellpri;
                Notificationinsert(sellpri, uid, types, band, pkgid, dec, curPrice);
            }
            else
            {

            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update Price set prdt_ID = '" + pv.prdt_ID + "', in_Price = '" + pv.in_Price + "',sell_Price = '" + pv.sell_Price + "',prevPrice = '" + sellpri + "', modifyDate = GETDATE(),userBy='" + types + "' where prdt_ID = '" + pv.prdt_ID + "'";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //PriceEdit(pv);

        }
        private double NotificationIds()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Notifications ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double sId = 1000 + counter;
            return sId;
        }
        private void Notificationinsert(double sellpri, string uid,string types,int band,int pkgid,double dec,double curPrice)
        {

            SqlDataAdapter sda = new SqlDataAdapter("select brnd_Name from Brand where brnd_ID = '" + band + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string brnd_Name = dt.Rows[0]["brnd_Name"].ToString();

            SqlDataAdapter sdas = new SqlDataAdapter("select pkg_Name from Prdt_Package where pkg_ID = '" + pkgid + "'", con);
            DataTable dtt = new DataTable();
            sdas.Fill(dtt);
            string pkg_Name = dtt.Rows[0]["pkg_Name"].ToString();

            SqlDataAdapter sdass = new SqlDataAdapter("select pc_ID from Prdt_Package where pkg_ID = '" + pkgid + "'", con);
            DataTable dttt = new DataTable();
            sdass.Fill(dttt);
            string pc_ID = dttt.Rows[0]["pc_ID"].ToString();

            double n_ID = NotificationIds();
            string msg = "Price Update:-  BrandName: " + brnd_Name + " PackageName: " + pkg_Name + " Price is increased by " + dec + " Current price " + curPrice + " TK" + " User: "+ types + " Phone: " + uid;
            DateTime addedDate = DateTime.Now.Date.AddDays(10);



            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Notifications values('" + n_ID + "','" + 2 + "','" + msg + "','" + msg + "',GETDATE(),'" + addedDate + "','" + pc_ID + "',GETDATE(),GETDATE(),'" + uid + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        private void Notificationinserts(double sellpri, string uid, string types, int band, int pkgid, double dec, double curPrice)
        {

            SqlDataAdapter sda = new SqlDataAdapter("select brnd_Name from Brand where brnd_ID = '" + band + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string brnd_Name = dt.Rows[0]["brnd_Name"].ToString();

            SqlDataAdapter sdas = new SqlDataAdapter("select pkg_Name from Prdt_Package where pkg_ID = '" + pkgid + "'", con);
            DataTable dtt = new DataTable();
            sdas.Fill(dtt);
            string pkg_Name = dtt.Rows[0]["pkg_Name"].ToString();

            SqlDataAdapter sdass = new SqlDataAdapter("select pc_ID from Prdt_Package where pkg_ID = '" + pkgid + "'", con);
            DataTable dttt = new DataTable();
            sdass.Fill(dttt);
            string pc_ID = dttt.Rows[0]["pc_ID"].ToString();

            double n_ID = NotificationIds();
            string msg = "Price Update:-  BrandName: " + brnd_Name + " PackageName: " + pkg_Name + " Price is decrease by " + dec + " Current price " + curPrice + " TK" ;
            DateTime addedDate = DateTime.Now.Date.AddDays(10);



            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Notifications values('" + n_ID + "','" + pc_ID + "','" + msg + "','" + msg + "',GETDATE(),'" + addedDate + "','" + pc_ID + "',GETDATE(),GETDATE(),'" + uid + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public ActionResult ProductUpdate()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult Promotions()
        {
            return View();
        }

        public ActionResult OrderStatus()
        {
            return View();
        }
    }
}