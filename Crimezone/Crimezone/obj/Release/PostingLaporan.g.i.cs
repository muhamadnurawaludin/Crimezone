﻿#pragma checksum "C:\Users\handika\Documents\Visual Studio 2012\Projects\Crimezone\Crimezone\PostingLaporan.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EA818E2D92868D9414F8EE75F1AF2F6C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Crimezone {
    
    
    public partial class PostingLaporan : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Image foto_kejahatan;
        
        internal System.Windows.Controls.TextBox judulKejahatan;
        
        internal System.Windows.Controls.TextBox deskripsi_kejahatan;
        
        internal Microsoft.Phone.Controls.ListPicker ListPickerJenisKejahatan;
        
        internal Microsoft.Phone.Controls.ListPicker ListPickerLokasi;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Crimezone;component/PostingLaporan.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.foto_kejahatan = ((System.Windows.Controls.Image)(this.FindName("foto_kejahatan")));
            this.judulKejahatan = ((System.Windows.Controls.TextBox)(this.FindName("judulKejahatan")));
            this.deskripsi_kejahatan = ((System.Windows.Controls.TextBox)(this.FindName("deskripsi_kejahatan")));
            this.ListPickerJenisKejahatan = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("ListPickerJenisKejahatan")));
            this.ListPickerLokasi = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("ListPickerLokasi")));
        }
    }
}
