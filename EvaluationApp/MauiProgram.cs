using CommunityToolkit.Maui;

using EvaluationApp.Services;
using EvaluationApp.ViewModels;
using EvaluationApp.Views;
using Microsoft.Extensions.Logging;

namespace EvaluationApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();

            builder.Services.AddScoped<ExamsViewModel>().
                AddScoped<ExamDetailsViewModel>();

            builder.Services.AddScoped<ExamsView>().
                AddScoped<ExamDetailsView>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
