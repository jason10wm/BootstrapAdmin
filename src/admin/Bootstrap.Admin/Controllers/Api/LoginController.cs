﻿using Bootstrap.Admin.Query;
using Bootstrap.DataAccess;
using Bootstrap.Security.Authentication;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Bootstrap.Admin.Controllers.Api
{
    /// <summary>
    /// 登陆接口
    /// </summary>
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// 获得登录历史记录
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public QueryData<LoginUser> Get([FromQuery]QueryLoginOption value) => value.RetrieveData();

        /// <summary>
        /// JWT 登陆认证接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public string Post([FromBody]User user)
        {
            var token = string.Empty;
            string userName = user.UserName;
            string password = user.Password;
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && UserHelper.Authenticate(userName, password))
            {
                token = BootstrapAdminJwtTokenHandler.CreateToken(userName);
            }
            HttpContext.Log(userName, token != null);
            return token;
        }

        /// <summary>
        /// 下发手机短信方法
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Put([FromServices]ISMSProvider provider, [FromQuery]string phone) => string.IsNullOrEmpty(phone) ? false : await provider.SendCodeAsync(phone);

        /// <summary>
        /// 跨域握手协议
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public string Options()
        {
            return null;
        }
    }
}
