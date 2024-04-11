using ApiDB;
using ApiDB.DB;
using ApiDB.DTO;
using EmailSenderSMTP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Text;
using XAct.Users;
using XSystem.Security.Cryptography;

namespace Kursovay2Api2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MemContext _memContext;
        private readonly MailService mail;
        private readonly CodeRequest codeRequest;

        public AccountController(MemContext userService, MailService mail, CodeRequest code)
        {
            //MemContext.OnConfigurate(userService);
            _memContext = userService;
            this.mail = mail;
            this.codeRequest = code;
        }

        [HttpGet("Name")]
        public ActionResult<string> GetUserName()
        {

            // Получение имени пользователя из контекста аутентификации
            var userName = User.Identity.Name;

            return userName;
        }



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
        private string GeneratePassword()
        {
            // Генерация случайного пароля (можно использовать любой другой метод)
            return Guid.NewGuid().ToString().Substring(0, 8);
        }


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


                //MailMessage message = EmailMessageService.sender.CreateMailMessageBodyIsText
                //    ("Словарь сленга", registerUser.Mail, "Регистрация", $"Ваш пароль: {password}");
                //EmailMessageService.sender.SendMail(message);

                await mail.Send(registerUser.Mail, "Регистрация в Словаре сленга"
                    , $"Ваш пароль: {password}");

                return Ok(loginUserDTO);



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

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword(ResetDTO resetUser)
        {
            var user = await _memContext.LoginUsers.FirstOrDefaultAsync(u => u.Mail == resetUser.Mail);

            if (user == null)
            {
                return NotFound("Почта не найдена");
            }

            // Генерация нового пароля
            var newPassword = GenerateRandomPassword();
            user.LoginPassword = HashPassword(newPassword);
            //user.LoginPassword = newPassword;

            _memContext.Entry(user).State = EntityState.Modified;
            await _memContext.SaveChangesAsync();

            // Отправка нового пароля на почту (реализация этой функции опускается)
            await mail.Send(resetUser.Mail, "Регистрация в Словаре сленга"
                 , $"Ваш пароль: {newPassword}");

            return Ok("Пароль был сброшен и обновлен");
        }



        [HttpPost("VerifyCode")]
        public IActionResult VerifyCode([FromBody] ResetDTO model)
        {
            // Ваша логика проверки кода здесь
            if (model.Code == codeRequest.GetCode(model.Mail)) // Пример проверки кода
            {
                return Ok(new { message = "Код подтвержден" });
            }
            else
            {
                return BadRequest(new { error = "Неверный код" });
            }
        }



        [HttpPost("GenerateCode")]
        public async Task<IActionResult> GenerateCode(ResetDTO resetUser)
        {
            try
            {
                var user = await _memContext.LoginUsers.FirstOrDefaultAsync(u => u.Mail == resetUser.Mail);

                if (user == null)
                {
                    return NotFound("Почта не найдена");
                }
                // Генерация нового кода
                var code = GenerateRandomCode();
                codeRequest.SetCode(user.Mail, code);
                // Отправка нового кода на почту
                await mail.Send(resetUser.Mail, "Потверждение почты для сброса пароля в Словаре сленга"
                    , $"Ваш код потверждения: {code}");

                return Ok(code);
            }
            catch (Exception ex)
            {
                return BadRequest("Код неверный");
            }

        }

        private string GenerateRandomCode()
        {
            // Генерация случайного кода
            Random random = new Random();
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var code = new string(Enumerable.Repeat(characters, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return code;
        }



        //Вывод всего
        //[HttpGet("RoflListAll")]
        //public async Task<ActionResult<IEnumerable<Rofl>>> GetRofls()
        //{
        //    return await _memContext.Rofls.ToListAsync();
        //}
        //Вывод всего


        //работает как надо

        [HttpGet("RoflList")]
        public async Task<ActionResult<IEnumerable<RoflDTO>>> RoflList()
        {
            var rofls = _memContext.Rofls.Include(s => s.RoflGenre).Include(s => s.RoflStart).
                Include(s => s.RoflEnd).Include(s => s.RoflStatus).Include(s => s.Teg).ToList();
            var result = rofls.Select(rofl => new RoflDTO
            {
                RoflName = rofl.RoflName,
                RoflId = rofl.RoflId,
                TegId = rofl.Teg.TegName,
                RoflGenreId = rofl.RoflGenre.GenreName,
                RoflStartId = rofl.RoflStart.StartName,
                RoflStatusId = rofl.RoflStatus.StatusName,
                RoflEndId = rofl.RoflEnd.EndName,
                RoflOpisanie = rofl.RoflOpisanie,
                RoflImage = rofl.RoflImage,
                RoflDateTime = rofl.RoflDateTime,

            });
            return Ok(result);

        }
        //работает как надо


        [HttpPost("AddRofl")]
        public async Task<IActionResult> AddRofl(RoflDTO rofl)
        {
            return null;
        }

        [HttpDelete("DeleteRofl")]
        public async Task<IActionResult> DeleteRofl(int id)
        {
            if (_memContext.Rofls == null)
            {
                return NotFound();
            }
            var rofl = await _memContext.Rofls.FindAsync(id);

            if (rofl == null)
            {
                return NotFound();
            }

            var rofls = _memContext.Rofls.Where(s => s.RoflId == id).ToList();
            _memContext.Rofls.RemoveRange(rofls);
            _memContext.Rofls.Remove(rofl);
            await _memContext.SaveChangesAsync();
            return NoContent();


        }

        [HttpPut("PutRofl")]
        public async Task<ActionResult<IEnumerable<RoflDTO>>> PutRofl(int id, RoflDTO rofl)
        {
            // Проверка, что переданные данные валидны
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Поиск существующей шутки в базе данных
            //var existingRofl = await _memContext.Rofls.FirstOrDefaultAsync(s => s.RoflId == id);

            //if (existingRofl == null)
            //{
            //    return NotFound();
            //}
            var rofls = _memContext.Rofls.Include(s => s.RoflGenre).Include(s => s.RoflStart).
               Include(s => s.RoflEnd).Include(s => s.RoflStatus).Include(s => s.Teg).ToList();
            var result = rofls.Select(rofl => new RoflDTO
            {
                RoflName = rofl.RoflName,
                RoflId = rofl.RoflId,
                TegId = rofl.Teg.TegName,
                RoflGenreId = rofl.RoflGenre.GenreName,
                RoflStartId = rofl.RoflStart.StartName,
                RoflStatusId = rofl.RoflStatus.StatusName,
                RoflEndId = rofl.RoflEnd.EndName,
                RoflOpisanie = rofl.RoflOpisanie,
                RoflImage = rofl.RoflImage,
                RoflDateTime = rofl.RoflDateTime,

            });
          
            // Обновление свойств существующей шутки
            //existingRofl.RoflName = rofl.RoflName;
            //existingRofl.RoflOpisanie = rofl.RoflOpisanie;
            //existingRofl.RoflStatusId = rofl.RoflStartId;
            //existingRofl.RoflEndId = rofl.RoflEndId;
            //existingRofl.RoflGenreId = rofl.RoflGenreId;
            //existingRofl.RoflDateTime = rofl.RoflDateTime;
            //existingRofl.RoflStartId = rofl.RoflStartId;
            //existingRofl.RoflImage = rofl.RoflImage;
          


            _memContext.Entry(result).State = EntityState.Modified;

            try
            {
                // Сохранение изменений в базе данных
                await _memContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Возвращение обновленного объекта
            return Ok(result);
            //return NoContent();
        }

        private bool DishExists(int id)
        {
            return (_memContext.Rofls?.Any(e => e.RoflId == id)).GetValueOrDefault();
        }
    }
    
}


   





    

