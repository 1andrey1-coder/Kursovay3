﻿using Kursovay2.Views;
using Newtonsoft.Json;
using System;
using System.Collections;
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

        public Client()
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
        public async Task<LoginUserDTO> GetRofl(int id)
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

        public async Task? UserReset(string mail)
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
                MessageBox.Show("Почты не существует", "Неудачный вход", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        //public async Task? Profile(int id, string password, string name, string mail, LoginUserDTO image)
        //{
        //    var resertPasswordProfile = new LoginUserDTO
        //    {
        //        LoginPassword = password,
        //        LoginName = name,
        //        Mail = mail,


        //    };
        //    HttpResponseMessage response = await httpClient.PutAsJsonAsync($"Account/UpdateProfile/{id}", resertPasswordProfile);
        //    if (!response.IsSuccessStatusCode)
        //        MessageBox.Show("Не получилось перезаписать новый пароль", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        MessageBox.Show("Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //}
        public async Task? Profile(LoginUserDTO data)
        {
            var resertPasswordProfile = new LoginUserDTO
            {
                LoginId = data.LoginId,
                LoginPassword = data.LoginPassword,
                LoginName = data.LoginName,
                Mail = data.Mail,
                LoginImage = data.LoginImage,


            };
            HttpResponseMessage response = await httpClient.PutAsJsonAsync($"Account/UpdateProfile/{data.LoginId}", resertPasswordProfile);
            if (!response.IsSuccessStatusCode)
                MessageBox.Show("Не получилось перезаписать новый пароль", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public async Task<LoginUserDTO>? PostSmsEmail(string mail)
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

            return null;
        }

        //Failed to load data from API
        public async Task<List<RoflDTO>> GetListRofl()
        {
            try
            {
                var response = await httpClient.GetAsync("Account/RoflList");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<RoflDTO>>(content);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            return null;
        }

        public async Task<bool> VerifyCode(string email, string code)
        {

            var verificationData = new ResetDTO
            {
                Mail = email,
                Code = code
            };

            //var json = JsonConvert.SerializeObject(verificationData);
            //var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsJsonAsync("Account/VerifyCode", verificationData);

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
        public async Task<List<StatusDTO>> GetComboBoxStatus()
        {

            HttpResponseMessage response = await httpClient.GetAsync("Account/ComboBoxStatus");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<StatusDTO> comboBoxData = JsonConvert.DeserializeObject<List<StatusDTO>>(json);
                return comboBoxData;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<EndDTO>> GetComboBoxEnd()
        {

            HttpResponseMessage response = await httpClient.GetAsync("Account/ComboBoxEnd");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<EndDTO> comboBoxData = JsonConvert.DeserializeObject<List<EndDTO>>(json);
                return comboBoxData;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<TegDTO>> GetComboBoxTeg()
        {

            HttpResponseMessage response = await httpClient.GetAsync("Account/ComboBoxTeg");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<TegDTO> comboBoxData = JsonConvert.DeserializeObject<List<TegDTO>>(json);
                return comboBoxData;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<StartDTO>> GetComboBoxStart()
        {

            HttpResponseMessage response = await httpClient.GetAsync("Account/ComboBoxStart");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<StartDTO> comboBoxData = JsonConvert.DeserializeObject<List<StartDTO>>(json);
                return comboBoxData;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<GenreDTO>> GetComboBoxGenre()
        {

            HttpResponseMessage response = await httpClient.GetAsync("Account/ComboBoxGenre");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<GenreDTO> comboBoxData = JsonConvert.DeserializeObject<List<GenreDTO>>(json);
                return comboBoxData;
            }

            else
            {
                return null;
            }
        }
        //, string minopis, string opis, string name, byte image
        public async Task<RoflDTO> SendUserData(RoflDTO combobox)
        {



            //TegId = combobox.TegId,
            //RoflStartId = combobox,
            //RoflGenreId = combobox,
            //RoflEndId = combobox,
            //RoflStatusId = combobox,
            combobox.RoflImage = combobox.RoflImage;
            combobox.RoflDateTime = combobox.RoflDateTime;
            combobox.RoflName = combobox.RoflName;
            combobox.RoflMinOpisanie = combobox.RoflMinOpisanie;
            combobox.RoflOpisanie = combobox.RoflOpisanie;

            // Отправка данных в контроллер через API
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Account/AddRofl", combobox);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Данные успешно добавлены в базу данных", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении данных в базу данных", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;



        }

        public async Task<RoflDTO> SendUserPutData(RoflDTO combobox)
        {



            //TegId = combobox.TegId,
            //RoflStartId = combobox,
            //RoflGenreId = combobox,
            //RoflEndId = combobox,
            //RoflStatusId = combobox,
            combobox.RoflId = combobox.RoflId;
            combobox.RoflImage = combobox.RoflImage;
            combobox.RoflName = combobox.RoflName;
            combobox.RoflMinOpisanie = combobox.RoflMinOpisanie;
            combobox.RoflOpisanie = combobox.RoflOpisanie;

            // Отправка данных в контроллер через API
            HttpResponseMessage response = await httpClient.PutAsJsonAsync($"Account/PutRofl/{combobox.RoflId}", combobox);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Данные успешно изменены в базе данных", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ошибка при изменении данных в базу данных", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;



        }

        public async Task<RoflDTO> DeleteDataAsync(int deleteid)
        {
            var deleteId = new RoflDTO
            {
                RoflId = deleteid,
            };
            // Отправка данных в контроллер через API
            HttpResponseMessage response = await httpClient.DeleteAsync($"Account/DeleteRofl/{deleteid}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Данные успешно удалены из базы данных", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ошибка при удаление из базы данных", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;



        }

        public async Task<List<RoflDTO>> SearchApi(string searchName, string comboboxTeg)
        {
            var Search = new RoflDTO
            {
                RoflName = searchName,
                Teg = comboboxTeg,
            };
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Account/SearchName", Search);


            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<RoflDTO> searchResults = JsonConvert.DeserializeObject<List<RoflDTO>>(json);

                return searchResults;

            }
            else
            {
                MessageBox.Show("Ошибка при поиске данных", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }

        public async Task<List<LoginUserDTO>> SearchApiClients(string searchName)
        {

            var search = new LoginUserDTO
            {
                LoginName = searchName,
            };
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Account/SearchNameClients", search); ;


            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<LoginUserDTO> searchResults = JsonConvert.DeserializeObject<List<LoginUserDTO>>(json);

                return searchResults;

            }
            else
            {
                MessageBox.Show("Ошибка при поиске данных", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }
        public async Task<List<RoflDTO>> SearchApiNotComboBox(string searchName)
        {
            var Search = new RoflDTO
            {
                RoflName = searchName,
            };
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Account/SearchNameAllRofl", Search);


            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<RoflDTO> searchResults = JsonConvert.DeserializeObject<List<RoflDTO>>(json);

                return searchResults;

            }
            else
            {
                MessageBox.Show("Ошибка при поиске данных", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }


        public async Task<List<SlangAndOldDTO>> GetSlangOld()
        {
            HttpResponseMessage response = await httpClient.GetAsync("Account/SlangOldList");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<SlangAndOldDTO> getSlang = JsonConvert.DeserializeObject<List<SlangAndOldDTO>>(json);
                return getSlang;
            }
            else
            {
                return null;
            }

        }


        public async Task<List<LoginUserDTO>> ListUserAdmin()
        {
            HttpResponseMessage response = await httpClient.GetAsync("Account/ListUserAdmin");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<LoginUserDTO> getSlang = JsonConvert.DeserializeObject<List<LoginUserDTO>>(json);
                return getSlang;
            }
            else
            {
                return null;
            }

        }


        public async Task<List<SlangAndOldDTO>> SearchApiLibrary(string searchName)
        {

            var search = new SlangAndOldDTO
            {
                Slang = searchName,
            };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Account/ListSlangAndOld", search);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<SlangAndOldDTO> getSlang = JsonConvert.DeserializeObject<List<SlangAndOldDTO>>(json);
                return getSlang;
            }
            else
            {
                return null;
            }

        }
        public async Task<string> TransletOldSlang(string textUser)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Account/TransletOldSlang", textUser);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                //string getSlang = JsonConvert.DeserializeObject<string>(json);
                return json;
            }
            else
            {
                return null;
            }
            //return null;
        }


        public async Task<List<RoflDTO>> DateTimePicker(DateTime date)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Account/Date", date.ToString("yyyy-MM-dd"));

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RoflDTO>>(json);

            }
            else
            {
                return null;
            }
            //return null;
        }



        public async Task<LoginUserDTO> promote(LoginUserDTO userAdmin)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync($"Account/promote/{userAdmin}", userAdmin);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"{userAdmin.LoginName} теперь Админа");

            }
            else
            {
                MessageBox.Show($"{userAdmin.LoginName} имеет роль Админа");
            }
            return null;
        }

        public async Task<LoginUserDTO> demote(LoginUserDTO userAdmin)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync($"Account/demote/{userAdmin}", userAdmin);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"{userAdmin.LoginName} теперь Пользователь");

            }
            else
            {
                MessageBox.Show($"{userAdmin.LoginName} имеет роль Пользователь");
            }
            return null;
        }


    }




    //ilchenkor1135@suz-ppk.ru
    //BU0GiMZa Дома
    //1 колледж(a4ayc/80/OGda4BO/1o/V0etpOqiLx1JwB5S3beHW0s=)



}





