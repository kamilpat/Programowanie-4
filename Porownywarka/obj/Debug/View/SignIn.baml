   M S B A M L   `   `   `  ����G  CPorownywarka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PorownywarkaPorownywarka  5   	       Porownywarka.SignIn   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Porownywarka     5   	     � Title$  Search Engine��5   	   $	��300��6   $	��300��6,    � WindowStartupLocation$ CenterScreen=�68    � 
ResizeMode$ CanMinimize=�6s   ��6]   Z� ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6    � Offset$ 0Q�6)   � 5      $ White��6   $ 1Q�6)   5      .��5      �  � ColumnDefinitions 5   
   �� 5      .+�5   
   �� -   5         ��Name$ 
SignIn_btn��5      $��Sign In��6%   $
��Left=�67   $��157,219,0,0q�6R   $	��Top=�6g   $��75��6   $��Candara)�6�   $��24(�6�   $��40��6�   ���	#FFDDDDDD��6�   Z� 6�   �� 5   
   -   5        	 ��Name$	 Username_TextBok��5      $
��Left=�6,   $��25��6G   $��135,107,0,0q�6S   
 �� TextWrapping$

 Wrap=�6h   $	��Top=�6|   $	��120��6�   ��6�   Z� �� 5   
   -   5         ��Name$ Label_Username��5      $��	Username:��6(   $
��Left=�6<   $��
37,106,0,0q�6W   $	��Top=�6k   $��
Constantia)�6�   $��16(�6�   
���������6�   Z� 6�   �� 5   
   -   5        $ Label_password��5      $��	Password:��6(   $
��Left=�6<   $��
42,140,0,0q�6W   $	��Top=�6k    J� RenderTransformOrigin$ -0.096,0.088&�6�   $��
Constantia)�6�   $��16(�6�   
���������6�   Z� 6�   G� 5   
   -   5         G�Name$ Password_PasswordBox��5      $
��Left=�64   $��135,142,0,0q�6O   $	��Top=�6d   $	��120��6|   $��25��6�   ��6�   Z� 6�   �� 5   
   -   5        $ return_button��5      $��Return��6(   $
��Left=�69   $��
69,219,0,0q�6T   $	��Top=�6h   $��75��6�   $��Candara)�6�   $��24(�6�   $��40��6�   ���	#FFDDDDDD��6�   Z� 6�   �� 5   
   -   5        $ Error_label��5      $�� ��6%   $
��Left=�60   $��	40,29,0,0q�6K   $	��Top=�6^   $ 3.618,0.833&�6v   $��
Constantia)�6�   $��16(�6�   
���������6�   Z� 6�   5      5      