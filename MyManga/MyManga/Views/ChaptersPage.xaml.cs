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
	public partial class ChaptersPage : ContentPage
	{
        private MangaResult _manga;

        public ChaptersPage()
		{
			InitializeComponent ();
		}
    }
}