using ClientSamokat.ViewModel;
using System.Windows;

namespace ClientSamokat.View
{
    /// <summary>
    /// Логика взаимодействия для BicycleLocksView.xaml
    /// </summary>
    public partial class BicycleLocksView : Window
    {
        private readonly BicycleLocksViewModel viewModel;
        public BicycleLocksView(BicycleLocksViewModel VM)
        {
            viewModel = VM;
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
