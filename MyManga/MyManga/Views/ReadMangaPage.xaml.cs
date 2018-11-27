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
	public partial class ReadMangaPage : ContentPage
	{
        private MangaResult _manga;
        private ChapterDetailResult _chapter;

        public ReadMangaPage ()
		{
			InitializeComponent ();
		}
        /*public ReadMangaPage(MangaResult manga, ChapterDetailResult chapter)
        {
            _manga = manga;
            _chapter = chapter;
            InitializeComponent();
            Title = $"{_manga.Name}: {_chapter.ShowChapter}";
        }
        private IEnumerable<MangaPage> mangaPages = new List<MangaPage>();
        protected async override void OnAppearing()
        {
            try
            {
                mangaPages = await App.InMangaService.GetListPageModels(_manga, _chapter);
                CvItems.ItemsSource = mangaPages.ToList();
            }
            catch (Utils.UnsuccessfulRequestException ex)
            {
                await DisplayAlert("Network error", ex.Message, "Ok");
            }

            base.OnAppearing();
        }*/

        private void ZoomGestureContainer_ZoomStarted(object sender, EventArgs e)
        {
            CvItems.IsPanInteractionEnabled = false;
        }

        private void ZoomGestureContainer_ZoomEnded(object sender, EventArgs e)
        {
            CvItems.IsPanInteractionEnabled = true;
        }
    }
}