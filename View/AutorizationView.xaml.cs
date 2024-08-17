using ClientSamokat.Components;
using ClientSamokat.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClientSamokat.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AutorizeView : Window, INotifyPropertyChanged
    {
        private string hash;
        private readonly IHashProvider Hash_Provider;

        public string HashForBind
        {
            get { return hash; }
            set
            {
                hash = value;
                OnPropertyChanged(nameof(HashForBind));
            }
        }

        public AutorizeView(IHashProvider _HashConverter, AutorizationVM DataContext)
        {
            this.DataContext = DataContext;
            Hash_Provider = _HashConverter;
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void PasswordTB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            HashForBind = Hash_Provider.GetHash(((PasswordBox)sender).Password);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}