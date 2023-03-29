using Microsoft.Extensions.Logging;
using HighlightTextApp.Services;


namespace HighlightTextApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        //#if WINDOWS
        //		builder.Services.AddSingleton<ITextService, HighlightTextApp.Windows.Services.TextService>();
        //#endif
        DependencyService.Register<ITextService, HighlightTextApp.Windows.Services.TextService>();
        return builder.Build();
	}
}
