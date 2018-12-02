using MyManga.Infrastructure.Services;
using MyManga.InMangaModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyManga.ViewModels
{
    public class MainPageViewModel : ViewModelListView
    {
        public MainPageViewModel(INavigationService navigationService, IInMangaService inMangaService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _inMangaService = inMangaService;
            Title = "My Manga Comodo";

            //command declaration
            FilterMangaCommand = new DelegateCommand<string>(FilterManga, CanFilter);
            SelectMangaCommand = new DelegateCommand<MangaResult>(SelectManga, CanSelect);

        }
        //Dependency Fields
        private IInMangaService _inMangaService;
        private INavigationService _navigationService;

        //Bindeable Props
        private ObservableCollection<MangaResult> _mangaResults = new ObservableCollection<MangaResult>();
        public ObservableCollection<MangaResult> MangaResults
        {
            get{ return _mangaResults; }
            set{ SetProperty(ref _mangaResults, value); }
        }
        
        //Util Fields
        private IEnumerable<MangaResult> _mangaFilter = new List<MangaResult>();

        //Delegate Commands
        public DelegateCommand<string> FilterMangaCommand { get; private set; }
        void FilterManga(string parameter)
        {
            IsListRefreshing = true;
            if (string.IsNullOrWhiteSpace(parameter) || string.IsNullOrEmpty(parameter))
            {
                MangaResults = new ObservableCollection<MangaResult>(_mangaFilter);
            }
            else
            {
                MangaResults = new ObservableCollection<MangaResult>(_mangaFilter
                                   .Where(x => x.Name.ToLower().Contains(parameter.ToLower())));
            }
            IsListRefreshing = false;
        }
        bool CanFilter(string parameter)
        {
            return true;
        }

        public DelegateCommand<MangaResult> SelectMangaCommand { get; private set; }
        async void SelectManga(MangaResult parameter)
        {
            var navParams = new NavigationParameters();
            navParams.Add("manga", parameter);
            await _navigationService.NavigateAsync("ChaptersPage", navParams);
        }
        bool CanSelect(object parameter)
        {
            return true;
        }

        public async Task InnitMangaListAsync()
        {
            IsListRefreshing = true;
            try
            {
                _mangaFilter = await _inMangaService.GetAllMangaResponse();
                MangaResults = new ObservableCollection<MangaResult>(_mangaFilter);
            }
            catch (Utils.UnsuccessfulRequestException ex)
            {
                //A form to notify error
            }
            IsListRefreshing = false;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await InnitMangaListAsync();
            base.OnNavigatedTo(parameters);            
        }
        

        /*private async void MangaList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new CapitulosPage(MangaList.SelectedItem as MangaResult));
            MangaList.SelectedItem = null;
        }*/
    }
}
