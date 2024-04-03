using Kursovay2.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kursovay2.Views
{
    /// <summary>
    /// Логика взаимодействия для CodeСonfirmation.xaml
    /// </summary>
    public partial class CodeСonfirmation : Window
    {
        string generatedCode; // пример сгенерированного кода
        public CodeСonfirmation()
        {
            InitializeComponent();
            generatedCode = GenerateRandomCode(); // пример сгенерированного кода
            // отправил на почту

        }
        public string Mail { get; set; }
        public bool Success { get; internal set; }

        private string GenerateRandomCode()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var password = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return password;
        }


        private async void CheckCode(object sender, RoutedEventArgs e)
        {
            // Получаем введенный пользователем код
            string enteredCode = txtCode.Text;

            // Логика для проверки кода сравнение с отправленным на почту кодом
            

            if (enteredCode == generatedCode)
            {
                Success = true;
                Close();
                //если все ок
              
            }
            else
            {
                MessageBox.Show("Неверный код");
            }
        }
    }
}
