using XamTools;
using Xamarin.Forms;

namespace XamTools.ViewModels
{
    public abstract class BaseVM : ObservableBase
    {
        protected BaseVM(INavigation navigation = null)
        {
            if(_navigation != null)
                _navigation = navigation;
        }
        
        INavigation _navigation;
        public INavigation CurrentNavigation => _navigation ?? Application.Current.MainPage.Navigation;
        
        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }

        public virtual bool OnBackButtonPressed() => false;

        public virtual void OnSoftBackButtonPressed() { }

        #region Properties
        
        public bool IsBusy
        {
            get => GetPropValue<bool>();
            set => SetPropValue(value);
        }

        public string PageTitle
        {
            get => GetPropValue<string>();
            set => SetPropValue(value);
        }

        string _icon = "icon.png";
        public string Icon
        {
            get => _icon;
            set => SetValue(ref _icon, value);
        }

        #endregion

    }

    public abstract class BaseVM<TModel> : BaseVM where TModel : class, new()
    {
        public TModel Model { get; set; } = new TModel();
        
    }
}
