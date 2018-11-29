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
	public class ChaptersPageViewModel : ViewModelListView
    {
        public ChaptersPageViewModel(INavigationService navigationService, IInMangaService inMangaService)
            : base(navigationService)
        {
            _inMangaService = inMangaService;
        }

        //Dependency Fields
        private IInMangaService _inMangaService;

        //Bindeable Props
        private ObservableCollection<ChapterDetailResult> _chapterResults = new ObservableCollection<ChapterDetailResult>();
        public ObservableCollection<ChapterDetailResult> ChapterResults
        {
            get { return _chapterResults; }
            set { SetProperty(ref _chapterResults, value); }
        }

        //utils fields
        private MangaResult _mangaResult;

        //Commands
        public DelegateCommand<ChapterDetailResult> SelectChapterCommand { get; private set; }
        void SelectChapter(ChapterDetailResult parameter)
        {

        }
        bool CanSelect(object parameter)
        {
            return true;
        }


        public async Task InnitChapterListAsync(MangaResult manga)
        {
            IsListRefreshing = true;
            try
            {                
                ChapterResults = new ObservableCollection<ChapterDetailResult>(await _inMangaService.GetMangaDetails(manga.Identification));
            }
            catch (Utils.UnsuccessfulRequestException ex)
            {
                //A form to notify error
            }
            IsListRefreshing = false;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            _mangaResult = parameters.GetValue<MangaResult>("manga");
            Title = _mangaResult.Name;
            await InnitChapterListAsync(_mangaResult);            
            base.OnNavigatedTo(parameters);
        }
    }
}
