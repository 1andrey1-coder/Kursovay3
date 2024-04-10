using Kursovay2.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Kursovay2.API
{
    public class Client
    {
        HttpClient httpClient = new HttpClient();

        private Client()
        {
            httpClient.BaseAddress = new Uri(@"https://localhost:7189/api/");
        }
        static Client instance = new();
        public static Client Instance
        {
            get
            {
                if (instance == null)
                    instance = new Client();
                return instance;
            }
        }

        public HttpClient Cli { get { return httpClient; } }
        //static Client instance = new();
        //public static Client Instance { get => instance; }

        //private readonly HttpClient _httpClient;

        public async Task<LoginUserDTO> UserLogin(string password, string mail)
        {
            var loginuUser = new LoginName
            {
                Password = password,
                Mail = mail
            };
            //Это хрень передает данные обратно в Api
            //("UserBank/GetAccountLogin", httpContent) 
            //Account - контроллер
            //Login - метод
            //httpContent - цепляем для проверки в самом Api
            var jsonContent = JsonConvert.SerializeObject(loginuUser);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("Account/Login", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Логин/пароль неправильный", "Неудачный вход", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var userAnswer = JsonConvert.DeserializeObject<LoginUserDTO>(await response.Content.ReadAsStringAsync());
            return userAnswer;
        }



        public async Task<LoginUserDTO> UserRegister(string login, string mail)
        {
            //почему то null

            var RegisterUser = new Registr
            {
                Login = login,
                Mail = mail,


            };
            //
            var jsonContent = JsonConvert.SerializeObject(RegisterUser);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("Account/Register", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Пользователь с таким логином уже существует",
                    "Неудачная регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var userAnswer = JsonConvert.DeserializeObject<LoginUserDTO>(await response.Content.ReadAsStringAsync());
            return userAnswer;
        }


        public async Task<LoginUserDTO> GetUser(int id)
        {

            //var UserID = new StudentUserDTO { Id = studentUserDTO };
            HttpResponseMessage responseId = await httpClient.GetAsync($"Account/Login/{id}");


            //HttpResponseMessage response = await responseId.Content.ReadAsStringAsync();



            //MessageBox.Show($"Ошибка при выполнении запроса. Код ошибки: {responseId.StatusCode}");


            if (!responseId.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve user information");
            }
            var content = await responseId.Content.ReadAsStringAsync();
            var userAnswer = JsonConvert.DeserializeObject<LoginUserDTO>(content);


            return userAnswer;




        }

        public async Task<LoginUserDTO> UserReset(string mail)
        {
            var loginuUser = new LoginName
            {
                Mail = mail
            };
            //Это хрень передает данные обратно в Api
            //("UserBank/GetAccountLogin", httpContent) 
            //Account - контроллер
            //Login - метод
            //httpContent - цепляем для проверки в самом Api
            var jsonContent = JsonConvert.SerializeObject(loginuUser);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("Account/reset", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Почты не существует", "Неудачный вход", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var userAnswer = JsonConvert.DeserializeObject<LoginUserDTO>(await response.Content.ReadAsStringAsync());
            return userAnswer;
        }





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


        public async Task<string> GetGeneratedCode(string mail)
        {

            //var loginuUser = new LoginName
            //{
            //    Mail = mail

            //};
            string code = "";
            var jsonContent = JsonConvert.SerializeObject(code);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            //HttpResponseMessage response = await httpClient.PostAsync($"Account/GenerateCode2", httpContent);
            HttpResponseMessage response = await httpClient.PostAsync($"Account/GenerateCode?mail={mail}",
                httpContent);

            // адрес метода в API, который возвращает сгенерированный код
            code = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {

                MessageBox.Show($"Код: {code} не был успешно сгенерирован. Ошибка: {response.ReasonPhrase}");
                code = null;
            }
            else
            {
                MessageBox.Show("Ошибка при получении кода");
            }


            return code;
        }
        //ilchenkor1135@suz-ppk.ru
        public async Task<LoginUserDTO> PostSmsEmail(string mail)
        {
            var loginuUser = new LoginName
            {
                Mail = mail
            };
            //httpContent - цепляем для проверки в самом Api
            var jsonContent = JsonConvert.SerializeObject(loginuUser);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync($"Account/GenerateCode", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Почты не существует", "Неудачный вход", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var userAnswer = JsonConvert.DeserializeObject<LoginUserDTO>(await response.Content.ReadAsStringAsync());
            return userAnswer;
        }


        public static async Task<bool> VerifyCode(string email, string code)
        {
            using (var client = new HttpClient())
            {
                var verificationData = new ResetDTO
                {
                    Mail = email,
                    Code = code
                };

                var json = JsonConvert.SerializeObject(verificationData);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("Account/VerifyCode", data);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    // Обработка случаев, когда проверка не прошла
                    return false;
                }
            }
        }

        public async Task GetListRofl()
        {

        }

    }



} 

