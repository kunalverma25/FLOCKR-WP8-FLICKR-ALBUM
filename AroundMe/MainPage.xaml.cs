﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AroundMe.Resources;
using System.Device.Location;
using Windows.Devices.Geolocation;

using System.Windows.Media.Imaging;


namespace AroundMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Loaded += MainPage_Loaded;

            BuildLocalizedApplicationBar();
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            SystemTray.ProgressIndicator = new ProgressIndicator();

            UpdateMap();
        }

        void AroundMeMap_Loaded(object sender, RoutedEventArgs e)
        {
            // before you push to the store, these must be set.
            // you get these values from the dev center
            //MapsSettings.ApplicationContext.ApplicationId = "ApplicationID";
            //MapsSettings.ApplicationContext.AuthenticationToken = "AuthenticationToken";
        }

        private async void UpdateMap()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            SetProgressIndicator(true);
            SystemTray.ProgressIndicator.Text = "Getting GPS Location";

            try
            {
                Geoposition position =
                    await geolocator.GetGeopositionAsync(
                    TimeSpan.FromMinutes(1),
                    TimeSpan.FromSeconds(30));

                SystemTray.ProgressIndicator.Text = "Acquired";

                var gpsCoorCenter =
                    new GeoCoordinate(
                        position.Coordinate.Latitude,
                        position.Coordinate.Longitude);

                AroundMeMap.Center = gpsCoorCenter;
                AroundMeMap.ZoomLevel = 15;

                SetProgressIndicator(false);
            }
            catch (UnauthorizedAccessException) {
                MessageBox.Show("Location is disable in phone settings.");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton appBarButton =
                new ApplicationBarIconButton(
                    new Uri("/Assets/feature.search.png", UriKind.Relative));
            appBarButton.Text = "Search";
            appBarButton.Click += SearchClick;
            ApplicationBar.Buttons.Add(appBarButton);
        }


        private void SearchClick(object sender, EventArgs e)
        {
            // http://msdn.microsoft.com/en-us/library/system.net.httputility.urlencode(v=VS.95).aspx
            string topic = HttpUtility.UrlEncode(SearchTopic.Text);

            string navTo = string.Format(
                "/SearchResults.xaml?latitude={0}&longitude={1}&topic={2}&radius={3}",
                AroundMeMap.Center.Latitude,
                AroundMeMap.Center.Longitude,
                topic,
                5);

            NavigationService.Navigate(new Uri(navTo, UriKind.RelativeOrAbsolute));
        }


        private static void SetProgressIndicator(bool isVisible)
        {
            SystemTray.ProgressIndicator.IsIndeterminate = isVisible;
            SystemTray.ProgressIndicator.IsVisible = isVisible;
        }


    }


}