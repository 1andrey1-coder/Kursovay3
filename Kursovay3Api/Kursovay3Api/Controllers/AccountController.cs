using ApiDB.DB;
using ApiDB.DTO;
using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kursovay3Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly MemContext _memContext;

        public AccountController(MemContext memUser)
        {
            _memContext = memUser;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<LoginUserDTO>> PostLogin(string Login, string password)
        {
            
                var login = _memContext.LoginUsers.FirstOrDefault(
                    s=>s.LoginName == Login && s.LoginPassword==password);

                if (login != null)
                {
                    var loginUserDTO = new LoginUserDTO
                    {
                        LoginId = login.LoginId,
                        LoginName = login.LoginName,
                        LoginPassword = login.LoginPassword,
                        RoleId = login.RoleId
                        // Add any other properties you want to include in the LoginUserDTO
                    };
                   
                    return Ok(loginUserDTO);
                }
                else
                {
                    return NotFound();
                }

            
            
            
        }
    }
}
