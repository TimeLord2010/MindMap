﻿#pragma checksum "..\..\..\..\Pages\ViewNode.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9DA4026AA0B0A62E80E785C281E9CCD8C11D6D1E6F1AA9E529323307F7D1E75B"
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
    /// ViewNode
    /// </summary>
    public partial class ViewNode : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\Pages\ViewNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ParentNameTBL;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Pages\ViewNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CurrentNameTBL;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Pages\ViewNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ChildrenSP;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Pages\ViewNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid CriarG;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Pages\ViewNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ContentTBL;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Pages\ViewNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UndoAllG;
        
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
            System.Uri resourceLocater = new System.Uri("/MindMap;component/pages/viewnode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\ViewNode.xaml"
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
            this.ParentNameTBL = ((System.Windows.Controls.TextBlock)(target));
            
            #line 22 "..\..\..\..\Pages\ViewNode.xaml"
            this.ParentNameTBL.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ParentNameTBL_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CurrentNameTBL = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.ChildrenSP = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.CriarG = ((System.Windows.Controls.Grid)(target));
            
            #line 32 "..\..\..\..\Pages\ViewNode.xaml"
            this.CriarG.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.CriarG_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ContentTBL = ((System.Windows.Controls.TextBox)(target));
            
            #line 41 "..\..\..\..\Pages\ViewNode.xaml"
            this.ContentTBL.LostFocus += new System.Windows.RoutedEventHandler(this.ContentTBL_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.UndoAllG = ((System.Windows.Controls.Grid)(target));
            
            #line 44 "..\..\..\..\Pages\ViewNode.xaml"
            this.UndoAllG.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.UndoAllG_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

