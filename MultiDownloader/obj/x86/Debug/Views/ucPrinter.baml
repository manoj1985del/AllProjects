   M S B A M L   `   `   `  ����J  FMultiDownloader, Version=1.0.0.2, Culture=neutral, PublicKeyToken=nullG(clr-namespace:MultiDownloader.ConvertersMultiDownloader.Converters  5      %    MultiDownloader.Views.ucPrinter   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9conv(clr-namespace:MultiDownloader.Converters     5      $	��250��5   7   $	��200��6C   .��5
      �� $��3q�6     �� CornerRadius$  5��6!   
���   �$��5q�6F   ._�5      �  � 	Resources 5   
      OrientationStyle �� 
TargetType �� Property �� Value/   )MultiDownloader.Converters.BooleanToColor  boolToColor��%�   &	        &	 r     �� 5      " ��6-   .�5      ��  � ��$
 Left=�68   �� 5       � ��$ Center=�66   5        5      6&   5       � RowDefinitions 5      �� 5      $
��0.2* �6    �� 5      $
��0.8* �6    .+�5      �� $��0��6   
���   �$  5��65   ._�5      �� 
�������$
��Bold"�6L   $��12(�6^   $��Center=�6l   $��Center=�6�   ��6   �� *	IPAddress+6�   5      � 5      $��1��6    5      �� 5       �� 5!      �� 5"      �� 5$      �� 5%       � ColumnDefinitions 5(   
   �� 5)      $
��0.5* �6   �� 5*      $��96 �6   �� 5+      $
��0.5* �6#   .+�5/   
   �� $��0��6   $��0��6!   $
��Ping��61   $��3,0,0,0q�6=   #��[  6N   �� 50   
   $��1��6   $��0��6!   $��
Connection��61   $��3,0,0,0q�6C   #��[  6T   �� 51      $��2��6   $��0��6%   $��Print Status��65   $��3,0,0,0q�6I   #��[  6Z   �� 52   
   $��3��6   $��0��6!   $��
Active Msg��61   $��3,0,0,0q�6D   #��[  6U   �� 53   
   $��4��6   $��0��6!   $��Status��61   $��3,0,0,0q�6@   #��[  6Q   A� 55      $��0��6   $��1��6#   $��10��63   $��10��6>   ��6J   �� *
PingStatus+ �� 	Converter# [ A� 56      $��1��6   $��1��6#   $��10��63   $��10��6>   ��6J   �� *IsConnected+# [ A� 57      $��2��6   $��1��6#   $��10��63   $��10��6>   ��6J   �� *PrintStatus+# [ �� 59      $��3��6   $��1��6%   #��[  6U   ��65   �� *ActiveMessage+6U   �� 5:      $��4��6   $��1��6%   $��3,0,0,0q�6N   #��[  6_   ��65   �� *Status+6_   �� 5<      $��1��6   $��2��6    $��Download��60   $��2q�6C   ��6N   �� *DownloadFileCommand+�� 5=      $��2��6   $��2��6"   $��Setting��62   $��2q�6D   ��6O   �� *SettingsCommand+5>      5?      5@      5A      