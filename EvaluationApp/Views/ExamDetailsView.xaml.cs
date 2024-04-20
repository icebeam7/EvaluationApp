using EvaluationApp.ViewModels;

namespace EvaluationApp.Views;

public partial class ExamDetailsView : ContentPage
{
	ExamDetailsViewModel vm;

	public ExamDetailsView(ExamDetailsViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
		this.BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		await vm.LoadDetailsCommand.ExecuteAsync(null);
    }
}