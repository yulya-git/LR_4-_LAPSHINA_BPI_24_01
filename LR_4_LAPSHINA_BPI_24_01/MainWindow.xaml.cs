using System.Windows;
using LR_4_LAPSHINA_BPI_24_01.ViewModel;

namespace LR_4_LAPSHINA_BPI_24_01
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}