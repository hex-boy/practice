namespace Edi.Events
{
  using System;
  using Microsoft.Practices.Composite.Events;
  using Microsoft.Practices.Composite.Presentation.Events;

  /// <summary>
  /// Class implements a simple PRISM LoadLayout string event
  /// 
  /// Sources:
  /// http://www.codeproject.com/Tips/591221/Simple-EventAggregator-in-WPF-PRISM-4
  /// http://stackoverflow.com/questions/11254032/how-to-return-data-from-a-subscribed-method-using-eventaggregator-and-microsoft
  /// </summary>
  public class SaveLayoutEvent : CompositePresentationEvent<SaveLayoutEventArgs>
  {
      /// <summary>
    /// Static class constructor
    /// </summary>
    static SaveLayoutEvent()
      {
          var eventAggregator = new EventAggregator();
          Instance = eventAggregator.GetEvent<SaveLayoutEvent>();
      }

    /// <summary>
    /// Get static instance of this class type
    /// </summary>
    public static SaveLayoutEvent Instance { get; }
  }
}
