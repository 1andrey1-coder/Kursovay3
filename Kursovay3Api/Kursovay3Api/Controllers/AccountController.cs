using ApiDB.DB;
using ApiDB.DTO;
using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

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
                s => s.LoginName == Login && s.LoginPassword == password);

            if (login != null)
            {
                var loginUserDTO = new LoginUserDTO
                {
                    LoginId = login.LoginId,
                    LoginName = login.LoginName,
                    LoginPassword = login.LoginPassword,
                  
                   
                };

                return Ok(loginUserDTO);
            }
            else
            {
                return NotFound();
            }




        }
        [HttpPost("Register")]
        public async Task<ActionResult<LoginUserDTO>> PostRegister(RegisterDTO registerUser)
        {
            try
            {
                // Проверка наличия пользователя с таким логином
                var ProverkaUser = _memContext.LoginUsers.
                    FirstOrDefaultAsync(u => u.LoginName == registerUser.Login);

                if (ProverkaUser != null)
                {
                    return BadRequest("Пользователь с таким логином уже существует");
                }
                else
                {
                    // Создание нового объекта LoginUser и добавление его в контекст
                    var newUser = new LoginUser
                    {
                        LoginName = registerUser.Login,
                        LoginPassword = registerUser.Password,
                    };

                    _memContext.LoginUsers.Add(newUser);
                    await _memContext.SaveChangesAsync();

                    //Возвращаем созданный объект в виде DTO для отправки ответа клиенту
                    var loginUserDTO = new LoginUserDTO
                    {
                        LoginId = newUser.LoginId,
                        LoginName = newUser.LoginName,
                        LoginPassword = newUser.LoginPassword,

                    };

                    return Ok(loginUserDTO);
                }

                
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Произошла ошибка при регистрации пользователя");
            }
        }
    }
}

