   M S B A M L   `   `   `  ????M  IMiraMaxCoursePaper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:MiraMaxCoursePaper.WindowsMiraMaxCoursePaper.Windows  5   	   ,    &MiraMaxCoursePaper.Windows.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:MiraMaxCoursePaper.Windows     5   	     ? Title$  MiraMax??5   	    ? WindowState$ 	Maximized=?5	       ? WindowStartupLocation$ CenterScreen=?5
      $	??720??5      $	??370??5      $
??1520??5      $	??760??5       ? 	Resources 5       ?? 
TargetType ?? Property ?? Value??%d  '??       ?? 5      " ??6
   .?5      ??  ? ??$ 20(?6    6-   ?? 5       ? ??$ Inter)?6"   62   ?? 5       ? ??$
 Bold"?6"   61   ?? 5       ? ?? ?#FFFFFF64   ?? 5       ? ?? ?#1F75A864   ?? 5       ? ??$ 10 10 10 10q?6   64   ?? 5       ? ??$ 	10 0 10 0q?6   63   5      5      .??5      ?  ? RowDefinitions 5      ?? 5      $??70 ?6   6!   ?? 5      6   .+?5"      ??  ?? Orientation$ 
Horizontal=?6   $??0??6(   $??Right=?65   $??0 0 20 0q?6Q   .+?5#      ?? -   5        $
??Exit??5#      .??6+   
Выйти62   5$      &   System.Windows.Controls.DataGrid  5&      -   5        $??Users??5&      $??1??6   	   
IsReadOnly	 . 
   AutoGenerateColumns
 .  $??
30 0 30 30q?6U      Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$ Имя??6      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6&   1  +System.Windows.Controls.DataGridBoundColumn   Binding 60   ?? *	GetName+  5)      $ 
Логин??6   $ * 6(    62   ?? *
GetLogin+  5*      $ 
Почта??6   $ * 6(    62   ?? *	GetMail+  5+      $ Роль??6   $ * 6'    61   ?? *	GetRole+  5,      $ Бан??6   $ * 6&    60   ?? *GetBan+4  .System.Windows.Controls.DataGridTemplateColumn  5.         CellTemplate 5/      ?? 50      .R?51   	   ?? $
??More??6   -   5        .??51   /   Подробнее53   
   54   	   56      58      59      5:      