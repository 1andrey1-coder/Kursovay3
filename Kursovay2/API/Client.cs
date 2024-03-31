using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

    }



}

