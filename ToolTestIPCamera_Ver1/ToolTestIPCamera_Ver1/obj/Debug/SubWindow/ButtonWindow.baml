   M S B A M L   `   `   `  ����P  LToolTestIPCamera_Ver1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ToolTestIPCamera_Ver1ToolTestIPCamera_Ver1  5   	   (    "ToolTestIPCamera_Ver1.ButtonWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:ToolTestIPCamera_Ver1     5   	   -   5          � Title$  ButtonWindow��5   	   $	��450��6   $	��800��6+      customWindow#��[  67    � WindowStartupLocation$ CenterScreen=�6]    � Topmost .  � ShowActivated .   � ShowInTaskbar .  .��5      �� -   5         ��Name$ 
MainBorder��5       �� CornerRadius$ 5��6!   ���#ffe738  colorBorder#��[ 6G   $��2,2,2,2q�6s   ._�5   
   �  � RowDefinitions 5      �� 5      $��70 �6    �� 5      $��* �6    �� 5      $	��100 �6    .+�5      �� $��0��6   ���#B94A0E$ 5,5,0,0��67   ._�5      � .+�5      �� -   5         ��Name$ lblTitle��5      $��Center=�6.   $
��Left=�6P   $��25(�6k   $��SemiBold"�6y   $&�� KIỂM TRA NÚT NHẤN IP CAMERA��6�   
�������6�   �� 5      -   5        $��Right=�5      $��40(�6T   $��50��6b   $
��Bold"�6m   $��0,-5,5,0q�6     colorTextTitle#��[ 6�   	 ^� ToolTip$	 Tắt ứng dụng��6�   $��X��6�   6�   5      5      �� 5      $��1��6   .+�5      �� 
 �� Padding$
 5,5,5,5q�6   $��70(�6.   $��Nhấn nút trên Camera:��6<   $
��Bold"�6Y   6m   � 5      .+�5      �� $
 5,5,5,5q�6    $��Center=�62   $
��Left=�6M   $��70(�6h   $
��Wrap=�6v   $��Timeout:��6�   6�   �� 5      -   5         ��Name$ 	tbTimeOut��5   !   $
 5,5,5,5q�64   $��Center=�6F   $
��Left=�6a   $��70(�6|   $
��Wrap=�6�   $�� ��6�   6�   �� 5      $
 5,5,5,5q�6    $��Center=�62   $
��Left=�6M   $��70(�6h   $
��Wrap=�6v   $��s��6�   6�   5      5!      �� 5"      $��2��6   .+�5#      �� $��22(�6   $��Thông tin chi tiết:��6&   $
��Bold"�6D   6X   �� 5$      $��17(�6   $
��Wrap=�6+   $����Nếu vượt quá thời gian timeout mà phần mềm không nhận được log của nút nhấn thì phần mềm sẽ báo FAIL.��6?   5%      5'      5(      5+      