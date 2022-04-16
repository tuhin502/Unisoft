using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AN.Entities.ViewModel;
using AN.Entities.CommonModel;
using AN.Service;

namespace ANOMS.Controllers.Web
{
    public class CheckController : Controller
    {

        private readonly ProductService _productService;
        public CheckController(ProductService productService) : base()
        {
            this._productService = productService;
        }


        // GET: Check
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult AddProduct(AddProductVM addProductVM)
        //{
        //     _productService.SaveProduct(addProductVM);
        //    return View();
        //}
    }
}