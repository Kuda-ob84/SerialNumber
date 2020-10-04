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
using System.Windows.Shapes;

namespace SerialNumber
{
    /// <summary>
    /// Логика взаимодействия для Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        //string keyFileName = @"C:\Users\Dauken\source\repos\SerialNumberUse\SerialNumberUse\symmetric_key.config";

        public Signup()
        {
            InitializeComponent();
            tbSerialNumber.Text = CryptData.getSN();
        }

        private void bSubmit_Click(object sender, RoutedEventArgs e)
        {
            WriteData();
        }


        public String doHash(byte[] val)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(val);
                return Convert.ToBase64String(hash);
            }
        }

        private void tbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void tbPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPassword.Text == String.Empty)
            {
                tbHash.Text = String.Empty;
            }
            else
            {
                byte[] passtohash = System.Text.Encoding.UTF8.GetBytes(tbPassword.Text.ToString());
                tbHash.Text = doHash(passtohash);
            }
        }

        public void WriteData()
        {
            if (tbLogin.Text.Length < 6 || tbPassword.Text.Length < 7)
            {
                MessageBox.Show("Username or password is too short");
            }
            else
            {
                string dir = tbLogin.Text;
                Directory.CreateDirectory("data\\" + dir);

                var sw = new StreamWriter("data\\" + dir + "\\data.txt");

                string encusr = CryptData.Encrypt(tbLogin.Text);

                string encpass = CryptData.Encrypt(tbPassword.Text);

                sw.WriteLine(encusr);
                sw.WriteLine(encpass);
                sw.WriteLine(CryptData.getSN());
                sw.Close();

                MessageBox.Show("User was succesfully created", tbLogin.Text);
                this.Close();
            }

        }
    }
}
