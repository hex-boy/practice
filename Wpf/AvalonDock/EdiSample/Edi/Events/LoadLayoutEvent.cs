namespace Edi.Events
{
  using Microsoft.Practices.Composite.Events;
  using Microsoft.Practices.Composite.Presentation.Events;

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
