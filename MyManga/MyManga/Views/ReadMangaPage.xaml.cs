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
    }
}