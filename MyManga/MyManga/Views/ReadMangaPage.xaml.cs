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
        public ReadMangaPage ()
		{
			InitializeComponent ();
		}

        private void ZoomGestureContainer_ZoomStarted(object sender, EventArgs e)
        {
            CvMangaReader.IsPanInteractionEnabled = false;
        }

        private void ZoomGestureContainer_ZoomEnded(object sender, EventArgs e)
        {
            CvMangaReader.IsPanInteractionEnabled = true;
        }
    }
}