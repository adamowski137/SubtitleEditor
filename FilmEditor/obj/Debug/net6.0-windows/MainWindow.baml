   M S B A M L   `   `   `  ����E  AFilmEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:FilmEditor
FilmEditor  5   	       FilmEditor.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:FilmEditor     5   	     � Title$  Subtitle Composer��5   	   $	��800��6#   $
��1280��60   $	��480��5	   	   $	��640��6    � WindowStartupLocation$ CenterScreen=�6(    � 	Resources 5
      "   FilmEditor.TimeSpanConverter   	converter*   $FilmEditor.TimeSpanToDoubleConverter  timespanToDouble"  FilmEditor.DurationConverter  durationConverter  	playImage �� 	UriSource  
pauseImage  	stopImage��%�   &	        &	      &	 &     &	 9     &	 f     &	 �       5   
   6"     5   
   6*     5   
   6"   �� 5   
   $ /Resources/play.png?�6)   �� 5   
   $ /Resources/pause.png?�6*   �� 5   
   $ /Resources/stop.png?�6)   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $	��50* �6   �� 5      $��5 �6   �� 5      $	��50* �6   �� 5      $��5 �6   �� 5      $	��100 �6   .+�5   
   o� $��0��6   .@�5      m� $
��File��6   .@�5      m� -   5        $��OpenFilm��5   )   $
��Open��6   69   m� 5      -   5        $
��Exit��5   )   $
��Exit��6   65   5      m� 5       $��	Subtitles��6   .@�5!      m� -   5        $
��Open��5!   )   $
��Open��6   6)   m� 5"      -   5        $
��Save��5"   )   $
��Save��6   6)   m� 5#      -   5        $��Save_Translation��5#   5   $��Save Translation��6   ${�False��6M   m� 5$      -   5        $��Translation��5$   0   $��Translation��6    m� IsCheckable$
 True��6C   6V   5%      m� 5&      $
��Help��6   .@�5'      m� -   5        $��About��5'   *   $��About��6   68   5(      5)      � 5*   
   $��1��6    5+      �� 5,      $��* �6    �� 5-      $��30 �6    .+�5/      � $��0��6   
���   �.+�50      r� -   5        $��MediaPlayer��50      $y�Visible=�62    r� LoadedBehavior$ Manual=�52       r� UnloadedBehavior$ Manual=�6S    r� Stretch$ Uniform=�6m   6   �� 53      -	   5        $��SubtitleTextBlock��53      $��Center=�65   $��Bottom=�6R   
�������$
��auto��6�   	 �� Padding$	 5q�6�   $��24(�6�   ��54      �� 55      
 �� Color$
 Black��6*    �� Opacity$	 0.5Q�68   57      58      �� 59      $��1��6   ��5:      �� 5;      .R�5<      �  � ColumnDefinitions 5=      �� 5>   "   $��30 �63   �� 5?   "   $��30 �63   �� 5@   "   $��30 �63   �� 5A   "   $
��auto �63   �� 5B   "   $��* �63   �� 5C   "   $��50 �63   5E      5F      .@�5H      �� $��0��6    .��5I      �� -
   5        $��Start��5I   9   $��Center=�6   .��5J      �� #��[ 6    5K      5L      �� 5M      $��1��6    .��5N      �� -   5        $��Pause��5N   8   $��Center=�6   .��5O      �� #��[ 6    5P      5Q      �� 5R      $��2��6    .��5S      �� -   5        $
��Stop��5S   :   $��Center=�6   .��5T      �� #��[ 6    5U      5V      �� 5W      $��3��6    .��5X      �� -   5        $��CurrentTime��5X   !   $��00:00:00.00��64   5Y      �� 5Z      $��4��6    $��Stretch=�60   .��5[      �� -   5        $��Slider��5\      $��Center=�5[       � Minimum$ 0Q�69   $��Stretch=�6E   5\   }   5]      �� 5^      $��5��6    $��Stretch=�60   .��5_      � -   5        $��	VolumeBar��5_   "   $��Center=�63   $��20��6N    � Maximum$ 1Q�6Z   $ 0Q�6f   6r   5`      5a      5b      �� 5c   
   $��2��6   $��Stretch=�6$   &   System.Windows.Controls.DataGrid  5d   
   -   5        $��SubtitleDataTable��5d       $��3��6      CanUserSortColumns$ False��69      CanUserAddRows$
 True��5e   C      AutoGenerateColumns$ False��5f       � ContextMenu 5g      �� 5h      .@�5i      m� -   5        $��ContextMenuAdd��5i   ,   $	��Add��6   6B   m� 5j      -   5        $��ContextMenuAddAfter��5j   2   $��	Add After��6   6M   m� 5k      -   5        $��ContextMenuDelete��5k   /   $��Delete��6   6H   5l         Columns 5n      0  *System.Windows.Controls.DataGridTextColumn  5o      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Show Time��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 68   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   ��  �� 	Converter# [   �� Path$ ShowTime� �� UpdateSourceTrigger$ PropertyChanged=�  5p      $ 	Hide Time��6%   $
 auto 68    6E   �� # [  $ HideTime�$ PropertyChanged=�  5q      $
 Text��6%   $	 50* 63    6?   �� $
 Text�$ PropertyChanged=�  5r      $ Translation��6%   $	 50* 6:      
