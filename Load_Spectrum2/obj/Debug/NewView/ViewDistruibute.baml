   M S B A M L   `   `   `  ����I  ELoad_Spectrum2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Load_Spectrum2.NewViewLoad_Spectrum2.NewView  5   	   /clr-namespace:Load_Spectrum2Load_Spectrum2  5   	   ,    &Load_Spectrum2.NewView.ViewDistruibute   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Load_Spectrum2.NewView     5   	   2	local_srcclr-namespace:Load_Spectrum2     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � UseLayoutRounding .  � 
ResizeMode$ CanMinimize=�5   	   
������� � Title$ View_Distribute��5   	   $	��750��6!   $
��1150��6.    � 	Resources 5      ��%    5      .��5      � .+�5   
   �� -   5         ��Name$ tab_distribute��5      $��5 5 5 5q�6-   .@�5      �� -   5         ��Name$ tabitem_LTE��5      $��설비로확산��6+   ���#c8ffe1.��5      �  � ColumnDefinitions 5      �� 5      $��5* �6+   �� 5      $��3* �6+    � RowDefinitions 5      �� 5      $��3* �6(   �� 5      $��3* �6(   �� 5      $��2* �6(   �� 5      $	��16* �6(   .+�5"      �� $B��<※ Refresh : EQP에서 업로드 폴더가 있는지 검색��6    $��2��6H   $��0��6U   $��	10 25 0 0q�6e   �� 5$      	 �� BitmapScalingMode$	 HighQuality=�6   $3��-pack://application:,,,/Resources/icon_LTE.png��6J   $��1��5%      $��0��6,   $��3��69   $	��600��6J   $	��190��6V   �� 5(      $��0��6   $��0��6,   $��10 5 0 0q�6<   $��1q�5)      ��5*      �� 5+      $4��.Local (1개의 파일 또는 폴더만 가능)��6$   .��5.      �� 
 �� Orientation$
 
Horizontal=�6%   $��1��6>   $��0��6K   .+�5/      �� -   5         ��Name$ select_LTE_localfile��5/   %   $��파일 선택��6C   $��10 0 0 0q�50   )   $��60��6;   $��20��6F   -  'System.Windows.Input.KeyboardNavigation   AcceptsReturn .  
������� . $��1��54   )   $
��Left=�55   )    J� Effect 57   "   3  -System.Windows.Media.Effects.DropShadowEffect  58   &      ShadowDepth$ 2Q�67      	Direction$	 315Q�6G      Color$ Black��6W      Opacity$	 0.5Q�6e   5:      �� 5<      -   5        $ select_LTE_localpath��5<   %   $��폴더 선택��6C   $��5 0 0 0q�5=   )   $��60��6:   $��20��6E   
������� .   . $��1��5A   )   $
��Left=�5B   )    5D   "     5E   &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5G      �� 5I      -   5         ��Name$ LTE_Local_path��5I   &   $	��420��6>   $��20��6J   $��10 0 0 0q�6V   5J      5K      �� 5N      $��1��6   $��0��6,   $��10 5 0 0q�6<   $��1q�5O      ��5P      �� 5Q      $L��FEQP (경로 입력 안하고 Refresh하면 FTP 연결여부만 확인)��6$   .��5S      �� $
 
Horizontal=�6%   $��1��6>   $��0��6K   .+�5T      �� -   5        $ select_ETL_eqppath��5T   %   $��폴더 선택��6A   $��10 0 0 0q�5U   )   $��60��6;   $��20��6F    .  
������� . $��1��5Y   )   $
��Left=�5Z   )    5\   "     5]   &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5_      �� 5a      -   5         ��Name$ LTE_combo_drive��5a   '   
�������$��60��6S   $��20��6^   $��7 0 0 0q�6j   $��Center=�5b   '    �� DisplayMemberPath$ strdrive��5d   '   ��5c   '   �� *source_drive+5f      �� 5g      -   5        $ LTE_eqp_path��5g   &   $	��418��6<   $��20��6H   $��10 0 0 0q�6T    �� CharacterCasing$ Upper=�6f   5h   &   5i      5j      �� 5m      -	   5         ��Name$ list_LTE��5m      $��3��61   $��0��6>   $��
10 20 0 10q�6N   $��Center=�5n       J� 	AllowDrop . ��5z      �� 5{       �� 
TargetType" ��6$   .�5|   "   ��  �� Property � �� �� Value$ Center=�6O   5}       �� View 5      �� 5�      .P�5�   "   �� $��EQPID��61    �� Width$ 85��6@    �� DisplayMemberBinding 5�   1   �� *strchkEQPID_Set3+�� 5�   "   $��Status��61   $	 120��5�   )    5�   )   �� *strStatus_Set3+�� 5�   "   $��EQP폴더유무��61   $	 100��5�   )    5�   )   �� *strRefresh_Set3+�� 5�   "   $��다운로드��61   $	 200��6?    �� CellTemplate 5�   &   �� 5�   *   .R�5�   .   � .+�5�   2   � $	��150��6>   $��15��6J    � Minimum$ 0Q�6V     � Maximum$	  100Q�6b   $��Center=�5�   ^   ! � Value! 6:   �� *strPgbValue_Set3+6^   �� 5�   2   $	��150��6<   $��15��6H   $��Center=�6T   " �� TextAlignment$" Center=�5�   <   $��0 0 0 2q�6S   ��5�   <   �� *strPgbTxt_Set3+5�   /   5�   +   5�   #   �� 5�   "   $��다운성공��61   $ 90��6?    5�   1   �� *strPassFail_Set3+5�      5�      �� 5�      $��3��6!   $��2��6.   $
 
Horizontal=�6>   .+�5�      �� .+�5�      �� $
 
Horizontal=�6)   .+�5�   "   �� $��EQPID(필수)��6,   $
��Bold"�6=   
�������
���   �$��Center=�5�   $   $��Center=�6A   $��	10 20 5 0q�6\   �� 5�   "   -
   5        $ EQPIDResetBtn_EQP1��5�   )   $��0 20 0 0q�6E   $��Right=�6W      Style_AllResetBtn#��[  6s    .  $	 HighQuality=�6�   # � ToolTip$# 모두 초기화��5�   )   $ ^� ShowDuration$$ 20000��6:   % ^� InitialShowDelay$	% 500��6^   5�   )   5�      �� 5�      -   5        $ tb_EQPID_eqp3��5�   &   
�������$��10 7 5 0q�5�   *   $ Upper=�6<   5�   *   �� 5�      -   5        $ List_EQPID_eqpSet3��5�   '   $	��266��6C   $	��100��6P   $��10 5 5 0q�6\   $ strEQPID_Set3��5�   '   & �� SelectionMode$& Extended=�5�   '   
������� . ��5�   '   �� *source_EQP_Info_EQPID3+5�      �� 5�      $
 
Horizontal=�6)   .+�5�   "   �� -   5        $ Eqp3_TrbackBtn��5�   )   $��10 5 0 0q�6A     Style_TrbackBtn#��[ 6S    .  $	 HighQuality=�6�   $# Alt+T��5�   %   $$ 20000��65   $	% 500��6Y   �� 5�   "   -   5        $ 
Eqp3_TrBtn��5�   )   $��5 5 0 0q�6=     Style_TransferBtn#��[ 6N    .  $	 HighQuality=�6�   $# Alt+T��5�   %   $$ 20000��65   $	% 500��6Y   5�      5�      �� 5�      $��5 0 0 0q�6%   .+�5�      �� .+�5�   "   �� $
 
Horizontal=�6-   .+�5�   &   �� $
��Line��60   $
��Left=�6<   $��Center=�6W   $��5 25 5 0q�6r   �� 5�   &   -   5        $ LineResetBtn_EQP1��5�   -   $��0 20 0 0q�6H   $��Right=�6Z     Style_ResetBtn#��[ 6v    .  $	 HighQuality=�5�   V   $# LINE 초기화��5�   M   $$ 20000��6`   $	% 500��6�   5�   #   �� 5�   "   -   5        ' ��Name$' List_Line_eqpSet3��5�   *   $	��130��6E   $	��100��6R   $��5 5 5 0q�6^   $ strLine_Set3��5�   *   $& Extended=�5�   *   ��5�   *   �� *source_EQP_Info_Line3+5�   #   5�      �� 5�      .+�5�   "   �� $
 
