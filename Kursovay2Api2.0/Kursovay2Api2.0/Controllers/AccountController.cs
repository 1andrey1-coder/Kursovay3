using ApiDB;
using ApiDB.DB;
using ApiDB.DTO;
using EmailSenderSMTP;
using Kursovay2Api2._0.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using XAct;
using XAct.Domain.Repositories;
using XAct.Messages;
using XAct.Users;
using XSystem.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace Kursovay2Api2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApiDB.DB.MemContext _memContext;
        private readonly MailService mail;
        private readonly CodeRequest codeRequest;

        public AccountController(ApiDB.DB.MemContext userService, MailService mail, CodeRequest code)
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
            //LoginImage
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
                user.LoginPassword = HashPassword(password);
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
                await mail.Send(registerUser.Mail, "Регистрация в Словаре сленга"
                    , $"Ваш пароль: {password}");

                return Ok(loginUserDTO);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Произошла ошибка при регистрации пользователя");
            }
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
                    LoginImage = user.LoginImage,
                    Mail = user.Mail,
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
            await mail.Send(resetUser.Mail, "Новый пароль в Словаре сленга"
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
        //ilchenkor1135@suz-ppk.ru
        //YPwYBwyp


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

                return Ok();
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


        [HttpGet("SlangOldList")]
        public async Task<ActionResult<IEnumerable<SlangAndOld>>> SlangList()
        {
            return await _memContext.SlangAndOlds.ToListAsync();
        }

        [HttpGet("ListUserAdmin")]
        public async Task<ActionResult<IEnumerable<LoginUser>>> ListUserAdmins()
        {
            return await _memContext.LoginUsers.ToListAsync();
        }



        [HttpGet("RoflList")]
        public async Task<ActionResult<IEnumerable<RoflDTO>>> RoflList()
        {
            var rofls = _memContext.Rofls.Include(s => s.RoflGenre).Include(s => s.RoflStart).
                Include(s => s.RoflEnd).Include(s => s.RoflStatus).Include(s => s.Teg).ToList();



            var result = rofls.Select(rofl =>
             new RoflDTO
             {
                 RoflId = rofl.RoflId,
                 Teg = rofl.Teg?.TegName,
                 RoflGenre = rofl.RoflGenre?.GenreName,
                 RoflStart = rofl.RoflStart?.StartName,
                 RoflStatus = rofl.RoflStatus?.StatusName,
                 RoflName = rofl.RoflName,
                 RoflOpisanie = rofl.RoflOpisanie,
                 RoflMinOpisanie = rofl.RoflMinOpisanie,
                 RoflDateTime = rofl.RoflDateTime,
                 RoflEnd = rofl.RoflEnd?.EndName,
                 RoflImage = rofl.RoflImage,
             });
            return Ok(result);






        }


        [HttpPost("ListSlangAndOld")]
        public async Task<ActionResult<IEnumerable<SlangAndOld>>> ListSlangAndOld([FromBody] SlangAndOld rofl)
        {
            try
            {
                var users = _memContext.SlangAndOlds.Where(s => s.Slang == rofl.Slang).ToList();

                IQueryable<SlangAndOldDTO> search;
                if (!string.IsNullOrEmpty(rofl.Slang))
                {
                    search = _memContext.SlangAndOlds
                        .Where(s => s.Slang.Contains(rofl.Slang))
                        .Select(s => new SlangAndOldDTO
                        {
                            Slang = s.Slang,
                            OldSlang = s.OldSlang,
                        });
                }
                if (rofl.Slang == "")
                {
                    var use = _memContext.SlangAndOlds.ToList();
                    return Ok(use);

                }
                return Ok(users);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("ComboBoxStatus")]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatusTable()
        {

            var data = await _memContext.Statuses.ToListAsync();
            var result = data.Select(s => new StatusDTO
            {
                StatusId = s.StatusId,
                StatusName = s.StatusName,
            });
            return Ok(result);
        }
        [HttpGet("ComboBoxTeg")]
        public async Task<ActionResult<IEnumerable<Teg>>> GetTegTable()
        {

            var data = await _memContext.Tegs.ToListAsync();
            var result = data.Select(s => new TegDTO
            {
                TegId = s.TegId,
                TegName = s.TegName,
            });
            return Ok(result);
        }
        [HttpGet("ComboBoxEnd")]
        public async Task<ActionResult<IEnumerable<End>>> GetEndTable()
        {

            var data = await _memContext.Ends.ToListAsync();
            var result = data.Select(s => new EndDTO
            {
                EndId = s.EndId,
                EndName = s.EndName,
            });
            return Ok(result);
        }
        [HttpGet("ComboBoxStart")]
        public async Task<ActionResult<IEnumerable<Start>>> GetStartTable()
        {

            var data = await _memContext.Starts.ToListAsync();
            var result = data.Select(s => new StartDTO
            {
                StartId = s.StartId,
                StartName = s.StartName,
            });
            return Ok(result);
        }
        [HttpGet("ComboBoxGenre")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenreTable()
        {

            var data = await _memContext.Genres.ToListAsync();
            var result = data.Select(s => new GenreDTO
            {
                GenreId = s.GenreId,
                GenreName = s.GenreName,
            });
            return Ok(result);
        }
        //работает как надо

        //кнопки




        [HttpPost("AddRofl")]
        public async Task<IActionResult> AddRofl(RoflDTO rofl)
        {
            var item = _memContext.Rofls.Include(s => s.RoflEnd).FirstOrDefault(i => i.RoflId == rofl.RoflId);
            if (rofl != null)
            {
                // Create new Rofl object
                Rofl newRofl = new Rofl
                {
                    RoflName = rofl.RoflName,
                    RoflMinOpisanie = rofl.RoflMinOpisanie,
                    RoflOpisanie = rofl.RoflOpisanie,
                    RoflGenreId = rofl.RoflGenreId,
                    RoflEndId = rofl.RoflEndId,
                    RoflStartId = rofl.RoflStartId,
                    RoflStatusId = rofl.RoflStatusId,
                    TegId = rofl.TegId,
                    RoflDateTime = rofl.RoflDateTime,
                    RoflImage = rofl.RoflImage,
                };

                // Add the new Rofl to the database
                _memContext.Rofls.Add(newRofl);
                await _memContext.SaveChangesAsync();

                return Ok("Rofl added successfully");
            }
            else
            {
                return BadRequest("Invalid Rofl data");
            }
        }


        [HttpDelete("DeleteRofl/{id}")]
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

        [HttpPut("PutRofl/{id}")]
        public IActionResult UpdateItem(int id, [FromBody] RoflDTO updatedItem)
        {
            var item = _memContext.Rofls.Include(s => s.RoflEnd).FirstOrDefault(i => i.RoflId == id);

            if (item == null)
            {
                return NotFound();
            }


            item.RoflName = updatedItem.RoflName;
            item.RoflMinOpisanie = updatedItem.RoflMinOpisanie;
            item.RoflOpisanie = updatedItem.RoflOpisanie;
            item.RoflImage = updatedItem.RoflImage;
            //item.RoflEndId = updatedItem.RoflEndId;
            item.RoflEnd = _memContext.Ends.FirstOrDefault(i => i.EndId == updatedItem.RoflEndId);
            item.RoflGenre = _memContext.Genres.FirstOrDefault(i => i.GenreId == updatedItem.RoflGenreId);
            item.RoflStatus = _memContext.Statuses.FirstOrDefault(i => i.StatusId == updatedItem.RoflStatusId);
            item.RoflStart = _memContext.Starts.FirstOrDefault(i => i.StartId == updatedItem.RoflStartId);
            item.Teg = _memContext.Tegs.FirstOrDefault(i => i.TegId == updatedItem.TegId);
            item.RoflDateTime = updatedItem.RoflDateTime;
            _memContext.SaveChanges();

            return NoContent();
        }
        //кнопки




        //Обнова пароля/логина/почты
        [HttpPut("UpdateProfile/{userId}")]
        public async Task<ActionResult> UpdatePassword([FromBody] LoginUserDTO newPassword)
        {
            var user = await _memContext.LoginUsers.FindAsync(newPassword.LoginId);

            if (user == null)
            {
                return NotFound();
            }

            user.LoginPassword = HashPassword(newPassword.LoginPassword);
            user.LoginName = newPassword.LoginName;
            user.Mail = newPassword.Mail;
            user.LoginImage = newPassword.LoginImage;

            _memContext.LoginUsers.Update(user);
            await _memContext.SaveChangesAsync();

            return Ok();

        }
        //Обнова пароля/логина/почты





        [HttpPost("SearchNameClients")]
        public async Task<ActionResult<IEnumerable<LoginUserDTO>>> SearchClients([FromBody] LoginUserDTO rofl)
        {
            try
            {
                var users = _memContext.LoginUsers.Where(s => s.LoginName == rofl.LoginName).ToList();

                IQueryable<LoginUserDTO> search;
                if (!string.IsNullOrEmpty(rofl.LoginName))
                {
                    search = _memContext.LoginUsers
                        .Where(s => s.LoginName.Contains(rofl.LoginName))
                        .Select(s => new LoginUserDTO
                        {
                            LoginId = s.LoginId,
                            LoginImage = s.LoginImage,
                            LoginName = s.LoginName,
                            LoginPassword = s.LoginPassword,
                        });
                }
                else if (rofl.LoginName == "")
                {
                    var use = _memContext.LoginUsers.ToList();
                    return Ok(use);

                }
                return Ok(users);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("SearchNameAllRofl")]
        public async Task<ActionResult<IEnumerable<RoflDTO>>> SearchAllRofl([FromBody] RoflDTO rofl)
        {
            try
            {
                var users = _memContext.Rofls.Where(s => s.RoflName == rofl.RoflName).ToList();

                IQueryable<RoflDTO> search;
                if (!string.IsNullOrEmpty(rofl.RoflName))
                {
                    search = _memContext.Rofls
                        .Where(s => s.RoflName.Contains(rofl.RoflName))
                        .Select(s => new RoflDTO
                        {
                            Teg = s.Teg.TegName,
                            RoflName = s.RoflName,
                            RoflOpisanie = s.RoflOpisanie,
                            RoflDateTime = s.RoflDateTime,
                            RoflEnd = s.RoflEnd.EndName,
                            RoflGenre = s.RoflGenre.GenreName,
                            RoflMinOpisanie = s.RoflMinOpisanie,
                            RoflStart = s.RoflStart.StartName,
                            RoflStatus = s.RoflStatus.StatusName,
                            RoflImage = s.RoflImage,

                        });
                }
                if (rofl.RoflName == "")
                {
                    var use = _memContext.Rofls.ToList();
                    return Ok(use);

                }
                return Ok(users);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("SearchName")]
        public async Task<ActionResult<IEnumerable<RoflDTO>>> Search([FromBody] RoflDTO rofl)
        {
            try
            {
                var rofls = await _memContext.Rofls.Include(s => s.RoflGenre).Include(s => s.RoflStart).
                Include(s => s.RoflEnd).Include(s => s.RoflStatus).Include(s => s.Teg).ToListAsync();

                IQueryable<RoflDTO> search;
                if (!string.IsNullOrEmpty(rofl.RoflName) && !string.IsNullOrEmpty(rofl.Teg))
                {
                    search = _memContext.Rofls
                        .Where(s => s.RoflName.Contains(rofl.RoflName) && s.Teg.TegName.Contains(rofl.Teg))
                        .Select(s => new RoflDTO
                        {
                            Teg = s.Teg.TegName,
                            RoflName = s.RoflName,
                            RoflOpisanie = s.RoflOpisanie,
                            RoflDateTime = s.RoflDateTime,
                            RoflEnd = s.RoflEnd.EndName,
                            RoflGenre = s.RoflGenre.GenreName,
                            RoflMinOpisanie = s.RoflMinOpisanie,
                            RoflStart = s.RoflStart.StartName,
                            RoflStatus = s.RoflStatus.StatusName,
                            RoflImage = s.RoflImage,

                        });
                }
                else if (!string.IsNullOrEmpty(rofl.RoflName))
                {
                    search = _memContext.Rofls
                        .Where(s => s.RoflName.Contains(rofl.RoflName))
                        .Select(s => new RoflDTO
                        {
                            Teg = s.Teg.TegName,
                            RoflName = s.RoflName,
                            RoflOpisanie = s.RoflOpisanie,
                            RoflDateTime = s.RoflDateTime,
                            RoflEnd = s.RoflEnd.EndName,
                            RoflGenre = s.RoflGenre.GenreName,
                            RoflMinOpisanie = s.RoflMinOpisanie,
                            RoflStart = s.RoflStart.StartName,
                            RoflStatus = s.RoflStatus.StatusName,
                            RoflImage = s.RoflImage,

                        });
                }
                else if (!string.IsNullOrEmpty(rofl.Teg))
                {
                    search = _memContext.Rofls
                        .Where(s => s.Teg.TegName.Contains(rofl.Teg))
                        .Select(s => new RoflDTO
                        {
                            Teg = s.Teg.TegName,
                            RoflName = s.RoflName,
                            RoflOpisanie = s.RoflOpisanie,
                            RoflDateTime = s.RoflDateTime,
                            RoflEnd = s.RoflEnd.EndName,
                            RoflGenre = s.RoflGenre.GenreName,
                            RoflMinOpisanie = s.RoflMinOpisanie,
                            RoflStart = s.RoflStart.StartName,
                            RoflStatus = s.RoflStatus.StatusName,
                            RoflImage = s.RoflImage,

                        });
                }
                else
                {
                    // Handle case when no search criteria provided
                    //    search = Enumerable.Empty<RoflDTO>().AsQueryable();
                    return Ok(rofls.Select(s => new RoflDTO
                    {
                        Teg = s.Teg.TegName,
                        RoflName = s.RoflName,
                        RoflOpisanie = s.RoflOpisanie,
                        RoflDateTime = s.RoflDateTime,
                        RoflEnd = s.RoflEnd.EndName,
                        RoflGenre = s.RoflGenre.GenreName,
                        RoflMinOpisanie = s.RoflMinOpisanie,
                        RoflStart = s.RoflStart.StartName,
                        RoflStatus = s.RoflStatus.StatusName,
                        RoflImage = s.RoflImage,
                    }));
                }

                return Ok(search);

                //IQueryable<RoflDTO> search;
                //search = _memContext.Rofls.Where(s => s.RoflName.Contains(searchName)).
                //Select(s => new RoflDTO 
                //{

                //Teg = s.Teg.TegName,
                //    RoflName = s.RoflName,
                //    RoflOpisanie = s.RoflOpisanie,
                //    RoflDateTime = s.RoflDateTime,
                //    RoflEnd = s.RoflEnd.EndName,
                //    RoflGenre = s.RoflGenre.GenreName,
                //    RoflMinOpisanie = s.RoflMinOpisanie,
                //    RoflStart = s.RoflStart.StartName,
                //    RoflStatus = s.RoflStatus.StatusName,



                //});
                //var data = _memContext.Rofls.AsQueryable();

                //var search = (IQueryable<RoflDTO>)_memContext.Rofls.Where(s => s.RoflName.Contains(searchName));


                //return Ok(search);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("TransletOldSlang")]
        public IActionResult ProcessText([FromBody] string inputText)
        {

            List<SlangAndOld> slangText = _memContext.SlangAndOlds.ToList();

            foreach (SlangAndOld slang in slangText)
            {

                inputText = inputText.ToLower().Replace(slang.Slang.ToLower(), slang.OldSlang.ToLower());


            }
            return Ok(inputText);
        }

        private string NormalizeString(string input1, string input2, bool toLower)
        {
            if (toLower)
            {
                return input1.ToLower();
            }
            return input1.ToUpper();
        }

        [HttpPost("Date")]
        public async Task<ActionResult<IEnumerable<RoflDTO>>> PostDate([FromBody] DateTime date)
        {

            var rofls = await _memContext.Rofls.Include(s => s.RoflGenre).Include(s => s.RoflStart).
               Include(s => s.RoflEnd).Include(s => s.RoflStatus).Include(s => s.Teg).ToListAsync();

            var dates = _memContext.Rofls.Where(s => s.RoflDateTime == date).ToList();

            return Ok(dates.Select(s => new RoflDTO
            {
                Teg = s.Teg.TegName,
                RoflName = s.RoflName,
                RoflOpisanie = s.RoflOpisanie,
                RoflDateTime = s.RoflDateTime,
                RoflEnd = s.RoflEnd.EndName,
                RoflGenre = s.RoflGenre.GenreName,
                RoflMinOpisanie = s.RoflMinOpisanie,
                RoflStart = s.RoflStart.StartName,
                RoflStatus = s.RoflStatus.StatusName,
                RoflImage = s.RoflImage,
            }));

        }

        [HttpPost("promote/{userId}")]
        public async Task<ActionResult> PromoteAdmin(LoginUserDTO userId)
        {
            var user = await _memContext.LoginUsers.FindAsync(userId.LoginId);

            if (user == null)
            {
                return NotFound();
            }

            // Заменяю роль пользователя на администратора
            user.RoleId = 1;

            await _memContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("demote/{userId}")]
        public async Task<ActionResult> DemoteAdmin(LoginUserDTO userId)
        {
            var user = await _memContext.LoginUsers.FindAsync(userId.LoginId);

            if (user == null)
            {
                return NotFound();
            }

            // Заменяю роль пользователя на обычного пользователя
            user.RoleId = 2;

            await _memContext.SaveChangesAsync();

            return Ok();
        }
    }
}












