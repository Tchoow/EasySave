   M S B A M L   `   `   `  ����K  GRemote_interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:remote_interfaceremote_interface  5   	   !    Remote_interface.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:remote_interface     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ 
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      � .+�5   
   �� -   5         ��Name$ playbtn��5      $
��Play��6"   $��
249,0,0,10q�61   ���	#FF83FF55$
��Left=�6\   $��77��6w   $��52��6�   $��Bottom=�6�   
������� � 	Resources 5       �� 
TargetType �� Property �� Value��%^   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 5��65   5      5      5      �� 5   
   -   5        $
��Stop��5   +   $��
441,0,0,10q�6:   ���	#FFFF3A3A$
��Left=�6e   $��77��6�   $��52��6�   $��Bottom=�6�   
������� 5      ��%^   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 5��65   5      5      5      �� 5   
   -   5        $��Pause��5   ,   $��
344,0,0,10q�6<   ���	#FFFFEC31$��52��6g   $��Bottom=�6s   $
��Left=�6�   $��78��6�   
������� 5      ��%^   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 5��65   5      5      5      �� 5    
   -   5         ��Name$ ipinput��5    #   $��	127.0.0.1��6   $
��Left=�64   $��
16,324,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��120��6�   $��17��6�   �� 5!   
   $��	Ip adress��6   $
��Left=�6$   $��26��6?   $	��Top=�6K   $	��120��6c   $��
16,298,0,0q�6o   �� 5"   
   -   5        $ 	portinput��5"      $��20167��6   $
��Left=�62   $��
16,362,0,0q�6M   $
 Wrap=�6a   $	��Top=�6u   $	��120��6�   $��17��6�   �� 5#   
   $
��Port��6   $
��Left=�6   $��26��6:   $	��Top=�6F   $	��120��6^   $��
16,338,0,0q�6j   �� 5$   
   -   5        $��Connect��5$      $
��Left=�6#   $��21��6>   $��
30,387,0,0q�6J   $	��Top=�6^   $��92��6v   ���	#FF5BDCF9
������� 5%      ��%c   '��       �� 5&      " ��6   .�5'      ��  �@��CornerRadius$ 5��65   6A   5(      5)      5*      &   System.Windows.Controls.DataGrid  5+   
   -   5        	  Name$	 jobdatagrid��5+      
   AutoGenerateColumns$
 False��6X   $��26,10,33,141q�6t      CanUserReorderColumns$ False��6�   
�������
���   �$��Verdana)�6�    J� Opacity$	 0.8Q�6�    5,      ��%d   '��       �� 5-      " ��6   .�5.      ��  �@��CornerRadius$ 10��65   6B   5/      50         Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly$
 True��6%      Header$
 Name��67      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6`   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *Name+6`     53      $
 True��6%   $ SourceFilePath��67   $ * 6r    6O   �� *SourceFilePath+6r     54      $
 True��6%   $ DestinationFilePath��67   $ * 6|    6T   �� *DestinationFilePath+6|     55      $
 True��6%   $ SaveType��67   $ * 6f    6I   �� *
SaveType+6f     56      $
 True��6%   $ State��67   $ * 6`    6F   �� *State+6`     57      $
 True��6%   $ TotalFileToCopy��67   $ * 6t    6P   �� *TotalFileToCopy+6t     58      $
 True��6%   $ TotalFileSize��67   $ * 6p    6N   �� *TotalFileSize+6p     59      $
 True��6%   $ NbFilesLeftToDo��67   $ * 6t    6P   �� *NbFilesLeftToDo+6t     5:      $
 True��6%   $ Progression��67   $ * 6l    6L   �� *Progression+6l     5;      $
 True��6%   $ Created��67   $ * 6d    6H   �� *	Created+6d   5=      5>      5?      