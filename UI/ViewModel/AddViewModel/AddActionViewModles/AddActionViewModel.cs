using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;
using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;
using BLL.ASAMPL.KeyWordsModel;
using IntegratedViewModel.Inf;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;

namespace UI.ViewModel.AddViewModel.AddActionViewModles
{
    internal class AddActionViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<AggregateDataContainer> _aggregateData;
        private ObservableCollection<RendererDataContainer> _rendererData;
        private ObservableCollection<HandlerDataContainer> _handlerData;
        private ObservableCollection<LibraryDataContainer> _libraryData;
        private ObservableCollection<ActionDataContainer> _actionData;
        private ObservableCollection<SetDataContainer> _setData;
        private ObservableCollection<SourceDataContainer> _sourceData;
        private ObservableCollection<TupleDataContainer> _tupleData;
        private ObservableCollection<ElementsDataContainer> _elementsData;
        private TabItem _selectedTab;
        public ActionDataContainer FinilizedDataContainerKeyWord;

        private HandlerDataContainer _selectedHandlerData;
        private LibraryDataContainer _selectedLibraryData;
        private ActionDataContainer _selectedActionData;
        private SetDataContainer _selectedSetData;
        private SourceDataContainer _selectedSourceData;
        private TupleDataContainer _selectedTupleData;
        private ElementsDataContainer _selectedElementsData;

        #region DataContainer

