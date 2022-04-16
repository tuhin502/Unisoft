using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AN.Entities.ViewModel;
using AN.Entities.CommonModel;
using AN.Entities.Entities;
using AN.infrastructure;
using System.Data.SqlClient;

namespace AN.Service
{

    public class UserService
    {
        private readonly ANOMSBDContext _dbcontext;
        public UserService(ANOMSBDContext Sdbcontext)
        {
            this._dbcontext = Sdbcontext;
        }

        public ResponseResult userLogind(SignInVM signIns)
        {
            ResponseResult responseResult = new ResponseResult();
            if (signIns == null)
            {
                responseResult.Content = null;
                responseResult.MessageCode = MessageCode.N.ToString();
                responseResult.SystemMessage = "Inputs is null!";
                return responseResult;
            }
            else if (signIns != null)
            {
                Usersd users = _dbcontext.AUserss.Where(x => x.id == signIns.id && x.password== signIns.password).FirstOrDefault();
                if (users != null)
                {
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Successfully User Login";
                    return responseResult;

                }
                else
                {
                    responseResult.Content = users;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Please Check Carefully Password and Phone Number";
                    return responseResult;
                }
            }
            else
            {
                responseResult.Content = null;
                responseResult.MessageCode = MessageCode.N.ToString();
                responseResult.SystemMessage = "Inputs is null!";
                return responseResult;
            }

        }
        public List<GetAllRegistrationVM> RegistrationListForAdmin()
        {
            SqlParameter t_ID = new SqlParameter("@t_ID", 2);
            List<GetAllRegistrationVM> RegistrationList = _dbcontext.Database.SqlQuery<GetAllRegistrationVM>("sp_GetRegistrationForAdmin @t_ID", t_ID).ToList();
            return RegistrationList;
        }
        public List<GetAllRegistrationVM> RegistrationList()
        {
            List<GetAllRegistrationVM> RegistrationList = _dbcontext.Database.SqlQuery<GetAllRegistrationVM>("sp_GetRegistration").ToList();
            return RegistrationList;
        }
        public List<GetAllNotificationVM> NotificationLists(string types)
        {
            SqlParameter prmReceiverId = new SqlParameter("@pcaregory", types);
            List<GetAllNotificationVM> NotificationList = _dbcontext.Database.SqlQuery<GetAllNotificationVM>("sp_GetNotificata @pcaregory", prmReceiverId).ToList();
            return NotificationList;

        }
        public List<GetAllNotificationVM> NotificationList()
        {
            List<GetAllNotificationVM> NotificationList = _dbcontext.Database.SqlQuery<GetAllNotificationVM>("sp_GetNotification").ToList();
            return NotificationList;

        }

        public List<GetOrderListVM> OrderList(string types)
        {
            SqlParameter prmReceiverId = new SqlParameter("@pcaregory", types);

            List<GetOrderListVM> ListOfVM = _dbcontext.Database.SqlQuery<GetOrderListVM>("sp_GetOrder @pcaregory", prmReceiverId).ToList();
            //if (ListOfVM.Count() > 0)
            //{
               return ListOfVM;
            //}
            //else
            //{
            //    return null;
            //}
            //List<GetOrderListVM> OrderList = _dbcontext.Database.SqlQuery<GetOrderListVM>("sp_GetOrder").ToList();
            //return OrderList;

        }
        public List<GetOrderListVM> OrderListss()
        {
            //SqlParameter prmReceiverId = new SqlParameter("@pcaregory", types);

            List<GetOrderListVM> ListOfVM = _dbcontext.Database.SqlQuery<GetOrderListVM>("sp_GetOrders").ToList();

            return ListOfVM;

        }
        public GetOrderListVM DetailsOrderlists(int id)
        {
            SqlParameter entryid = new SqlParameter("@prdt_ID", id);
            GetOrderListVM DetailsEntry = _dbcontext.Database.SqlQuery<GetOrderListVM>("sp_GetOrderdetails @prdt_ID", entryid).FirstOrDefault();
            return DetailsEntry;
        }
        public GetOrderListVM DetailsOrderlist(int id)
        {
            SqlParameter entryid = new SqlParameter("@prdt_ID", id);
            GetOrderListVM DetailsEntry = _dbcontext.Database.SqlQuery<GetOrderListVM>("sp_GetOrderdetails @prdt_ID", entryid).FirstOrDefault();
            return DetailsEntry;
        }
        public GetOrderListVMs OrderStatus(int id)
        {
            SqlParameter entryid = new SqlParameter("@o_ID", id);
            GetOrderListVMs StatusEntry = _dbcontext.Database.SqlQuery<GetOrderListVMs>("sp_OrderStatusnew @o_ID", entryid).FirstOrDefault();
            return StatusEntry;
        }
        public UserDetailsVM userdetails(string id)
        {
            SqlParameter entryid = new SqlParameter("@id", id);
            UserDetailsVM userentry = _dbcontext.Database.SqlQuery<UserDetailsVM>("sp_GetUserDetails @id", entryid).FirstOrDefault();
            return userentry;
        }

        public ActivityupdateVM activityUpdate(string id)
        {
            SqlParameter entryid = new SqlParameter("@id", id);
            ActivityupdateVM activityentry = _dbcontext.Database.SqlQuery<ActivityupdateVM>("sp_GetActivity @id", entryid).FirstOrDefault();
            return activityentry;
        }
        public enum MessageCode
        {
            Y,
            N
        }
        
    }
}
