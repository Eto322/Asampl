using System;
using System.Windows;
using BLL.ASAMPL.DataContainerModel;
using GongSolutions.Wpf.DragDrop;
using UI.ViewModel;

namespace IntegratedViewModel.Inf.DragAndDropHandlers
{
    public class LibraryDropHandler : IDropTarget
    {
        private readonly MainViewModel _viewModel;

        public LibraryDropHandler(MainViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is LibraryDataContainer)
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is LibraryDataContainer libraryData)
            {
                string dataRepresentation = libraryData.GetDataRepresentation();
                _viewModel.AddLibraryRepresentation(dataRepresentation);
            }
        }
    }
}