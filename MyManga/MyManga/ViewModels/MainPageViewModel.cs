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
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IInMangaService inMangaService)
            : base(navigationService)
        {
            _inMangaService = inMangaService;
            Title = "My Manga Comodo";

            //command declaration
            FilterMangaCommand = new DelegateCommand<string>(FilterManga, CanFilter);
            SelectMangaCommand = new DelegateCommand<MangaResult>(SelectManga, CanSelect);

        }
        //Dependency Fields
        private IInMangaService _inMangaService;

        //Bindeable Props
        private ObservableCollection<MangaResult> _mangaResults = new ObservableCollection<MangaResult>();
        public ObservableCollection<MangaResult> MangaResults
        {
            get{ return _mangaResults; }
            set{ SetProperty(ref _mangaResults, value); }
        }

        private bool _isListRefreshing = false;
        public bool IsListRefreshing
        {
            get { return _isListRefreshing; }
            set { SetProperty(ref _isListRefreshing, value); }
        }

        
        //Util Fields
        private IEnumerable<MangaResult> mangaResults = new List<MangaResult>();

        //Delegate Commands
        public DelegateCommand<string> FilterMangaCommand { get; private set; }
        void FilterManga(string parameter)
        {
            IsListRefreshing = true;
            if (string.IsNullOrWhiteSpace(parameter) || string.IsNullOrEmpty(parameter))
            {
                MangaResults = new ObservableCollection<MangaResult>(mangaResults);
            }
            else
            {
                MangaResults = new ObservableCollection<MangaResult>(mangaResults
                                   .Where(x => x.Name.ToLower().Contains(parameter.ToLower())));
            }
            IsListRefreshing = false;
        }
        bool CanFilter(string parameter)
        {
            return true;
        }

        public DelegateCommand<MangaResult> SelectMangaCommand     { get; private set; }
        void SelectManga(MangaResult parameter)
        {
            
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
                mangaResults = await _inMangaService.GetAllMangaResponse();
                MangaResults = new ObservableCollection<MangaResult>(mangaResults);
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
