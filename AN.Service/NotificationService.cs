using AN.Entities.ViewModel;
using AN.infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Service
{
    public class NotificationService
    {
        private readonly ANOMSBDContext _dbcontext;
        public NotificationService(ANOMSBDContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
        public List<NotificationVM> getAllNotice(string userBy)
        {
            SqlParameter prmUserBy = new SqlParameter("@UserBy", userBy);
            //SqlParameter prmReceiverId = new SqlParameter("@ReceiverId", listOfMessageInput.ReceiverId);

            List<NotificationVM> ListOfProducts = _dbcontext.Database.SqlQuery<NotificationVM>("sp_Notification @UserBy", prmUserBy).ToList();
            if (ListOfProducts.Count() > 0)
            {
                return ListOfProducts;
            }
            else
            {
                return null;
            }
            //throw new NotImplementedException();
        }

    }
}
