   M S B A M L   `   `   `  ����J  FMultiDownloader, Version=1.0.0.2, Culture=neutral, PublicKeyToken=null=#clr-namespace:MultiDownloader.ViewsMultiDownloader.Views  5   	        MultiDownloader.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   2vw#clr-namespace:MultiDownloader.Views     5   	   $	��350��5   "   $	��525��6/     � WindowState$  	Maximized=�6;    � Title 6	   �� *Title+.��5      �  � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5	      $
��0.7* �6   �� 5
      $
��0.1* �6   .+�5   
   �� $��0��6   
���   � �� CornerRadius$ 10��61   $��3q�6C   ._�5      �� $��Center=�6   $��Center=�66   .+�5      �� $+��%Multiple Printer Downloader - JP NANO��6   
�������$��Center=�6X   $��Center=�6u   $
��Bold"�5      $��18(�6*   �� 5      
�������$��Center=�6I   $��Center=�6f   ��6   �� *	Version+6f   5      5      � 5   
   $��1��6   $��5q�6    � ColumnDefinitions 5      �� 5      $
��0.2* �6#   �� 5      $
��0.8* �6#   .+�5      �� $��0��6   .+�5      �� $��Connect��6   ��6,   �� *ConnectCommand+�� 5      $��
Disconnect��6   ��6/   �� *DisconnectCommand+�� 5      $��Download��6   ��6,   �� *DownloadFileCommand+�� 5      $��Start Print��6   ��6/   �� *StartPrintCommand+�� 5       $��
Stop Print��6   ��6.   �� *StopPrintCommand+�� 5!      $��Add/Remove Printer��6   ��66   �� *AddRemovePrinterCommand+5"      �� 5%      $��1��6   
���   �$��3q�69   $ 10��6M   $��5q�6_   ._�5&      �� $��1��6   $��1��6,   $��5q�6`   ��6<   �� *ControlList+��5'      �� 5(      .R�5)      � 5*      ��5-      �� 5.      .R�5/      %   MultiDownloader.Views.ucPrinter  50      53      54      55      � 57   
   $��2��6    58      �� 59      $
��0.7* �6#   �� 5:      $
��0.3* �6#   .+�5=      � $��0��6   $��0��6#    5>      �� 5?      �� 5@      .+�5C      �� $��0��6    �� Orientation$ 
Horizontal=�6*   .+�5D      �� $��File To Download:   ��6    �� 5E      ��6    �� *FileToDownload+5F      5I      �� 5J      $��1��6   $��..\Resources\Cyklop_1.png��6$   5K      5N      5O      