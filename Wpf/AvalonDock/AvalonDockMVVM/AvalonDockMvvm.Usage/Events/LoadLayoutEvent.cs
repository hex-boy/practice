using Microsoft.Practices.Prism.Events;

namespace AvalonDockMvvm.Usage.Events
{
    /// <summary>
  /// Class implements a simple PRISM LoadLayout string event
  /// </summary>
  public class LoadLayoutEvent : CompositePresentationEvent<string>
  {
      static LoadLayoutEvent()
      {
          var eventAggregator = new EventAggregator();
          Instance = eventAggregator.GetEvent<LoadLayoutEvent>();
      }

    public static LoadLayoutEvent Instance { get; }
  }
}
