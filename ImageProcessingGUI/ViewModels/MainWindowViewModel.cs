using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ImageProcessingGUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _statusText = "Ready";

    [ObservableProperty]
    private WriteableBitmap? _originalImage;

    [ObservableProperty]
    private WriteableBitmap? _filter1Image;

    [ObservableProperty]
    private WriteableBitmap? _filter2Image;

    [ObservableProperty]
    private WriteableBitmap? _filter3Image;

    [ObservableProperty]
    private WriteableBitmap? _filter4Image;

    private byte[]? _originalPixels;
    private int _imgWidth;
    private int _imgHeight;
    private Avalonia.Vector _dpi = new Avalonia.Vector(96, 96);
    private PixelFormat _format = PixelFormat.Bgra8888;
    private AlphaFormat _alpha = AlphaFormat.Premul;
    private int _stride;

    public void LoadImageFromStream(Stream stream)
    {
        var wb = WriteableBitmap.Decode(stream);
        
        _imgWidth = wb.PixelSize.Width;
        _imgHeight = wb.PixelSize.Height;
        _dpi = wb.Dpi;
        _format = wb.Format ?? PixelFormat.Bgra8888;
        _alpha = wb.AlphaFormat ?? AlphaFormat.Premul;

        OriginalImage = wb;
        Filter1Image = null;
        Filter2Image = null;
        Filter3Image = null;
        Filter4Image = null;

        using (var locked = wb.Lock())
        {
            _stride = locked.RowBytes;
            int size = _stride * _imgHeight;
            _originalPixels = new byte[size];
            Marshal.Copy(locked.Address, _originalPixels, 0, size);
        }
        StatusText = "Obraz wczytany!";
    }

    [RelayCommand]
    private void ProcessImages()
    {
        if (_originalPixels == null) return;
        StatusText = "Przetwarzanie...";

        Thread t1 = new Thread(() => ApplyFilter(1, InvertFilter));
        Thread t2 = new Thread(() => ApplyFilter(2, GrayscaleFilter));
        Thread t3 = new Thread(() => ApplyFilter(3, SepiaFilter));
        Thread t4 = new Thread(() => ApplyFilter(4, RedFilter));

        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();
    }

    private void ApplyFilter(int targetIndex, Func<byte, byte, byte, (byte r, byte g, byte b)> filterFunc)
    {
        if (_originalPixels == null) return;

        int size = _stride * _imgHeight;
        byte[] newPixels = new byte[size];

        for (int y = 0; y < _imgHeight; y++)
        {
            int rowOffset = y * _stride;
            for (int x = 0; x < _imgWidth; x++)
            {
                int pOffset = rowOffset + x * 4;
                byte b = _originalPixels[pOffset + 0];
                byte g = _originalPixels[pOffset + 1];
                byte r = _originalPixels[pOffset + 2];
                byte a = _originalPixels[pOffset + 3];

                var (newR, newG, newB) = filterFunc(r, g, b);

                newPixels[pOffset + 0] = newB;
                newPixels[pOffset + 1] = newG;
                newPixels[pOffset + 2] = newR;
                newPixels[pOffset + 3] = a;
            }
        }

        Dispatcher.UIThread.InvokeAsync(() =>
        {
            var wb = new WriteableBitmap(new Avalonia.PixelSize(_imgWidth, _imgHeight), _dpi, _format, _alpha);
            using (var locked = wb.Lock())
            {
                Marshal.Copy(newPixels, 0, locked.Address, size);
            }
            
            switch (targetIndex)
            {
                case 1: Filter1Image = wb; break;
                case 2: Filter2Image = wb; break;
                case 3: Filter3Image = wb; break;
                case 4: Filter4Image = wb; break;
            }

            StatusText = "Zakończono!";
        });
    }

    private (byte r, byte g, byte b) InvertFilter(byte r, byte g, byte b) => ((byte)(255 - r), (byte)(255 - g), (byte)(255 - b));
    private (byte r, byte g, byte b) GrayscaleFilter(byte r, byte g, byte b)
    {
        byte gray = (byte)(0.299 * r + 0.587 * g + 0.114 * b);
        return (gray, gray, gray);
    }
    private (byte r, byte g, byte b) SepiaFilter(byte r, byte g, byte b)
    {
        int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
        int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
        int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);
        return ((byte)Math.Min(255, tr), (byte)Math.Min(255, tg), (byte)Math.Min(255, tb));
    }
    private (byte r, byte g, byte b) RedFilter(byte r, byte g, byte b) => (r, 0, 0);
}