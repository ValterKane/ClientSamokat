using System.ComponentModel;
using System.Windows;

namespace ClientSamokat.View
{
    /// <summary>
    /// Логика взаимодействия для PopUpErrorMessage.xaml
    /// </summary>
    public partial class PopUpErrorMessage : Window, INotifyPropertyChanged
    {
        private string errorMessage;

        private string header;

        public string Header
        {
            get => header;
            set { header = value; OnPropertyChanged(nameof(Header)); }
        }
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }
        public PopUpErrorMessage(string errorMessage, string header)
        {
            InitializeComponent();
            ErrorMessage = errorMessage;   
            Header = header;
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void This_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