        public TabItem SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                foreach (var actionDataContainer in ActionData)
                {
                    actionDataContainer.IsChekedForView = false;
                }
                NotifyOfPropertyChanged(nameof(SelectedTab));
                /*FinishedKeyWord = _selectedTab.Header.ToString();*/
            }
        }

        public ObservableCollection<ElementsDataContainer> ElementsData
        {
            get => _elementsData;
            set
            {
                _elementsData = value;
                NotifyOfPropertyChanged();
            }
        }

        public ObservableCollection<TupleDataContainer> TupleData
        {
            get => _tupleData;
            set
            {
                _tupleData = value;
                NotifyOfPropertyChanged();
            }
        }

        public ObservableCollection<SetDataContainer> SetData
        {
            get => _setData;
            set
            {
                _setData = value;
                NotifyOfPropertyChanged();
            }
        }

        public ObservableCollection<SourceDataContainer> SourceData
        {
            get => _sourceData;
            set
            {
                _sourceData = value;
                NotifyOfPropertyChanged();
            }
        }

        public ObservableCollection<ActionDataContainer> ActionData
        {
            get => _actionData;

            set
            {
                _actionData = value;
                NotifyOfPropertyChanged();
            }
        }

        public ObservableCollection<LibraryDataContainer> LibraryData
        {
            get => _libraryData;
            set
            {
                _libraryData = value;
                NotifyOfPropertyChanged();
            }
        }

        public ObservableCollection<HandlerDataContainer> HandlerData
        {
            get => _handlerData;

            set
            {
                _handlerData = value;
                NotifyOfPropertyChanged();
            }
        }

        public ObservableCollection<AggregateDataContainer> AggregateData
        {
            get => _aggregateData;
            set
            {
                _aggregateData = value;
                NotifyOfPropertyChanged();
            }
        }

        public ObservableCollection<RendererDataContainer> RenderData
        {
            get => _rendererData;
            set
            {
                _rendererData = value;
                NotifyOfPropertyChanged();
            }
        }

        #endregion DataContainer

        #region RenderWithregion(tab1)

        private AggregateDataContainer _selectedAggregateData_Tab1;
        private RendererDataContainer _selectedRenderData_Tab1;

        public AggregateDataContainer SelectedFirstIdentifierRenderWith
        {
            get => _selectedAggregateData_Tab1;
            set
            {
                _selectedAggregateData_Tab1 = value;
                NotifyOfPropertyChanged();
            }
        }

        public RendererDataContainer SelectedSecondIdentifierRenderWith
        {
            get => _selectedRenderData_Tab1;
            set
            {
                _selectedRenderData_Tab1 = value;
                NotifyOfPropertyChanged();
            }
        }

        public ICommand OkCommand { get; }

        public Action CloseAction { get; set; }

        #endregion RenderWithregion(tab1)

        #region DownloadWithRegion(tab2)

        private ElementsDataContainer _selectedElementsData_tab2;
        private SourceDataContainer _selectedSourceData_tab2;
        private HandlerDataContainer _selectedHandlerData_tab2;

        public ElementsDataContainer SelectedFirstIndetiferDownloadWith
        {
            get => _selectedElementsData_tab2;
            set
            {
                _selectedElementsData_tab2 = value;
                NotifyOfPropertyChanged();
            }
        }

        public SourceDataContainer SelectedSecondIndetiferDownloadWith
        {
            get => _selectedSourceData_tab2;
            set
            {
                _selectedSourceData_tab2 = value;
                NotifyOfPropertyChanged();
            }
        }

        public HandlerDataContainer SelectedThirdIndetiferDownloadWith
        {
            get => _selectedHandlerData_tab2;
            set
            {
                _selectedHandlerData_tab2 = value;
                NotifyOfPropertyChanged();
            }
        }

        #endregion DownloadWithRegion(tab2)

        #region UploadWithRegion(tab3)

        private ElementsDataContainer _selectedElementsData_tab3;
        private SourceDataContainer _selectedSourceData_tab3;
        private HandlerDataContainer _selectedHandlerData_tab3;

        public ElementsDataContainer SelectedFirstIndetiferUploadWith
        {
            get => _selectedElementsData_tab3;
            set
            {
                _selectedElementsData_tab3 = value;
                NotifyOfPropertyChanged();
            }
        }

        public SourceDataContainer SelectedSecondIndetiferUploadWith
        {
            get => _selectedSourceData_tab3;
            set
            {
                _selectedSourceData_tab3 = value;
                NotifyOfPropertyChanged();
            }
        }

        public HandlerDataContainer SelectedThirdIndetiferUploadWith
        {
            get => _selectedHandlerData_tab3;
            set
            {
                _selectedHandlerData_tab3 = value;
                NotifyOfPropertyChanged();
            }
        }

        #endregion UploadWithRegion(tab3)

        #region IF THEN(tab5)

        private ElementsDataContainer _selectedElementsDataContainer_tab5;
        private string _selectedComaprisonSighn_tab5;
        private string _selectedComaprisonItem_tab5;
        public List<string> ComparisonOperators { get; } = new List<string> { "==", "!=", ">", "<" };

        public ElementsDataContainer SelectedFirstIdentifireIf
        {
            get => _selectedElementsDataContainer_tab5;
            set
            {
                _selectedElementsDataContainer_tab5 = value;
                NotifyOfPropertyChanged();
            }
        }

        public string SelectedSecondIdentifireIf
        {
            get => _selectedComaprisonSighn_tab5;
            set
            {
                _selectedComaprisonSighn_tab5 = value;
                NotifyOfPropertyChanged();
            }
        }

        public string SelectedThirdIdentifireIf
        {
            get => _selectedComaprisonItem_tab5;
            set
            {
                _selectedComaprisonItem_tab5 = value;
                NotifyOfPropertyChanged();
            }
        }

        #endregion IF THEN(tab5)

        #region SUBSTITUTE(tab7)

        private TupleDataContainer _selectedFirstTupleDataContainer_tab6;
        private TupleDataContainer _selectedSeconIdentifireSubTab6;

        public TupleDataContainer SelectedFirstContainerIdentifireSub
        {
            get => _selectedFirstTupleDataContainer_tab6;
            set
            {
                _selectedFirstTupleDataContainer_tab6 = value;
                NotifyOfPropertyChanged();
            }
        }

        public TupleDataContainer SelectedSecondIdentifireSub
        {
            get => _selectedSeconIdentifireSubTab6;
            set
            {
                _selectedSeconIdentifireSubTab6 = value;
                NotifyOfPropertyChanged();
            }
        }

        #endregion SUBSTITUTE(tab7)

        #region dbg

        private string _DbgIdentfire;

        public string DbgIdentfire
        {
            get => _DbgIdentfire;
            set
            {
                _DbgIdentfire = value;
                NotifyOfPropertyChanged();
            }
        }

        #endregion dbg

        public AddActionViewModel(AggregateKeyWordModel aggregateKeyWord, HandlerKeyWordModel handlerKeyWord, LibraryKeyWordModel libraryKeyWord
            , ActionsKeyWordModel actionsKeyWord, RenderersKeyWordModel renderersKeyWord, SetsKeyWordModel setsKeyWord,
            SourceKeyWordModel sourceKeyWord, TupleKeyWordModel tupleKeyWord, ElementsKeyWordModel elementsKeyWord)
        {
            LibraryData = new ObservableCollection<LibraryDataContainer>(libraryKeyWord.Libraries);
            HandlerData = new ObservableCollection<HandlerDataContainer>(handlerKeyWord.Handlers);
            AggregateData = new ObservableCollection<AggregateDataContainer>(aggregateKeyWord.Aggregates);
            RenderData = new ObservableCollection<RendererDataContainer>(renderersKeyWord.Renderers);
            ActionData = new ObservableCollection<ActionDataContainer>(actionsKeyWord.Actions);
            SetData = new ObservableCollection<SetDataContainer>(setsKeyWord.Sets);
            SourceData = new ObservableCollection<SourceDataContainer>(sourceKeyWord.Sources);
            TupleData = new ObservableCollection<TupleDataContainer>(tupleKeyWord.Tuples);
            ElementsData = new ObservableCollection<ElementsDataContainer>(elementsKeyWord.Elements);

            OkCommand = new RelayCommand(_ => ExecuteOkCommand());
        }

        private void ExecuteOkCommand()
        {
            FormKeyWordContainer();
            CloseAction?.Invoke();
        }

        public AddActionViewModel()
        {
        }

        private void FormKeyWordContainer()
        {
            switch (SelectedTab.Header.ToString())
            {
                case "DOWNLOAD":
                    var downloadlist = new List<AsamplKeyWordDataContainer>();
                    downloadlist.Add(SelectedFirstIndetiferDownloadWith);
                    downloadlist.Add(SelectedSecondIndetiferDownloadWith);
                    downloadlist.Add(SelectedThirdIndetiferDownloadWith);
                    FinilizedDataContainerKeyWord = new ActionDataContainer("DOWNLOAD", downloadlist);
                    break;

                case "UPLOAD":
                    var uploadtlist = new List<AsamplKeyWordDataContainer>();
                    uploadtlist.Add(SelectedFirstIndetiferUploadWith);
                    uploadtlist.Add(SelectedSecondIndetiferUploadWith);
                    uploadtlist.Add(SelectedThirdIndetiferUploadWith);
                    FinilizedDataContainerKeyWord = new ActionDataContainer("UPLOAD", uploadtlist);
                    break;

                case "TIMELINE":

                    break;

                case "IF THEN":

                    var IfThenList = new List<AsamplKeyWordDataContainer>();
                    var Condition = new ConditionDataContainer(SelectedFirstIdentifireIf.DataName, SelectedSecondIdentifireIf.ToString(),
                        SelectedThirdIdentifireIf.ToString());
                    IfThenList.Add(Condition);
                    foreach (var actionDataContainer in ActionData)
                    {
                        if (actionDataContainer.IsChekedForView == true)
                        {
                            IfThenList.Add(actionDataContainer);
                        }
                    }

                    FinilizedDataContainerKeyWord = new ActionDataContainer("IF THEN", IfThenList);
                    break;

                case "RENDER":

                    var renderlist = new List<AsamplKeyWordDataContainer>();
                    renderlist.Add(SelectedFirstIdentifierRenderWith);
                    renderlist.Add(SelectedSecondIdentifierRenderWith);
                    FinilizedDataContainerKeyWord = new ActionDataContainer("RENDER", renderlist);

                    break;

                case "SUBSTITUTE":
                    var Sublist = new List<AsamplKeyWordDataContainer>();
                    Sublist.Add(SelectedFirstContainerIdentifireSub);
                    Sublist.Add(SelectedSecondIdentifireSub);
                    FinilizedDataContainerKeyWord = new ActionDataContainer("SUBSTITUTE", Sublist);
                    break;

                case "SEQUENCE":

                    var SequenceList = new List<AsamplKeyWordDataContainer>();
                    foreach (var actionDataContainer in ActionData)
                    {
                        if (actionDataContainer.IsChekedForView == true)
                        {
                            SequenceList.Add(actionDataContainer);
                        }
                    }

                    FinilizedDataContainerKeyWord = new ActionDataContainer("SEQUENCE", SequenceList);
                    break;

                case "dbg":
                    var dbglist = new List<AsamplKeyWordDataContainer>();
                    dbglist.Add(new ElementsDataContainer(DbgIdentfire, new SetDataContainer(DbgIdentfire, "DBG"),
                        DbgIdentfire));
                    FinilizedDataContainerKeyWord = new ActionDataContainer("dbg", dbglist);
                    break;

                default:

                    Trace.WriteLine("Невідоме ключове слово: " + SelectedTab.Header.ToString());
                    break;
            }
        }
    }
}