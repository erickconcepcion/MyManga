using MyManga.Infrastructure.Services;
using MyManga.InMangaModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyManga.ViewModels
{
	public class ReadMangaPageViewModel : ViewModelBase
	{
        //dependency field
        private INavigationService _navigationService;
        private IInMangaService _inMangaService;

        public ReadMangaPageViewModel(INavigationService navigationService, IInMangaService inMangaService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _inMangaService = inMangaService;
        }

        //bindable props
        private ObservableCollection<MangaPage> _mangaPages = new ObservableCollection<MangaPage>();
        public ObservableCollection<MangaPage> MangaPages
        {
            get { return _mangaPages; }
            set { SetProperty(ref _mangaPages, value); }
        }

        private bool _onGoing = true;
        public bool OnGoing
        {
            get { return _onGoing; }
            set { SetProperty(ref _onGoing, value); }
        }

        //utils fields
        private MangaResult _mangaResult;
        private ChapterDetailResult _chapterDetail;
        
        public async Task InnitMangaPageCarouselAsync(MangaResult manga, ChapterDetailResult chapter)
        {
            try
            {
                MangaPages = new ObservableCollection<MangaPage>(await _inMangaService.GetListPageModels(manga, chapter));
            }
            catch (Utils.UnsuccessfulRequestException ex)
            {
                //A form to notify error
            }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            OnGoing = true;
            base.OnNavigatedTo(parameters);
            _mangaResult = parameters.GetValue<MangaResult>("manga");
            _chapterDetail = parameters.GetValue<ChapterDetailResult>("chapter");
            Title = $"{_mangaResult.Name}: {_chapterDetail.ShowChapter}";
            await InnitMangaPageCarouselAsync(_mangaResult, _chapterDetail);
            OnGoing = false;
        }

    }
}
