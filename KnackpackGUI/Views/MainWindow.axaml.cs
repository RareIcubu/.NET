using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab1Knacpack;
namespace AvaloniaApplication1.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
   
    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        int n = (int) Items.Value;
        int seed = (int) Seed.Value;
        int capacity = (int) Capacity.Value; 
        Problem problem = new Problem(n,seed);
        Result result = problem.Solve(capacity);
        Results.Text = result.ToString();
    }
 
    
}