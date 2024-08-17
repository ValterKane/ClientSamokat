using ClientSamokat.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSamokat.ViewModel
{
    public class PopUpCurierViewModel: ObservableObject
    {
        private Courier person;
        public Courier Object 
        {
            get => person ?? new Courier(); set => SetProperty(ref person,value);
        }

        public PopUpCurierViewModel(Courier obj)
        {
            this.Object = obj;  
        }

    }
}
