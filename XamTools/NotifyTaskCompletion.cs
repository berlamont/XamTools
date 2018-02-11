using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace XamTools
{
    public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        public NotifyTaskCompletion(Func<Task<TResult>> task)
        {
            if (Task == null)
                TaskCompletion = WatchTask(task);
        }

        public Task<TResult> Task { get; private set; }

        public Task TaskCompletion { get; set; }

        public TaskStatus Status => Task.Status;

        public bool IsCompleted => Task.IsCompleted;

        public bool IsNotCompleted => !Task.IsCompleted;

        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;

        public bool IsCanceled => Task.IsCanceled;

        public bool IsFaulted => Task.IsFaulted;

        public AggregateException Exception => Task.Exception;

        public Exception InnerException => Exception?.InnerException;

        public string ErrorMessage => InnerException?.Message;

        public TResult Result => Task.Status == TaskStatus.RanToCompletion ? Task.Result : default(TResult);

        public event PropertyChangedEventHandler PropertyChanged;

        async Task WatchTask(Func<Task<TResult>> task)
        {
            try
            {
                Task = task();
                await Task;
            }
            catch { }

            OnTaskCompletion();
        }

        void OnTaskCompletion()
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged(this, new PropertyChangedEventArgs("Status"));
            propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
            propertyChanged(this, new PropertyChangedEventArgs("IsNotCompleted"));
            if (Task.IsCanceled)
                propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
            else if (Task.IsFaulted)
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
                propertyChanged(this, new PropertyChangedEventArgs("Exception"));
                propertyChanged(this, new PropertyChangedEventArgs("InnerException"));
                propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
            }
            else
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
                propertyChanged(this, new PropertyChangedEventArgs("Result"));
            }
        }
    }
}