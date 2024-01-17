using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;
using BLL.ASAMPL;
using BLL.ASAMPL.KeyWordsModel;
using IntegratedViewModel.Inf;

namespace UI.ViewModel.AddViewModel
{
    public class AddElementsViewModel : NotifyPropertyChanged
    {
        public AddElementsViewModel()
        {
        }

        private ObservableCollection<ElementsDataContainer> _elements;

        public ObservableCollection<ElementsDataContainer> Elements
        {
            get { return _elements; }
            set
            {
                _elements = value;
                NotifyOfPropertyChanged();
            }
        }

        private ObservableCollection<SetDataContainer> _sets;

        public ObservableCollection<SetDataContainer> Sets
        {
            get { return _sets; }
            set
            {
                _sets = value;
                NotifyOfPropertyChanged();
            }
        }

        private SetDataContainer _selectedSet;

        public SetDataContainer SelectedSet
        {
            get { return _selectedSet; }
            set
            {
                _selectedSet = value;
                NotifyOfPropertyChanged();
            }
        }

        public Action CloseAction { get; set; } // Для закриття вікна

        private string _name;

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

        private string _elementValue;

        public string ElementValue
        {
            get
            {
                return _elementValue;
            }
            set
            {
                if (_elementValue != value)
                {
                    _elementValue = value;
                    NotifyOfPropertyChanged();
                }
            }
        }

        public ICommand OkCommand { get; }

        private void ExecuteOkCommand()
        {
            CloseAction?.Invoke();
        }

        public AddElementsViewModel(ElementsKeyWordModel elementsKeyWordModel, SetsKeyWordModel setsKeyWordModel)
        {
            OkCommand = new RelayCommand(_ => ExecuteOkCommand());
            Elements = new ObservableCollection<ElementsDataContainer>(elementsKeyWordModel.Elements);
            Sets = new ObservableCollection<SetDataContainer>(setsKeyWordModel.Sets);
        }
    }
}