   M S B A M L   `   `   `  ����P  LToolTestIPCamera_Ver1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ToolTestIPCamera_Ver1ToolTestIPCamera_Ver1  5   	   -    'ToolTestIPCamera_Ver1.LightSensorWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:ToolTestIPCamera_Ver1     5   	   -   5          � Title$  LightSensorWindow��5   	   $	��450��6#   $	��800��60      customWindow#��[  6<    � WindowStartupLocation$ CenterScreen=�6b    � Topmost .  � ShowActivated .   � ShowInTaskbar .  .��5      �� -   5         ��Name$ 
MainBorder��5       �� CornerRadius$ 5��6!   ���#ffe738  colorBorder#��[ 6G   $��2,2,2,2q�6s   ._�5   
   �  � RowDefinitions 5      �� 5      $��70 �6    �� 5      $��* �6    .+�5      �� $��0��6   ���#B94A0E$ 5,5,0,0��67   ._�5      � .+�5      �� -   5         ��Name$ lblTitle��5      $��Center=�6.   $
��Left=�6P   $��25(�6k   $��SemiBold"�6y   $2��,KIỂM TRA CẢM BIẾN ÁNH SÁNG IP CAMERA��6�   
�������6�   �� 5      -   5        $��Right=�5      $��40(�6T   $��50��6b   $
��Bold"�6m   $��0,-5,5,0q�6     colorTextTitle#��[ 6�   	 ^� ToolTip$	 Tắt ứng dụng��6�   $��X��6�   6�   5      5      �� 5      $��1��6   .+�5      �� 
 �� Padding$
 5,5,5,5q�6   $��70(�6.   $
��Bold"�6�   ��6<   ��  �� Path$ 	STEPTITLE� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�   �� 5      $
 5,5,5,5q�6   $��Center=�6.   $
��Left=�6I   $��50(�6d   $
��Wrap=�6r   $��SemiBold"�6�   
���  ����6�   �� $ 
STEPLEGEND�$ PropertyChanged=�$ TwoWay=�6  � 5      .+�5      �� $
 5,5,5,5q�6    $��Center=�62   $
��Left=�6M   $��35(�6h   $
��Wrap=�6v   $��Kết quả ADC Value = ��6�   6�   �� 5      $
 5,5,5,5q�6    $��Center=�62   $
��Left=�6M   $��35(�6h   $
��Wrap=�6v   ��6�   �� $ ADCVALUE�$ PropertyChanged=�$ TwoWay=�6�   5       � 5"      $��0,20,0,0q�6   .+�5#      �� $
 5,5,5,5q�6    $��Center=�62   $
��Left=�6M   $��70(�6h   $
��Wrap=�6v   $��Timeout:��6�   6�   �� 5$      -   5         ��Name$ 	tbTimeOut��5$   !   $
 5,5,5,5q�64   $��Center=�6F   $
��Left=�6a   $��70(�6|   $
��Wrap=�6�   ��6�   �� $ TIMEOUT�$ PropertyChanged=�$ TwoWay=�6�   �� 5%      $
 5,5,5,5q�6    $��Center=�62   $
��Left=�6M   $��70(�6h   $
��Wrap=�6v   $��s��6�   6�   5&      5)      5*      5+      5-      