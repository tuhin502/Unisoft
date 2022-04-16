using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AN.infrastructure;
using AN.Entities.ViewModel;
using AN.Entities.CommonModel;
using AN.Entities.Entities;
using System.Data.SqlClient;

namespace AN.Service
{
    public class ProductService
    {
        private readonly ANOMSBDContext _dbcontext;
        public ProductService(ANOMSBDContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }


        public List<GetAllProductVM> ProductList( string types)
        {
            SqlParameter prmReceiverId = new SqlParameter("@pcaregory", types);

            // List<OrderVM> ListOfType = db.Database.SqlQuery<OrderVM>("sp_OderStatus @pcaregory", prmReceiverId).ToList();
          
            List<GetAllProductVM> NotificationList = _dbcontext.Database.SqlQuery<GetAllProductVM>("sp_GetProduct @pcaregory", prmReceiverId).ToList();
            return NotificationList;

        }
        public List<GetAllProductVM> ProductList()
        {
           // SqlParameter prmReceiverId = new SqlParameter("@pcaregory", types);

            // List<OrderVM> ListOfType = db.Database.SqlQuery<OrderVM>("sp_OderStatus @pcaregory", prmReceiverId).ToList();

            List<GetAllProductVM> NotificationList = _dbcontext.Database.SqlQuery<GetAllProductVM>("sp_GetProducts").ToList();
            return NotificationList;

        }
        public ProductEntryEditView EditProductEntry(int id)
        {
            SqlParameter entryid = new SqlParameter("@prdt_ID", id);
            ProductEntryEditView EditEntry = _dbcontext.Database.SqlQuery<ProductEntryEditView>("sp_GetProductEdit @prdt_ID", entryid).FirstOrDefault();
            return EditEntry;
        }

        public List<GetAllProductVM> getAllProducts(string userBy)
        {
            SqlParameter prmUserBy = new SqlParameter("@UserBy", userBy);
            //SqlParameter prmReceiverId = new SqlParameter("@ReceiverId", listOfMessageInput.ReceiverId);

            List<GetAllProductVM> ListOfProducts = _dbcontext.Database.SqlQuery<GetAllProductVM>("sp_GetAllProducts @UserBy", prmUserBy).ToList();
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

        public enum MessageCode
        {
            Y,
            N
        }
    }
}
