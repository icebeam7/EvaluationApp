using EvaluationApp.Views;

namespace EvaluationApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ExamDetailsView), 
                typeof(ExamDetailsView));
        }
    }
}
