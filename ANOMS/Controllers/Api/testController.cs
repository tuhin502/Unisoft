using AN.Entities.CommonModel;
using AN.Entities.ViewModel;
using AN.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ANOMS.Controllers.Api
{
    [RoutePrefix("Api/test")]
    public class testController : ApiController
    {
        private readonly UserService _UserService;
        public testController(UserService UserService):base()
        {
            this._UserService = UserService;
        }

        public testController()
        {

        }
        //[HttpPost]
        //[Route("userSignIn")]
        //public ResponseResult userSignIn(string id,string password)
        //{
        //    ResponseResult responseResult = _UserService.userLogin(id, password);
        //    return responseResult;
        //}
        //GET: api/test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/test/5
        public void Delete(int id)
        {
        }
    }
}
