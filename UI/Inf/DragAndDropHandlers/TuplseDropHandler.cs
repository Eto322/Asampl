using System;
using System.Windows;
using BLL.ASAMPL.DataContainerModel;
using GongSolutions.Wpf.DragDrop;
using UI.ViewModel;

namespace IntegratedViewModel.Inf.DragAndDropHandlers
{
    public class TuplseDropHandler : IDropTarget
    {
        private readonly MainViewModel _viewModel;

        public TuplseDropHandler(MainViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is TupleDataContainer)
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is TupleDataContainer data)
            {
                string dataRepresentation = data.GetDataRepresentation();
                _viewModel.AddTuplesRepresentations(dataRepresentation);
            }
        }
    }
}