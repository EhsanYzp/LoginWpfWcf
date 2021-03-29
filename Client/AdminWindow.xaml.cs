using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml(Admin Panel)
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event triggered once the data grid is loaded for the first time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDataGrid();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error after data grid loaded for the first time: {err}");
            }
        }

        /// <summary>
        /// Event triggered once the "Delete Users" button is clicked.
        /// Users to delete will be obtained from UI, the list will be send to server
        /// and the applied changes in the server will be communicated with client to reflect in UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Step1: Get list of Users to Delete from UI.
                List<string> usersToDelete = new List<string>();
                foreach (Users item in UsersDataGrid.ItemsSource)
                {
                    if (item.Delete == true)
                        usersToDelete.Add(item.UserName);
                }

                //Step 2: Send list of Users to Delete to Server (if any)
                //DataGrid will be updated at the end of deletion process
                if (usersToDelete.Count == 0)
                    MessageBox.Show("No users selected for Delete!");
                else
                {
                    ServiceReference.ServiceClient server = new ServiceReference.ServiceClient();
                    int numberOfDeletedUsers = server.DeleteUsers(usersToDelete.ToArray());
                    MessageBox.Show($"{numberOfDeletedUsers} users deleted");
                    UpdateDataGrid();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error after Delete Users button clicked: {err}");
            }
        }

        /// <summary>
        /// Event triggered once the data grid format is adjusted for the first time.
        /// All needed manual configurations to the column names, headers and features should be implemented here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsersDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                if (e.Column.Header.ToString() == "UserName")
                {
                    e.Column.IsReadOnly = true;
                    e.Column.Header = "User Name";
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error while trying to configure properties of data grid: {err}");
            }
        }

        /// <summary>
        /// Updates the data grid according to the most recent version of the users obtained from server.
        /// </summary>
        private void UpdateDataGrid()
        {
            try
            {
                ServiceReference.ServiceClient server = new ServiceReference.ServiceClient();
                List<string> allUsers = server.GetAllUsers().ToList();
                List<Users> users = new List<Users>();
                foreach (var item in allUsers)
                {
                    users.Add(new Users(item));
                }
                UsersDataGrid.ItemsSource = users;
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error while trying to update the data grid: {err}");
            }
        }
    }

    /// <summary>
    /// Auxiliary class for adding more features to the registered users(e.g: Delete)
    /// </summary>
    public class Users
    {
        public string UserName { get; set; }
        public bool Delete { get; set; }
        public Users(string name)
        {
            this.UserName = name;
            this.Delete = false;
        }
    }
}
