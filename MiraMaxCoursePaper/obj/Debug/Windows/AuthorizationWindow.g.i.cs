﻿#pragma checksum "..\..\..\Windows\AuthorizationWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D5FFAEBCCDF7D9EFFAA1A31B4EE19B081F71608B22504D953E4E9D28AD3A2396"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MiraMaxCoursePaper.Windows;
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


namespace MiraMaxCoursePaper.Windows {
    
    
    /// <summary>
    /// AuthorizationWindow
    /// </summary>
    public partial class AuthorizationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 81 "..\..\..\Windows\AuthorizationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewbox ContentBox;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\Windows\AuthorizationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Login;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\Windows\AuthorizationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Password;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\Windows\AuthorizationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Enter;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Windows\AuthorizationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Registration;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\Windows\AuthorizationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GetPassword;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\Windows\AuthorizationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Advertisement;
        
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
            System.Uri resourceLocater = new System.Uri("/MiraMaxCoursePaper;component/windows/authorizationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\AuthorizationWindow.xaml"
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
            
            #line 15 "..\..\..\Windows\AuthorizationWindow.xaml"
            ((MiraMaxCoursePaper.Windows.AuthorizationWindow)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.Window_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ContentBox = ((System.Windows.Controls.Viewbox)(target));
            return;
            case 3:
            this.Login = ((System.Windows.Controls.TextBox)(target));
            
            #line 85 "..\..\..\Windows\AuthorizationWindow.xaml"
            this.Login.GotFocus += new System.Windows.RoutedEventHandler(this.Login_GotFocus);
            
            #line default
            #line hidden
            
            #line 85 "..\..\..\Windows\AuthorizationWindow.xaml"
            this.Login.LostFocus += new System.Windows.RoutedEventHandler(this.Login_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Password = ((System.Windows.Controls.TextBox)(target));
            
            #line 86 "..\..\..\Windows\AuthorizationWindow.xaml"
            this.Password.GotFocus += new System.Windows.RoutedEventHandler(this.Password_GotFocus);
            
            #line default
            #line hidden
            
            #line 86 "..\..\..\Windows\AuthorizationWindow.xaml"
            this.Password.LostFocus += new System.Windows.RoutedEventHandler(this.Password_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Enter = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\..\Windows\AuthorizationWindow.xaml"
            this.Enter.Click += new System.Windows.RoutedEventHandler(this.Enter_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Registration = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\Windows\AuthorizationWindow.xaml"
            this.Registration.Click += new System.Windows.RoutedEventHandler(this.Registration_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.GetPassword = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\..\Windows\AuthorizationWindow.xaml"
            this.GetPassword.Click += new System.Windows.RoutedEventHandler(this.GetPassword_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Advertisement = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
