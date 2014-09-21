using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Animation;
using AroundMe.Resources;
using AroundMe.Core;

namespace AroundMe
{
    public partial class SearchResults : PhoneApplicationPage
    {
        private double _latitude;
        private double _longitude;
        private double _radius;
        private string _topic;

        // Your API key ... REPLACE THIS WITH YOURS:
        // http://www.flickr.com/services/api/keys/
        private const string FlickrApiKey = "7d5f185549fc180222065b9e0e2dcecd";
        /*
         * Key:
7d5f185549fc180222065b9e0e2dcecd 
        old:8244c11a9c3b02b45c127873ac225958
Secret:
ec568404ed131167 
*/

        public SearchResults()
        {
            InitializeComponent();

            Loaded += SearchResults_Loaded;

            BuildLocalizedApplicationBar();
        }

        protected async void SearchResults_Loaded(object sender, RoutedEventArgs e)
        {
            Overlay.Visibility = Visibility.Visible;
            OverlayProgressBar.IsIndeterminate = true;

            var images = await FlickrImage.GetFlickrImages(
                FlickrApiKey, 
                _topic,
                _latitude, 
                _longitude,
                _radius 
                );

            DataContext = images;

            if (images.Count == 0)
                NoPhotosFound.Visibility = Visibility.Visible;
            else
                NoPhotosFound.Visibility = Visibility.Collapsed;

            Overlay.Visibility = Visibility.Collapsed;
            OverlayProgressBar.IsIndeterminate = false;
        }

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsVisible = false;

            // Create a new button and set the text value to the 
            //localized string from AppResources.
            ApplicationBarIconButton appBarButton = 
                new ApplicationBarIconButton(
                    new Uri("/Toolkit.Content/ApplicationBar.Check.png", 
                        UriKind.RelativeOrAbsolute));

            appBarButton.Text = AppResources.AppBarSet;
            appBarButton.Click += appBarButton_Click;

            ApplicationBar.Buttons.Add(appBarButton);
        }

        private async void appBarButton_Click(object sender, EventArgs e)
        {
            // Get a list of selected images
            List<FlickrImage> imgs = new List<FlickrImage>();

            foreach (object item in PhotosForLockscreen.SelectedItems)
            {
                FlickrImage img = item as FlickrImage;

                if (img != null)
                    imgs.Add(img);
            }

            // Clean out / remove all images currently in IsolatedStorage
            LockScreenHelpers.CleanStorage();

            // Save this new list of selected images to IsolatedStorage
            LockScreenHelpers.SaveSelectedBackgroundScreens(imgs);

            // Randomly select one item and set it as the lockscreen
            await LockScreenHelpers.SetRandomImageFromLocalStorage();

            // Test by hitting F12 twice in the emulator

            MessageBox.Show("You have a new background!", "Set!", MessageBoxButton.OK);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _latitude = Convert.ToDouble(NavigationContext.QueryString["latitude"]);
            _longitude = Convert.ToDouble(NavigationContext.QueryString["longitude"]);
            _radius = Convert.ToDouble(NavigationContext.QueryString["radius"]);
            _topic = NavigationContext.QueryString["topic"];
        }

        private void PhotosForLockscreen_SelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
        {
            if (PhotosForLockscreen.SelectedItems.Count == 0)
                ApplicationBar.IsVisible = false;
            else
                ApplicationBar.IsVisible = true;
        }

        private void Image_ImageOpened(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;

            if (img == null)
                return;

            // using System.Windows.Media.Animation
            Storyboard s = new Storyboard();

            DoubleAnimation doubleAni = new DoubleAnimation();
            doubleAni.To = 1;
            doubleAni.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            Storyboard.SetTarget(doubleAni, img);
            Storyboard.SetTargetProperty(doubleAni, new PropertyPath(OpacityProperty));

            s.Children.Add(doubleAni);

            s.Begin();
        }
    }
}