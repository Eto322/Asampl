using System;
using System.Windows;
using BLL.ASAMPL.DataContainerModel;
using GongSolutions.Wpf.DragDrop;
using UI.ViewModel;

namespace IntegratedViewModel.Inf.DragAndDropHandlers
{
    public class AggregateDropHandler : IDropTarget
    {
        private readonly MainViewModel _viewModel;

        public AggregateDropHandler(MainViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is AggregateDataContainer)
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is AggregateDataContainer data)
            {
                string dataRepresentation = data.GetDataRepresentation();
                _viewModel.AddAggregateRepresentations(dataRepresentation);
            }
        }
    }
}