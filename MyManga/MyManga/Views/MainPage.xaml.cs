using MyManga.InMangaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyManga.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {            
            InitializeComponent();
        }
        private IEnumerable<MangaResult> mangaResults = new List<MangaResult>();
        protected override async void OnAppearing()
        {
            try
            {
                MangaList.ItemsSource = mangaResults = await App.InMangaService.GetAllMangaResponse();
            }
            catch (Utils.UnsuccessfulRequestException ex)
            {
                await DisplayAlert("Network error", ex.Message, "Ok");
            }
            base.OnAppearing();
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            MangaList.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                MangaList.ItemsSource = mangaResults;
            }
            else
            {
                MangaList.ItemsSource = mangaResults.Where(x =>x.Name.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            MangaList.EndRefresh();
        }

        private async void MangaList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new CapitulosPage(MangaList.SelectedItem as MangaResult));
            MangaList.SelectedItem = null;
        }
    }
}
