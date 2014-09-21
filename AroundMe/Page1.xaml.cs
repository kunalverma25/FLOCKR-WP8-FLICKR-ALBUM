using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace AroundMe
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            string navTo = "https://iqdrga.bn1.livefilestore.com/y2meuhzrw5zau4mXl_gqrgMknwRAM-OkBx66etkKnRVz--veJFQGEa37c-tCerOl-sHq9rLjDY1Mll6H5b54gVycEdc4Lu4xPsH3NipL-16cXg/PP.txt";
            //NavigationService.Navigate(new Uri(navTo, UriKind.Absolute));
            Windows.System.Launcher.LaunchUriAsync(new Uri(navTo));
        }
    }
}