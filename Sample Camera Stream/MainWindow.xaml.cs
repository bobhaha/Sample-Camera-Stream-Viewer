using System.Windows;
using System.Windows.Media;
using Ozeki.Media;
using Ozeki.Camera;

namespace Sample_Camera_Stream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoViewerWPF _videoViewer;
        private IWebCamera _webCamera;
        private IIPCamera _ipCamera;
        private DrawingImageProvider _imageProvider;
        private MediaConnector _mediaConnector;

        public MainWindow()
        {
            InitializeComponent();
            _videoViewer = new VideoViewerWPF();
            _imageProvider = new DrawingImageProvider();
            _mediaConnector = new MediaConnector();
            _webCamera = new WebCamera();

            SetVideoViewer();
            UpdateUSBCameraList();
        }

        private void UpdateUSBCameraList()
        {
            if (_webCamera != null)
            {
                CameraList.Items.Add(_webCamera.DeviceName);
                CameraList.SelectedIndex = 0;
            }
        }

        private void SetVideoViewer()
        {
            _videoViewer = new VideoViewerWPF
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = Brushes.Black
            };
            CameraBox.Children.Add(_videoViewer);
            _videoViewer.SetImageProvider(_imageProvider);
        }

        private void ConnectUSBCamera_Click(object sender, RoutedEventArgs e)
        {

            _webCamera = new WebCamera();
            _mediaConnector.Connect(_webCamera.VideoChannel, _imageProvider);
            _videoViewer.SetImageProvider(_imageProvider);
            _webCamera.Start();
            _videoViewer.Start();
        }

        private void DisconnectUSBCamera_Click(object sender, RoutedEventArgs e)
        {
            _videoViewer.Stop();
            _webCamera.Stop();
            _mediaConnector.Disconnect(_webCamera.VideoChannel, _imageProvider);
            _webCamera = null;

        }

        private void ConnectIPCamera_Click(object sender, RoutedEventArgs e)
        {
            var host = HostAddress.Text;
            var port = HostPort.Text;
            var user = HostUserName.Text;
            var pass = HostPassword.Password;

            _ipCamera = IPCameraFactory.GetCamera(host + ":" + port, user, pass);
            if (_ipCamera == null) { statusBar.Content = "No Camera Found!"; return; }

            _mediaConnector.Connect(_ipCamera.VideoChannel, _imageProvider);
            _videoViewer.SetImageProvider(_imageProvider);
            _ipCamera.Start();
            _videoViewer.Start();
        }

        private void DisonnectIPCamera_Click(object sender, RoutedEventArgs e)
        {
            _videoViewer.Stop();
            _ipCamera.Disconnect();
            _ipCamera.Dispose();
            _mediaConnector.Disconnect(_ipCamera.VideoChannel, _imageProvider);
        }
    }
}
