using ClientSamokat.ViewModel;
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
using System.Windows.Shapes;

namespace ClientSamokat.View
{
    /// <summary>
    /// Логика взаимодействия для CourierView.xaml
    /// </summary>
    public partial class CourierView : Window
    {
        private readonly CourierVM courierVM;
        public CourierView(CourierVM VM)
        {
            this.courierVM = VM;
            InitializeComponent();
            this.DataContext = VM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            { this.DragMove(); }
        }
    }
}
