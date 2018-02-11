using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamTools.ViewModels
{
    public abstract class BaseListViewVM<T> : ObservableObject where T : class
    {
        INavigation _navigation;
        
        RelayCommand _refreshCommand;

        RelayCommand _selectItemCommand;

        protected BaseListViewVM()
        {
            CanRefresh = true;
        }

        protected BaseListViewVM(INavigation navigation = null) : this()
        {
            if (navigation != null)
                _navigation = navigation;
        }

        public RelayCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new RelayCommand(async o => await RefreshAsync()));
        
        public RelayCommand SelectItemCommand => _selectItemCommand ?? (_selectItemCommand = new RelayCommand(async item => await SelectItemAsync(item as T)));

        public ObservableRangeCollection<T> Items { get; } = new ObservableRangeCollection<T>();
        
        public INavigation CurrentNavigation => _navigation ?? App.CurrentPage.Navigation;

        public bool IsBusy
        {
            get => GetPropValue<bool>();
            set => SetPropValue(value);
        }

        public string PageTitle
        {
            get => GetPropValue<string>() ?? "";
            set => SetPropValue(value);
        }

        public string Icon
        {
            get => GetPropValue<string>() ?? "";
            set => SetPropValue(value);
        }

        public bool CanRefresh
        {
            get => GetPropValue<bool>() && !IsBusy;
            set => SetPropValue(value);
        }

        public bool IsEmpty
        {
            get => GetPropValue<bool>();
            set => SetPropValue(value);
        }

        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }

        public virtual bool OnBackButtonPressed() => false;

        public virtual void OnSoftBackButtonPressed() { }

        protected virtual async Task RefreshAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsEmpty = false;

            try
            {
                var items = await GetItemsAsync();
                Items.ReplaceRange(items);

                if (Items.Count == 0)
                    IsEmpty = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual Task SelectItemAsync(T item) => Task.FromResult(true);

        protected abstract Task<IEnumerable<T>> GetItemsAsync();
    }
}