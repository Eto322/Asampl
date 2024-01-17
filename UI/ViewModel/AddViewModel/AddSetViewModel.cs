using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using IntegratedViewModel.Inf;

namespace UI.ViewModel.AddViewModel
{
    internal class AddSetViewModel : NotifyPropertyChanged
    {
        private string _name;
        private string _selectedType;

        private static readonly HashSet<string> _ValidTypes = new HashSet<string>
        {
            "ATIME", "RTIME", // Абсолютний і відносний час
            "ADATE", "RDATE", // Абсолютна і відносна дата
            "INTEGER", // Ціле число
            "REAL", // Дійсне число
            "DOUBLE", // Дійсне число подвійної точності
            "FRAME", // Кадр
            "TEXT", // Текст
            "LINK", // Посилання або повне ім’я файлу
            "LOGIC", // Логічний тип
            "EVENT" // Подія
        };

        public ObservableCollection<string> ValidTypes { get; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyOfPropertyChanged();
                }
            }
        }

        public string SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                if (_selectedType != value)
                {
                    _selectedType = value;
                    NotifyOfPropertyChanged();
                }
            }
        }

        public ICommand OkCommand { get; }
        public Action CloseAction { get; set; } // Для закриття вікна

        public AddSetViewModel()
        {
            OkCommand = new RelayCommand(_ => ExecuteOkCommand());

            _selectedType = "ATIME";
            ValidTypes = new ObservableCollection<string>(_ValidTypes);
        }

        private void ExecuteOkCommand()
        {
            CloseAction?.Invoke();
        }
    }
}