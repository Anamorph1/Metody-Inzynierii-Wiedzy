   M S B A M L   `   `   `  ����G  CAGDSDatabase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AGDSDatabase.ViewsAGDSDatabase.Views  5   	   +clr-namespace:AGDSDatabaseAGDSDatabase  5   	   U QGraphSharp.Controls, Version=1.0.3438.42202, Culture=neutral, PublicKeyToken=nullV>clr-namespace:GraphSharp.Controls;assembly=GraphSharp.ControlsGraphSharp.Controls 5   	   O KWPFExtensions, Version=1.0.3437.34043, Culture=neutral, PublicKeyToken=nullV;clr-namespace:WPFExtensions.Controls;assembly=WPFExtensionsWPFExtensions.Controls 5	   	   #    AGDSDatabase.Views.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1self clr-namespace:AGDSDatabase.Views     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:AGDSDatabase     5   	   U
graphsharp>clr-namespace:GraphSharp.Controls;assembly=GraphSharp.Controls     5   	   Lzoom;clr-namespace:WPFExtensions.Controls;assembly=WPFExtensions     5	   	   -   5          �Name$
  root��5   	    � Title$ 
MainWindow��5   	   $	��350��6   $	��525��6)    � WindowState$ 	Maximized=�65   .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $
��File��6   .@�5      m� -   5         m�Name$ MenuNew��5   (   $	��New��6   69   �� 5      m� 5      $
��Exit��6   5      m� 5      $
��_Run��6   .@�5      m� $��Validate��6   m� 5      -   5        $ MenuExecute��5   ,   $��Execute��6   6A   5      m� 5      $
��Help��6   5      ]� 5   
   $	��Top=�6   
��� �����. .+�5      �� $��	Command: ��6   $
��Left=�6)   $��Center=�6?   �� 5      -   5         ��Name$ 	QueryTBox��5      $
��Auto��6)   5      �� 5    
   -   5         ��Name$ 	ErrorInfo��5       $��Bottom=�6?   $
��auto��6W   ��6#   �� *	Message+6W   �� 5!   
   $
��Left=�6   .+�5"      R� -   5        $��treeView��5"   y   $
��Auto=�6   $
��Auto=�6I   $	��250��6�   $	��500��6�   ��5#      �� 5$       �� DataType" m�6/    �� ItemsSource 6L   �� *Items+.R�5%      �� ��6$   �� *Title+5&      5(      �� 5)      -   5        $��Output��5)   x   $
��Auto=�6   $
��Auto=�6F   $	��100��6�   $	��250��6�   $��0q�6�    �� 
IsReadOnly . 6�   5*      � 5+   
   -   5        	 �Name$	 Graph��5+      $��Right=�6   .+�5,      (  "WPFExtensions.Controls.ZoomControl  .��5-      %  GraphSharp.Controls.GraphLayout  -	   5        
  Name$
 graphLayout��5-   )   �  �GraphSharp.Controls.GraphLayout`3[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[QuickGraph.IEdge`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], QuickGraph, Version=3.2.40122.0, Culture=neutral, PublicKeyToken=f3fb40175eec2af3],[QuickGraph.IBidirectionalGraph`2[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[QuickGraph.IEdge`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], QuickGraph, Version=3.2.40122.0, Culture=neutral, PublicKeyToken=f3fb40175eec2af3]], QuickGraph, Version=3.2.40122.0, Culture=neutral, PublicKeyToken=f3fb40175eec2af3]]   LayoutAlgorithmType$ FR��5.   )   "   OverlapRemovalAlgorithmType$	 FSA��6B      HighlightAlgorithmType$ Simple��5/   )      Graph 5-   >   ��  �� ElementName$
 root�� �� Path$ GraphToVisualize�5/   )   50      52      54      57      