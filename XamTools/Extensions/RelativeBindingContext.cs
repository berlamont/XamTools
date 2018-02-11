using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eoTouchx.Extensions
{
	/// <summary>
	///     Markup extension to simplify syntax required to forward a different BindingContext from an other element.
	/// </summary>
	[ContentProperty("Name")]
    public class RelativeBindingContext : IMarkupExtension
    {
        BindableObject _associatedObject;

        public RelativeBindingContext()
        {
            TrackBindingChanges = true;
        }

        /// <summary>
        ///     Target XAML Element to grab the BindingContext from.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Track binding changes
        /// </summary>
        public bool TrackBindingChanges { get; set; }

        /// <summary>
        ///     Retrieves the BindingContext from the named element.
        /// </summary>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            Debug.Assert(serviceProvider != null);
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException(nameof(serviceProvider), "RelativeBindingContext: Name must be provided.");

            var pvt = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var rootProvider = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            Debug.Assert((pvt != null) && (rootProvider != null));

            // INFO: Xam Forms does not currently implement the 'TargetProperty'
            // value from IProvideValueTarget. That means we cannot tell what propery is
            // being assigned here, and by default we assume the BindingContext. 
            // However you can manually switch that off through the TrackBindingChanges property on this extension.

            var root = rootProvider.RootObject as Element;
            var namedElement = root?.FindByName<Element>(Name);
            if (namedElement == null)
                return null;

            if (!TrackBindingChanges)
                return namedElement.BindingContext;

            _associatedObject = pvt.TargetObject as BindableObject;

            if (_associatedObject == null)
                return namedElement.BindingContext;

            _associatedObject.BindingContext = namedElement.BindingContext;
            namedElement.BindingContextChanged += OnBindingContextChanged;
            return namedElement.BindingContext;
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            if (_associatedObject != null)
                _associatedObject.BindingContext = ((BindableObject) sender).BindingContext;
        }
    }
}