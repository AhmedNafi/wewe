   M S B A M L   `   `   `  ����K  GCorrectionSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:CorrectionSystemCorrectionSystem  5      *    $CorrectionSystem.ucStudentsForMentor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:CorrectionSystem     5      .��5	      � $	��771��6   $
��1240��6     � RowDefinitions  5
   
   �� 5      $
��386* �6   �� 5      $
��385* �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ btnHomeInMentorPNL��5      $��55��6-   $��55��68    � Cursor$
 Hand��6D   $��331,0,234,305q�6R   $��Bottom=�6i   $��1��6�   ��5      �� 5       �� ImageSource$ Home-Orange.png��6   ��5      �� 5      .R�5      � .+�5      A�  A�Name$ Ellipse��6"   #��z��63   5      5      5      �� 5    
   -   5        $&  btnAddStudentsAnswersInMentorPNL��5       $��55��6;   $��55��6F   $
 Hand��6R   $��398,0,167,305q�6`   $��Bottom=�6w   $��1��6�   ��5!      �� 5"      $ Add-White.png��6   ��5$      �� 5%      .R�5&      � .+�5'      A� $��Ellipse��6"   #��z��61   5(      5)      5+      �� 5-   
   -   5        $ btnEditStudentInMentorPNL��5-      $��55��64   $��55��6?   $
 Hand��6K   $��465,0,100,305q�6Y   $��Bottom=�6p   $��1��6�   ��5.      �� 5/      $ Edit-White.png��6   ��51      �� 52      .R�53      � .+�54      A� $��Ellipse��6"   #��z��61   55      56      58      �� 5:   
   -   5        $! btnRemoveStudentInMentorPNL��5:      $��55��66   $��55��6A   $
 Hand��6M   $��
0,0,33,305q�6[   $��Bottom=�6o   $��1��6�   $��Right=�6�   ��5;      �� 5<      $ Remove-White.png��6   ��5>      �� 5?      .R�5@      � .+�5A      A� $��Ellipse��6"   #��z��61   5B      5C      5E      �� 5H   
   -   5        $#��stkPanelHomeStudentsForMentor��5H      $��	0,200,0,0q�6:   $��2��6M   $��2��6^   $y�Hidden=�6r   .+�5I      � $	��571��6   $
��1240��6      5J      �� 5K      $
��286* �6$   �� 5L      $
��285* �6$    5N      �� 5O      �� 5P      .+�5R      �� -   5         ��Name$) #txtBoxSearchInDatagridInStudentsPNL��5R         placeHolder#��[  6G   
 � Tag$ Search here��6l   $��2��6~   $
��Left=�6�   $��50��6�   $��33,5,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $
��1174��6�   $��25(�6  6  �� 5S      $
��Left=�6   $��29��63   $��	47,16,0,0q�6?   $	��Top=�6R   $��29��6j   $��
Search.png��6u   &   System.Windows.Controls.DataGrid  5T      -   5        	  Name$	 DGForStudents��5T      
   RowHeaderWidth$
 45Q�62   $	��501��6F   $��
33,60,33,0q�6S      AutoGenerateColumns .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   $	��Top=�6�   $��2��6�   $��2��6�   
���   �   HorizontalGridLinesBrush
 �   �   CanUserResizeRows .     CanUserReorderColumns .     AlternatingRowBackground �	#FFE4F1EE$��20(�6v     SelectionUnit$ CellOrRowHeader=�6�   �� AlternationCount$ 1��6�     
IsReadOnly .    RowBackground
 �����   	RowHeight$ 40Q�6�     MinColumnWidth$	 200Q�6     SelectionMode$ Single=�6B     CanUserResizeColumns .  ��6;  Z� ��6�  Z�  � 	Resources 5U      :  4System.Windows.Controls.Primitives.DataGridRowHeader �� BasedOn �� 
TargetType �� Property �� Value �� 
TargetType*  $System.Windows.Controls.DataGridCell �� Triggers  P� Property! P� Value" �� Padding# J� SnapsToDevicePixels=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%  '        7 '  �    '  ]    7 �� 5V      8   "  6V   .�5W      ��  � ��
 �   �6;   �� 5X       � ��
 �����6;   �� 5Y       � ��$ 40��67   �� 5Z       � ��$ 18(�69   �� 5[       � ��$
 Bold"�6;   �� 5\       � �� 5]   "   �� 5^   &   "  66   .R�5_   *   � #��z��6/   .+�5`   .   �� $��Center=�6?   $��Center=�6\   6y   5a   +   5b   '   5d      5e      �� 5g      "  6     5h      P� 5i   "     �@ 
IsSelected! . . �5j   &   ��  � �� �	#FFFFC4596C   5k   #   .�5m      ��  � ��$ 0q�6@   �� 5n       �@�FocusVisualStyle 6A   Z� �� 5o       � ��$ 8 5 0 0q�68   �� 5p       � �� 5q   "   �� 5r   &   "  66   .R�5s   *   �� #" z��61   #��z��6U   #��z��6�   #��z��6�   # . ._�5t   .   �� ## z# 6?   5u   +   5v   '   5x      5y      �� 5z      8   "  6Y   .�5{      ��  � ��
 �   �6;   �� 5|       � ��
 �����6;   �� 5}       � ��$ 50��67   �� 5~       � ��$ 20(�69   �� 5       � ��$
 Bold"�6;   �� 5�       � �� 5�   "   �� 5�   &   "  66   .R�5�   *   � #��z��6/   .+�5�   .   �� $��Center=�6?   $��Center=�6\   6y   5�   +   5�   '   5�      5�      5�      5�      5�      5�      �� 5�   
   -   5        $)��#stkPanelAddStudentsAnswersForMentor��5�      $��	0,200,0,0q�6@   $��2��6S   $��2��6d   .+�5�      � $	��571��6   $
��1240��6      5�      �� 5�      $
��286* �6$   �� 5�      $
��285* �6$    5�      �� 5�      �� 5�      .+�5�      �� $��Path Of Your File:��6   $
��Left=�65   $��	33,24,0,0q�6P   $	��Top=�6c   
�������$��28(�6�   $
��Bold"�6�   $��50��6�   �� 5�      -	   5        $" txtBoxPathOfFileForStuAnswer��5�      $��2��6@   $
��Left=�6T   $��50��6o   $��
296,24,0,0q�6{   $
 Wrap=�6�   $	��Top=�6�   $	��697��6�   $��28(�6�   $��5,5,0,0q�6�   �� 5�      -
   5        $ btnChooseFileForStuAnswer��5�   F     innerButtonsColors#���  6   $	��Add��6i   $
��Left=�6w   $��
392,24,0,0q�6�   $	��Top=�6�   $	��160��6�   $��50��6�   $��30(�6�   $
��Bold"�6�   $
 Hand��6�   $��1��6  4  .System.Windows.Automation.AutomationProperties$   Name$$ btnChooseFileForStuAnswer��6  6J  5�      5�      5�      5�      