﻿#pragma checksum "..\..\DiscussLayer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "82EAFC87805191711590BE480796B7F1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Controls.Primitives;
using Microsoft.Surface.Presentation.Controls.TouchVisualizations;
using Microsoft.Surface.Presentation.Input;
using Microsoft.Surface.Presentation.Palettes;
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


namespace BeastPhotoRiver {
    
    
    /// <summary>
    /// DiscussLayer
    /// </summary>
    public partial class DiscussLayer : Microsoft.Surface.Presentation.Controls.ScatterView, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\DiscussLayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PresentImage1;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\DiscussLayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.ScatterViewItem Team1Lock;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\DiscussLayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PresentImage2;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\DiscussLayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.ScatterViewItem Team2Lock;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\DiscussLayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PresentImage3;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\DiscussLayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.ScatterViewItem Team3Lock;
        
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
            System.Uri resourceLocater = new System.Uri("/BeastPhotoRiver;component/discusslayer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DiscussLayer.xaml"
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
            this.PresentImage1 = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.Team1Lock = ((Microsoft.Surface.Presentation.Controls.ScatterViewItem)(target));
            return;
            case 3:
            
            #line 37 "..\..\DiscussLayer.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.LockButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PresentImage2 = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.Team2Lock = ((Microsoft.Surface.Presentation.Controls.ScatterViewItem)(target));
            return;
            case 6:
            
            #line 66 "..\..\DiscussLayer.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.LockButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PresentImage3 = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.Team3Lock = ((Microsoft.Surface.Presentation.Controls.ScatterViewItem)(target));
            return;
            case 9:
            
            #line 95 "..\..\DiscussLayer.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.LockButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

