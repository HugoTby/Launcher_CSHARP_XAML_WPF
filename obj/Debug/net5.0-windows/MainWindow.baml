   M S B A M L   `   `   `  ����C  ?Launcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:LauncherLauncher  5   	       Launcher.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Launcher     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � Title$ BetaGames Launcher��5
   	   $	��576��6$   $
��1080��61   .��5      � .+�5   
   �� $��UniformToFill=�6   $��Images/Background.png��6(   $��Right=�6G   5      �� 5   
   $��Uniform=�6   $��Images/betagames.jpg��6"   $	��Top=�6@   $��Center=�6X   $	��365��6u   $	��198��6�    J� RenderTransformOrigin$ 
0.54,0.595&�6�   $��0,76,0,0q�6�   5      �� 5   
   -   5        $��VersionTextPrev��5      $��Version��6+   $��Bottom=�6:   $��Right=�6U   $��20(�6q   
�������$��0,0,53,0q�6�   �� 5   
   -   5        $��VersionText��5      $��Bottom=�6'   $��Right=�6B   $��20(�6^   
�������$��
0, 0, 5, 0q�6   �� 5   
   -   5        $��
PlayButton��5      $��60��6<   $��28(�6H   $��Normal"�6V   $��0,0,0,15q�6j   $��Bottom=�6|   $��Center=�6�   $	��150��6�   ��5      �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� $��Images/logo.png��6   $��40��65   $��0,0,10,0q�6A   �� 5       �� Padding$ 10q�6    $ ��Recherche de mises à jour��6-   ���	#DDFFFFFF6N   5      ��5      �� 5       �� 
TargetType" ��6"   .R�5      � .+�5      ��  ��Name$ Border��6!   ���	#FFC32929���	#FFC32929$��1q�6`    �� CornerRadius$ 5��6t   �� 5       ��Name$ ContentPresenter��6+   #��z��6E   $��Center=�6i   $��Center=�6�   5       	 �� Triggers	 5!      P� 5"      
 P� Property
 �@J�IsMouseOver P� Value$
 True��69   . �5#      ��  �� 
TargetName$ Border��6%    �� Property � �� �� Value �	#FFD649496O   �� 5$      $ Border��6%    � �� �	#FFD649496P   5%      P� 5&      
 � ��$
 True��67   . �5'      �� $ Border��6%    � �� �	#FFB819196O   �� 5(      $ Border��6%    � �� �	#FFB819196P   5)      P� 5*      
 � {�$ False��67   . �5+      �� $ Border��6%    � �� �	#FF9999996O   �� 5,      $ Border��6%    � �� �	#FF9999996P   �� 5-      $ ContentPresenter��6%    �@J�Opacity$	 0.5Q�6V   5.      50      52      53      54      