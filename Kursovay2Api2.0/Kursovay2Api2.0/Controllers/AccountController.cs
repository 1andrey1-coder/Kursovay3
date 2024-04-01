using ApiDB;
using ApiDB.DB;
using ApiDB.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text;
using XSystem.Security.Cryptography;

namespace Kursovay2Api2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MemContext _memContext;
        private readonly MailService mail;

        public AccountController(MemContext userService, MailService mail)
        {
            //MemContext.OnConfigurate(userService);
            _memContext = userService;
            this.mail = mail;
        }

        [HttpGet("Name")]
        public ActionResult<string> GetUserName()
        {
           
            // Получение имени пользователя из контекста аутентификации
            var userName = User.Identity.Name;

            return userName;
        }

        //[HttpPost]
        //public async Task<IActionResult> RegisterUser(LoginUserDTO userDto)
        //{
        //    var user = new LoginUser
        //    {
        //        LoginName = userDto.LoginName,
        //        Mail = userDto.Mail,
        //        RoleId = 2
        //    };

        //    var password = GenerateRandomPassword();

        //    user.LoginPassword = HashPassword(password); // Хэшируем пароль перед сохранением в базу данных

        //    _memContext.LoginUsers.Add(user);
        //    _memContext.SaveChanges();

        //    // Отправляем пароль на почту
        //    var emailService = new EmailService();
        //    await emailService.SendEmailAsync(userDto.Mail, "Регистрация", $"Ваш пароль: {password}");

        //    return Ok();
        //}

        private string GenerateRandomPassword()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var password = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return password;
        }
        //хэширую пароль в бд
        private string HashPassword(string password)
        {
            var sha256 = new SHA256Managed();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedPasswordBytes);
        }

        //получаю хэшированый пароль и делаю обратное хеширование
        //private string DecryptHashedPassword(string hashedPassword)
        //{
        //    byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

        //    var sha256 = new SHA256Managed();
        //    byte[] decryptedPasswordBytes = sha256.ComputeHash(hashedPasswordBytes);

        //    return Encoding.UTF8.GetString(decryptedPasswordBytes);
        //}

        //public static bool VerifyPassword(string inputPassword, string storedPassword)
        //{
        //    return HashPassword(inputPassword) == storedPassword;
        //}

        [HttpPost("Login")]
        public ActionResult<LoginUserDTO> GetActionLogin(UserLoginDTO userData)
        {
            var hashedPassword = HashPassword(userData.Password);
            var user = _memContext.LoginUsers.FirstOrDefault(u => u.Mail == userData.Mail
            && u.LoginPassword == hashedPassword); // предполагается, что в базе сохраняется хэшированный пароль

            if (user != null)
            {
                return new LoginUserDTO
                {
                    Mail = user.Mail,
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

        // 16 юзер
        //Nwgf1pr9 - Пароль
        //o - Логин

        [HttpPost("Register")]
        public async Task<ActionResult<LoginUserDTO>> GetActionRegister(RegisterDTO registerUser)
        {
            try
            {

                // Проверка наличия пользователя с таким логином
                var ProverkaUser = await _memContext.LoginUsers.
                    FirstOrDefaultAsync(u => u.Mail == registerUser.Mail);

                if (ProverkaUser != null)
                {
                    return BadRequest("Пользователь с такой почтой уже существует");
                }

                var user = new LoginUser
                {
                    LoginId = registerUser.Id,
                    LoginName = registerUser.Login,
                    Mail = registerUser.Mail,
                    RoleId = 2
                };


                var password = GenerateRandomPassword();

                user.LoginPassword = HashPassword(password); // Хэшируем пароль перед сохранением в базу данных

                _memContext.LoginUsers.Add(user);
                _memContext.SaveChanges();
                //Возвращаем созданный объект в виде DTO для отправки ответа клиенту
                    var loginUserDTO = new LoginUserDTO
                    {
                        LoginId = user.LoginId,
                        LoginName = user.LoginName,
                        LoginPassword = user.LoginPassword,
                        Mail = registerUser.Mail,
                        RoleId = user.RoleId
                    };

              
                // Отправляем пароль на почту
                //СингТон
               
                await mail.Send("ilchenkor17@mail.ru", registerUser.Mail, "Регистрация", $"Ваш пароль: {password}");

                return Ok(loginUserDTO);

                //// Проверка наличия пользователя с таким логином
                //var ProverkaUser = await _memContext.LoginUsers.
                //    FirstOrDefaultAsync(u => u.LoginName == registerUser.Login);

                //if (ProverkaUser != null)
                //{
                //    return BadRequest("Пользователь с таким логином уже существует");
                //}
                //else
                //{
                //    // Создание нового объекта LoginUser и добавление его в контекст
                //    var newUser = new LoginUser
                //    {
                //        LoginId = registerUser.Id,
                //        LoginName = registerUser.Login,
                //        LoginPassword = registerUser.Password,
                //        RoleId = 2

                //    };


                //    _memContext.LoginUsers.Add(newUser);
                //    await _memContext.SaveChangesAsync();

                //    //Возвращаем созданный объект в виде DTO для отправки ответа клиенту
                //    var loginUserDTO = new LoginUserDTO
                //    {
                //        LoginId = newUser.LoginId,
                //        LoginName = newUser.LoginName,
                //        LoginPassword = newUser.LoginPassword,
                //        RoleId = newUser.RoleId
                //    };

                //    return Ok(loginUserDTO);
                //}


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
    

    }
}
