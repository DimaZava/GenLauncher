   M S B A M L   `   `   `  ����F  BGenLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:GenLauncherNetGenLauncherNet  5   	   *    $GenLauncherNet.AddModificationWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:GenLauncherNet     5   	     � Title$  Select mod to add��5   	   $��468.861��6#   $	��368��64    � 
ResizeMode$ NoResize=�6@    � WindowStyle$
 None=�6V    � Topmost .  � 	Resources 5	         FocusVisual2 �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background	 �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Foreground1  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonPressedBackground
 �� EndPoint �� 
StartPoint � Color � Offset �� 
TargetType �� 
TargetType ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName��%S  &	        &	 �     &	 �     &	 �     &	     &	 0    &	 Q    &	 r    &	 �    &		 �    &	
 �    '�� �    7   7  7  7 
 7  7  7  7  7  7  7 	 �� 5
   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $	 	#B3000000��6;   �� 5   
   $	 #00e3ff��67   �� 5   
   $	 #2534ff��6>   �� 5   
   $	 #00e3ff��6:   �� 5   
   $	 #baff0c��6=   �� 5   
   $	 #00e3ff��68   �� 5   
   $	 Black��6=   �� 5   
   $	 #7a7db0��69   �� 5   
   $	 DarkGray��6=   �� 5   
   $	
 1,0&�6>   $	 0,0&�6M   .��5      � $ 	#B3000000��6   $ 0Q�6-   � 5      $ #2534ff��6   $	 0.1Q�6+   � 5       $ #2534ff��6   $	 0.9Q�6+   � 5!      $ 	#B3000000��6   $ 1Q�6-   5"      �� 5$   
   " ��6   .�5%      ��  �@�FocusVisualStyle8   61   �� 5&       � ��8  6+   �� 5'       � ��8  6,   �� 5(       � ��# � ��6+   �� 5)       � ��$ 2q�60   �� 5*       � ��$ Center=�6;   �� 5+       � ��$ Center=�69   �� 5,       � ��$ 1q�6(   �� 5.       � ��
 �����6+   �� 5/       � �� 50      �� 51      " ��6&   .R�52      �� $ border��6!   #��z��61   #��z��6]   #��z��6�    . ._�53      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  54       55      P� 56       �@��IsDefaulted . . �57   "   �� $ border��6@   # � ��6T    � ��58      P� 59       �@J�IsMouseOver . . �5:   "   �� 8   � ��6?   �� 5;   "   $ border��6@   8   � ��6T   �� 5<   "   8   � ��6?   5=      P� 5>       � �� . . �5?   "   �� 8   � ��6?   �� 5@   "   $ border��6@   8   � ��6T   5A      P� 5B       � {� .  . �5C   "   �� $ border��6?   8   � ��6S   �� 5D   "   $ border��6@   8 	  � ��6T   �� 5E   "   $ contentPresenter��6K   8 
  � ��6i   5F      5H      5J      5K      5M      .��5O      � ��5P   
   �� 5Q       �� ImageSource$ /Images/AddModBG.png��6   .+�5S   
   �� $��Select Mod to add��6   $
��Bold"�6,   $��12,6,0,0q�6>   $��3��6P   $��4��6`   
�������$��18(�6�   $
��Left=�6�   $	��Top=�6�   �� 5T   
   -   5         ��Name$ ModificationsList��5T      $��12,44,12,31q�6-   $��16(�6B   
������ $��0q�6i   
�������6}   �� 5U   
   -   5        $
��Add!��5U      $��
70,443,0,0q�6    $	��Top=�64   $
��Left=�6L   $	��102��6g   6s   �� 5V   
   -   5        $��Cancel��5V      $��
0,443,70,0q�6"   $	��Top=�66   $��Right=�6N   $	��102��6j   6v   5X      5Y      