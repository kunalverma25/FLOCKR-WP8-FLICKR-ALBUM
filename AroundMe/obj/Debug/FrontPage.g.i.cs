﻿#pragma checksum "E:\TUT\WP8 Absolute Beginners\Source Codes\AroundMe\AroundMe\FrontPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F41D60FAB5328FB5CF01D37B7EA386FE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Inneractive.Ad;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
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


namespace AroundMe {
    
    
    public partial class FrontPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.HubTile nature;
        
        internal Microsoft.Phone.Controls.HubTile art;
        
        internal Microsoft.Phone.Controls.HubTile beach;
        
        internal Microsoft.Phone.Controls.HubTile graffiti;
        
        internal Microsoft.Phone.Controls.HubTile fashion;
        
        internal Microsoft.Phone.Controls.HubTile flowers;
        
        internal Microsoft.Phone.Controls.HubTile party;
        
        internal Microsoft.Phone.Controls.HubTile music;
        
        internal System.Windows.Controls.TextBox SearchTopic;
        
        internal Inneractive.Ad.InneractiveAd InneractiveXamlAd;
        
        internal Microsoft.Phone.Controls.ToggleSwitch locSearch;
        
        internal Microsoft.Phone.Maps.Controls.Map AroundMeMap;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/AroundMe;component/FrontPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.nature = ((Microsoft.Phone.Controls.HubTile)(this.FindName("nature")));
            this.art = ((Microsoft.Phone.Controls.HubTile)(this.FindName("art")));
            this.beach = ((Microsoft.Phone.Controls.HubTile)(this.FindName("beach")));
            this.graffiti = ((Microsoft.Phone.Controls.HubTile)(this.FindName("graffiti")));
            this.fashion = ((Microsoft.Phone.Controls.HubTile)(this.FindName("fashion")));
            this.flowers = ((Microsoft.Phone.Controls.HubTile)(this.FindName("flowers")));
            this.party = ((Microsoft.Phone.Controls.HubTile)(this.FindName("party")));
            this.music = ((Microsoft.Phone.Controls.HubTile)(this.FindName("music")));
            this.SearchTopic = ((System.Windows.Controls.TextBox)(this.FindName("SearchTopic")));
            this.InneractiveXamlAd = ((Inneractive.Ad.InneractiveAd)(this.FindName("InneractiveXamlAd")));
            this.locSearch = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("locSearch")));
            this.AroundMeMap = ((Microsoft.Phone.Maps.Controls.Map)(this.FindName("AroundMeMap")));
        }
    }
}

