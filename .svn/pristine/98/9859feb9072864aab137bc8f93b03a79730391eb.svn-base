using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Eam.PrintService;

namespace PrintTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //PrintHelper.PrintImage(tbFile.Text);
                ChannelFactory<IPrintService> cf = new ChannelFactory<IPrintService>(new WebHttpBinding(),
                    "http://127.0.0.1:7777/PrintService");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                IPrintService channel = cf.CreateChannel();
                //channel.PrintIamge(new PrintItem{ QrPath= tbFile.Text,AssetsNum="090"});
                Console.Read();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}