   M S B A M L   `   `   `  ����P  LToolTestIPCamera_Ver1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ToolTestIPCamera_Ver1ToolTestIPCamera_Ver1  5   	   ,    &ToolTestIPCamera_Ver1.SpeakerMicWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:ToolTestIPCamera_Ver1     5   	   -   5          � Title$  SpeakerMicWindow��5   	   $	��450��6"   $	��800��6/      customWindow#��[  6;    � WindowStartupLocation$ CenterScreen=�6a    � Topmost .  � ShowActivated .   � ShowInTaskbar .  .��5
      �� -   5         ��Name$ 
MainBorder��5
       �� CornerRadius$ 5��6!     colorBackground#��[ 62     colorBorder#��[ 6`   $��2,2,2,2q�6�   ._�5   
   �  � RowDefinitions 5      �� 5      $��70 �6    �� 5      $��* �6    �� 5      $	��100 �6    .+�5      �� $��0��6   ���#B94A0E$ 5,5,0,0��67   ._�5      � .+�5      �� -   5         ��Name$ lblTitle��5      $��Center=�6.   $
��Left=�6P   $��25(�6k   $��SemiBold"�6y   $4��.KIỂM TRA SPEAKER, MICROPHONE CỦA IP CAMERA��6�   
�������6�   �� 5      -   5        $��Right=�5      $��40(�6T   $��50��6b   $
��Bold"�6m   $��0,-5,5,0q�6     colorTextTitle#��[ 6�   	 ^� ToolTip$	 Tắt ứng dụng��6�   $��X��6�   6�   5      5      �� 5      $��1��6   .+�5      �� 
 �� Padding$
 5,5,5,5q�6   $��70(�6.   $
��Bold"�6�   ��6<   ��  �� Path$ TITLE� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�   � 5      .+�5      �� $
 5,5,5,5q�6    $��Center=�62   $
��Left=�6M   $��70(�6h   $
��Wrap=�6v   $��Time:��6�   6�   �� 5      -   5         ��Name$ tbTime��5   !   $
 5,5,5,5q�61   $��Center=�6C   $
��Left=�6^   $��70(�6y   $
��Wrap=�6�   ��6�   �� $
 TIME�$ PropertyChanged=�$ TwoWay=�6�   �� 5      $
 5,5,5,5q�6    $��Center=�62   $
��Left=�6M   $��70(�6h   $
��Wrap=�6v   $��s��6�   6�   5      5!      �� 5#      $��2��6   $��5,5,5,5q�6&   .+�5%      � .+�5&      �� -   5        $��80��5&   �   $	��150��6�   $��30(�6�   $
��Left=�6�   $
��FAIL��6�   
���  ��{�62   �� $ FAILVISIBLE�$ PropertyChanged=�$ TwoWay=�6�   � 5'      $��Center=�6    $��Right=�6;   .+�5(      �� -   5        $��80��5(   �   $	��150��6�   $��30(�6�   $��RETRY��6�   ���#ffe738{�66   �� $ RETRYVISIBLE�$ PropertyChanged=�$ TwoWay=�6�   �� 5)      -   5        $��20,0,0,0q�5)   �   $��80��6�   $	��150��6�   $��30(�6�   $
��PASS��6�   
��� � �{�66   �� $ PASSVISIBLE�$ PropertyChanged=�$ TwoWay=�6�   5*      5+      5,      5/      50      52      