using System;
using System.Windows;
using BLL.ASAMPL.DataContainerModel;
using GongSolutions.Wpf.DragDrop;
using UI.ViewModel;

namespace IntegratedViewModel.Inf.DragAndDropHandlers
{
    public class HandlerDropHandler : IDropTarget
    {
        private readonly MainViewModel _viewModel;

        public HandlerDropHandler(MainViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is HandlerDataContainer)
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is HandlerDataContainer handler)
            {
                string dataRepresentation = handler.GetDataRepresentation();
                _viewModel.AddHandlerRepresentations(dataRepresentation);
            }
        }
    }
}