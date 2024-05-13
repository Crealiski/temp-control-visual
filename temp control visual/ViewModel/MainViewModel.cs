
using System.ComponentModel;

namespace temp_control_visual.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(String Name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
    }
}
//192.168.0.1 eller 127.0.0.1