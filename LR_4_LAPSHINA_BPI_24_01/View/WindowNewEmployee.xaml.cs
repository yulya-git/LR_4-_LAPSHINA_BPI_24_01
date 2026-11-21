using System.Windows;
using LR_4_LAPSHINA_BPI_24_01.ViewModel;

namespace LR_4_LAPSHINA_BPI_24_01.View
{
    public partial class WindowNewEmployee : Window
    {
        public WindowNewEmployee()
        {
            InitializeComponent();
            // Загружаем список должностей в ComboBox
            RoleViewModel vmRole = new RoleViewModel();
            CbRole.ItemsSource = vmRole.ListRole;
            CbRole.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}