Horizontal=�6-   .+�5�   &   �� $
��ZONE��60   $��Center=�6<   $��Center=�6Y   $��5 10 5 0q�6t   �� 5�   &   -   5        $ ZONEResetBtn_EQP1��5�   -   $��0 5 0 0q�6H   $��Right=�6Y   #��[ 6u    .  $	 HighQuality=�6�   $# ZONE 초기화��5�   -   $$ 20000��6@   $	% 500��6d   5�   -   5�   #   �� 5�   "   -   5        $' List_ZONE_eqpSet3��5�   *   $	��130��6E   $	��100��6R   $��5 5 5 0q�6^   $ strZONE_Set3��5�   )   $& Extended=�5�   )   ��5�   )   �� *source_EQP_Info_ZONE3+5�   #   5�      5�      �� 5�      $��5 0 0 0q�6%   .+�5�      �� .+�5�   "   �� $
 
Horizontal=�6-   .+�5�   &   �� $��EQPTYPE��60   $��Center=�6?   $��Center=�6\   $��5 25 5 0q�6w   �� 5�   &   -   5        $ EQPTYPEResetBtn_EQP1��5�   -   $��0 20 0 0q�6K   $��Right=�6]   #��[ 6y    .  $	 HighQuality=�6�   $# TYPE 초기화��5�   1   $$ 20000��6D   $	% 500��6h   5�   1   5�   #   �� 5�   "   -   5        $' List_EQPTYPE_eqpSet3��5�   *   $	��130��6H   $	��100��6U   $��5 5 5 0q�6a   $ strEQPTYPE_Set3��5�   .   $& Extended=�5   .   ��5�   .   �� *source_EQP_Info_EQPTYPE3+5  #   5     �� 5     .+�5  "   �� $
 
