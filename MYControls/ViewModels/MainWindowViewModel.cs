using MYLibrary.Bindings;
using MYLibrary.Bindings.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYControls.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {

        }

        private RelayCommand _TestCommand;

        public RelayCommand TestCommand
        {
            get {
                if (_TestCommand is null)
                {
                    _TestCommand = new RelayCommand((o) =>
                    {
                        PostMessage(o is null ? "Null" : o.ToString());
                    }
                    );
                }
                return _TestCommand; }
        }

        public void PostMessage(string sMsg)
        {
            MessageList.Add(sMsg);
        }

        public ObservableCollection<string> MessageList { get; set; } = new ObservableCollection<string>();
    }
}
