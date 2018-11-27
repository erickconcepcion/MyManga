using MyManga.InMangaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyManga.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CapitulosPage : ContentPage
	{
        private MangaResult _manga;

        public CapitulosPage ()
		{
			InitializeComponent ();
		}
        /*public CapitulosPage(MangaResult manga)
        {
            _manga = manga;
            InitializeComponent();
            Title = _manga.Name;
        }
        private IEnumerable<ChapterDetailResult> chapterResults = new List<ChapterDetailResult>();
        protected async override void OnAppearing()
        {
            try
            {
                var res = await App.InMangaService.GetMangaDetails(_manga.Identification);
                chapterResults = res.result.OrderByDescending(c => c.Number);
                ChapterList.ItemsSource = chapterResults;
            }
            catch (Utils.UnsuccessfulRequestException ex)
            {
                await DisplayAlert("Network error", ex.Message, "Ok");
            }
            
            base.OnAppearing();
        }*/
        private async void MangaList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            /*await Navigation.PushAsync(new ReadMangaPage(_manga, ChapterList.SelectedItem as ChapterDetailResult));
            ChapterList.SelectedItem = null;*/
        }
    }
}