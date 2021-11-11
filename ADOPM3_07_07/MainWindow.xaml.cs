using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Threading;

namespace ADOPM3_07_07
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SynchronizationContext uiSyncCtx;
        public MainWindow()
        {
            InitializeComponent();
            uiSyncCtx = SynchronizationContext.Current;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myCounter.Content = (int.Parse((string)myCounter.Content) + 1).ToString();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Thread((object arg) =>
            {
                //Simulate some timeconsuming work
                //UI is responsive and can do other work
                Thread.Sleep(5000);

                //SynchronizationContext makes it easy to update UI controls from thread
                uiSyncCtx.Post(_ => myGreetings.Text = arg as string, null);
            })
            .Start("Hello from thread!");
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Simulate some timeconsuming work without a thread
            //the UI hangs
            Thread.Sleep(5000);
            myGreetings.Text = "Hello without thread!";
        }
    }
    //Exercise
    //1.    Discuss in the group the differences of Button click 1 and 2 and why it happens
    //2.    Create a new button starting another thread updating myGreeting with another message
    //      after a longer time
    //3.    Discuss in the group: is SynchronizationContext.Post() a thread safe update of UI components?
}
