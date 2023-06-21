using System;
using System.IO;
using System.IO.Compression;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;

namespace Launcher
{
    enum LauncherStatus
    {
        ready,
        failed,
        downloadingGame,
        downloadingUpdate
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string rootPath;
        private string versionFile;
        private string gameZip;
        private string gameExe;

        private LauncherStatus _status;
        internal LauncherStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                switch (_status)
                {
                    case LauncherStatus.ready:
                        PlayButton.Content = "  Accéder à UniChat  ";
                        PlayButton.Foreground = System.Windows.Media.Brushes.White;
                        PlayButton.FontSize = 20;
                        break;
                    case LauncherStatus.failed:
                        PlayButton.Content = "  Échec de la mise à jour, veuillez réessayer  ";
                        PlayButton.Foreground = System.Windows.Media.Brushes.White;
                        PlayButton.FontSize = 20;
                        break;
                    case LauncherStatus.downloadingGame:
                        PlayButton.Content = "  Téléchargement d'UniChat en cours ...  ";
                        PlayButton.Foreground = System.Windows.Media.Brushes.White;
                        PlayButton.FontSize = 20;
                        break;
                    case LauncherStatus.downloadingUpdate:
                        PlayButton.Content = "  Mise à jour en cours ...  ";
                        PlayButton.Foreground = System.Windows.Media.Brushes.White;
                        PlayButton.FontSize = 20;
                        break;
                    default:
                        break;
                }

            }
        }

        public MainWindow()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            versionFile = Path.Combine(rootPath, "Version.txt");
            gameZip = Path.Combine(rootPath, "Build.zip");
            gameExe = Path.Combine(rootPath, "Build", "tictactoe.exe");
            /* Le nom de l'.exe est snesible à la casse [!] */
        }

        private void CheckForUpdates()
        {
            if (File.Exists(versionFile))
            {
                Version localVersion = new Version(File.ReadAllText(versionFile));
                VersionText.Text = localVersion.ToString();

                try
                {
                    WebClient webClient = new WebClient();
                    Version onlineVersion = new Version(webClient.DownloadString("http://betagames.alwaysdata.net/launcher/Version.txt"));

                    if (onlineVersion.IsDifferentThan(localVersion))
                    {
                        InstallGameFiles(true, onlineVersion);
                    }
                    else
                    {
                        Status = LauncherStatus.ready;
                    }
                }
                catch (Exception ex)
                {
                    Status = LauncherStatus.failed;
                    //MessageBox.Show($"Erreur, impossible de vérifier les mises à jour d'UniChat: {ex}");
                }
            }
            else
            {
                InstallGameFiles(false, Version.zero);
            }
        }

        private void InstallGameFiles(bool _isUpdate, Version _onlineVersion)
        {
            try
            {
                WebClient webClient = new WebClient();
                if (_isUpdate)
                {
                    Status = LauncherStatus.downloadingUpdate;
                }
                else
                {
                    Status = LauncherStatus.downloadingGame;
                    _onlineVersion = new Version(webClient.DownloadString("http://betagames.alwaysdata.net/launcher/Version.txt"));
                }

                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadGameCompletedCallback);
                webClient.DownloadFileAsync(new Uri("http://betagames.alwaysdata.net/launcher/Build.zip"), gameZip, _onlineVersion);
            }
            catch (Exception ex)
            {
                Status = LauncherStatus.failed;
                //MessageBox.Show($"Erreur, impossible d'installer les fichiers d'UniChat: {ex}");
            }
        }

        private void DownloadGameCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string onlineVersion = ((Version)e.UserState).ToString();
                ZipFile.ExtractToDirectory(gameZip, rootPath, true);
                File.Delete(gameZip);

                File.WriteAllText(versionFile, onlineVersion);

                VersionText.Text = onlineVersion;
                Status = LauncherStatus.ready;
            }
            catch (Exception ex)
            {
                Status = LauncherStatus.failed;
                //MessageBox.Show($"Erreur, impossible de finir le téléchargement d'UniChat: {ex}");
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(gameExe) && Status == LauncherStatus.ready)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(gameExe);
                startInfo.WorkingDirectory = Path.Combine(rootPath, "Build");
                Process.Start(startInfo);

                Close();
            }
            else if (Status == LauncherStatus.failed)
            {
                CheckForUpdates();
            }
        }

        private void closeApp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void minimizeApp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resizeApp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (WindowState == WindowState.Normal)
                {
                    // Si la fenêtre est actuellement en mode normal, bascule en mode maximisé
                    WindowState = WindowState.Maximized;
                }
                else if (WindowState == WindowState.Maximized)
                {
                    // Si la fenêtre est actuellement maximisée, bascule en mode normal
                    WindowState = WindowState.Normal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void cursorChange(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                var element = sender as FrameworkElement;
                if (element != null)
                {
                    element.Cursor = System.Windows.Input.Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    struct Version
    {
        internal static Version zero = new Version(0, 0, 0);

        private short major;
        private short minor;
        private short subMinor;

        internal Version(short _major, short _minor, short _subMinor)
        {
            major = _major;
            minor = _minor;
            subMinor = _subMinor;
        }
        internal Version(string _version)
        {
            string[] _versionStrings = _version.Split('.');
            if(_versionStrings.Length != 3)
            {
                major = 0;
                minor = 0;
                subMinor = 0;
                return;
            }
            major = short.Parse(_versionStrings[0]);
            minor = short.Parse(_versionStrings[1]);
            subMinor = short.Parse(_versionStrings[2]);
        }
        internal bool IsDifferentThan(Version _otherVersion)
        {
            if(major != _otherVersion.major)
            {
                return true;
            }
            else
            {
                if(minor != _otherVersion.minor)
                {
                    return true;
                }
                else
                {
                    if(subMinor != _otherVersion.subMinor)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override string ToString()
        {
            return $"{major}.{minor}.{subMinor}";
        }
    }
}
