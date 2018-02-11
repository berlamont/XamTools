using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using eoTouchx.VMs;
using Xamarin.Forms;

namespace eoTouchx.Helpers
{
    public static class PageService
    {

        static Dictionary<string, Lazy<BaseVM>> _vmCache = new Dictionary<string, Lazy<BaseVM>>();
        static Dictionary<string, Lazy<Page>> _pageCache = new Dictionary<string, Lazy<Page>>();

        public static Page Get(Type boundpage, Type boundvm)
        {
            var page = boundpage;
            var vm = boundvm;
            
            BaseVM GetVM(Type targetVm)
            {
                if (_vmCache.TryGetValue(nameof(vm), out var vmObject))
                    return vmObject.Value;
                
                return (BaseVM) Activator.CreateInstance(targetVm);
            }

            Page GetPage(Type targetPage)
            {
                if (_pageCache.TryGetValue(nameof(page), out var pageObject))
                    return pageObject.Value;

                return (Page)Activator.CreateInstance(targetPage);
            }

            var boundPage = GetPage(page);
            boundPage.BindingContext = GetVM(vm);

            return boundPage;
        }

    }

    public static class ViewModelFactory
    {
        public static TViewModel Create<TViewModel>()
        {
            var viewModelConstructor = InstanceCreatorOfViewModel<TViewModel>();

            return viewModelConstructor();
        }

        static Func<TViewModel> InstanceCreatorOfViewModel<TViewModel>()
        {
            var ctor = typeof(TViewModel).GetConstructor(Type.EmptyTypes);
            return ctor == null ? null : Expression.Lambda<Func<TViewModel>>(Expression.New(ctor)).Compile();
        }
    }

    public static class Instantiator
    {
        static Dictionary<Type, Func<object>> _compiledExpressions = new Dictionary<Type, Func<object>>();

        static Dictionary<Tuple<Type, Type>, Func<object>> _genericCompiledExpressions =
            new Dictionary<Tuple<Type, Type>, Func<object>>();

        public static object CreateGenericInstance(Type genericTypeDefinition, Type genericParameter)
        {
            var key = Tuple.Create(genericTypeDefinition, genericParameter);

            if (_genericCompiledExpressions.TryGetValue(key, out var instance))
                return instance();

            var genericType = genericTypeDefinition.MakeGenericType(genericParameter);

            instance = Expression.Lambda<Func<object>>(Expression.New(genericType)).Compile();

            _genericCompiledExpressions.Add(key, instance);

            return instance();
        }

        public static T CreateInstance<T>() => (T)CreateInstance(typeof(T));

        public static object CreateInstance(Type typeDefinition)
        {
            if (_compiledExpressions.TryGetValue(typeDefinition, out var instance))
                return instance();

            instance = Expression.Lambda<Func<object>>(Expression.New(typeDefinition)).Compile();

            _compiledExpressions.Add(typeDefinition, instance);

            return instance();
        }
    }
}
