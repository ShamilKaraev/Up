   M S B A M L   `   `   `  ����D  @pavilions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:pavilions	pavilions  5          pavilions.Admin   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:pavilions     5        N� Title$  Admin��5	      .��5      � ���	#FFFF5656.+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ Employees_Grid��5      $
��Left=�6+   $	��394��6F   $��	10,46,0,0q�6S   $	��Top=�6f   $	��780��6~   
�������   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Photo��6)      CellTemplate 5      �� 5      .R�5      �� ��6$   �� *photo+5      5        5      $ surname_employee��6)    5      �� 5      .R�5      �� ��6(   �� *surname_employee+5      5        5      $ mid_name_employee��6)    5      �� 5       .R�5!      �� ��6(   �� *mid_name_employee+5"      5$        5&      $ phone��6)    5'      �� 5(      .R�5)      �� ��6(   �� *phone+5*      5,        5.      $ gender��6)    5/      �� 50      .R�51      �� ��6(   �� *gender+52      54        56      $ login��6)    57      �� 58      .R�59      �� ��6(   �� *login+5:      5<        5>      $ password��6)    5?      �� 5@      .R�5A      �� ��6(   �� *
password+5B      5D        5F      $
 role��6)    5G      �� 5H      .R�5I      �� ��6(   �� *role+5J      5L      5O      �� 5P   
   -   5        $��Добавть��5P   &   $
��Left=�68   $��
403,10,0,0q�6S   $	��Top=�6g   $	��129��6   $��31��6�   
�������6�   �� 5Q   
   -   5        $ ��Редактировать��5Q   '   $
��Left=�6?   $��
532,10,0,0q�6Z   $	��Top=�6n   $	��129��6�   $��31��6�   
�������6�   �� 5R   
   -   5        $��Удалить��5R   '   $
��Left=�69   $��
661,10,0,0q�6T   $	��Top=�6h   $	��129��6�   $��31��6�   
�������6�   �� 5S   
   -   5         ��Name$ Search_TextBox��5S      $
��Left=�6*   $��27��6E   $��
128,14,0,0q�6Q    �� TextWrapping$
 Wrap=�6e   $�� ��6y   $	��Top=�6�   $	��250��6�   $��16(�6�   �� 5T   
   -   5        $��
Назад��5T      $
��Left=�6.   $��	10,10,0,0q�6I   $	��Top=�6\   $��93��6t   $��31��6   
�������6�   5V      5W      