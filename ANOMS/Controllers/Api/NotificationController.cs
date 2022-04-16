using AN.Entities.CommonModel;
using AN.Entities.Entities;
using AN.Entities.ViewModel;
using AN.infrastructure;
using AN.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ANOMS.Controllers.Api
{
    
    [RoutePrefix("Api/Notification")]
    public class NotificationController : ApiController
    {

        private readonly NotificationService _NotificationService;
        public NotificationController(NotificationService userService) : base()
        {
            this._NotificationService  = userService;
        }

        public  NotificationController()
            {
            }


        private ANOMSBDContext db = new ANOMSBDContext();

        // GET: api/Notifications
        public IQueryable<Notifications> GetNotificationss()
        {
            return db.Notificationss;
        }
        [HttpPost]
       
        [HttpGet]
        [Route("getAllNotice")]
        public List<NotificationVM> getAllNotice(string userBy)

        {
            SqlParameter prmUserBy = new SqlParameter("@UserBy", userBy);
            //SqlParameter prmReceiverId = new SqlParameter("@ReceiverId", listOfMessageInput.ReceiverId);

            List<NotificationVM> ListOfProducts = db.Database.SqlQuery<NotificationVM>("sp_Notification @UserBy", prmUserBy).ToList();
            if (ListOfProducts.Count() > 0)
            {
                return ListOfProducts;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("Get")]
        // GET: api/Notification
        public IEnumerable<NotificationVM> Get(string userBy)
        {
            List<NotificationVM> listOfProducts = _NotificationService.getAllNotice(userBy);
            if (listOfProducts.Count() > 0)
            {
                return listOfProducts;
            }
            else
            {
                return null;
            }
        }

        // GET: api/Notification/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Notification
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Notification/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Notification/5
        public void Delete(int id)
        {
        }
    }
}
