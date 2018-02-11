using Xamarin.Forms;

namespace XamTools.Pages
{
    public abstract class BasePage : ContentPage
    {
        public static readonly BindableProperty NeedOverrideSoftBackButtonProperty =
            BindableProperty.Create(nameof(NeedOverrideSoftBackButton), typeof(bool), typeof(BasePage), false);

        public static readonly BindableProperty OverrideBackButtonProperty =
            BindableProperty.Create(nameof(OverrideBackButton), typeof(bool), typeof(BasePage), false);

        public static readonly BindableProperty OverrideBackTextProperty =
            BindableProperty.Create(nameof(OverrideBackText), typeof(string), typeof(BasePage), "Back");

        public bool NeedOverrideSoftBackButton
        {
            get => (bool) GetValue(NeedOverrideSoftBackButtonProperty);
            set => SetValue(NeedOverrideSoftBackButtonProperty, value);
        }

        public bool OverrideBackButton
        {
            get => (bool) GetValue(OverrideBackButtonProperty);
            set => SetValue(OverrideBackButtonProperty, value);
        }

        public string OverrideBackText
        {
            get => (string) GetValue(OverrideBackTextProperty);
            set => SetValue(OverrideBackTextProperty, value);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is BaseVM bindingContext)
                bindingContext.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is BaseVM bindingContext)
                bindingContext.OnDisappearing();
        }

        public void OnSoftBackButtonPressed()
        {
            var bindingContext = BindingContext as BaseVM;
            bindingContext?.OnSoftBackButtonPressed();
        }
    }
}
