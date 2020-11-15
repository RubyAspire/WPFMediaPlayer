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
using System.Windows.Media.Effects;
using System.Windows.Automation;
using System.Collections.ObjectModel;

namespace AudioVideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isPlaying = false;
        int count = 0;
        public static ObservableCollection<Files> paths = new ObservableCollection<Files>();
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
                    /*Will be for Repeat Functionality*/
                    //sliDuration.Value = 0;
                    //mediaPlayer.Stop();
                    //mediaPlayer.Play();
                    if (count >= paths.Count)
                        count = 0;
                    mediaPlayer.Source = new Uri(paths[count].FilePath);
                    Mp3Image();
                }
                txtFileName.Text = paths[count].Filename;
               
                if (sliVolume.Value <= 0)
                    MuteButton.Content = FindResource("Muted");
                else
                    MuteButton.Content = FindResource("Unmuted");

            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(isPlaying || !isPlaying)
                e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;

            if (fileDialog.ShowDialog() == true)
            {
                count = 0;
                paths.Clear();
                foreach (var file in fileDialog.FileNames)
                {
                    var path = new Files()
                    {
                        Filename = Path.GetFileName(file),
                        FilePath = file
                    };
                    paths.Add(path);
                    
                }
                mediaPlayer.Source = new Uri(fileDialog.FileName);
                txtFileName.Text = Path.GetFileName(fileDialog.FileName);
                if (Path.GetExtension(fileDialog.FileName) == ".mp3")
                {
                    Mp3Image();
                    mediaPlayer.Visibility = Visibility.Hidden;
                    imgDefault.Visibility = Visibility.Visible;
                }
                else
                {
                    mediaPlayer.Visibility = Visibility.Visible;
                    imgDefault.Visibility = Visibility.Hidden;
                }
                mediaPlayer.Play();
                isPlaying = true;
                lstPlaylist.ItemsSource = paths;
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

        private void SliDuration_DragCompleted(object sender, DragCompletedEventArgs e)
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
            if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                mediaPlayer.Play();
                isPlaying = true;

                if (mediaPlayer.Position.TotalSeconds == mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds)
                {
                    mediaPlayer.Stop();
                }
            }
            
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
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
                sliVolume.Value = 0;
            else
                sliVolume.Value = 0.5;
        }

        private void BtnSeeking_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Name == "BtnNext")
            {
                count++;
                if (count >= paths.Count)
                    count = 0;

                mediaPlayer.Source = new Uri(paths[count].FilePath);
                txtFileName.Text = paths[count].Filename;
                Mp3Image();
            }
            else if (button.Name == "BtnPrevious")
            {
                count--;
                if (count < 0)
                    count = paths.Count - 1;

                mediaPlayer.Source = new Uri(paths[count].FilePath);
                txtFileName.Text = paths[count].Filename;
                Mp3Image();
            }
            
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
            TagLib.File file = TagLib.File.Create(paths[count].FilePath);//Path to audio file

            if (file.Tag.Pictures.Length == 0)
            {
                imgDefault.Source = null;
            }
            else
            {
                TagLib.IPicture pic = file.Tag.Pictures[0];//this contains data for image

                MemoryStream ms = new MemoryStream(pic.Data.Data);//create a memory stream

                var bmp = BitmapFrame.Create(ms);
                imgDefault.Source = bmp;

            }
        }

        private void ViewPlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (mediaGrid.ColumnDefinitions[2].Width == new GridLength(0))
                mediaGrid.ColumnDefinitions[2].Width = new GridLength(150);
            else
                mediaGrid.ColumnDefinitions[2].Width = new GridLength(0);
        }

        private void PlaylistItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                mediaPlayer.Source = new Uri(paths[lstPlaylist.SelectedIndex].FilePath);
                count = lstPlaylist.SelectedIndex;
                Mp3Image();
            }
        }

        private void AddToPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                foreach (var file in fileDialog.FileNames)
                {
                    var path = new Files()
                    {
                        Filename = Path.GetFileName(file),
                        FilePath = file
                    };
                    paths.Add(path);
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    sliVolume.Value += 0.1;
                    break;
                case Key.Down:
                    sliVolume.Value += -0.1;
                    break;
                case Key.Right:
                    sliDuration.Value += 5;
                    mediaPlayer.Position = TimeSpan.FromSeconds(sliDuration.Value);
                    break;
                case Key.Left:
                    sliDuration.Value -= 5;
                    mediaPlayer.Position = TimeSpan.FromSeconds(sliDuration.Value);
                    break;
                case Key.M:
                    if (sliVolume.Value > 0)
                        sliVolume.Value = 0;
                    else
                        sliVolume.Value = 0.5;
                    break;
                case Key.Enter:
                    SwitchWindowState();
                    break;
            }
        }
    }
}