   M S B A M L   `   `   `  ����G  CPorownywarka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PorownywarkaPorownywarka  5   	       Porownywarka.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Porownywarka     5   	     � Title$  Search Engine��5   	   $	��300��6   $	��300��6,    � WindowStartupLocation$ CenterScreen=�68    � 
ResizeMode$ CanMinimize=�6s   ��6]   Z� ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6    � Offset$ 0Q�6)   � 5      $ White��6   $ 1Q�6)   5      .��5      �  � ColumnDefinitions 5   
   �� 5      .+�5   
   �� -   5         ��Name$ 	Login_btn��5      $��Login��6$   $
��Left=�64   $��157,219,0,0q�6O   $	��Top=�6d   $��75��6|   $��Candara)�6�   $��24(�6�   $��40��6�   ���	#FFDDDDDD��6�   Z� 6�   �� 5   
   -   5        	 ��Name$	 Username_TextBok��5      $
��Left=�6,   $��25��6G   $��
132,38,0,0q�6S   
 �� TextWrapping$

 Wrap=�6g   $	��Top=�6{   $	��120��6�   ��6�   Z� �� 5   
   -   5         ��Name$ Label_Username��5      $��	Username:��6(   $
��Left=�6<   $��	36,36,0,0q�6W   $	��Top=�6j   $��
Constantia)�6�   $��16(�6�   
���������6�   Z� 6�   �� 5   
   -   5        $ Label_password��5      $��	Password:��6(   $
��Left=�6<   $��	42,71,0,0q�6W   $	��Top=�6j    J� RenderTransformOrigin$ -0.096,0.088&�6�   $��
Constantia)�6�   $��16(�6�   
���������6�   Z� 6�   �� 5   
   -   5        $ Label_Warnings��5      $�� ��6(   $
��Left=�63   $��
28,139,0,0q�6N   $	��Top=�6b   $	��254��6z   G� 5   
   -   5         G�Name$ Password_PasswordBox��5      $
��Left=�64   $��
132,73,0,0q�6O   $	��Top=�6c   $	��120��6{   $��25��6�   ��6�   Z� 6�   �� 5   
   -   5        $ return_button��5      $��Return��6(   $
��Left=�69   $��
69,219,0,0q�6T   $	��Top=�6h   $��75��6�   $��Candara)�6�   $��24(�6�   $��40��6�   ���	#FFDDDDDD��6�   Z� 6�   �� 5   
   -   5        $ label��5      $�� ��6   $
��Left=�6*   $��120,165,0,0q�6E   $	��Top=�6Z   5      5      