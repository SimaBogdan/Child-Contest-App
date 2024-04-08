using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using src.Model;
using src.Service;

namespace src
{
    public partial class LogInForm : Form
    {
        private static IService _service;
        
        public LogInForm(IService service)
        {
            InitializeComponent();
            _service = service;
        }
        
        private static string hashPassword(string parola)
        {
            try
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(parola));
                    return Convert.ToBase64String(hashedBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while hashing password: " + ex.Message);
                return null;
            }
        }


        private void handleLogIn(object sender, EventArgs e)
        {
            var username = usernameTextBox.Text;
            var password = passwordTextBox.Text;
            var hashedPassword = hashPassword(password);
            
            Console.WriteLine(username);
            Console.WriteLine(password);
            Console.WriteLine(hashedPassword);

            var organizator = _service.findOneByUsernameAndPassword(username, hashedPassword);

            if (organizator != null)
            {
                ShowAccountDialog(organizator);
            }
            else
            {
                MessageAlert.ShowErrorMessage(null, "Wrong username or password!");
            }
        }
        
        private static void ShowAccountDialog(Organizator organizator)
        {
            try
            {
                var accountForm = new AccountForm(_service, organizator);
                accountForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while showing account dialog: " + ex.Message);
            }
        }
    }
}