using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
 
using cww.Components.Pages.Admin;

namespace cww
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
                });

            builder.Services.AddMauiBlazorWebView();

         

            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
 
            AdminController adminController = new AdminController();
            adminController.SeedAdminData();
            return builder.Build();
        }
    }
}
