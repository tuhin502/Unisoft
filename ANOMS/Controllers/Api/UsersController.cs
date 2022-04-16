using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using AN.Entities.ViewModel;
using AN.Entities.CommonModel;
using AN.Service;
using AN.infrastructure;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using AN.Entities.Entities;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;

namespace ANOMS.Controllers.Api

{
   
    [FromUri]
    [RoutePrefix("Api/Users")]
    public class UsersController : ApiController
    {
        private readonly UserService _userServices;
        public UsersController(UserService userService) : base()
        {
            this._userServices = userService;
        }
         SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ANOMSBDConnectionString"].ConnectionString);
        
        public UsersController()
        {

        }
        ResponseResult responseResult = new ResponseResult();
        private ANOMSBDContext db = new ANOMSBDContext();
        [HttpPost]
        [Route("logins")]
        public UserTypeVM logins(SignInVM signIns)
        {
            SqlParameter prmUserBy = new SqlParameter("@id", signIns.id);
            SqlParameter prmReceiverId = new SqlParameter("@password", signIns.password);
            List<UserTypeVM> ListOfType = db.Database.SqlQuery<UserTypeVM>("sp_Login @id , @password", prmUserBy, prmReceiverId).ToList();

            UserTypeVM v = new UserTypeVM();
            v.pc_ID = ListOfType[0].pc_ID;
            v.t_ID = ListOfType[0].t_ID;
            ArrayList arrlistofOptions = new ArrayList (ListOfType);
            UserTypeViewModelTest t = new UserTypeViewModelTest();
            t.user.Add(v);
            var list = arrlistofOptions.Cast<UserTypeVM>().ToList();

            if (ListOfType.Count() > 0)
            {
                return v;
            }
            else
            {
                return null;
            }
        }


