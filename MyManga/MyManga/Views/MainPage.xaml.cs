using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace MyManga.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            SearchFrame.TranslateTo(transition, 0,1);
            SearchFrame.FadeTo(0, 1);
            SearchFrame.IsVisible = false;
        }
        private double transition = (DeviceDisplay.ScreenMetrics.Width / DeviceDisplay.ScreenMetrics.Density) / 2;
        private async void Search_Tapped(object sender, EventArgs e)
        {
            SearchFrame.IsVisible = true;
            TitleControls.FadeTo(0, 200);
            await SearchIndicator.ScaleTo(1.4, 85);
            SearchIndicator.ScaleTo(1, 85);
            SearchFrame.FadeTo(1,170);
            await SearchFrame.TranslateTo(0, 0, 200);
            SearchBar.Focus();
        }

        private async void SearchBar_Unfocused(object sender, FocusEventArgs e)
        {
            SearchFrame.TranslateTo(transition, 0, 200);
            TitleControls.FadeTo(1, 200);
            await SearchFrame.FadeTo(0, 200);
            SearchFrame.IsVisible = false;
        }        
    }
}