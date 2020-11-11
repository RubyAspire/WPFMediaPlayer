using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AudioVideoPlayer.Commands
{
    public static class CustomCommands
    {
        //Having the command static allows me to use it in window.xaml
        public static RoutedUICommand Open = new RoutedUICommand
            (
                "Open",//Text of the Command
                "Open",//The name of the command
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.O, ModifierKeys.Control)//Key gesture used to use the command
                }
           );

        public static RoutedUICommand Exit = new RoutedUICommand
            (
                //Text of the Command - meaning when this command is placed on a control the text value of that control will have this string value
                "Exit",
                "Exit",//The name of the command
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)//Key gesture used to use the command
                }
           );

        public static RoutedUICommand Pause = new RoutedUICommand(
                "Pause",
                "Pause",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.Space)
                }
            );

        public static RoutedUICommand Play = new RoutedUICommand(
                "Play",
                "Play",
                typeof(CustomCommands)
            );

        public static RoutedUICommand Stop = new RoutedUICommand(
                "Stop",
                "Stop",
                typeof(CustomCommands)
            );

        public static RoutedUICommand Mute = new RoutedUICommand(
                "Mute",
                "Mute",
                typeof(CustomCommands)
            );
    }
}
