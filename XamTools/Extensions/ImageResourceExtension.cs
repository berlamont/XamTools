using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eoTouchx.Extensions
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension<ImageSource>
    {
        public Type AssemblyResolverType { get; set; }

        public string Source { get; set; }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) =>
            (this as IMarkupExtension<ImageSource>).ProvideValue(serviceProvider);

        public ImageSource ProvideValue(IServiceProvider serviceProvider)
        {
            Assembly assembly = null;
            if (AssemblyResolverType != null)
                assembly = AssemblyResolverType.GetTypeInfo().Assembly;
            else
            {
                var app = Application.Current;
                if (app != null)
                    assembly = app.GetType().GetTypeInfo().Assembly;
            }

            return Source == null ? null : ImageSource.FromResource(Source, assembly);
        }
    }
}