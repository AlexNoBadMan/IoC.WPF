using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC.WPF.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string _title = "Test1";

        public string Title { get => _title; set => Set(ref _title, value); }
        public MainViewModel()
        {

        }
    }
}