Horizontal=�6-   .+�5  &   �� $��EQPMODEL��60   $��Center=�6@   $��Center=�6]   $��5 10 5 0q�6x   �� 5  &   -   5        $ EQPMODELRsesetBtn_EQP1��5  -   $��0 5 0 0q�6M   $��Right=�6^   #��[ 6z    .  $	 HighQuality=�6�   $# MODEL 초기화��5	  -   $$ 20000��6A   $	% 500��6e   5
  1   5  #   �� 5  "   -   5        $' List_EQPMODEL_eqpSet3��5  *   $	��130��6I   $	��100��6V   $��5 5 5 5q�6b   $ strEQPMODEL_Set3��5  *   $& Extended=�5  *   ��5  *   �� *source_EQP_Info_EQPMODEL3+5  #   5     5     5     �� 5     $��2��6!   $��0��6.   $	��Top=�6>   $��0,20,0,0q�5  !   $��Right=�5  !   .+�5     �� $
 
Horizontal=�6%   .+�5     #   Load_Spectrum2.perBusySpinner  -   5        (  Name$( LTE_Spin��5  7   $��17��5  +   $��10 0 0 0q�5   +   
������ 
����  �$��Right=�5#  +   $��Center=�5$  +   � 5&     $��10 0 0 0q�6#   .+�5'  "   � -   5        ) �Name$) LTE_pgb��5'  .   $	��100��6?   $��20��6K   $ 0Q�6W   $	  100Q�6c   $! 0Q�5(  .   �� 5)  "   -   5        * ��Name$* LTE_pgb_txt��5)  ,   $��80��6A   $��15��5*  ,   $��0 0 0 2q�68   $��Center=�5+  ,   $��Center=�5,  ,   $" Center=�5-  ,   5.     �� 50     -   5        $ btn_LTE_Reset��50  %   $��List초기화��6<   $��10 0 0 0q�51  %   $��70��67   $��25��6B   
����� � . $��Bottom=�54  %   $��Right=�55  %    57  "     58  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5:     �� 5<     -   5        $ LTE_Refresh��5<  %   $��Refresh��6:   $��10 0 0 0q�5=  %   $��80��67   $��25��6B   
������� . $
��Left=�5@  %   $��Bottom=�5A  %    5C  "     5D  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5F     �� 5H     -   5        $ LTE_distribute��5H  %   $��확산하기��6=   $��10 0 0 0q�5I  %   $��80��67   $��25��6B   
������� . $
��Left=�5L  %   $��Bottom=�5N  %   $# Refresh 필요��5O  %   + ^� ShowOnDisabled+ . $$ 20000��6_   $	% 500��6�    5P  "     5Q  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5S     5U     5W     5X     5Y     �� 5[     -   5        $ tabitem_ETL��5[     $��로컬로확산��6+   ���#c8ffe1.��5\     �  5^     �� 5_     $��5* �6+   �� 5`     $��3* �6+    5d     �� 5e     $��3* �6(   �� 5f     $��3* �6(   �� 5g     $��3* �6(   �� 5h     $	��13* �6(   .+�5l     �� $J��D※ Refresh : EQP에 다운받을 파일(폴더)가 있는지 검색��6    $��2��6L   $��0��6Y   $��	10 25 0 0q�6i   �� 5m     $B��<※ 파일이 있는 설비에서는 유사파일 못찾음 ��6    $��2��6@   $��0��6M   $��	10 50 0 0q�6]   �� 5n     $G��A※ 2개 파일 이상 취합시 Refresh 전 다운 개수 입력��6    $��2��6I   $��0��6V   $��	10 75 0 0q�6f   
����  �6y   �� 5t     $��0��6   $��0��6,   $��10 5 0 0q�6<   $��1q�5u     ��5v     �� 5w     $2��,EQP (1개의 파일 또는 폴더만 가능)��6$   .��5y     �� $
 
