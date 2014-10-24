﻿#pragma checksum "..\..\..\PhotoCard\PhotoCard.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "680306CD098227485B4CEFB21F3A3486"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
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


namespace BeastPhotoRiver.PhotoCard {
    
    
    /// <summary>
    /// PhotoCard
    /// </summary>
    public partial class PhotoCard : Microsoft.Surface.Presentation.Controls.ScatterViewItem, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\..\PhotoCard\PhotoCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\PhotoCard\PhotoCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Front;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\PhotoCard\PhotoCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgFront;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\PhotoCard\PhotoCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton CloseButton;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\PhotoCard\PhotoCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton VoteButton;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\PhotoCard\PhotoCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Back;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\PhotoCard\PhotoCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgBack;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\PhotoCard\PhotoCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ImageDescription;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\PhotoCard\PhotoCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton Inspiring;
        
        #line default
        #line hidden
        
        
        #line 192 "..\..\..\PhotoCard\PhotoCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton Pensive;
        
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
            System.Uri resourceLocater = new System.Uri("/BeastPhotoRiver;component/photocard/photocard.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PhotoCard\PhotoCard.xaml"
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
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Front = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.imgFront = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.CloseButton = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            
            #line 73 "..\..\..\PhotoCard\PhotoCard.xaml"
            this.CloseButton.Click += new System.Windows.RoutedEventHandler(this.CloseButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.VoteButton = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            
            #line 86 "..\..\..\PhotoCard\PhotoCard.xaml"
            this.VoteButton.Click += new System.Windows.RoutedEventHandler(this.VoteButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Back = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.imgBack = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.ImageDescription = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            
            #line 132 "..\..\..\PhotoCard\PhotoCard.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.CommentButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Inspiring = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            
            #line 137 "..\..\..\PhotoCard\PhotoCard.xaml"
            this.Inspiring.Click += new System.Windows.RoutedEventHandler(this.TagButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Pensive = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            
            #line 186 "..\..\..\PhotoCard\PhotoCard.xaml"
            this.Pensive.Click += new System.Windows.RoutedEventHandler(this.TagButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 239 "..\..\..\PhotoCard\PhotoCard.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.BackToImageButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

