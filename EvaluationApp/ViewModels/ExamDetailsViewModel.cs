using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using EvaluationApp.Models;
using EvaluationApp.Services;

namespace EvaluationApp.ViewModels
{
    [QueryProperty(nameof(Exam), nameof(Exam))]
    public partial class ExamDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Exam exam;

        [ObservableProperty]
        Question newQuestion;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotNew))]
        bool isNew;

        public bool IsNotNew => !IsNew;

        public ObservableCollection<Question> Questions { get; }

        IDatabaseService dbService;

        public ExamDetailsViewModel(IDatabaseService dbService)
        {
            this.dbService = dbService;
            this.Title = "Exam Details";
            this.Questions = new();
            this.NewQuestion = new();
        }

        async Task loadQuestions()
        {
            Exam.Questions = await dbService.GetQuestions(Exam.Id);

            Questions.Clear();

            foreach (var question in Exam.Questions)
                Questions.Add(question);
        }

        [RelayCommand]
        async Task LoadDetailsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                IsNew = Exam.Id == 0;

                if (!IsNew)
                {
                    Exam = await dbService.GetExamById(Exam.Id);
                    await loadQuestions();
                }
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
        async Task CreateExamAsync()
        {
            if (IsBusy || !IsNew)
                return;

            try
            {
                IsBusy = true;
                await dbService.AddExam(Exam);
                IsNew = Exam.Id == 0;
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
        async Task DeleteExamAsync()
        {
            if (IsBusy || IsNew)
                return;

            try
            {
                IsBusy = true;

                var confirm =
                    await Shell.Current.DisplayAlert(
                        "Confirm operation",
                        "Do you really want to delete this exam?",
                        "Yes",
                        "No");

                if (confirm)
                {
                    foreach (var question in Exam.Questions)
                        await dbService.DeleteQuestion(question);

                    await dbService.DeleteExam(Exam);

                    IsBusy = false;
                    await Shell.Current.Navigation.PopAsync();
                }
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
        async Task AddQuestionAsync()
        {
            if (IsBusy || IsNew)
                return;

            try
            {
                IsBusy = true;
                NewQuestion.ExamId = Exam.Id;
                await dbService.AddQuestion(NewQuestion);
                await loadQuestions();
                this.NewQuestion = new();
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

    }
}
