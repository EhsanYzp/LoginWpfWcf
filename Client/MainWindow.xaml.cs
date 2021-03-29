using System;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml(Login Panel)
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Encoded version of password obtained from login form(to be sent to server)
        /// </summary>
        private string EncodedPassword { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event triggered once the login button is clicked in the Login panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EncodePasswordToBase64();
                TryLogin();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error after login button click: {err}");
            }
        }


        /// <summary>
        /// Communicates with server and exchange login info. 
        /// Upon successful login of normal user, it shows a popup
        /// Upon successful login of admin user, it shows a popup + load admin panel
        /// If login credentials are not correct, it shows a popup reporting this
        /// </summary>
        private void TryLogin()
        {
            try
            {
                ServiceReference.ServiceClient login = new ServiceReference.ServiceClient();

                bool isFound = login.MakeLogin(UserNameTextBox.Text, EncodedPassword);
                if (isFound == false)
                    MessageBox.Show("User Not Found");
                else
                {
                    if (isFound == true && UserNameTextBox.Text == "admin")
                    {
                        MessageBox.Show($"Successfull Login. Welcome {UserNameTextBox.Text}!");
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                    }
                    if (isFound == true && UserNameTextBox.Text != "admin")
                        MessageBox.Show($"Successfull Login. Welcome {UserNameTextBox.Text}!");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error while trying to login in client side: {err}");
            }
        }

        /// <summary>
        /// Encodes an input string to base64(used for password encoding in client side)
        /// </summary>
        private void EncodePasswordToBase64()
        {
            try
            {
                byte[] encData_byte = new byte[PasswordTextBox.Password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(PasswordTextBox.Password);
                EncodedPassword = Convert.ToBase64String(encData_byte);
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error while trying to encode the password: {err}");
            }
        }
    }
}