Horizontal=�6%   $��1��6>   $��0��6K   .+�5z     �� -   5        $ ETL_btn��5z  %   $��경로 선택��66   $��10 0 0 0q�5{  )   $��60��6;   $��20��6F   
������� . $��1��5~  )   $
��Left=�5  )    .   5�  "     5�  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5�     �� 5�     -   5        $ ETL_combo_drive��5�  '   
�������$��60��6S   $��20��6^   $��5 0 0 0q�6j   $��Center=�5�  '   $ strdrive��5�  '   ��5�  '   �� *source_drive+5�     �� 5�     -    5        $ ETL_eqp_path��5�  &   $	��420��6<   $��20��6H   $��10 0 0 0q�6T   $ Upper=�6f   5�  &   5�     5�     �� 5�     $��1��6   $��0��6,   $��10 5 0 0q�6<   $��1q�5�     ��5�     �� 5�     $K��ELocal (파일이 존재하는 설비에서는 유사파일 못찾음)��6$   .��5�     �� $
 
Horizontal=�6%   $��1��6>   $��0��6K   .+�5�     �� -!   5        $ select_ETL_localpath��5�  %   $��폴더 선택��6C   $��10 0 0 0q�5�  %   $	��125��67   $��20��6C   
������� .  .  $��1��5�  %   $
��Left=�5�  %    5�  "     5�  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5�     �� 5�     -"   5        $ ETL_Local_path��5�  &   $	��420��6>   $��20��6J   $��10 0 0 0q�6V   5�     5�     �� 5�     $��0��6   $��1��6,   $��10 5 0 0q�6<   $��3��6N   $��1q�5�     ��5�     �� 5�     $+��%두께 데이터 취합(확산필수)��6$   .��5�     �� .+�5�     �� $
 
Horizontal=�6)   .+�5�  "   �� $��5 0 0 0q�6-   .+�5�  &   �� $,��&Item / Layer (첫번째 설비 기준)��6,   $��Center=�6O   �� 5�  &   -#   5        $ List_RNR_Item_Layer��5�  /   $	��205��6L   $	��235��6Y   ��5�  /   �� *source_RNR_ItemLayer+ 5�  *   �� 5�  .   .P�5�  2   �� $	��Chk��6A   $ 40��6N    5�  6   �� 5�  :   .R�5�  >   �� $
��5, 0q�6G   , d� 	IsChecked, 6U   �� *
strRNR_Chk+5�  ;   5�  3   �� 5�  2   $
��Item��6A   $ 50��6O    5�  A   �� *strRNR_Item+�� 5�  2   $��Layer��6A   $	 160��6P    5�  A   �� *strRNR_Layer+5�  /   5�  '   5�  #   �� 5�  "   $��10 0 0 0q�6-   .+�5�  &   �� $��Wafer��6,   $��Center=�6<   �� 5�  &   -$   5        $' List_RNR_Wafer��5�  .   $��70��6F   $	��100��6Q   $& Extended=�6^   �� 5�  &   $��IN/EG��6,   $��Center=�6<   �� 5�  &   -%   5        $' List_RNR_INEG��5�  .   $��70��6E   $��80��6P   $& Extended=�6\   5�  #   5�     5�     5�     �� 5�     -&   5        $ list_ETL��5�     $��3��61   $��0��6>   $��
10 20 0 10q�6N   $��Center=�5�      . ��5�     �� 5�     " ��6$   .�5�  "   ��  � ��$ Center=�6O   5�      5�     �� 5�     .P�5�  "   �� $��EQPID��61   $ 85��6@    5�  1   �� *strchkEQPID_Set4+�� 5�  "   $��Status��61   $	 120��5�  )    5�  )   �� *strStatus_Set4+�� 5�  "   $��EQP경로유무��61   $	 160��5�  )    5�  )   �� *strRefresh_Set4+�� 5�  "   $��다운개수��61   $ 60��6?    5�  &   �� 5�  *   .R�5�  .   � .+�5�  2   �� $��40��6:   $��20��6E   $��Center=�6Q   - �� TextAlignment$- Center=�5�  :   $��0 0 0 2q�6Q   ��5�  :   �� *strNum_Set4+. J� IsHitTestVisible. 5   :   �� *strHit_Set4+��5  :   �� *strColor_Set4+5  /   5  +   5  #   �� 5  "   $��다운로드��61   $	 160��6?    5	  &   �� 5
  *   .R�5  .   � .+�5  2   � $	��140��6>   $��15��6J   $ 0Q�6V   $	  100Q�6b   $��Center=�5  ^   ! 6:   �� *strPgbValue_Set4+6^   �� 5  2   $	��140��6<   $��15��6H   $��Center=�6T   $" Center=�5  <   $��0 0 0 2q�6S   ��5  <   �� *strPgbTxt_Set4+5  /   5  +   5  #   �� 5  "   $��다운성공��61   $ 90��6?    5  1   �� *strPassFail_Set4+5     5     �� 5     $��3��6!   $��1��6.   $
 
Horizontal=�6>   .+�5      �� .+�5!     �� $
 
