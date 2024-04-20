using CommunityToolkit.Mvvm.ComponentModel;

namespace EvaluationApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        string title;

        [ObservableProperty]
        bool isBusy;
    }
}
