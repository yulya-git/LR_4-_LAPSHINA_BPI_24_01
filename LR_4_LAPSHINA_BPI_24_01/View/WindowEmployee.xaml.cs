using System.Windows;
using LR_4_LAPSHINA_BPI_24_01.ViewModel;

namespace LR_4_LAPSHINA_BPI_24_01.View
{
    public partial class WindowEmployee : Window
    {
        public WindowEmployee()
        {
            InitializeComponent();
            DataContext = new PersonViewModel();
        }
    }
}