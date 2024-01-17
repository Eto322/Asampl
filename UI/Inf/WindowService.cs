using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using UI.View;
using UI.View.AddView;
using UI.View.AddView.AddActionViews;
using UI.ViewModel.AddViewModel;
using UI.ViewModel.AddViewModel.AddActionViewModles;

namespace IntegratedViewModel.Inf
{
    public class WindowService : IWindowService
    {
        public void ShowWindow(object viewModel)
        {
            Window window = CreateWindowBasedOnViewModel(viewModel);
            window.DataContext = viewModel;
            Trace.WriteLine($"Showing window for ViewModel type: {viewModel.GetType()}. DataContext set.");
            window.ShowDialog();
        }

        private Window CreateWindowBasedOnViewModel(object viewModel)
        {
            switch (viewModel)
            {
                case AddViewModelNamePath _:
                    return new AddNamePathView();

                case AddSetViewModel _:
                    return new AddSetView();

                case AddElementsViewModel _:
                    return new AddElementView();

                case AddTupleViewModel _:
                    return new AddTupleView();

                case AddAggregateViewModel _:
                    return new AddAggregateView();

                case AddActionViewModel:
                    return new AddActionView();

                default:
                    // Логіка для обробки невідомих типів
                    throw new ArgumentException("No window exists for the specified ViewModel", nameof(viewModel));
            }
        }

        public void CloseWindow(object viewModel)
        {
            Trace.WriteLine($"Attempting to close window for ViewModel type: {viewModel.GetType()}");

            var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == viewModel);

            if (window != null)
            {
                Trace.WriteLine("Window found. Closing now.");
                window.Close();
            }
            else
            {
                Trace.WriteLine("No window found for this ViewModel.");
            }
        }
    }
}