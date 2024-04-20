using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using EvaluationApp.Services;
using System.Collections.ObjectModel;

using EvaluationApp.Views;
using EvaluationApp.Models;

namespace EvaluationApp.ViewModels
{
    public partial class ExamsViewModel : BaseViewModel
    {
        public ObservableCollection<Exam> Exams { get; }

        IDatabaseService dbService;

        [ObservableProperty]
        Exam selectedExam;

        public ExamsViewModel(IDatabaseService dbService)
        {
            Title = "Exam List";
            this.dbService = dbService;
            Exams = new();
        }

        [RelayCommand]
        async Task GetExamsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var exams = await dbService.GetExams();

                Exams.Clear();

                foreach (var exam in exams)
                    Exams.Add(exam);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToDetailsAsync()
        {
            Exam exam = SelectedExam ?? new Exam();

            var data = new Dictionary<string, object>
            {
                {nameof(Exam), exam }
            };

            await Shell.Current.GoToAsync(
                nameof(ExamDetailsView), true, data);

            SelectedExam = null;
        }
    }
}