Horizontal=�6)   .+�5"  "   �� $��EQPID(필수)��6,   $
��Bold"�6=   
�������
���   �$��Center=�5#  $   $��Center=�6A   $��	10 20 5 0q�6\   �� 5$  "   -'   5        $ EQPIDResetBtn_EQP4��5$  )   $��0 20 0 0q�6E   $��Right=�6W   #��[  6s    .  $	 HighQuality=�6�   $# 모두 초기화��5%  )   $$ 20000��6:   $	% 500��6^   5&  )   5'     �� 5)     -(   5        $ tb_EQPID_eqp4��5)  &   
�������$��10 7 5 0q�5*  &   $ Upper=�68   5+  &   �� 5-     -)   5        $ List_EQPID_eqpSet4��5-  '   $	��266��6C   $	��100��6P   $��10 5 5 0q�6\   $ strEQPID_Set4��5/  '   $& Extended=�50  '   
������� . ��5.  '   �� *source_EQP_Info_EQPID4+5=     �� 5?     $
 
Horizontal=�6)   .+�5@  "   �� -*   5        $ Eqp4_TrbackBtn��5@  )   $��10 5 0 0q�6A   #��[ 6S    .  $	 HighQuality=�6�   $# Alt+T��5A  %   $$ 20000��65   $	% 500��6Y   5B  )   �� 5D  "   -+   5        $ 
Eqp4_TrBtn��5D  )   $��5 5 0 0q�6=   #��[ 6N    .  $	 HighQuality=�6�   $# Alt+T��5E  %   $$ 20000��65   $	% 500��6Y   5F  )   5G     5H     �� 5J     $��5 0 0 0q�6%   .+�5L     �� .+�5M  "   �� $
 
Horizontal=�6-   .+�5N  &   �� $
��Line��60   $
��Left=�6<   $��Center=�6W   $��5 25 5 0q�6r   �� 5O  &   -,   5        $ LineResetBtn_EQP4��5O  -   $��0 20 0 0q�6H   $��Right=�6Z   #��[ 6v    .  $	 HighQuality=�5P  V   $# LINE 초기화��5Q  -   $$ 20000��6@   $	% 500��6d   5R  -   5S  #   �� 5U  "   --   5        $' List_Line_eqpSet4��5U  *   $	��130��6E   $	��100��6R   $��5 5 5 0q�6^   $ strLine_Set4��5W  *   $& Extended=�5Y  *   ��5V  *   �� *source_EQP_Info_Line4+5Z  #   5[     �� 5^     .+�5_  "   �� $
 
