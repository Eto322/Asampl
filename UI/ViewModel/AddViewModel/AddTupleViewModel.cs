using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BLL.ASAMPL.KeyWordsModel;
using IntegratedViewModel.Inf;

namespace UI.ViewModel.AddViewModel
{
    internal class AddTupleViewModel : NotifyPropertyChanged
    {
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

        private ObservableCollection<SetDataContainer> _sets;

        public ObservableCollection<SetDataContainer> Sets
        {
            get { return _sets; }
            set
            {
                if (_sets != value)
                {
                    _sets = value;
                    NotifyOfPropertyChanged();
                }
            }
        }

        public ICommand OkCommand { get; }
        public Action CloseAction { get; set; }

        private void ExecuteOkCommand()
        {
            SelectedSets = GetSelectedSets();
            ResetSelected();
            CloseAction?.Invoke();
        }

        private void ResetSelected()
        {
            foreach (var setDataContainer in Sets)
            {
                setDataContainer.IsChekedForView = false;
            }
        }

        private List<SetDataContainer> GetSelectedSets()
        {
            return Sets.Where(e => e.IsChekedForView).ToList();
        }

        public List<SetDataContainer> SelectedSets { get; private set; }

        public AddTupleViewModel(SetsKeyWordModel sets)
        {
            Sets = new ObservableCollection<SetDataContainer>(sets.Sets);
            OkCommand = new RelayCommand(_ => ExecuteOkCommand());
        }

        public AddTupleViewModel()
        {
        }
    }
}