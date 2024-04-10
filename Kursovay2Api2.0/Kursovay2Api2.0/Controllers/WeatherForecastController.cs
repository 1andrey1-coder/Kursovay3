using Microsoft.AspNetCore.Mvc;

namespace Kursovay2Api2._0.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
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

        //private string GenerateRandomCode2()
        //{
        //    string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    var random = new Random();
        //    var password = new string(
        //        Enumerable.Repeat(chars, 4)
        //                  .Select(s => s[random.Next(s.Length)])
        //                  .ToArray());
        //    return password.ToString();
        //}

        //[HttpGet("GenerateCode")]
        //public async Task<IActionResult> GenerateCode3(string email)
        //{
        //    // Генерируем случайный код (например, строку из символов)
        //    string generatedCode = GenerateRandomCode();

        //    await mail.Send("slovarsleng@mail.ru", email, "Потверждение почты для сброса пароля в Словаре сленга"
        //         , $"Ваш код потверждения: {generatedCode}");
        //    return Ok(new { code = generatedCode });
        //}
        //public async Task<string> GetGeneratedCode()
        //{
        //    string apiUrl = @"https://localhost:7189/api/"; 
        //    string generatedCode = null;

        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = await client.GetAsync(apiUrl);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            generatedCode = await response.Content.ReadAsStringAsync();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Кода такого не существует", "Неудачный потверждение", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }

        //    return generatedCode;
        //}
    }
}