Horizontal=�6-   .+�5`  &   �� $
��ZONE��60   $��Center=�6<   $��Center=�6Y   $��5 10 5 0q�6t   �� 5a  &   -.   5        $ ZONEResetBtn_EQP4��5a  -   $��0 5 0 0q�6H   $��Right=�6Y   #��[ 6u    .  $	 HighQuality=�6�   $# ZONE 초기화��5b  -   $$ 20000��6@   $	% 500��6d   5c  -   5d  #   �� 5f  "   -/   5        $' List_ZONE_eqpSet4��5f  *   $	��130��6E   $	��100��6R   $��5 5 5 0q�6^   $ strZONE_Set4��5h  *   $& Extended=�5j  *   ��5g  *   �� *source_EQP_Info_ZONE4+5k  #   5l     5m     �� 5o     $��5 0 0 0q�6%   .+�5q     �� .+�5r  "   �� $
 
Horizontal=�6-   .+�5s  &   �� $��EQPTYPE��60   $��Center=�6?   $��Center=�6\   $��5 25 5 0q�6w   �� 5t  &   -0   5        $ EQPTYPEResetBtn_EQP4��5t  -   $��0 20 0 0q�6K   $��Right=�6]   #��[ 6y    .  $	 HighQuality=�6�   $# TYPE 초기화��5u  1   $$ 20000��6D   $	% 500��6h   5v  1   5w  #   �� 5x  "   -1   5        $' List_EQPTYPE_eqpSet4��5x  *   $	��130��6H   $	��100��6U   $��5 5 5 0q�6a   $ strEQPTYPE_Set4��5z  *   $& Extended=�5|  *   ��5y  *   �� *source_EQP_Info_EQPTYPE4+5}  #   5~     �� 5�     .+�5�  "   �� $
 
Horizontal=�6-   .+�5�  &   �� $��EQPMODEL��60   $��Center=�6@   $��Center=�6]   $��5 10 5 0q�6x   �� 5�  &   -2   5        $ EQPMODELRsesetBtn_EQP4��5�  -   $��0 5 0 0q�6M   $��Right=�6^   #��[ 6z    .  $	 HighQuality=�6�   $# MODEL 초기화��5�  -   $$ 20000��6A   $	% 500��6e   5�  -   5�  #   �� 5�  "   -3   5        $' List_EQPMODEL_eqpSet4��5�  *   $	��130��6I   $	��100��6V   $��5 5 5 5q�6b   $ strEQPMODEL_Set4��5�  *   $& Extended=�5�  *   ��5�  *   �� *source_EQP_Info_EQPMODEL4+5�  #   5�     5�     5�     �� 5�     $��2��6!   $��0��6.   $	��Top=�6>   $��0,20,0,0q�6V   .+�5�     �� $
 
Horizontal=�6%   $��Right=�6>   $��0 0 5 0q�6Z   .+�5�       -4   5        $( ETL_Spin��5�  7   $��17��5�  +   $��10 0 0 0q�5�  +   
������ 
����  �$��Right=�5�  +   $��Center=�5�  +   � 5�     $��10 0 0 0q�6#   .+�5�  "   � -5   5        $) ETL_pgb��5�  .   $	��100��6?   $��20��6K   $ 0Q�6W   $	  100Q�6c   $! 0Q�5�  .   �� 5�  "   -6   5        $* ETL_pgb_txt��5�  ,   $��80��6A   $��15��5�  ,   $��0 0 0 2q�68   $��Center=�5�  ,   $��Center=�5�  ,   $" Center=�5�  ,   5�     �� 5�     -7   5        $ ETL_Refresh��5�  %   $��Refresh��6:   $��10 0 0 0q�5�  %   $��80��67   $��25��6B   
������� . $
��Left=�5�  %   $��Bottom=�5�  %    5�  "     5�  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5�     �� 5�     -8   5        $ ETL_distribute��5�  %   $��확산하기��6=   $��10 0 0 0q�5�  %   $��80��67   $��25��6B   
������� . $��Bottom=�5�  %   $# Refresh 필요��5�  %   + . $$ 20000��6_   $	% 500��6�    5�  "     5�  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5�     5�     �� 5�     $��0 15 5 0q�6%   $
 
Horizontal=�67   $��Right=�6P   .+�5�     �� -9   5        / ��Name$/ 
ETL_imgChk��5�  '   $��이미지 다운 제외��6;   $��5 6 0 0q�6O   �� 5�     -:   5        $ btn_RNR_Reset��5�  %   $��List초기화��6<   $��10 0 0 0q�5�  %   $��70��67   $��25��6B   
����� � . $��Bottom=�5�  %   $��Right=�5�  %    5�  "     5�  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5�     �� 5�     -;   5        $ btn_RNR_down��5�  %   $��데이터취합��6;   $��10 0 0 0q�5�  %   $��80��67   $��25��6B   
������� . $��Bottom=�5�  %   $��Right=�5�  %    5�  "     5�  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5�     �� 5�     -<   5        $ btn_FolderOpen��5�  %   $��RNR폴더열기��6=   $��10 0 0 0q�5�  %   $��80��67   $��25��6B   
������� . $��Bottom=�5�  %   $��Right=�5�  %    5�  "     5�  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5�     5�     5�     5�     5�     �� 5�     -=   5        $ tabitem_ETE��5�     $��설비→설비��6+   ���#c8ffe1.��5�     �  5�     �� 5�     $��5* �6+   �� 5�     $��3* �6+    5�     �� 5�     $��3* �6(   �� 5�     $��3* �6(   �� 5�     $��2* �6(   �� 5�     $	��16* �6(   .+�5�     �� $I��C※ Refresh : Target EQP에서 업로드 폴더가 있는지 검색��6    $��2��6O   $��0��6\   $��	10 25 0 0q�6l   �� 5     $	 HighQuality=�6   $3��-pack://application:,,,/Resources/icon_ETE.png��6J   $��1��5     $��0��6,   $��3��69   $	��600��6J   $	��190��6V   �� 5     $��0��6   $��0��6,   $��10 5 0 0q�6<   $��1q�5     ��5     �� 5     $9��3Source EQP (1개의 파일 또는 폴더만 가능)��6$   .��5
     �� $
 
Horizontal=�6%   $��1��6>   $��0��6K   .+�5     �� ->   5        $ ETE_src_btn��5  %   $��경로 선택��6:   $��10 0 0 0q�5  %   $��60��67   $��20��6B   
������� . $��1��5  %   $
��Left=�5  %    .   5  "     5  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5     �� 5     -?   5        $ ETE_src_combo_drive��5  '   
�������$��60��6W   $��20��6b   $��5 0 0 0q�6n   $��Center=�5  '   $ strdrive��5  '   ��5  '   �� *source_drive+5     �� 5     -@   5        $ ETE_src_eqp��5  &   $��70��6;   $��20��6F   $��10 0 0 0q�6R   $ Upper=�6d   �� 5     -A   5        $ ETE_src_path��5  &   $	��340��6<   $��20��6H   $��10 0 0 0q�6T   $ Upper=�6f   5      5!     �� 5$     $��1��6   $��0��6,   $��10 5 0 0q�6<   $��1q�5%     ��5&     �� 5'     $��
Target EQP��6$   .��5)     �� $
 
Horizontal=�6%   $��1��6>   $��0��6K   .+�5*     �� -B   5        $ ETE_tar_btn��5*  %   $��경로 선택��6:   $��10 0 0 0q�5+  %   $��60��67   $��20��6B   
������� . $��1��5.  %   $
��Left=�5/  %    .   52  "     53  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   55     �� 57     -C   5        $ ETE_tar_combo_drive��57  '   
�������$��60��6W   $��20��6b   $��5 0 0 0q�6n   $��Center=�58  '   $ strdrive��5:  '   ��59  '   �� *source_drive+5<     �� 5=     -D   5        $ ETE_tar_path��5=  &   $	��420��6<   $��20��6H   $��10 0 0 0q�6T   $ Upper=�6f   5>  &   5?     5@     �� 5C     -E   5        $ list_ETE��5C     $��3��61   $��0��6>   $��
10 20 0 10q�6N   $��Center=�5D      . ��5P     �� 5Q     " ��6$   .�5R  "   ��  � ��$ Center=�6O   5S      5U     �� 5V     .P�5W  "   �� $��EQPID��61   $ 85��6@    5X  1   �� *strchkEQPID_Set5+�� 5Z  "   $��Status��61   $	 120��5[  )    5\  )   �� *strStatus_Set5+�� 5^  "   $��EQP경로유무��61   $	 100��5_  )    5`  )   �� *strRefresh_Set5+�� 5b  "   $��다운로드��61   $	 200��6?    5c  &   �� 5d  *   .R�5e  .   � .+�5f  2   � $	��150��6>   $��15��6J   $ 0Q�6V   $	  100Q�6b   $��Center=�5g  ]   ! 6:   �� *strPgbValue_Set5+6]   �� 5h  2   $	��150��6<   $��15��6H   $��Center=�6T   $" Center=�5i  <   $��0 0 0 2q�6S   ��5j  <   �� *strPgbTxt_Set5+5k  /   5m  +   5o  #   �� 5q  "   $��다운성공��61   $ 90��6?    5r  1   �� *strPassFail_Set5+5s     5u     �� 5y     $��3��6!   $��1��6.   $
 
