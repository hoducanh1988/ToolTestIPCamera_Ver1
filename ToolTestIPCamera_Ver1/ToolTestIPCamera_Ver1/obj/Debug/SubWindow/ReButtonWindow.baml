   M S B A M L   `   `   `  ����P  LToolTestIPCamera_Ver1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ToolTestIPCamera_Ver1ToolTestIPCamera_Ver1  5   	   *    $ToolTestIPCamera_Ver1.ReButtonWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:ToolTestIPCamera_Ver1     5   	   -   5          � Title$  ReButtonWindow��5   	   $	��450��6    $	��800��6-      customWindow#��[  69    � WindowStartupLocation$ CenterScreen=�6_    � Topmost .  � ShowActivated .   � ShowInTaskbar .  .��5      �� -   5         ��Name$ 
MainBorder��5       �� CornerRadius$ 5��6!     colorBackground#��[ 62     colorBorder#��[ 6`   $��2,2,2,2q�6�   ._�5   
   �  � RowDefinitions 5      �� 5      $��70 �6    �� 5      $��* �6    .+�5      �� $��0��6   ���#B94A0E$ 5,5,0,0��67   ._�5      � .+�5      �� -   5         ��Name$ lblTitle��5      $��Center=�6.   $
��Left=�6P   $��25(�6k   $��SemiBold"�6y   $,��&KIỂM TRA NÚT NHẤN CỦA IP CAMERA��6�   
�������6�   �� 5      -   5        $��Right=�5      $��40(�6T   $��50��6b   $
��Bold"�6m   $��0,-5,5,0q�6     colorTextTitle#��[ 6�   	 ^� ToolTip$	 Tắt ứng dụng��6�   $��X��6�   6�   5      5      � 5      $��1��6   
 � ColumnDefinitions
 5      �� 5      $	��450 �6'   �� 5      $��* �6'   .+�5      � $��0��6    5       �� 5!      $��* �6(   �� 5"      $	��100 �6(   .+�5&      �� $��0��6!   $��5,10,0,0q�6.   .+�5(      �� $)��#KẾT QUẢ KIỂM TRA NÚT NHẤN:��6    $��25(�6E   $
��Bold"�6S   
��� � �6e   �� 5)      $��2��6!   $	��400��6,   $
��Left=�68   
��� � ���5*      �� 5+   "    �� 
TargetType" ��6(    �� Triggers 5,   &   �� 5-   *    �� Binding 66   ��  �� ElementName$ bdGreen�� �� Path$ 
Background� �� Value Lime6o   .a�5.   .   ��  �� Property � �� �� Value
 � � �6K   5/   +   �� 50   *    66   �� $ bdGreen��$ 
Background� Red6o   .a�51   .   ��  � ��
 �  ��6K   52   +   54   #   56      � 57      $��0,20,0,0q�6   .+�58      �� -   5        $ bdResult��58   %   $
��Left=�6U   $	��100��6p   $	��100��6|   $ 50��6�   
���   �$��2,5,5,2q�6�   
��� � �6�   �� 59      $
��Left=�6$   $��	110,0,0,0q�6?   $��70(�6R   
���   �$��SemiBold"�6s   ��5:   "   �� 5;   &   " ��6,    5<   *   �� 5=   .    6:   �� $ bdResult��$ 
Background� Lime6t   .a�5>   2   ��  � ��$
 PASS��6L   5?   /   �� 5@   .    6:   �� $ bdResult��$ 
Background� Red6t   .a�5A   2   ��  � ��$
 FAIL��6L   5B   /   5D   '   5F      5G      � 5I      $��0,30,0,0q�6$   .+�5J      ��  �� Padding$ 5,5,5,5q�6(   $��Center=�6:   $
��Left=�6U   $��40(�6p   $
��Wrap=�6~   $��Timeout:��6�   6�   �� 5K      -   5         ��Name$ 	tbTimeOut��5K   )   $ 5,5,5,5q�6<   $��Center=�6N   $
��Left=�6i   $��40(�6�   $
��Wrap=�6�   $��30��6�   6�   �� 5L      $ 5,5,5,5q�6(   $��Center=�6:   $
��Left=�6U   $��40(�6p   $
��Wrap=�6~   $��s��6�   6�   5M      5O      �� 5R      $��1��6!   $��	10,20,0,0q�6.   .+�5S      �� -   5        $��OK��5S   6   $	��220��6C   $��60��6O   #��[ 6[   
�������$��40(�6�   $
��Left=�6�   5T      5V      �� 5Y      $��1��6   .+�5Z      �� $
��Wrap=�6    $��20(�64   
���   �$ 5,5,5,5q�6U   $j��dNhấn và giữ nút nhấn trong khoảng 5s. Nhả nút nhấn và quan sát đèn trạng thái:��6g   �� 5[      $��0,20,0,0q�6    $
��Wrap=�62   $��20(�6F   
���   �$ 5,5,5,5q�6g   $2��,- Nhấp nháy màu đỏ : Nút nhấn PASS��6y   $��Italic$�6�   �� 5\      $��0,5,0,0q�6    $
��Wrap=�61   $��20(�6E   
���   �$ 5,0,5,5q�6f   $6��0- Các trường hợp khác : Nút nhấn FAIL.��6x   $��Italic$�6�   �� 5]      $��0,20,0,0q�6    $
��Wrap=�62   $��20(�6F   
���   �$ 5,0,5,5q�6g   $'��!Click nút OK để hoàn thành.��6y   $
��Bold"�6�   5^      5`      5a      5b      5e      