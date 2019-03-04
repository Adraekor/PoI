using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace PoI.ViewModels
{
	public class MyMapViewModel : BindableBase
	{
        private ObservableCollection<Pin> _pinCollection = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> PinCollection { get { return _pinCollection; } set { _pinCollection = value; OnPropertyChanged(); } }
    
        public MyMapViewModel()
        {
            }
	}
}
