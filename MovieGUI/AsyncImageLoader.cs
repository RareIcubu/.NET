using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieGUI.Views; 

public static class AsyncImageLoader
{
    private static readonly HttpClient HttpClient = new HttpClient();

    public static readonly AttachedProperty<string?> SourceProperty =
        AvaloniaProperty.RegisterAttached<Image, string?>("Source", typeof(AsyncImageLoader));

    public static string? GetSource(Image element) => element.GetValue(SourceProperty);
    public static void SetSource(Image element, string? value) => element.SetValue(SourceProperty, value);

    static AsyncImageLoader()
    {
        SourceProperty.Changed.AddClassHandler<Image>(OnSourceChanged);
    }

    private static void OnSourceChanged(Image imageControl, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is string url && !string.IsNullOrWhiteSpace(url))
        {
            _ = SetImageAsync(imageControl, url);
        }
        else
        {
            imageControl.Source = null;
        }
    }

    private static async Task SetImageAsync(Image imageControl, string url)
    {
        try
        {
            var response = await HttpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            if (!response.IsSuccessStatusCode) return;

            using var stream = await response.Content.ReadAsStreamAsync();
            
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;
            var bitmap = new Bitmap(ms);

            await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
            {
                imageControl.Source = bitmap;
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd pobierania zdjęcia: {ex.Message}");
        }
    }
}