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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings
                ["WpfApp2.Properties.Settings.usersConnectionString"].ConnectionString;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    MessageBox.Show("Соединение установлено");

                    DataTable data = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select login, password, FIO ,status from Table_1", connect);
                    adapter.Fill(data);
                    usersDataGrid.ItemsSource = data.DefaultView;


                }
            }
            catch
            {
                MessageBox.Show("Соединение установлено");
            }
            finally
            {

            }
            WpfApp2.usersDataSet usersDataSet = ((WpfApp2.usersDataSet)(this.FindResource("usersDataSet")));
            // Загрузить данные в таблицу users. Можно изменить этот код как требуется.
            WpfApp2.usersDataSetTableAdapters.usersTableAdapter usersDataSetusersTableAdapter = new WpfApp2.usersDataSetTableAdapters.usersTableAdapter();
            usersDataSetusersTableAdapter.Fill(usersDataSet.users);
            System.Windows.Data.CollectionViewSource usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usersViewSource")));
            usersViewSource.View.MoveCurrentToFirst();
        }

    }
}
