using ModuleFour.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModuleFour.View
{
    /// <summary>
    /// Логика взаимодействия для Validation.xaml
    /// </summary>
    public partial class Validation : Window
    {
        private ServerRequest request = new ServerRequest();
        public Validation()
        {
            InitializeComponent();
        }

        private async void GetRequestButtonClick(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost:4444/TransferSimulator/fullName";

            string result = await request.GetStringAsync(url);

            FullNameTextBlock.Text = GetFullNameFromString(result);
        }

        private string GetFullNameFromString(string result)
        {
            return result
                .Substring(result.IndexOf(":") + 2)
                .Replace("\"", "")
                .Replace("}", "");
        }

        private void SendResultButtonClick(object sender, RoutedEventArgs e)
        {
            if(FullNameTextBlock.Text.Equals(""))
            {
                WarningFullNameTextBlock.Text = "Данные с сервера еще не получены. Отправьте запрос";
            }
            else
            {
                string filePath = "C:\\Users\\admin\\Desktop\\Тест.docx";
                int tableIndex = 1;
                int columnIndex = 1;
                string data = FullNameTextBlock.Text;

                WordWriter wordWriter = new WordWriter();
                wordWriter.WriteToWord(filePath, tableIndex, columnIndex, data);
                if(!Regex.IsMatch(FullNameTextBlock.Text, @"^[a-яA-я\s]*$"))
                {
                    WarningFullNameTextBlock.Text = "Данные ФИО не валидны. Попробуйте еще раз";
                }
                else
                {
                    WarningFullNameTextBlock.Text = "Данные ФИО успешно прошли валидацию";
                }
            }
        }
    }
}
