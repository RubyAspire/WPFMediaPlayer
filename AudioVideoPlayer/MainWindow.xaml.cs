using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.IO;
using System.Windows.Controls.Primitives;
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
using System.Windows.Threading;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Media.Effects;
using System.Windows.Automation;

namespace AudioVideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer audio = new MediaPlayer();
        bool isPlaying = false;
        int count = 0;
        List<string> paths = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
            sliDuration.ApplyTemplate();
            Thumb thumb = (sliDuration.Template.FindName("PART_Track", sliDuration) as Track).Thumb;
            thumb.MouseEnter += new MouseEventHandler(Thumb_MouseEnter);

            WindowResizer resizer = new WindowResizer(this);
        }



        private void Thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.MouseDevice.Captured == null)
            {
                MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);
                args.RoutedEvent = MouseLeftButtonUpEvent;
                (sender as Thumb).RaiseEvent(args);
                mediaPlayer.Position = TimeSpan.FromSeconds(sliDuration.Value);
            }

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                sliDuration.Minimum = 0;
                sliDuration.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliDuration.Value = mediaPlayer.Position.TotalSeconds;
                txtTime.Text = TimeSpan.FromSeconds(sliDuration.Value).ToString(@"hh\:mm\:ss");
                txtFullTime.Text = TimeSpan.FromSeconds(sliDuration.Maximum).ToString(@"hh\:mm\:ss");

                if (sliDuration.Value == sliDuration.Maximum)
                {
                    count++;
                    //sliDuration.Value = 0;
                    //mediaPlayer.Stop();
                    //mediaPlayer.Play();
                    if (count >= paths.Count)
                        count = 0;
                    mediaPlayer.Source = new Uri(paths[count]);
                    txtFileName.Text = Path.GetFileName(paths[count]);
                    //Mp3Image();
                }
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;

            if (fileDialog.ShowDialog() == true)
            {
                paths.Clear();
                foreach (var file in fileDialog.FileNames)
                {
                    paths.Add(file);
                }
                mediaPlayer.Source = new Uri(fileDialog.FileName);
                txtFileName.Text = Path.GetFileName(fileDialog.FileName);
                if (Path.GetExtension(fileDialog.FileName) == ".mp3")
                {
                    Mp3Image();
                    mediaPlayer.Visibility = Visibility.Hidden;
                    imgDefault.Visibility = Visibility.Visible;
                    this.Height = 90;
                    this.Width = 340;
                }
                else
                {
                    MainGrid.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 34, 34));
                    BackGroundImage.Source = null;
                    mediaPlayer.Visibility = Visibility.Visible;
                    imgDefault.Visibility = Visibility.Hidden;
                    this.Height = 380;
                    this.Width = 380;
                }
                mediaPlayer.Play();
                isPlaying = true;
            }
            
        }

        private void Exit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void sliDuration_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(sliDuration.Value);
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (isPlaying)
            {
                mediaPlayer.Pause();
                isPlaying = false;
            }
            else
            {
                mediaPlayer.Play();
                isPlaying = true;
            }
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (mediaPlayer.Source != null)
            {
                mediaPlayer.Play();
                isPlaying = true;
            }
            if (mediaPlayer.Position.TotalSeconds == mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds)
            {
                mediaPlayer.Stop();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            SwitchWindowState();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.MouseDevice.Captured == null)
                this.DragMove();

            if (e.ClickCount == 2)
                SwitchWindowState();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Stop();
            isPlaying = false;
        }

        private void Mute_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Mute_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (sliVolume.Value > 0)
            {
                sliVolume.Value = 0;
                MuteButton.Content = FindResource("Muted");
            }
            else
            {
                sliVolume.Value = 0.5;
                MuteButton.Content = FindResource("Unmuted");
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            count++;
            if (count >= paths.Count)
                count = 0;

            mediaPlayer.Source = new Uri(paths[count]);
            txtFileName.Text = Path.GetFileName(paths[count]);
            Mp3Image();
        }

        private void SwitchWindowState()
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    {
                        WindowState = WindowState.Maximized;
                        break;
                    }
                case WindowState.Maximized:
                    {
                        WindowState = WindowState.Normal;
                        break;
                    }
            }
        }

        private void Mp3Image()
        {
            TagLib.File file = TagLib.File.Create(paths[count]);//Path to audio file

            if (file.Tag.Pictures.Length == 0)
            {
                imgDefault.Source = null;
                MainGrid.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 34, 34));
                BackGroundImage.Source = null;
            }
            else
            {
                TagLib.IPicture pic = file.Tag.Pictures[0];//this contains data for image

                MemoryStream ms = new MemoryStream(pic.Data.Data);//create a memory stream

                var bmp = BitmapFrame.Create(ms);
                imgDefault.Source = bmp;

                BackGroundImage.Source = bmp;
                BackGroundImage.Effect = new BlurEffect() { Radius = 10 };

            }
        }
    }
}