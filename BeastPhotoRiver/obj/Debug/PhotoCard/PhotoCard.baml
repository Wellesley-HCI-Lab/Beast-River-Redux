   M S B A M L   `   `   `  ����  BeastPhotoRiver)    #BeastPhotoRiver.PhotoCard.PhotoCard   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  5      G  AMicrosoft.Surface.Presentation.Controls.ScatterContentControlBase    Deceleration$	  500Q�5	      
������ $	��500��6@   $	��500��6M    � 	Resources 5       
  sbFlip g� 	BeginTime �� 
TargetName �� TargetProperty O� KeyTime O� Value  	sbReverse��%6  &	        &	 �    �� 5   
   .��5      U� $ 00:00:00X�5      $ Front��5      $9 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�5      .\�5      �� $ 
00:00:00.2��6'   $ 0Q�6<   5      U� 5      $ 
00:00:00.2X�5      $
 Back��5      $9 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�5      .\�5      �� $ 
00:00:00.4��6'   $ 1Q�6<   5      5      �� 5   
   .��5      U� $ 00:00:00X�5      $
 Back��5      $9 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�5      .\�5       �� $ 
00:00:00.2��6'   $ 0Q�6<   5!      U� 5"      $ 
00:00:00.2X�5#      $ Front��5$      $9 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�5%      .\�5&      �� $ 
00:00:00.4��6'   $ 1Q�6<   5'      5(      5)      .��5+      � -   5         �Name$ 
LayoutRoot��5+      $	��500��6   $	��500��6,   .+�5-   
   � -   5        $ Front��5-   )   $	��400��6   $	��400��6    J� RenderTransformOrigin$ 0.5,0.5&�68   z�5.      �� 5/      .+�52      �� 
���   �
�������$��2q�54      	 �� CornerRadius$	 10��55      56      �� 58      -   5        
 ��Name$
 imgFront��58      $
��Fill=�59      $��5,5,5,5q�6#   5I      � 5K   
   -   5        $
 Back��5K      $ 0.5,0.5&�6   z�5L      �� 5M       �� ScaleX$ 0Q�6!   .+�5P      �� 
���   �
�������$��1q�5R      $	 10��5S      5T      �� 5V      -   5        $
 imgBack��5V      $��UniformToFill=�5W      $��96,9,92,219q�5X      �� 5Z      -   5         ��Name$ ImageDescription��5Z      
�������$	��160��5\      $��
95,90,90,0q�5]      $y�	Collapsed=�5^      $��14(�5_      ;  5Microsoft.Surface.Presentation.Controls.SurfaceButton  5l      -   5        $��Bottom=�5m      
������ |�.  $	��161��5p      $��
95,0,90,50q�5q      5r        5v      -   5          Name$ negResp��5}      $��14(�5x      
���Gc��
���   �$��Center=�5{      $��Center=�5|      $	��180��5}   "   $��10, 10, 10, 10q�5~      $	��250��6)   $��Right=�66   $	��Top=�6R   .��5      �� $3��-/BeastPhotoRiver;component/Resources/down.png��6   6Q   5�        5�      -	   5        $ posResp��5�      $��14(�5�      
���   �$��Center=�5�      $��Center=�5�      $	��180��5�      $��10,10,10,10q�5�      $	��250��6&   $
��Left=�63   $	��Top=�6N   
���� �.��5�      �� $4��./BeastPhotoRiver;component/Resources/thumb.png��6   6R   5�        5�      -
   5        
�������
���   �$��Center=�5�      $��Center=�5�      $��3, 270, 3, 3q�5�      $��Go Back to Image��5�      5�      5�        5�   
   -   5        $ CloseButton��5�   .   $y�Hidden=�6   $��close��6C   
���""��$	��Top=�6j   $��Right=�6�   $��Center=�6�   $��Center=�6�   $	��150��6�   $	��150��6�   $��0,-25,-28,0q�6  $��20��6,  $��20��6;  $��1q�6I  6W    5�   
   -   5        $ 	YesButton��5�   .   $y�Hidden=�6   
�����w�$	��yes��6]   $	��153��6k   $��Right=�6x   $��Center=�6�   $��0,364,-28,0q�6�   $��20��6�   $��20��6�   $��1q�6�   $	��Top=�6�   $��Center=�6  $	��150��60  6\    5�   
   -   5        $ NoButton��5�   .   $y�Hidden=�6   
�����w�$��no��6\   $	��148��6i   $��Right=�6v   $��Center=�6�   $��0,210,-28,0q�6�   $��20��6�   $��20��6�   $��1q�6�   $	��Top=�6�   $��Center=�6  $	��150��6/  6Z  5�      5�      