using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BLL.ASAMPL;
using BLL.ASAMPL.AbstractModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using BLL.ASAMPL.DataContainerModel;
using BLL.ASAMPL.KeyWordsModel;
using BLL.Integration;
using DAL.Inf.deserealizationManager;
using DAL.Inf.serealizationManager;
using UI.View.AddView;
using UI.ViewModel.AddViewModel;
using Point = System.Windows.Point;
using GongSolutions.Wpf.DragDrop;
using IntegratedViewModel.Inf;
using IntegratedViewModel.Inf.DragAndDropHandlers;
using UI.ViewModel.AddViewModel.AddActionViewModles;
using Newtonsoft.Json;

namespace UI.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        #region KeyWords and DataContainers

        private AsamplManager asamplManager;
        private CompilerManager compilerManager;
        private SerializationManager serializationManager;
        private DeserealizationManager deserealizationManager;
        private ObservableCollection<AsamplKeyWordModel> _asamplKeyWords;

        private ActionsKeyWordModel _ActionKeyWord { get; set; }
        private AggregateKeyWordModel _aggregateKeyWord { get; set; }

        private ElementsKeyWordModel _elementsKeyWord { get; set; }

        private HandlerKeyWordModel _handlerKeyWordModel { get; set; }

        private LibraryKeyWordModel _libraryKeyWord { get; set; }

        private RenderersKeyWordModel _renderersKeyWord { get; set; }

        private SetsKeyWordModel _setKeyWordModel { get; set; }

        private SourceKeyWordModel _sourceKeyWord { get; set; }

        private TupleKeyWordModel _tupleKeyWord { get; set; }

        private string _consoleText { get; set; }

        public string ConsoleText
        {
            get => _consoleText;
            set
            {
                _consoleText = value;
                NotifyOfPropertyChanged();
            }
        }

        public ObservableCollection<AsamplKeyWordModel> AsamplKeyWord
        {
            get => _asamplKeyWords;
            set
            {
                _asamplKeyWords = value;
                NotifyOfPropertyChanged();
            }
        }

        public ActionsKeyWordModel ActionKeyWord
        {
            get => _ActionKeyWord;
            set
            {
                _ActionKeyWord = value;
                NotifyOfPropertyChanged();
            }
        }

        public AggregateKeyWordModel AggregateKeyWord
        {
            get => _aggregateKeyWord;
            set
            {
                _aggregateKeyWord = value;
                NotifyOfPropertyChanged();
            }
        }

        public ElementsKeyWordModel ElementsKeyWord
        {
            get => _elementsKeyWord;
            set
            {
                _elementsKeyWord = value;
                NotifyOfPropertyChanged();
            }
        }

        public HandlerKeyWordModel HandlerKeyWord
        {
            get => _handlerKeyWordModel;
            set
            {
                _handlerKeyWordModel = value;
                NotifyOfPropertyChanged();
            }
        }

        public LibraryKeyWordModel LibraryKeyWord
        {
            get => _libraryKeyWord;
            set
            {
                _libraryKeyWord = value;
                NotifyOfPropertyChanged();
            }
        }

        public RenderersKeyWordModel RenderersKeyWord
        {
            get => _renderersKeyWord;
            set
            {
                _renderersKeyWord = value;
                NotifyOfPropertyChanged();
            }
        }

        public SetsKeyWordModel SetKeyWord
        {
            get => _setKeyWordModel;
            set
            {
                _setKeyWordModel = value;
                NotifyOfPropertyChanged();
            }
        }

        public SourceKeyWordModel SourceKeyWord
        {
            get => _sourceKeyWord;
            set
            {
                _sourceKeyWord = value;
                NotifyOfPropertyChanged();
            }
        }

        public TupleKeyWordModel TupleKeyWord
        {
            get => _tupleKeyWord;
            set
            {
                _tupleKeyWord = value;
                NotifyOfPropertyChanged();
            }
        }

        private AsamplKeyWordModel _selectedAsamplKeyWordModel;
        private ObservableCollection<AsamplKeyWordDataContainer> _DataContainers { get; set; }

        public ObservableCollection<AsamplKeyWordDataContainer> DataContainers
        {
            get => _DataContainers;
            set
            {
                _DataContainers = value;
                NotifyOfPropertyChanged();
            }
        }

        public AsamplKeyWordModel SelectedAsamplKeyWordModel
        {
            get => _selectedAsamplKeyWordModel;
            set
            {
                _selectedAsamplKeyWordModel = value;
                NotifyOfPropertyChanged();
                UpdateDataContainers();
            }
        }

        private void UpdateDataContainers()
        {
            if (SelectedAsamplKeyWordModel != null)
            {
                DataContainers.Clear();
                foreach (var container in SelectedAsamplKeyWordModel.DataContainer)
                {
                    DataContainers.Add(container);
                }
            }
        }

        #endregion KeyWords and DataContainers

        #region setting

        private int _selectedThemeIndex;
        private string _libpath;
        private string _handlerpath;

        public string LibraryPath
        {
            get => _libpath;
            set
            {
                _libpath = value;
                NotifyOfPropertyChanged();
            }
        }

        public string Handlerpath
        {
            get => _handlerpath;
            set
            {
                _handlerpath = value;
                NotifyOfPropertyChanged();
            }
        }

        public int SelectedThemeIndex
        {
            get => _selectedThemeIndex;
            set
            {
                _selectedThemeIndex = value;
                NotifyOfPropertyChanged();
                ChangeTheme(value);
            }
        }

        private void ChangeTheme(int themeIndex)
        {
            var appResources = Application.Current.Resources.MergedDictionaries;

            Uri lightThemeUri =
                new Uri(
                    "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml",
                    UriKind.Absolute);
            Uri darkThemeUri =
                new Uri(
                    "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml",
                    UriKind.Absolute);

            ResourceDictionary newTheme = null;

            switch (themeIndex)
            {
                case 1: // Світла тема
                    newTheme = new ResourceDictionary { Source = lightThemeUri };

                    break;

                case 0: // Темна тема
                    newTheme = new ResourceDictionary { Source = darkThemeUri };

                    break;

                case 2: // Тема системи
                    // Тут можна використати логіку для визначення теми системи
                    break;
            }

            if (newTheme != null)
            {
                ChangeThemeResource(appResources, newTheme);
            }
        }

        private void ChangeThemeResource(Collection<ResourceDictionary> appResources, ResourceDictionary newTheme)
        {
            // Знайти та замінити існуючий ресурс теми
            var existingTheme = appResources.FirstOrDefault(d => d.Source.ToString().Contains("MaterialDesignTheme."));
            if (existingTheme != null)
            {
                var index = appResources.IndexOf(existingTheme);
                appResources[index] = newTheme;
            }
            else
            {
                appResources.Add(newTheme);
            }
        }

        #endregion setting

        #region Commands

        private ICommand _AddCommand;
        private ICommand _testCommand;
        private int _AddCommandIndex = 0;
        private ICommand _openInputDialogCommand;
        private ICommand _compile;
        private ICommand _sereialize;
        private ICommand _desereialize;
        private ICommand _serealizeCode;

        public ICommand SerealizeCode
        {
            get
            {
                if (_serealizeCode != null)
                {
                    _serealizeCode = new RelayCommand(param =>
                    {
                        var openFileDialog = new Microsoft.Win32.OpenFileDialog
                        {
                            Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",

                            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                        };

                        if (openFileDialog.ShowDialog() == true)
                        {
                            var allRepresentations = new List<string>();
                            allRepresentations.AddRange(_librariesRepresentations);
                            allRepresentations.AddRange(_handlerRepresentation);
                            allRepresentations.AddRange(_renderRepresentation);
                            allRepresentations.AddRange(_sourcesRepresentation);
                            allRepresentations.AddRange(_setsRepresentation);
                            allRepresentations.AddRange(_elementsRepresentation);
                            allRepresentations.AddRange(_tuplesRepresentation);
                            allRepresentations.AddRange(_aggregateRepresentation);
                            allRepresentations.AddRange(_actionsRepresentation);
                            serializationManager.SerealizeCodeList(allRepresentations, openFileDialog.FileName);
                        }
                    });
                }

                return _serealizeCode;
            }
        }

        public ICommand Deserealize
        {
            get
            {
                if (_desereialize == null)
                {
                    _desereialize = new RelayCommand(param =>
                    {
                        var openFileDialog = new Microsoft.Win32.OpenFileDialog
                        {
                            Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",

                            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                        };

                        if (openFileDialog.ShowDialog() == true)
                        {
                            AsamplKeyWord = new ObservableCollection<AsamplKeyWordModel>(deserealizationManager.DeserializeKeyWordListFromFile(openFileDialog.FileName));
                            /* AsamplKeyWord = new ObservableCollection<AsamplKeyWordModel>()
                             {
                                 LibraryKeyWord,
                                 HandlerKeyWord,
                                 RenderersKeyWord,
                                 SourceKeyWord,
                                 SetKeyWord,
                                 ElementsKeyWord,
                                 TupleKeyWord,
                                 AggregateKeyWord,
                                 ActionKeyWord
                             };*/

                            LibraryKeyWord = (LibraryKeyWordModel)AsamplKeyWord[0];
                            HandlerKeyWord = (HandlerKeyWordModel)AsamplKeyWord[1];
                            RenderersKeyWord = (RenderersKeyWordModel)AsamplKeyWord[2];
                            SourceKeyWord = (SourceKeyWordModel)AsamplKeyWord[3];
                            SetKeyWord = (SetsKeyWordModel)AsamplKeyWord[4];
                            ElementsKeyWord = (ElementsKeyWordModel)AsamplKeyWord[5];
                            TupleKeyWord = (TupleKeyWordModel)AsamplKeyWord[6];
                            AggregateKeyWord = (AggregateKeyWordModel)AsamplKeyWord[7];
                            ActionKeyWord = (ActionsKeyWordModel)AsamplKeyWord[8];
                        }
                    });
                }

                return _desereialize;
            }
        }

        public ICommand Sereialize
        {
            get
            {
                if (_sereialize == null)
                {
                    _sereialize = new RelayCommand(param =>
                    {
                        var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

                        var result = folderBrowserDialog.ShowDialog();

                        if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                        {
                            string filePath = System.IO.Path.Combine(folderBrowserDialog.SelectedPath, "SerializedKeywords.json");

                            serializationManager.SerializeKeyWordListToFile(AsamplKeyWord.ToList(), filePath);
                        }
                    });
                }

                return _sereialize;
            }
        }

        public ICommand Compile
        {
            get
            {
                if (_compile == null)
                {
                    _compile = new RelayCommand(param => compilerManager.Compile(compilerManager.GenerateAsamplRepresentation(librariesRepresentations,
                        HandlerRepresentation, renderRepresentation, sourcesRepresentation, setsRepresentation, elementsRepresentation, tuplesRepresentation, aggregateRepresentation,
                        actionsRepresentation)));
                }

                return _compile;
            }
        }

        public ICommand OpenInputDialogCommand
        {
            get
            {
                if (_openInputDialogCommand == null)
                {
                    _openInputDialogCommand = new RelayCommand(param => OpenInputDialog());
                }

                return _openInputDialogCommand;
            }
        }

        private void OpenInputDialog()
        {
            if (SelectedAsamplKeyWordModel != null)
            {
                switch (SelectedAsamplKeyWordModel.KeyWordName)
                {
                    case "LIBRARIES":
                        var addLibraryViewModel = new AddViewModelNamePath("Library");
                        addLibraryViewModel.CloseAction = () => _windowService.CloseWindow(addLibraryViewModel);
                        _windowService.ShowWindow(addLibraryViewModel);

                        Trace.Write(LibraryKeyWord.AddLibrary(addLibraryViewModel.Name, addLibraryViewModel.Path));
                        break;

                    case "HANDLERS":
                        var addHanlerViewModel = new AddViewModelNamePath("Handler");
                        addHanlerViewModel.CloseAction = () => _windowService.CloseWindow(addHanlerViewModel);
                        _windowService.ShowWindow(addHanlerViewModel);

                        Trace.Write(HandlerKeyWord.AddHandler(addHanlerViewModel.Name, addHanlerViewModel.Path));
                        break;

                    case "RENDERERS":
                        var addRenderViewModel = new AddViewModelNamePath("Render");
                        addRenderViewModel.CloseAction = () => _windowService.CloseWindow(addRenderViewModel);
                        _windowService.ShowWindow(addRenderViewModel);

                        Trace.Write(RenderersKeyWord.AddRenderer(addRenderViewModel.Name, addRenderViewModel.Path));
                        break;

                    case "SOURCES":
                        var addSourceViewModel = new AddViewModelNamePath("Source");
                        addSourceViewModel.CloseAction = () => _windowService.CloseWindow(addSourceViewModel);
                        _windowService.ShowWindow(addSourceViewModel);

                        Trace.Write(SourceKeyWord.AddSource(addSourceViewModel.Name, addSourceViewModel.Path));
                        break;

                    case "SETS":
                        var addSetViewModel = new AddSetViewModel();
                        addSetViewModel.CloseAction = () => _windowService.CloseWindow(addSetViewModel);
                        _windowService.ShowWindow(addSetViewModel);

                        Trace.Write(SetKeyWord.AddSet(addSetViewModel.Name, addSetViewModel.SelectedType));
                        break;

                    case "ELEMENTS":
                        if (SetKeyWord.Sets.Count < 1)
                        {
                            MessageBox.Show(
                                "Cant create element without alredy existing type as set, Try creating a set first");
                            break;
                        }

                        var addElemetsViewModel = new AddElementsViewModel(ElementsKeyWord, SetKeyWord);
                        addElemetsViewModel.CloseAction = () => _windowService.CloseWindow(addElemetsViewModel);
                        _windowService.ShowWindow(addElemetsViewModel);

                        Trace.Write(ElementsKeyWord.AddElement(addElemetsViewModel.Name,
                            addElemetsViewModel.SelectedSet, addElemetsViewModel.ElementValue));
                        break;

                    case "TUPLES":
                        if (SetKeyWord.Sets.Count < 1)
                        {
                            MessageBox.Show(
                                "Cant create Tuple ,without exiting set,try creating set first");
                            break;
                        }

                        var addTupleViewModel = new AddTupleViewModel(SetKeyWord);
                        addTupleViewModel.CloseAction = () => _windowService.CloseWindow(addTupleViewModel);
                        _windowService.ShowWindow(addTupleViewModel);

                        Trace.Write(TupleKeyWord.AddTuple(addTupleViewModel.Name, addTupleViewModel.SelectedSets));
                        break;

                    case "AGGREGATES":
                        if (TupleKeyWord.Tuples.Count < 1)
                        {
                            MessageBox.Show(
                                "Cant create Aggregate ,without exiting Tuple,try creating Tuple first");
                            break;
                        }

                        var addAggregateModel = new AddAggregateViewModel(TupleKeyWord);
                        addAggregateModel.CloseAction = () => _windowService.CloseWindow(addAggregateModel);
                        _windowService.ShowWindow(addAggregateModel);

                        Trace.Write(AggregateKeyWord.AddAggregate(addAggregateModel.Name,
                            addAggregateModel.SelectedTuple));
                        break;

                    case "ACTIONS":
                        var addActionViewModel = new AddActionViewModel(AggregateKeyWord, HandlerKeyWord, LibraryKeyWord, ActionKeyWord, RenderersKeyWord,
                            SetKeyWord, SourceKeyWord, TupleKeyWord, ElementsKeyWord);
                        addActionViewModel.CloseAction = () =>
                            _windowService.CloseWindow(addActionViewModel);
                        _windowService.ShowWindow(addActionViewModel);

                        ActionKeyWord.AddAction(addActionViewModel.FinilizedDataContainerKeyWord);
                        Trace.WriteLine(addActionViewModel.FinilizedDataContainerKeyWord.GetDataRepresentation());
                        ActionKeyWord.GetFormattedActions();

                        break;
                }
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(param =>
                    {
                        OpenInputDialog();
                        UpdateDataContainers();
                    });
                }

                return _AddCommand;
            }
        }

        #endregion Commands

        #region CodeSection

        #region Library

        private ObservableCollection<string> _librariesRepresentations;
        public LibraryDropHandler librariesDropHandler { get; private set; }
        public ICommand DeleteLibraryRepresentationCommand { get; }

        public ObservableCollection<string> librariesRepresentations
        {
            get { return _librariesRepresentations; }
            set
            {
                _librariesRepresentations = value;
                NotifyOfPropertyChanged();
            }
        }

        public void AddLibraryRepresentation(string representation)
        {
            librariesRepresentations.Add(representation);
        }

        private void DeleteLibraryRepresentation(object param)
        {
            if (param is string representation)
            {
                librariesRepresentations.Remove(representation);
            }
        }

        #endregion Library

        #region Handlers

        private ObservableCollection<string> _handlerRepresentation;
        public HandlerDropHandler handlerDropHandler { get; private set; }

        public ICommand DeleteHandlerRepresentationCommand { get; }

        public ObservableCollection<string> HandlerRepresentation
        {
            get { return _handlerRepresentation; }
            set
            {
                _handlerRepresentation = value;
                NotifyOfPropertyChanged();
            }
        }

        public void AddHandlerRepresentations(string representation)
        {
            HandlerRepresentation.Add(representation);
        }

        private void DeleteHandlerRepresentation(object param)
        {
            if (param is string representation)
            {
                HandlerRepresentation.Remove(representation);
            }
        }

        #endregion Handlers

        #region Renders

        private ObservableCollection<string> _renderRepresentation;
        public RenderDropHandler renderDropHandler { get; private set; }

        public ICommand DeleteRenderRepresentationCommand { get; }

        public ObservableCollection<string> renderRepresentation
        {
            get { return _renderRepresentation; }
            set
            {
                _renderRepresentation = value;
                NotifyOfPropertyChanged();
            }
        }

        public void AddRenderRepresentations(string representation)
        {
            renderRepresentation.Add(representation);
        }

        private void DeleteRenderRepresentation(object param)
        {
            if (param is string representation)
            {
                renderRepresentation.Remove(representation);
            }
        }

        #endregion Renders

        #region Sources

        private ObservableCollection<string> _sourcesRepresentation;
        public SourcesDropHandler sourcesDropHandler { get; private set; }

        public ICommand DeleteSourcesRepresentationCommand { get; }

        public ObservableCollection<string> sourcesRepresentation
        {
            get { return _sourcesRepresentation; }
            set
            {
                _sourcesRepresentation = value;
                NotifyOfPropertyChanged();
            }
        }

        public void AddSourcesRepresentations(string representation)
        {
            sourcesRepresentation.Add(representation);
        }

        private void DeleteSourcesRepresentation(object param)
        {
            if (param is string representation)
            {
                sourcesRepresentation.Remove(representation);
            }
        }

        #endregion Sources

        #region Sets

        private ObservableCollection<string> _setsRepresentation;
        public SetsDropHandler setsDropHandler { get; private set; }

        public ICommand DeleteSetsRepresentationCommand { get; }

        public ObservableCollection<string> setsRepresentation
        {
            get { return _setsRepresentation; }
            set
            {
                _setsRepresentation = value;
                NotifyOfPropertyChanged();
            }
        }

        public void AddSetsRepresentations(string representation)
        {
            setsRepresentation.Add(representation);
        }

        private void DeleteSetsRepresentation(object param)
        {
            if (param is string representation)
            {
                setsRepresentation.Remove(representation);
            }
        }

        #endregion Sets

        #region Elements

        private ObservableCollection<string> _elementsRepresentation;
        public ElementsDropHandler elementsDropHandler { get; private set; }

        public ICommand DeleteElementsRepresentationCommand { get; }

        public ObservableCollection<string> elementsRepresentation
        {
            get { return _elementsRepresentation; }
            set
            {
                _elementsRepresentation = value;
                NotifyOfPropertyChanged();
            }
        }

        public void AddElementsRepresentations(string representation)
        {
            elementsRepresentation.Add(representation);
        }

        private void DeleteElementsRepresentation(object param)
        {
            if (param is string representation)
            {
                elementsRepresentation.Remove(representation);
            }
        }

        #endregion Elements

        #region TUPLES

        private ObservableCollection<string> _tuplesRepresentation;
        public TuplseDropHandler tupleseDropHandler { get; private set; }

        public ICommand DeleteTupleseRepresentationCommand { get; }

        public ObservableCollection<string> tuplesRepresentation
        {
            get { return _tuplesRepresentation; }
            set
            {
                _tuplesRepresentation = value;
                NotifyOfPropertyChanged();
            }
        }

        public void AddTuplesRepresentations(string representation)
        {
            tuplesRepresentation.Add(representation);
        }

        private void DeleteTuplesRepresentation(object param)
        {
            if (param is string representation)
            {
                tuplesRepresentation.Remove(representation);
            }
        }

        #endregion TUPLES

        #region AGGREGATES

        private ObservableCollection<string> _aggregateRepresentation;
        public AggregateDropHandler aggregateDropHandler { get; private set; }

        public ICommand DeleteAggregateRepresentationCommand { get; }

        public ObservableCollection<string> aggregateRepresentation
        {
            get { return _aggregateRepresentation; }
            set
            {
                _aggregateRepresentation = value;
                NotifyOfPropertyChanged();
            }
        }

        public void AddAggregateRepresentations(string representation)
        {
            aggregateRepresentation.Add(representation);
        }

        private void DeleteAggregateRepresentation(object param)
        {
            if (param is string representation)
            {
                aggregateRepresentation.Remove(representation);
            }
        }

        #endregion AGGREGATES

        #region ACTIONS

        private ObservableCollection<string> _actionsRepresentation;
        public ActionDropHandler actionsDropHandler { get; private set; }

        public ICommand DeleteActionRepresentationCommand { get; }

        public ObservableCollection<string> actionsRepresentation
        {
            get { return _actionsRepresentation; }
            set
            {
                _actionsRepresentation = value;
                NotifyOfPropertyChanged();
            }
        }

        public void AddActionRepresentations(string representation)
        {
            actionsRepresentation.Add(representation);
        }

        private void DeleteActionRepresentation(object param)
        {
            if (param is string representation)
            {
                actionsRepresentation.Remove(representation);
            }
        }

        #endregion ACTIONS

        #endregion CodeSection

        private IWindowService _windowService;

        public MainViewModel()
        {
            DeleteLibraryRepresentationCommand = new RelayCommand(param => { DeleteLibraryRepresentation(param); });
            librariesDropHandler = new LibraryDropHandler(this);
            librariesRepresentations = new ObservableCollection<string>();

            DeleteHandlerRepresentationCommand = new RelayCommand(param => { DeleteHandlerRepresentation(param); });
            handlerDropHandler = new HandlerDropHandler(this);
            HandlerRepresentation = new ObservableCollection<string>();

            DeleteRenderRepresentationCommand = new RelayCommand(param => { DeleteRenderRepresentation(param); });
            renderDropHandler = new RenderDropHandler(this);
            renderRepresentation = new ObservableCollection<string>();

            DeleteSourcesRepresentationCommand = new RelayCommand(param => { DeleteSourcesRepresentation(param); });
            sourcesDropHandler = new SourcesDropHandler(this);
            sourcesRepresentation = new ObservableCollection<string>();

            DeleteSetsRepresentationCommand = new RelayCommand(param => { DeleteSetsRepresentation(param); });
            setsDropHandler = new SetsDropHandler(this);
            setsRepresentation = new ObservableCollection<string>();

            DeleteElementsRepresentationCommand = new RelayCommand(param => { DeleteElementsRepresentation(param); });
            elementsDropHandler = new ElementsDropHandler(this);
            elementsRepresentation = new ObservableCollection<string>();

            DeleteTupleseRepresentationCommand = new RelayCommand(param => { DeleteTuplesRepresentation(param); });
            tupleseDropHandler = new TuplseDropHandler(this);
            tuplesRepresentation = new ObservableCollection<string>();

            DeleteAggregateRepresentationCommand = new RelayCommand(param => { DeleteAggregateRepresentation(param); });
            aggregateDropHandler = new AggregateDropHandler(this);
            aggregateRepresentation = new ObservableCollection<string>();

            DeleteActionRepresentationCommand = new RelayCommand(param => { DeleteActionRepresentation(param); });
            actionsDropHandler = new ActionDropHandler(this);
            actionsRepresentation = new ObservableCollection<string>();

            asamplManager = new AsamplManager();
            compilerManager = new CompilerManager();
            serializationManager = new SerializationManager();
            deserealizationManager = new DeserealizationManager();

            _windowService = new WindowService();
            LibraryKeyWord = new LibraryKeyWordModel();
            AggregateKeyWord = new AggregateKeyWordModel();
            ElementsKeyWord = new ElementsKeyWordModel();
            RenderersKeyWord = new RenderersKeyWordModel();
            ActionKeyWord = new ActionsKeyWordModel();
            HandlerKeyWord = new HandlerKeyWordModel();
            SetKeyWord = new SetsKeyWordModel();
            SourceKeyWord = new SourceKeyWordModel();
            TupleKeyWord = new TupleKeyWordModel();
            AsamplKeyWord = new ObservableCollection<AsamplKeyWordModel>()
            {
                LibraryKeyWord,
                HandlerKeyWord,
                RenderersKeyWord,
                SourceKeyWord,
                SetKeyWord,
                ElementsKeyWord,
                TupleKeyWord,
                AggregateKeyWord,
                ActionKeyWord
            };

            /*LibraryKeyWord.AddLibrary("asdasdasd", "asdasdasdasdasd");
            ((LibraryKeyWordModel)AsamplKeyWord[0]).AddLibrary("adasdasd", "sadasdasdasd");*/

            DataContainers = new ObservableCollection<AsamplKeyWordDataContainer>(LibraryKeyWord.DataContainer);
            NotifyOfPropertyChanged();
        }
    }
}