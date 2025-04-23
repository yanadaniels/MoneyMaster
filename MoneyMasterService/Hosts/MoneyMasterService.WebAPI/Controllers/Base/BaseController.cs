using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MoneyMasterService.WebAPI.Controllers.Base
{
    [ApiController]
    public abstract class BaseController : ControllerBase
        
    {
        protected readonly IMapper _mapper;

        protected BaseController( IMapper mapper)
        {
            _mapper = mapper;
        }

        //protected virtual Guid UserId => Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "ID").Value);
        //protected virtual string UserRole => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
        //protected virtual string UserName => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
        //protected virtual string UserEmail => User.Claims.FirstOrDefault(c => c.Type == "EMail").Value;

        /// <summary>
        /// Получить токен из заголовка запроса;
        /// </summary>
        /// <returns></returns>        //protected virtual string? GetToken()
        //{
        //    if (HttpContext.Request.Headers.ContainsKey("Authorization"))
        //        return HttpContext.Request.Headers["Authorization"].First();

        //    return null;
        //}

    }
}
