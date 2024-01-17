using System;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using UI.ViewModel;

namespace IntegratedViewModel.Inf.DragAndDropHandlers
{
    public class ElementsDropHandler : IDropTarget
    {
        private readonly MainViewModel _viewModel;

        public ElementsDropHandler(MainViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is ElementsDataContainer)
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is ElementsDataContainer data)
            {
                string dataRepresentation = data.GetDataRepresentation();
                _viewModel.AddElementsRepresentations(dataRepresentation);
            }
        }
    }
}