﻿#pragma checksum "..\..\..\Pages\GoodPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2B8BB232455CCD7A3EE002A4F117C145326C6D4898E585DD379A619375488287"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MiraMaxCoursePaper.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MiraMaxCoursePaper.Pages {
    
    
    /// <summary>
    /// GoodPage
    /// </summary>
    public partial class GoodPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 101 "..\..\..\Pages\GoodPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GoodNameBox;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\Pages\GoodPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GoodStatus;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\Pages\GoodPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddGood;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\Pages\GoodPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView GoodsList;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MiraMaxCoursePaper;component/pages/goodpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\GoodPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.GoodNameBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 101 "..\..\..\Pages\GoodPage.xaml"
            this.GoodNameBox.GotFocus += new System.Windows.RoutedEventHandler(this.GoodNameBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 101 "..\..\..\Pages\GoodPage.xaml"
            this.GoodNameBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.GoodNameBox_TextChanged);
            
            #line default
            #line hidden
            
            #line 101 "..\..\..\Pages\GoodPage.xaml"
            this.GoodNameBox.LostFocus += new System.Windows.RoutedEventHandler(this.GoodNameBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.GoodStatus = ((System.Windows.Controls.ComboBox)(target));
            
            #line 102 "..\..\..\Pages\GoodPage.xaml"
            this.GoodStatus.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GoodStatus_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AddGood = ((System.Windows.Controls.Button)(target));
            
            #line 103 "..\..\..\Pages\GoodPage.xaml"
            this.AddGood.Click += new System.Windows.RoutedEventHandler(this.AddGood_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.GoodsList = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 119 "..\..\..\Pages\GoodPage.xaml"
            ((System.Windows.Controls.Grid)(target)).PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 138 "..\..\..\Pages\GoodPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditGood_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

