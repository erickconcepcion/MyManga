using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyManga.ViewModels
{
    public class ViewModelListView: ViewModelBase
    {
        public ViewModelListView(INavigationService navigationService)
            : base(navigationService)
        {

        }
        private bool _isListRefreshing = false;
        public bool IsListRefreshing
        {
            get { return _isListRefreshing; }
            set { SetProperty(ref _isListRefreshing, value); }
        }
    }
}
