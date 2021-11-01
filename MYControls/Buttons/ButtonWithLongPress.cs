using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Concurrency;

namespace MYControls.Buttons
{
    public class ButtonWithLongPress : Button
    {
        IObservable<string> source;
        IDisposable disposable;

        public ButtonWithLongPress()
        {
            source = Observable.Interval(TimeSpan.FromSeconds(0.2))
                .DelaySubscription(TimeSpan.FromSeconds(1.0))
                .Timestamp()
                .ObserveOn(DispatcherScheduler.Current)
                .Select(o => $"ClickInLongPress: {o.Timestamp.ToString()}")
                .Publish()
                .RefCount();

            PreviewMouseLeftButtonDown += (s, e) =>
            {
                Command?.Execute("First click!");
                disposable = source.Subscribe(o =>
                {
                    Command?.Execute(o);
                });
            };

            PreviewMouseLeftButtonUp += (s, e) =>
            {
                disposable.Dispose();
                ((Button)s).ReleaseMouseCapture();
                e.Handled = true;
            };
        }

    }
}
