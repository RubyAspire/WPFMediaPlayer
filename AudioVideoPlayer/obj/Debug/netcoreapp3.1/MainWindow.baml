   M S B A M L   `   `   `  ����K  GAudioVideoPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:AudioVideoPlayerAudioVideoPlayer  5   	   E'clr-namespace:AudioVideoPlayer.CommandsAudioVideoPlayer.Commands  5   	   !    AudioVideoPlayer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:AudioVideoPlayer     5   	   8coms'clr-namespace:AudioVideoPlayer.Commands     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5       � WindowStyle$
 None=�5	   	    � AllowsTransparency$
 True��6   
������ $	��400��5
   	   $	��400��6    � Title$ Media Player��5   	   $	��580��6   $	��485��6*    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ coms:CustomCommands.Open��5      6Y     5   
   -   5        $ coms:CustomCommands.Exit��5      6Y     5   
   -   5        $ coms:CustomCommands.Play��5      6Y     5   
   -   5        $ coms:CustomCommands.Pause��5      6[     5   
   -   5        $ coms:CustomCommands.Stop��5      6Y     5   
   -   5        $ coms:CustomCommands.Mute��5      6Y    � 	Resources 5         Unmuted 	 Muted��%�   &	        &	 H     �� 5   
   $��/Images/volume.png��6    $��13��6<   $��13��6H   �� 5   
   $%��/Images/mute-volume-control.png��6   $��13��6H   $��13��6T   5      '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      ResizeBorderThickness$ 5q�6   	   CaptionHeight$	 0Q�5      
   GlassFrameThickness$
 0q�5      .��5       �� 
���   �$��3q�6!   ._�5!   
   � -   5         �Name$ MainGrid��5!   !   ���#222 � RowDefinitions 5"      �� 5#      $
��Auto �6    �� 5$      $
��Auto �6    �� 5%      $��* �6    �� 5&      $
��Auto �6    �� 5'      $
��Auto �6    .+�5*      � -	   5        $ TitleBar��5*       � ColumnDefinitions 5+      �� 5,      $
��Auto �6'   �� 5-      $��* �6'   �� 5.      $
��Auto �6'   .+�51      �� $��Cactus Player��6   
������� �� Padding$ 5q�52      $��15(�6(   �� 54      -
   5         ��Name$ txtFileName��54      ���#999$��1��6C   $��15(�55      $ 5q�6*   $��1��66   � 57      $��2��6   $��Right=�6,   .+�58      �� -   5         ��Name$ BtnMinimize��58      $��__��59      6*   �� 5;      -   5        $ btnMaximize��5;      $
��Bold"�62   .��5=      �� $&�� /Images/maximize-size-option.png��6    $��12��6J   $��0 3 0 0q�6U   5>      �� 5?      $��coms:CustomCommands.Exit��6   $	�� X ��5@      $��15(�6,   5A      5B      � 5D      ���#333$��1��6%    5E      �� 5F      $
��Auto �6'   �� 5G      $��* �6'   �� 5H      $��* �6'   .+�5K      o� ���#333
������� � Cursor$
 Hand��5L      $��13(�6!   .@�5M      m� $
��File��6   $��4q�6-   .@�5N      m�  m� Command$ coms:CustomCommands.Open��6#   
���   �$��13(�5O   .   m� 5P      $ coms:CustomCommands.Exit��6#   
���   �$��13(�5Q   .   5R      m� 5S      $
��View��6   .@�5T      m� -   5        $��View Playlist��5T   #   
���   �5U   #   5V      5W      5Y      � 5[      -   5        $ 	mediaGrid��5[       $��2��6    5\      �� 5]      $��* �6'   �� 5^      $��5 �6'   �� 5_      $��0 �6'   $	��150��61    � ContextMenu 5b      �� 5c      .@�5d      m� -   5        $��View Playlist��5d   #   6:   m� 5e      -   5        $��Add To Playlist��5e   #   6<   5f      .+�5k      r� -   5         r�Name$ mediaPlayer��5k       r� LoadedBehavior$ Manual=�64   �� 5m      -   5         ��Name$ 
imgDefault��5m   ,   $y�Hidden=�6   6,   �� 5n      $��1��6   $��2��6/   ���#333$��Stretch=�5o      �� 5p      -   5        $��lstPlaylist��5p   X   $��2��6   
������ $��0q�6D   
���������5r      �� 5s      .R�5t      �  5u   "   �� 5v   &   $��* �67   �� 5w   &   $
��Auto �67   .+�5y   "   �� -   5        ��5y   ,   �� *
Filename+6F   5z      5|      5~      5      �� 5�      $��3��6   ���#555��5�      �� 5�      .R�5�      �  5�      �� 5�   "   $
��Auto �63   �� 5�   "   $��* �63   �� 5�   "   $
��Auto �63   �� 5�   "   $
��Auto �63   5�      5�      .@�5�      � .+�5�      �� -   5        $ txtTime��5�   +   $��00:00:00��5�       $��16(�60   
�������5�   +   �� 5�      $��/��6    $��16(�6)   
�������5�      �� 5�      -   5        $ txtFullTime��5�   *   $��00:00:00��5�       $��16(�60   ���#8885�   *   5�      �� 5�      $��1��6    $��Stretch=�60   .��5�      �� -   5         ��Name$ sliDuration��5�       �� IsMoveToPointEnabled$
 True��5�      5�      �� 5�      $��2��6    .��5�      �� -   5        $ 
MuteButton��5�      $��coms:CustomCommands.Mute��61   .��5�      C�  C� ResourceKey$ Unmuted��6*   5�      5�      �� 5�      $��3��6    $��Stretch=�60   $��80��6U   .��5�      �� -   5        $ 	sliVolume��5�       � Maximum$ 1Q�5�      $
 True��6)    � Value 5�   0   ��  �� ElementName$ mediaPlayer�� �� Path$ Volume�5�   )   5�      5�      �� 5�      $��4��6    �� Orientation$ 
Horizontal=�6&   ���#333.+�5�      �� -   5        $��BtnPrevious��5�      .��5�      �� $��/Images/Previous.png��6   $��23��6:   5�      �� 5�      $��5q�6   $|�False��6%   $��coms:CustomCommands.Play��5�      .��5�      �� $'��!/Images/play-button-arrowhead.png��6   $��11��5�      5�      �� 5�      $��5q�6   $|�False��6%   $��coms:CustomCommands.Pause��5�      .��5�      �� $��/Images/pause.png��6   $��11��5�      5�      �� 5�      $��5q�6   $|�False��6%   $��coms:CustomCommands.Stop��5�      .��5�      �� $��/Images/stop.png��6   $��11��5�      5�      �� 5�      -   5        $��BtnNext��5�      .��5�      �� $��/Images/Next.png��6   $��20��66   5�      5�      5�      5�      5�      