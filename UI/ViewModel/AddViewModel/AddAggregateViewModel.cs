using BLL.ASAMPL.KeyWordsModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BLL.ASAMPL.DataContainerModel;
using IntegratedViewModel.Inf;

namespace UI.View.AddView
{
    internal class AddAggregateViewModel : NotifyPropertyChanged
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

        private ObservableCollection<TupleDataContainer> _tuple;

        public ObservableCollection<TupleDataContainer> Tuple
        {
            get { return _tuple; }
            set
            {
                if (_tuple != value)
                {
                    _tuple = value;
                    NotifyOfPropertyChanged();
                }
            }
        }

        public ICommand OkCommand { get; }
        public Action CloseAction { get; set; }

        private void ExecuteOkCommand()
        {
            SelectedTuple = GetSelectedTuple();
            ResetSelected();
            CloseAction?.Invoke();
        }

        private void ResetSelected()
        {
            foreach (var setDataContainer in Tuple)
            {
                setDataContainer.IsChekedForView = false;
            }
        }

        private List<TupleDataContainer> GetSelectedTuple()
        {
            return Tuple.Where(e => e.IsChekedForView).ToList();
        }

        public List<TupleDataContainer> SelectedTuple { get; private set; }

        public AddAggregateViewModel(TupleKeyWordModel tuple)
        {
            Tuple = new ObservableCollection<TupleDataContainer>(tuple.Tuples);
            OkCommand = new RelayCommand(_ => ExecuteOkCommand());
        }

        public AddAggregateViewModel()
        {
        }
    }
}