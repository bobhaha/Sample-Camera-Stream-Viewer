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
using Ozeki.Media;
using Ozeki.Camera;

namespace Sample_Camera_Stream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoViewerWPF _videoViewerWPF;
        private DrawingImageProvider _provider;
        private IIPCamera _ipCamera;
        private WebCamera _webCamera;
        private MediaConnector _connector;

        public MainWindow()
        {
            InitializeComponent();
            _connector = new MediaConnector();
            _provider = new DrawingImageProvider();
            SetVideoViewer();
        }

        private void SetVideoViewer()
        {
            _videoViewerWPF = new VideoViewerWPF
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = Brushes.Black
            };
            CameraBox.Children.Add(_videoViewerWPF);
            _videoViewerWPF.SetImageProvider(_provider);
        }

        private void ConnectUSBCamera_Click(object sender, RoutedEventArgs e)
        {
            _webCamera = new WebCamera();

            if(_webCamera != null)
            {
                _connector.Connect(_webCamera.VideoChannel, _provider);
                _videoViewerWPF.SetImageProvider(_provider);
                _webCamera.Start();
                _videoViewerWPF.Start();
            }
        }

        private void DisconnectUSBCamera_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ConnectIPCamera_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DisonnectIPCamera_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
