using AN.infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AN.Service;
using AN.Entities.CommonModel;
using AN.Entities.ViewModel;
using System.Data.SqlClient;

namespace ANOMS.Controllers.Api
{

    [RoutePrefix("Api/Login")]
    public class LoginController : ApiController
    {
        private readonly UserService _UserService;
        public LoginController(UserService UserService)
        {
            this._UserService = UserService;
        }

        [HttpPost]
        [Route("userSignIn")]
        public ResponseResult userSignIn(string signIn)
        {
            //ResponseResult responseResult = _UserService.userLogin(signIn);
            return null;
        }
        private ANOMSBDContext db = new ANOMSBDContext();
        [HttpPost]
        [Route("login")]
        public List<UserTypeVM> login(SignInVM signIns)
        {
            SqlParameter prmUserBy = new SqlParameter("@UserBy", signIns.id);
            SqlParameter prmReceiverId = new SqlParameter("@ReceiverId", signIns.password);

            List<UserTypeVM> ListOfType = db.Database.SqlQuery<UserTypeVM>("sp_Login @UserBy , @UserBy", prmUserBy , prmReceiverId).ToList();
            if (ListOfType.Count() > 0)
            {
                return ListOfType;
            }
            else
            {
                return null;
            }
        }
    }
}