Horizontal=�6>   .+�5z     �� .+�5{     �� $
 
Horizontal=�6)   .+�5|  "   �� $��EQPID(필수)��6,   $
��Bold"�6=   
�������
���   �$��Center=�5}  $   $��Center=�6A   $��	10 20 5 0q�6\   �� 5~  "   -F   5        $ EQPIDResetBtn_EQP5��5~  )   $��0 20 0 0q�6E   $��Right=�6W   #��[  6s    .  $	 HighQuality=�6�   $# 모두 초기화��5  )   $$ 20000��6:   $	% 500��6^   5�  )   5�     �� 5�     -G   5        $ tb_EQPID_eqp5��5�  &   
�������$��10 7 5 0q�5�  &   $ Upper=�68   5�  &   �� 5�     -H   5        $ List_EQPID_eqpSet5��5�  '   $	��266��6C   $	��100��6P   $��10 5 5 0q�6\   $ strEQPID_Set5��5�  '   $& Extended=�5�  '   
������� . ��5�  '   �� *source_EQP_Info_EQPID5+5�     �� 5�     $
 
Horizontal=�6)   .+�5�  "   �� -I   5        $ Eqp5_TrbackBtn��5�  )   $��10 5 0 0q�6A   #��[ 6S    .  $	 HighQuality=�6�   $# Alt+T��5�  %   $$ 20000��65   $	% 500��6Y   5�  )   �� 5�  "   -J   5        $ 
Eqp5_TrBtn��5�  )   $��5 5 0 0q�6=   #��[ 6N    .  $	 HighQuality=�6�   $# Alt+T��5�  %   $$ 20000��65   $	% 500��6Y   5�  )   5�     5�     �� 5�     $��5 0 0 0q�6%   .+�5�     �� .+�5�  "   �� $
 
Horizontal=�6-   .+�5�  &   �� $
��Line��60   $
��Left=�6<   $��Center=�6W   $��5 25 5 0q�6r   �� 5�  &   -K   5        $ LineResetBtn_EQP5��5�  -   $��0 20 0 0q�6H   $��Right=�6Z   #��[ 6v    .  $	 HighQuality=�5�  V   $# LINE 초기화��5�  -   $$ 20000��6@   $	% 500��6d   5�  -   5�  #   �� 5�  "   -L   5        $' List_Line_eqpSet5��5�  *   $	��130��6E   $	��100��6R   $��5 5 5 0q�6^   $ strLine_Set5��5�  *   $& Extended=�5�  *   ��5�  *   �� *source_EQP_Info_Line5+5�  #   5�     �� 5�     .+�5�  "   �� $
 
