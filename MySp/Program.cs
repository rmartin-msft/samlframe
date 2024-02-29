using Rsk.AspNetCore.Authentication.Saml2p;

namespace MySp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();        
        
        builder.Services.AddAuthentication(configOptions => {
           configOptions.DefaultAuthenticateScheme = "cookie";
           configOptions.DefaultChallengeScheme = "saml2";
        }).AddCookie("cookie")
        .AddSaml2p("saml2", options => {
            options.Licensee = "DEMO";
            // options.LicenseKey = "eyJTb2xkRm9yIjowLjAsIktleVByZXNldCI6NiwiU2F2ZUtleSI6ZmFsc2UsIkxlZ2FjeUtleSI6ZmFsc2UsIlJlbmV3YWxTZW50VGltZSI6IjAwMDEtMDEtMDFUMDA6MDA6MDAiLCJhdXRoIjoiREVNTyIsImV4cCI6IjIwMjQtMDMtMjJUMDE6MDA6MDAuNjM1NzUxNiswMDowMCIsImlhdCI6IjIwMjQtMDItMjFUMDE6MDA6MDAiLCJvcmciOiJERU1PIiwiYXVkIjoyfQ==.EgOpxHZ7KI27nF/3LKEEyYyFor1XOpSQiTn9YZBurtbf8Gdo12BFakofQPuk2KBEHswxnbwUeLuQZSZ4HNJT+GdPFVpdOACAMc9IJ8ynEkKMLOPKyAPrahejtrXZ50law2c8PQFXlg2JUfG1MMDyppvXcB2DPuT08rfubpZaUTK596byrOGHkj14iI/Z7RN2Jb66mLGYWL9+SHMUQ21P6WlFTpLAYccqMpIuI+I/oIYB5fdF8gD/zl2Qdckf8IG20/YJtJ57WyMRZY4yanGjDIuuTF29d2lYTaIhlh35pyLhPIJobny84HK4yGq0OWy0BvAgUUF5Lgjf5hibcQ6he9Uirx+D9X/jjY7EVYv9jpMbOx2oBPEx4EmUZT0i5zJqzBrfmjyzASRBkrcOPlOA6rlNEivC9fJJgi3NmI+IqHdoFCaUD2VHVQkcihEne1kbh3A1oiV4AbTv4spWym7o9P06lHNEJQSJSHUZP7UYKL+vVzXszcZM0IG2u7sMrJysvVjXa4nbG97K8wNxvCJAY4w7cl8yDLQu0nTHbdJxWrF5otVtt3hw9rEcq7/mLLfz+hWfUirwl1Nexmckt3bGTNAuxkJw3x8tCSesZey7Eg121cFUO90VdyEHL9lla0s/gjconvWvOQpzkYq+wat6z55gsQA2CovLYVSH5d8n9WA=";
            options.LicenseKey = builder.Configuration["saml2:RskLicenseKey"];
            options.CallbackPath = "/signin-saml";
            options.SignInScheme = "cookie";

            options.ServiceProviderOptions = new SpOptions() {
                EntityId = "https://localhost:7260/saml",
            };

            options.IdentityProviderMetadataAddress = builder.Configuration["saml2:idpMetadataPath"];
            // options.IdentityProviderMetadataAddress = @"https://login.microsoftonline.com/23be3909-518e-4dc0-b31c-2f93a49b0d8f/federationmetadata/2007-06/federationmetadata.xml?appid=ca972b2a-0a16-4e7e-b2a6-1c35ef2487da";
            options.TimeComparisonTolerance = 120;
        });        

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();        
        }

        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
