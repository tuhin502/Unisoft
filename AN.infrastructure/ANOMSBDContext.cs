using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AN.Entities.Entities;


namespace AN.infrastructure
{
    public class ANOMSBDContext: DbContext
    {
        public ANOMSBDContext() :
           base("ANOMSBDConnectionString")
        {
        }
        public DbSet<Usersd> AUserss { set; get; }
        public DbSet<Product> products { get; set; }
        public DbSet<Brand> Brands { set; get; }
        public DbSet<Inventory> Inventorys { get; set; }
        public DbSet<Prdt_Package> Prdt_Packages { set; get; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Product_Order> Product_Orders { set; get; }

        public DbSet<UserDetails> UserDetailss { set; get; }
        public DbSet<User_Type> User_Types { get; set; }
        public DbSet<P_Category> P_Categorys { set; get; }
        public DbSet<N_Category> N_Categorys { get; set; }
        public DbSet<Notifications> Notificationss { set; get; }
        //public System.Data.Entity.DbSet<AN.Entities.ViewModel.GetAllRegistrationVM> GetAllRegistrationVMs { get; set; }
    }
}
