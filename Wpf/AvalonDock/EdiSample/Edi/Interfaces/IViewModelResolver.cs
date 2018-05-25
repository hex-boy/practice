using System.ComponentModel;
using Microsoft.Practices.Prism.ViewModel;


namespace Edi.Interfaces
{


    /// <summary>
    /// Interface to resolve string id into a
    /// matching viewmodel that represents a tool window or document.
    /// </summary>
    public interface IViewModelResolver
    {
        INotifyPropertyChanged GetContentViewModelFromId(string contentId);
    }
}
