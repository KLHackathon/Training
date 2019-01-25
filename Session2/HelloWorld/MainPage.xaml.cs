using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Btn1.Click += Btn1_Click;
        }
        private async Task WriteLine(string line)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                tb1.Text += line + '\n';
            });
        }
        private async Task Something()
        {
            await WriteLine("Async task started");
            await Task.Delay(100); //We simulate a time consumnig job by delay
            await WriteLine("Async task ends");
        }
        private async void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            await WriteLine("Before running something");
            Task something = Something();
            Task delay = Task.Delay(200); //another time consuming job
            await WriteLine("After running something, but before await");
            await Task.WhenAll(something, delay);
            await WriteLine("After awaiting the task and delay");
            stopWatch.Stop();
            await WriteLine(stopWatch.ElapsedMilliseconds.ToString());
            await WriteLine("--------------");
        }
    }
}
