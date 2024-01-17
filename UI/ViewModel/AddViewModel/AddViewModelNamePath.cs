using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IntegratedViewModel.Inf;
using Microsoft.Win32;

namespace UI.ViewModel.AddViewModel
{//LIBRARIES
 // HANDLERS
 // RENDERERS
 // SOURCES
    internal class AddViewModelNamePath : NotifyPropertyChanged
    {
        private string _name;
        private string _path;
        private string _windowTitle;

        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                if (_windowTitle != value)
                {
                    _windowTitle = value;
                    NotifyOfPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChanged();
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                NotifyOfPropertyChanged();
            }
        }

        public ICommand OkCommand { get; }
        public Action CloseAction { get; set; } // Для закриття вікна

        private ICommand _fromFile { get; set; }

        public ICommand FromFile
        {
            get
            {
                if (_fromFile == null)
                {
                    _fromFile = new RelayCommand(param =>
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "All files (*.*)|*.*";
                        openFileDialog.InitialDirectory = "c:\\";

                        if (openFileDialog.ShowDialog() == true)
                        {
                            Path = openFileDialog.FileName;
                            Name = System.IO.Path.GetFileName(Path);
                        }
                    });
                }

                return _fromFile;
            }
        }

        public AddViewModelNamePath(string keyWordName)
        {
            OkCommand = new RelayCommand(_ => ExecuteOkCommand());
            WindowTitle = "Add " + keyWordName;
        }

        public AddViewModelNamePath()
        {
        }

        private void ExecuteOkCommand()
        {
            CloseAction?.Invoke();
        }
    }
}