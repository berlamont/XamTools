using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamTools
{
    public class ObservableBase : INotifyPropertyChanged
    {
        readonly object _syncRoot = new object();
        readonly Dictionary<string, object> _fieldValuesDictionary = new Dictionary<string, object>();
        
        public event PropertyChangedEventHandler PropertyChanged = delegate{};

        public void OnPropChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Notify(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Fires OnPropertyChanged on all properties
        /// </summary>
        protected void NotifyAll() => PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));

        /// <summary>
        /// Sets the property value and calls Notify when field value was changed. 
        /// Uses internal dictionary for field storing
        /// </summary>
        protected bool SetPropValue<T>(T value, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {

            if (_fieldValuesDictionary.ContainsKey(propertyName) && 
                EqualityComparer<T>.Default.Equals(GetPropValue<T>(propertyName), value))
                return false;

            lock (_syncRoot)
                _fieldValuesDictionary[propertyName] = value;

            onChanged?.Invoke();

            OnPropChanged(propertyName);
            return true;
        }

        protected T GetPropValue<T>([CallerMemberName] string propertyName = null)
        {
            if (_fieldValuesDictionary.TryGetValue(propertyName, out var value))
                return value == null ? default(T) : (T) value;

            return default(T);
        }

        /// <summary>
        /// Sets the field and calls Notify when field value was changed.
        /// Bypasses internal dictionary 
        /// </summary>
        protected bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                return false;

            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            
            onChanged?.Invoke();
            
            OnPropChanged(propertyName);

            return true;
        }
    }
}