        [HttpGet]
        [Route("PCategory")]
        public List<PCategoryVM> PCategory()
        {
            
            List<PCategoryVM> ListOfType = db.Database.SqlQuery<PCategoryVM>("sp_Pcategory").ToList();
            if (ListOfType.Count() > 0)
            {
                return ListOfType;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("OrderStatus")]
        public List<OrderVM> OrderStatus()
        {
          
           // List<OrderVM> ListOfType = db.Database.SqlQuery<OrderVM>("sp_OderStatus @pcaregory", prmReceiverId).ToList();
           List<OrderVM> ListOfType = db.Database.SqlQuery<OrderVM>("sp_OderStatus").ToList();
            if (ListOfType.Count() > 0)
            {
                return ListOfType;
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        [Route("GetNotice")]
        public async Task<List<GetNotificationListVM>> GetNotice(List<GetNoticeVM> getNoticeVM)
        {
            int usid = getNoticeVM[0].Userid;
            string usidd = Convert.ToString(usid);
            SqlDataAdapter sdaa = new SqlDataAdapter("select t_ID from Usersd where id = '" + usid + "'", con);
            DataTable dtt = new DataTable();
            sdaa.Fill(dtt);
            string typess = dtt.Rows[0]["t_ID"].ToString();
            int tid = Convert.ToInt32(typess);
            List<GetNotificationListVM> ListOfVM = new List<GetNotificationListVM>();
            if (1==tid)
            {
                SqlDataAdapter sda = new SqlDataAdapter("select pc_ID from Usersd where id = '" + usid + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                string types = dt.Rows[0]["pc_ID"].ToString();
                int pcid = Convert.ToInt32(types);

                GetNotificationListVM details = null;
                await con.OpenAsync();
                var table = new DataTable();
                table.Columns.Add("NotificationId", typeof(int));
                //table.Columns.Add("userid", typeof(int));

                for (int i = 0; i < getNoticeVM.Count; i++)
                {
                    GetNoticeVM n = getNoticeVM[i];
                    var row = table.NewRow();

                    row["NotificationId"] = n.ntfn_ID;
                    //row["userid"] = pcid;

                    table.Rows.Add(row);
                }

                var command = con.CreateCommand();
                command.CommandText = "dbo.sp_getNotificationsNew";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.TypeName = "dbo.NotificationLists";
                //parameter.TypeName = "dbo.UserList";
                parameter.Value = table;
                parameter.ParameterName = "@NID";

                //parameter.ParameterName = "@uid";
                command.Parameters.Add(parameter);
                //var parameters = command.CreateParameter();
                //parameters.TypeName = "dbo.UserList";
                //parameters.Value = table;
                //parameters.ParameterName = "@uid";

                //command.Parameters.Add(parameters);
               
                using (var reader = await command.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        details = new GetNotificationListVM();
                        if (pcid == Int16.Parse(reader["ntfn_for"].ToString()) || 0 == Int16.Parse(reader["ntfn_for"].ToString()))
                        {
                            details.ntfn_ID = Int16.Parse(reader["ntfn_ID"].ToString());
                            details.ntfn_for = Int16.Parse(reader["ntfn_for"].ToString());
                            details.n_msg = reader["n_msg"].ToString();
                            details.ntfn_date_msg = DateTime.Parse(reader["ntfn_date_msg"].ToString());
                            details.validity = DateTime.Parse(reader["validity"].ToString());
                            ListOfVM.Add(details);
                        }

                    }

                    var result = new DataTable();
                    result.Load(reader);
                }
            }
              else if(3 == tid)
            {
                SqlDataAdapter sda = new SqlDataAdapter("select pc_ID from Usersd where id = '" + usid + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                string types = dt.Rows[0]["pc_ID"].ToString();
                int pcid = Convert.ToInt32(types);

                GetNotificationListVM details = null;
                await con.OpenAsync();
                var table = new DataTable();
                table.Columns.Add("NotificationId", typeof(int));
                //table.Columns.Add("userid", typeof(int));

                for (int i = 0; i < getNoticeVM.Count; i++)
                {
                    GetNoticeVM n = getNoticeVM[i];
                    var row = table.NewRow();

                    row["NotificationId"] = n.ntfn_ID;
                    // row["userid"] = pcid;

                    table.Rows.Add(row);
                }

                var command = con.CreateCommand();
                command.CommandText = "dbo.sp_getNotificationsNew";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.TypeName = "dbo.NotificationLists";
                //parameter.TypeName = "dbo.UserList";
                parameter.Value = table;
                parameter.ParameterName = "@NID";

                //parameter.ParameterName = "@uid";
                command.Parameters.Add(parameter);
                //var parameters = command.CreateParameter();
                //parameters.TypeName = "dbo.UserList";
                //parameters.Value = table;
                //parameters.ParameterName = "@uid";

                //command.Parameters.Add(parameters);

                using (var reader = await command.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        details = new GetNotificationListVM();
                        if (usidd == reader["userBy"].ToString() || 0 == Int16.Parse(reader["ntfn_for"].ToString()))
                        {
                            details.ntfn_ID = Int16.Parse(reader["ntfn_ID"].ToString());
                            details.ntfn_for = Int16.Parse(reader["ntfn_for"].ToString());
                            details.n_msg = reader["n_msg"].ToString();
                            details.ntfn_date_msg = DateTime.Parse(reader["ntfn_date_msg"].ToString());
                            details.validity = DateTime.Parse(reader["validity"].ToString());
                            ListOfVM.Add(details);
                        }

                    }

                    var result = new DataTable();
                    result.Load(reader);
                }
            }
                return ListOfVM;
            

        }

        //Price change Notification
        [HttpPost]
        [Route("GetNoticeprice")]
        public async Task<List<GetNotificationListVM>> GetNoticeprice(List<GetNoticeVM> getNoticeVM)
        {
            GetNotificationListVM details = null;
            await con.OpenAsync();
            var table = new DataTable();
            table.Columns.Add("NotificationId", typeof(int));

            for (int i = 0; i < getNoticeVM.Count; i++)
            {
                GetNoticeVM n = getNoticeVM[i];
                var row = table.NewRow();

                row["NotificationId"] = n.ntfn_ID;

                table.Rows.Add(row);
            }

            var command = con.CreateCommand();
            command.CommandText = "dbo.sp_getNotificationsNew";
            command.CommandType = CommandType.StoredProcedure;

            var parameter = command.CreateParameter();
            parameter.TypeName = "dbo.NotificationList";
            parameter.Value = table;
            parameter.ParameterName = "@NID";

            command.Parameters.Add(parameter);
            List<GetNotificationListVM> ListOfVM = new List<GetNotificationListVM>();
            using (var reader = await command.ExecuteReaderAsync())
            {

                while (await reader.ReadAsync())
                {
                    details = new GetNotificationListVM();
                    details.ntfn_ID = Int16.Parse(reader["ntfn_ID"].ToString());
                    details.ntfn_for = Int16.Parse(reader["ntfn_for"].ToString());
                    details.n_msg = reader["n_msg"].ToString();
                    details.ntfn_date_msg = DateTime.Parse(reader["ntfn_date_msg"].ToString());
                    details.validity = DateTime.Parse(reader["validity"].ToString());
                    ListOfVM.Add(details);
                }

                var result = new DataTable();
                result.Load(reader);
            }

            return ListOfVM;
        }

        [HttpGet]
        [Route("PPerGivencategory")]
        public List<GetPPerGivencategoryVM> PPerGivencategory(int pcaregoryID, string userBy)
        {
            SqlParameter prmUserBy = new SqlParameter("@pcaregoryID", pcaregoryID);
            SqlParameter prmReceiverId = new SqlParameter("@userBy", userBy);

            List<GetPPerGivencategoryVM> ListOfVM = db.Database.SqlQuery<GetPPerGivencategoryVM>("sp_PPerGivencategory @pcaregoryID , @userBy", prmUserBy, prmReceiverId).ToList();
            if (ListOfVM.Count() > 0)
            {
                return ListOfVM;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("ListOfNotification")]
        public List<GetNotificationVM> ListOfNotification(string id)
        {
           // SqlParameter prmUserBy = new SqlParameter("@pcaregoryID", pcaregoryID);
            SqlParameter prmReceiverId = new SqlParameter("@pcaregory", id);

            List<GetNotificationVM> ListOfVM = db.Database.SqlQuery<GetNotificationVM>("sp_ListNotifications @pcaregory", prmReceiverId).ToList();
            if (ListOfVM.Count() > 0)
            {
                return ListOfVM;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("getNotification")]
        public List<GetNotificationVM> getNotification(List<ListNotificationVM> listNotificationVM, List<ListNotificationforVM> listNotificationforVM)
        {
            SqlParameter prmUserBy = new SqlParameter("@listNotiVM", listNotificationVM);
            SqlParameter prmReceiverId = new SqlParameter("@listNotiforVM", listNotificationforVM);

            List<GetNotificationVM> ListOfVM = db.Database.SqlQuery<GetNotificationVM>("sp_getNotifications @listNotiVM , @listNotiforVM", prmUserBy, prmReceiverId).ToList();
            if (ListOfVM.Count() > 0)
            {
                return ListOfVM;
            }
            else
            {
                return null;
            }
        }

        

        private double OrderId()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Product_Order", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            double counter = Convert.ToDouble(dt.Rows[0][0].ToString());
            double sId = 1000 + counter;
            return sId;
        }


        string sell_Quans = "";
        string sell_Quan ="";
        string pk_ID="" ;
        string pc_ID = "";
        string sell_Quanss = "";
        [HttpPost]
        [Route("OrderPosts")]
        public ResponseResult OrderPosts(List<OrderPostVM> orderPostVM)
        {
            foreach (var row in orderPostVM)
            {
                SqlDataAdapter _ta = new SqlDataAdapter("Select * From Prdt_Package where prdt_ID = '" + row.prdt_ID + "'", con);
                DataTable _dta = new DataTable();
                _ta.Fill(_dta);
                foreach (DataRow rows in _dta.Rows)
                {

                    sell_Quans = rows["min_Quantity"].ToString();
                    pc_ID = rows["pc_ID"].ToString();
                    pk_ID=rows["pkg_ID"].ToString();
                }
                SqlDataAdapter _taa = new SqlDataAdapter("Select * From Product_Order where pkg_ID = '" + pk_ID + "'", con);
                DataTable _dtaa = new DataTable();
                _taa.Fill(_dtaa);
                foreach (DataRow rowss in _dtaa.Rows)
                {

                    sell_Quanss = rowss["sellQuantity"].ToString();
                    
                }
                int prSell = Convert.ToInt32(sell_Quanss) - row.total_quanti;
                int pcid = Convert.ToInt32(pc_ID);
                int p_id = row.prdt_ID;
                int total_quanti = Convert.ToInt32(sell_Quans) - row.total_quanti;
                int sell_qunttitys= row.total_quanti;
                double o_ID = OrderId();
                string od = o_ID.ToString();
                string ordarby = row.orderBy;
                SqlCommand cmd = new SqlCommand(); 
                cmd.CommandText = "insert into Product_Order(o_ID,prdt_ID,orderBy,orderDate,entryDate,modifyDate,userBy,amount,quantity,sellQuantity,miniQuantity) values('" + o_ID + "','" + row.prdt_ID + "','" + row.orderBy + "','" + row.orderDate + "',GETDATE(),GETDATE(),'" + ordarby + "','" + row.totalAmout + "','" + 0 + "','" + 0 + "','" + 0 + "')";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Notificationinsert(p_id, ordarby, pcid, sell_qunttitys);
                ProductPackageEdit(p_id, total_quanti);
                ProductOrderEdit(pk_ID, prSell);
                ProductInventory(p_id, sell_qunttitys, od);
                
            }

            responseResult.MessageCode = MessageCode.N.ToString();
            responseResult.SystemMessage = "Successfully Product Order";
            return responseResult;

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
        private void Notificationinsert(int p_id,string ordarby,int pcid,int m_qunttitys)
        {

            SqlDataAdapter sda = new SqlDataAdapter("select brnd_Name from Brand where prdt_ID = '" + p_id + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string BrndName = dt.Rows[0]["brnd_Name"].ToString();

            SqlDataAdapter sdas = new SqlDataAdapter("select pkg_Name from Prdt_Package where prdt_ID = '" + p_id + "'", con);
            DataTable dtt = new DataTable();
            sdas.Fill(dtt);
            string PkgName = dtt.Rows[0]["pkg_Name"].ToString();

            SqlDataAdapter sdaaa = new SqlDataAdapter("select name from Usersd where id = '" + ordarby + "'", con);
            DataTable dttt = new DataTable();
            sdaaa.Fill(dttt);
            string Name = dttt.Rows[0]["name"].ToString();

            double n_ID = NotificationId();
            string msg = "Product Order:-  BrndName: " + BrndName + " PackageName: " + PkgName + " Qunttity: " + m_qunttitys + " User: " + Name +" Phone: "+ ordarby;
            DateTime addedDate = DateTime.Now.AddDays(10);



            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Notifications values('" + n_ID + "','" + pcid + "','" + msg + "','" + msg + "',GETDATE(),'" + addedDate + "','" + pcid + "',GETDATE(),GETDATE(),'" + ordarby + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }



        [HttpPost]
        [Route("OrderPost")]
        public ResponseResult OrderPost(OrderPostVM orderPostVM)
        {
            
            Usersd user = new Usersd();
            Prdt_Package p_package = new Prdt_Package();
            Product_Order p_Order = new Product_Order();
            SqlDataAdapter sda = new SqlDataAdapter("select prdt_ID from Product where prdt_ID = '" + orderPostVM.prdt_ID + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

           
            if (dt !=null)
            {
                string types = "Ripon";

                int p_id = orderPostVM.prdt_ID;
                double o_ID = OrderId();
                string od = o_ID.ToString();
                int m_qunttity = orderPostVM.total_quanti;
                string prdt_IDs = orderPostVM.prdt_ID.ToString();

                SqlDataAdapter _ta = new SqlDataAdapter("Select * From Prdt_Package where prdt_ID = '" + prdt_IDs + "'", con);
                DataTable _dta = new DataTable();
                _ta.Fill(_dta);

                foreach (DataRow row in _dta.Rows)
                {

                   sell_Quan = row["min_Quantity"].ToString();
                    pk_ID = row["pkg_ID"].ToString();
                }

                int total_quanti = Convert.ToInt32(sell_Quan) - m_qunttity;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Product_Order(o_ID,prdt_ID,pkg_ID,orderBy,orderDate,entryDate,modifyDate,userBy,amount) values('" + o_ID + "','" + prdt_IDs + "','" + pk_ID + "','" + orderPostVM.orderBy + "','" + orderPostVM.orderDate + "',GETDATE(),GETDATE(),'" + types + "','" + orderPostVM.totalAmout + "')";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
                ProductPackageEdit(p_id, total_quanti);
                ProductInventory(p_id, m_qunttity, od);
                responseResult.MessageCode = MessageCode.N.ToString();
                responseResult.SystemMessage = "Successfully Product Order";
                return responseResult;
            }
            else
            {
                responseResult.MessageCode = MessageCode.N.ToString();
                responseResult.SystemMessage = "Faild Product Order";
                return responseResult;
            }
            
        }

        private void ProductInventory(int p_id, int total_quanti,string o_ID)
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into Inventory(prdt_ID,total_Qunty,modifyDate,o_ID) values('" + p_id + "','" + total_quanti + "',GETDATE(),'" + o_ID + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void ProductPackageEdit(int p_id,int total_quanti)
        {
           
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update Prdt_Package set prdt_ID = '" + p_id + "', min_Quantity = '" + total_quanti + "', modifyDate = GETDATE() where prdt_ID = '" + p_id + "'";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
           
        }
        private void ProductOrderEdit(string pk_ID, int prSell)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update Product_Order set pkg_ID = '" + pk_ID + "', sellQuantity = '" + prSell + "', modifyDate = GETDATE() where pkg_ID = '" + pk_ID + "'";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

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

        
        public enum MessageCode
        {
            Y,
            N
        }


        [HttpGet]
        [Route("Userlist")]
        public List<GetAllRegistrationVM> Userlist()
        {
            List<GetAllRegistrationVM> GetAllRegistrationVMList = _userServices.RegistrationList();
            
            if (GetAllRegistrationVMList.Count() > 0)
            {
                return GetAllRegistrationVMList;
            }
            else
            {
                return null;
            }
        }
        

    }
}
