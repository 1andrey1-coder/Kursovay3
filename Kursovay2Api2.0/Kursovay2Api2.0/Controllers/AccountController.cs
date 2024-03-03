using ApiDB.DB;
using ApiDB.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kursovay2Api2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MemContext _memContext;
        public AccountController(MemContext userService)
        {
            _memContext = userService;
        }

        [HttpPost("Login")]
        public ActionResult<LoginUserDTO> GetActionLogin(UserLoginDTO userData)
        {
            var user = _memContext.LoginUsers.FirstOrDefault(u => u.LoginName == userData.Login && u.LoginPassword == userData.Password);

            if (user != null)
            {
                return new LoginUserDTO
                {
                    LoginName = user.LoginName,
                    LoginPassword = user.LoginPassword,
                    LoginId = user.LoginId,
                    RoleId = user.RoleId,
                };

            }
            else
            {
                return BadRequest("Логин/пароль неправильный");
            }

        }

    }
}
