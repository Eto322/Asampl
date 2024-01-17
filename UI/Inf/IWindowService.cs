namespace IntegratedViewModel.Inf
{
    public interface IWindowService
    {
        void ShowWindow(object viewModel);

        void CloseWindow(object viewModel);
    }
}