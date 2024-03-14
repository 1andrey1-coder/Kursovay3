using ApiDB;
using ApiDB.DB;
using ApiDB.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("Name")]
        public ActionResult<string> GetUserName()
        {
           
            // Получение имени пользователя из контекста аутентификации
            var userName = User.Identity.Name;

            return userName;
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
        [HttpPost("Register")]
        public async Task<ActionResult<LoginUserDTO>> GetActionRegister(RegisterDTO registerUser)
        {
            try
            {
                // Проверка наличия пользователя с таким логином
                var ProverkaUser = await _memContext.LoginUsers.
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
                        LoginId = registerUser.Id,
                        LoginName = registerUser.Login,
                        LoginPassword = registerUser.Password,
                        RoleId = 2

                    };


                    _memContext.LoginUsers.Add(newUser);
                    await _memContext.SaveChangesAsync();

                    //Возвращаем созданный объект в виде DTO для отправки ответа клиенту
                    var loginUserDTO = new LoginUserDTO
                    {
                        LoginId = newUser.LoginId,
                        LoginName = newUser.LoginName,
                        LoginPassword = newUser.LoginPassword,
                        RoleId = newUser.RoleId
                    };

                    return Ok(loginUserDTO);
                }


            }
            catch (Exception ex)
            {

                return StatusCode(500, "Произошла ошибка при регистрации пользователя");
            }


            //return null;
        }
        [HttpPost("RegisterToAdmin")]
        public async Task<ActionResult<LoginUserDTO>> GetActionRegisterAdmin(RegisterDTO registerUser)
        {
            try
            {
                // Проверка наличия пользователя с таким логином
                var ProverkaUser = await _memContext.LoginUsers.
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
                        LoginId = registerUser.Id,
                        LoginName = registerUser.Login,
                        LoginPassword = registerUser.Password,
                        RoleId = 1

                    };


                    _memContext.LoginUsers.Add(newUser);
                    await _memContext.SaveChangesAsync();

                    //Возвращаем созданный объект в виде DTO для отправки ответа клиенту
                    var loginUserDTO = new LoginUserDTO
                    {
                        LoginId = newUser.LoginId,
                        LoginName = newUser.LoginName,
                        LoginPassword = newUser.LoginPassword,
                        RoleId = newUser.RoleId
                    };

                    return Ok(loginUserDTO);
                }


            }
            catch (Exception ex)
            {

                return StatusCode(500, "Произошла ошибка при регистрации пользователя");
            }


            //return null;
        }


        [HttpGet("Rofl")]
        public ActionResult<RoflDTO> GetBrawlerById(string name)
        {
            var user = _memContext.Rofls.FirstOrDefault(r => r.RoflName == name);

            if (user == null)
            {
                return NotFound();
            }

            // Создаем экземпляр DTO и заполняем его данными из найденного пользователя
            var roflDto = new RoflDTO
            {
                RoflName = user.RoflName,
                TegId = user.TegId,
                RoflDateTime = user.RoflDateTime,
                RoflEndId = user.RoflEndId,
                RoflGenreId = user.RoflGenreId,
                RoflId = user.RoflId,
                RoflImage = user.RoflImage,
                RoflOpisanie = user.RoflOpisanie,
                RoflStartId = user.RoflStartId,
                RoflStatusId = user.RoflStatusId,
            };

            return roflDto;
        }



        [HttpGet("Login/{id}")]
        public ActionResult<LoginUserDTO> GetUser(int id)
        {
            LoginUser user = _memContext.LoginUsers.FirstOrDefault(u => u.LoginId == id);


            if (user != null)
            {
                LoginUserDTO userDTO = new LoginUserDTO
                {
                    LoginId = user.LoginId,
                    LoginName = user.LoginName,
                    RoleId = user.RoleId,
                };


                return Ok(userDTO);
            }
            else
            {
                return NotFound(); // Возвращаем ошибку 404 если пользователя не найден
            }
        }
        //// Создаем словари для соответствия цифровых значений и текстовых представлений
        //Dictionary<int, string> statusDict = new Dictionary<int, string>
        //{
        //    { 1, "Active" },
        //    { 2, "Inactive" },
        //    // добавьте остальные статусы
        //};

        //// Здесь также можно создать словари для других полей (genre, start и т.д.)

        //// Получаем текстовое представление для каждого поля
        //string status = statusDict.ContainsKey(user.RoflStatusId) ? statusDict[user.RoflStatusId] : "Unknown";
        //// Аналогично для остальных полей

        //// Создаем экземпляр DTO и заполняем его данными из найденного пользователя
        //var roflDto = new RoflDTO
        //{
        //    RoflName = user.RoflName,
        //    RoflDateTime = user.RoflDateTime,
        //    RoflEndId = $"{user.RoflEndId} - {endDict[user.RoflEndId]}",
        //    RoflGenreId = $"{user.RoflGenreId} - {genreDict[user.RoflGenreId]}",
        //    RoflId = user.RoflId,
        //    RoflImage = user.RoflImage,
        //    RoflOpisanie = user.RoflOpisanie,
        //    RoflStartId = $"{user.RoflStartId} - {startDict[user.RoflStartId]}",
        //    RoflStatusId = $"{user.RoflStatusId} - {status}",
        //};

        //return roflDto;

    }
}
