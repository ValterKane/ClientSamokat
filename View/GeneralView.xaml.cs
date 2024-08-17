using ClientSamokat.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace ClientSamokat.View
{
    /// <summary>
    /// Логика взаимодействия для GeneralView.xaml
    /// </summary>
    public partial class GeneralView : Window
    {
        private readonly GeneralVM ViewModel;
        public GeneralView(GeneralVM dataContext)
        {
            ViewModel = dataContext;
            this.DataContext = ViewModel;
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            ViewModel.LoadData();
        }
    }
}
