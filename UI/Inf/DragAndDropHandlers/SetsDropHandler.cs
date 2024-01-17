using System;
using System.Windows;
using BLL.ASAMPL.KeyWordsModel;
using GongSolutions.Wpf.DragDrop;
using UI.ViewModel;

namespace IntegratedViewModel.Inf.DragAndDropHandlers
{
    public class SetsDropHandler : IDropTarget
    {
        private readonly MainViewModel _viewModel;

        public SetsDropHandler(MainViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is SetDataContainer)
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is SetDataContainer data)
            {
                string dataRepresentation = data.GetDataRepresentation();
                _viewModel.AddSetsRepresentations(dataRepresentation);
            }
        }
    }
}