Horizontal=�6-   .+�5�  &   �� $
��ZONE��60   $��Center=�6<   $��Center=�6Y   $��5 10 5 0q�6t   �� 5�  &   -M   5        $ ZONEResetBtn_EQP5��5�  -   $��0 5 0 0q�6H   $��Right=�6Y   #��[ 6u    .  $	 HighQuality=�6�   $# ZONE 초기화��5�  -   $$ 20000��6@   $	% 500��6d   5�  -   5�  #   �� 5�  "   -N   5        $' List_ZONE_eqpSet5��5�  *   $	��130��6E   $	��100��6R   $��5 5 5 0q�6^   $ strZONE_Set5��5�  *   $& Extended=�5�  *   ��5�  *   �� *source_EQP_Info_ZONE5+5�  #   5�     5�     �� 5�     $��5 0 0 0q�6%   .+�5�     �� .+�5�  "   �� $
 
Horizontal=�6-   .+�5�  &   �� $��EQPTYPE��60   $��Center=�6?   $��Center=�6\   $��5 25 5 0q�6w   �� 5�  &   -O   5        $ EQPTYPEResetBtn_EQP5��5�  -   $��0 20 0 0q�6K   $��Right=�6]   #��[ 6y    .  $	 HighQuality=�6�   $# TYPE 초기화��5�  1   $$ 20000��6D   $	% 500��6h   5�  1   5�  #   �� 5�  "   -P   5        $' List_EQPTYPE_eqpSet5��5�  *   $	��130��6H   $	��100��6U   $��5 5 5 0q�6a   $ strEQPTYPE_Set5��5�  *   $& Extended=�5�  *   ��5�  *   �� *source_EQP_Info_EQPTYPE5+5�  #   5�     �� 5�     .+�5�  "   �� $
 
Horizontal=�6-   .+�5�  &   �� $��EQPMODEL��60   $��Center=�6@   $��Center=�6]   $��5 10 5 0q�6x   �� 5�  &   -Q   5        $ EQPMODELRsesetBtn_EQP5��5�  -   $��0 5 0 0q�6M   $��Right=�6^   #��[ 6z    .  $	 HighQuality=�6�   $# MODEL 초기화��5�  -   $$ 20000��6A   $	% 500��6e   5�  -   5�  #   �� 5�  "   -R   5        $' List_EQPMODEL_eqpSet5��5�  *   $	��130��6I   $	��100��6V   $��5 5 5 5q�6b   $ strEQPMODEL_Set5��5�  *   $& Extended=�5�  *   ��5�  *   �� *source_EQP_Info_EQPMODEL5+5�  #   5�     5�     5�     �� 5�     $��2��6!   $��0��6.   $	��Top=�6>   $��0,20,0,0q�6V   .+�5�     �� $
 
Horizontal=�6%   $��Right=�6>   .+�5�       -S   5        $( ETE_Spin��5�  7   $��17��5�  +   $��10 0 0 0q�5�  +   
������ 
����  �$��Right=�5�  +   $��Center=�5�  +   � 5�     $��10 0 0 0q�6#   .+�5�  "   � -T   5        $) ETE_pgb��5�  .   $	��100��6?   $��20��6K   $ 0Q�6W   $	  100Q�6c   ! 5�  .   �� *pgbVal_Set5+�� 5�  "   -U   5        $* ETE_pgb_txt��5�  ,   $��80��6A   $��15��5�  ,   $��0 0 0 2q�68   $��Center=�5�  ,   $��Center=�5   ,   $" Center=�5  ,   5     �� 5     -V   5        $ btn_ETE_Reset��5  %   $��List초기화��6<   $��10 0 0 0q�5  %   $��70��67   $��25��6B   
����� � . $��Bottom=�5  %   $��Right=�5	  %    5  "     5  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5     �� 5     -W   5        $ ETE_Refresh��5  %   $��Refresh��6:   $��10 0 0 0q�5  %   $��80��67   $��25��6B   
������� . $
��Left=�5  %   $��Bottom=�5  %    5  "     5  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5     �� 5     -X   5        $ ETE_distribute��5  %   $��확산하기��6=   $��10 0 0 0q�5  %   $��80��67   $��25��6B   
������� . $
��Left=�5   %   $��Bottom=�5"  %   $# Refresh 필요��5#  %   + . $$ 20000��6_   $	% 500��6�    5%  "     5&  &   $ 2Q�67   $	 315Q�6G   $ Black��6W   $	 0.5Q�6e   5(     5*     �� 5,     $
 
Horizontal=�6%   5/     52     53     55     57     �� 59  
   $A��;※ Refresh를 먼저 해야만 확산할 수 있습니다.��6   $
��Left=�69   $��	250,7,0,0q�6T   $
��Wrap=�6g   $	��Top=�6{   $	��250��6�   $��20��6�   5:     5<     