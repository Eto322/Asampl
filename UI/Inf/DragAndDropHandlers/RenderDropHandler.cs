using System;
using System.Windows;
using BLL.ASAMPL.DataContainerModel;
using GongSolutions.Wpf.DragDrop;
using UI.ViewModel;

namespace IntegratedViewModel.Inf.DragAndDropHandlers
{
    public class RenderDropHandler : IDropTarget
    {
        private readonly MainViewModel _viewModel;

        public RenderDropHandler(MainViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is RendererDataContainer)
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is RendererDataContainer rendererDataContainer)
            {
                string dataRepresentation = rendererDataContainer.GetDataRepresentation();
                _viewModel.AddRenderRepresentations(dataRepresentation);
            }
        }
    }
}