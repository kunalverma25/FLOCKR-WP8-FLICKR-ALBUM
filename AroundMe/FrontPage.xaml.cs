using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Windows.Devices.Geolocation;
using System.Device.Location;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Maps;

namespace AroundMe
{
    public partial class FrontPage : PhoneApplicationPage
    {
        private IsolatedStorageSettings settings;
        public FrontPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            settings = IsolatedStorageSettings.ApplicationSettings;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Retrieving values");
                locSearch.IsChecked = (bool)settings["locSearch"];

            }

            catch (KeyNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine("First Time using the app");
                settings.Add("locSearch", false);
                settings.Save();
            }

            if (locSearch.IsChecked == true)
            {
                AroundMeMap.Visibility = System.Windows.Visibility.Visible;
                UpdateMap();
            }
            else
                AroundMeMap.Visibility = System.Windows.Visibility.Collapsed;

        }

        private async void UpdateMap()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            //SetProgressIndicator(true);
            //SystemTray.ProgressIndicator.Text = "Getting GPS Location";

            try
            {
                Geoposition position =
                    await geolocator.GetGeopositionAsync(
                    TimeSpan.FromMinutes(1),
                    TimeSpan.FromSeconds(30));

                //SystemTray.ProgressIndicator.Text = "Acquired";

                var gpsCoorCenter =
                    new GeoCoordinate(
                        position.Coordinate.Latitude,
                        position.Coordinate.Longitude);

                AroundMeMap.Center = gpsCoorCenter;
                AroundMeMap.ZoomLevel = 15;

                //SetProgressIndicator(false);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Location is disable in phone settings.");
                locSearch.IsChecked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void SetProgressIndicator(bool isVisible)
        {
            SystemTray.ProgressIndicator.IsIndeterminate = isVisible;
            SystemTray.ProgressIndicator.IsVisible = isVisible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) 
        { 
            System.Diagnostics.Debug.WriteLine("into the app"); 
            
            try 
            { 
                System.Diagnostics.Debug.WriteLine("Retrieving values"); 
                locSearch.IsChecked = (bool)settings["locSearch"]; 
                
            } 
            
            catch (KeyNotFoundException ex) 
            { 
                System.Diagnostics.Debug.WriteLine("First Time using the app");
                settings.Add("locSearch", false); 
                settings.Save(); 
            } 
        }        
        
        protected override void OnNavigatedFrom(NavigationEventArgs e) 
        { 
            System.Diagnostics.Debug.WriteLine("Exiting, so save now");
            settings["locSearch"] = locSearch.IsChecked; 
            settings.Save(); 
        }

        private void AroundMeMap_Loaded(object sender, RoutedEventArgs e)
        {
            MapsSettings.ApplicationContext.ApplicationId = "ecbaa076-b8e2-4111-95a0-996cbb7d4598";
            MapsSettings.ApplicationContext.AuthenticationToken = "yvvUmYudkHb6fR5fmQ3eeg";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void locSearch_Click(object sender, RoutedEventArgs e)
        {
            if (locSearch.IsChecked == false)
            {
                AroundMeMap.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (locSearch.IsChecked == true)
            {
                AroundMeMap.Visibility = System.Windows.Visibility.Visible;
                UpdateMap();
            }
        }

        private void Tile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var control = (sender as HubTile);
            string topic = HttpUtility.UrlEncode(control.Title);
            
            string navTo = string.Format(
                "/SearchResults.xaml?latitude={0}&longitude={1}&topic={2}&radius={3}",
                AroundMeMap.Center.Latitude,
                AroundMeMap.Center.Longitude,
                topic,
                5);

            NavigationService.Navigate(new Uri(navTo, UriKind.RelativeOrAbsolute));
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            AboutPrompt aboutMe = new AboutPrompt();
            aboutMe.Show("Super Lazy", "superlazyapps", "superlazyapps@live.in", "http://www.superlazy.tk");
        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            //string navTo = "https://iqdrga.bn1.livefilestore.com/y2meuhzrw5zau4mXl_gqrgMknwRAM-OkBx66etkKnRVz--veJFQGEa37c-tCerOl-sHq9rLjDY1Mll6H5b54gVycEdc4Lu4xPsH3NipL-16cXg/PP.txt";
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}