Visibility$ 	Collapsed=�6�    6F   �� $ Translation�$ PropertyChanged=�6�   5t      �� 5u   
   $��4��6   $��Stretch=�6$   � 5v   
   $��5��6    5w      �� 5x      $	��200 �6#   �� 5y      $	��80* �6#   .+�5{      �� $��0��6   .��5|      �  5}      �� 5~      $
��auto �6+   �� 5       5�      �� 5�      �� 5�      �� 5�      .+�5�      �� $��0��6   $��0��6)   .��69   Show6?   �� 5�      $��1��6   $��0��6)   .��69   Hide6?   �� 5�      $��2��6   $��0��6)   .��69   
Duration6C   �� 5�      $��0��6   $��1��6+   $��7q�6;   ��6F   ��  �� ElementName$ SubtitleDataTable��$ SelectedItem.ShowTime� �� Mode$ TwoWay=�$ PropertyChanged=�# [  �� 5�      $��1��6   $��1��6+   $��7q�6;   ��6F   �� $ SubtitleDataTable��$ SelectedItem.HideTime�$ PropertyChanged=�$ TwoWay=�# [  �� 5�      $��2��6   $��1��6+   $��7q�6;   .��6G   ��5�      `� 5�       `� 	Converter# [ 6+    `� UpdateSourceTrigger$ PropertyChanged=�6Z   .1�5�   "   �� $ SubtitleDataTable��6*   $ SelectedItem.ShowTime�6J   $ TwoWay=�6g   �� 5�   "   $ SubtitleDataTable��6*   $ SelectedItem.HideTime�6J   $ TwoWay=�6g   5�      .��5�   (   5�      5�      5�      � 5�      $��1��6    5�      �� 5�      -   5         �Name$ FirstColumn��5�   '   $��* �6:   �� 5�      -   5        $ SecondColumn��5�   '   $��0 �6;   .+�5�      �� -   5        $��TextGroupBox��5�      $��0��6/     �� HeaderStringFormat$  Text: {0} characters��6�   ��6?   �� $ SubtitleDataTable��$ SelectedItem.Text.Length�$ PropertyChanged=�.��5�      �� ! �� TextAlignment$! Center=�6�   $
��auto��6�   " �� TextWrapping$
" Wrap=�6�   "# �� VerticalScrollBarVisibility$
# Auto=�6�   ��6   �� $ SubtitleDataTable��$ SelectedItem.Text�$ PropertyChanged=�$ TwoWay=�6�   5�      �� 5�      -   5        $��TranslationGroupBox��5�      $��1��66   $!  Translation: {0} characters��6�   $y�	Collapsed=�6�   ��6F   �� $ SubtitleDataTable��$% SelectedItem.Translation.Length�.��5�      �� $! Center=�6�   $
��auto��6�   $
" Wrap=�6�   $
# Auto=�6�   ��6   �� $ SubtitleDataTable��$ SelectedItem.Translation�$ PropertyChanged=�$ TwoWay=�6�   5�      5�      5�      5�      5�      