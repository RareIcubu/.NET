using BlazorApp1.Components;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.ML;

namespace BlazorApp1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate(); 
        builder.Services.AddPredictionEnginePool<SentimentModel.ModelInput, SentimentModel.ModelOutput>().FromFile("SentimentModel.mlnet"); 
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        app.UseAuthentication();
        app.UseAuthorization();
        app.Run();
    }
}