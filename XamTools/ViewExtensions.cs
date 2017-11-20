using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamTools
{
    public static class ViewExtensions
    {
        public static void AddTapGesture(this View view, string modelCommandName, object param = null)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.SetBinding(TapGestureRecognizer.CommandProperty, modelCommandName);
            tapGesture.CommandParameter = param;
            view.GestureRecognizers.Add(tapGesture);
        }

        public static void Layout<TView>(this TView view, double x, double y, double width, double height)
            where TView : View =>
            view.Layout(new Rectangle(x, y, width, height));

        public static Task<bool> LayoutTo<TView>(this TView view, double x, double y, double width, double height,
            uint lenght = 250, Easing easing = null) where TView : View =>
            view.LayoutTo(new Rectangle(x, y, width, height), lenght, easing);

        public static TView WithAbsBounds<TView>(this TView view, double x, double y, double width, double height)
            where TView : View
        {
            AbsoluteLayout.SetLayoutBounds(view, new Rectangle(x, y, width, height));
            return view;
        }

        public static TView WithAbsFlags<TView>(this TView view, AbsoluteLayoutFlags flags) where TView : View
        {
            AbsoluteLayout.SetLayoutFlags(view, flags);
            return view;
        }

        public static TView WithBinding<TView>(this TView view, BindableProperty bindProp, string propName,
            BindingMode mode = BindingMode.Default) where TView : View
        {
            view.SetBinding(bindProp, propName, mode);
            return view;
        }

        public static TView WithClickHandler<TView>(this TView view, Action action) where TView : Button
        {
            view.Clicked += (sender, e) => action?.Invoke();
            return view;
        }

        public static TView WithGesture<TView>(this TView view, GestureRecognizer gesture) where TView : View
        {
            view.GestureRecognizers.Add(gesture);
            return view;
        }

        public static TView WithTap<TView>(this TView view, string modelCommandName, object param = null)
            where TView : View
        {
            view.AddTapGesture(modelCommandName, param);
            return view;
        }

        public static TView WithTap<TView>(this TView view, Action<TView> action) where TView : View
        {
            var gesture = new TapGestureRecognizer();
            gesture.Tapped += (sender, e) => action?.Invoke(view);
            view.WithGesture(gesture);
            return view;
        }

        public static T FindParent<T>(this Element element, bool isNearest = true) where T : class
        {
            T result = null;
            while (element != null)
            {
                if (element is T)
                {
                    result = element as T;
                    if (isNearest)
                        break;
                }
                element = element.Parent;
            }
            return result;
        }
    }
}