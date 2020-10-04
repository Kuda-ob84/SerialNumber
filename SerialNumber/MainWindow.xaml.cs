using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerialNumber
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



 

        private void bSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var sr = new StreamReader("data\\" + tbLogin.Text + "\\data.txt");
                string encusr = sr.ReadLine();
                string encpass = sr.ReadLine();
                string serialNumber = sr.ReadLine();
                sr.Close();

                string decusr = CryptData.Decrypt(encusr);
                string decpass = CryptData.Decrypt(encpass);

                if (decusr == tbLogin.Text && decpass == tbPassword.Password && serialNumber == CryptData.getSN())
                {
                    MessageBox.Show("Добро пожаловать", decusr);


                }
                else
                {
                    MessageBox.Show("Пароль или логин не правильны.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Пароль или логин не правильны.");
            }
        }

        private void bSighUp_Click(object sender, RoutedEventArgs e)
        {
            Signup signUp = new Signup();
            signUp.Show();
        }

  
    }
}
