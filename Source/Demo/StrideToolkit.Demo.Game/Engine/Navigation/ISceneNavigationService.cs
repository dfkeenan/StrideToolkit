using System.Threading.Tasks;
using Stride.Engine;

namespace StrideToolkit.Engine.Navigation
{
    public interface ISceneNavigationService 
    {
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        bool IsNavigating { get; }

        void ClearHistory();
        Task<bool> GoBackAsync(bool rememberCurrent = true);
        Task<bool> GoForwardAsync(bool rememberCurrent = true);
        Task<bool> NavigateAsync(string url, bool keepLoaded = false, bool rememberCurrent = true);
    }
}