using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AN.Entities.Entities;
using AN.infrastructure;

namespace ANOMS.Controllers.Api
{
    [RoutePrefix("Api/Notifications")]
    public class NotificationsController : ApiController
    {
        private ANOMSBDContext db = new ANOMSBDContext();

        // GET: api/Notifications
        public IQueryable<Notifications> GetNotificationss()
        {
            return db.Notificationss;
        }

        // GET: api/Notifications/5
        [ResponseType(typeof(Notifications))]
        public IHttpActionResult GetNotifications(int id)
        {
            Notifications notifications = db.Notificationss.Find(id);
            if (notifications == null)
            {
                return NotFound();
            }

            return Ok(notifications);
        }

        // PUT: api/Notifications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNotifications(int id, Notifications notifications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notifications.ntfn_ID)
            {
                return BadRequest();
            }

            db.Entry(notifications).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Notifications
        [ResponseType(typeof(Notifications))]
        public IHttpActionResult PostNotifications(Notifications notifications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notificationss.Add(notifications);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = notifications.ntfn_ID }, notifications);
        }

        // DELETE: api/Notifications/5
        [ResponseType(typeof(Notifications))]
        public IHttpActionResult DeleteNotifications(int id)
        {
            Notifications notifications = db.Notificationss.Find(id);
            if (notifications == null)
            {
                return NotFound();
            }

            db.Notificationss.Remove(notifications);
            db.SaveChanges();

            return Ok(notifications);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotificationsExists(int id)
        {
            return db.Notificationss.Count(e => e.ntfn_ID == id) > 0;
        }
    }
}