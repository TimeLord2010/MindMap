﻿#pragma checksum "..\..\..\Pages\ChooseRoot.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6EF149D3AA0B529526BE43CF9FCD3963D346BE2DB4CF88048588161DE1E9FE6A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MindMap.Pages;
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


namespace MindMap.Pages {
    
    
    /// <summary>
    /// ChooseRoot
    /// </summary>
    public partial class ChooseRoot : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Pages\ChooseRoot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NomeTB;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\ChooseRoot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid RootsDG;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\ChooseRoot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SelecionarG;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Pages\ChooseRoot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle SelecionarR;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Pages\ChooseRoot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SelecionarTBL;
        
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
            System.Uri resourceLocater = new System.Uri("/MindMap;component/pages/chooseroot.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\ChooseRoot.xaml"
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
            this.NomeTB = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\..\Pages\ChooseRoot.xaml"
            this.NomeTB.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NomeTB_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RootsDG = ((System.Windows.Controls.DataGrid)(target));
            
            #line 23 "..\..\..\Pages\ChooseRoot.xaml"
            this.RootsDG.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RootsDG_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SelecionarG = ((System.Windows.Controls.Grid)(target));
            
            #line 27 "..\..\..\Pages\ChooseRoot.xaml"
            this.SelecionarG.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.SelecionarG_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SelecionarR = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 5:
            this.SelecionarTBL = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

