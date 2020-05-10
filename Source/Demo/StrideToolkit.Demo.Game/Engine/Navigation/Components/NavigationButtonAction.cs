using Stride.Core;
using System.Threading.Tasks;

namespace StrideToolkit.Engine.Navigation.Components
{
    [DataContract]
    public abstract class NavigationButtonAction : INavigationButtonAction
    {
        public bool KeepLoaded { get; set; }
        public bool RememberCurrent { get; set; } = true;

        public abstract Task<bool> Handle(ISceneNavigationService navigationService);
        
    }

    [DataContract]
    public class NavigateToScreen : NavigationButtonAction
    {
        public string SceneUrl { get; set; }

        public override async Task<bool> Handle(ISceneNavigationService navigationService)
        {
            if (navigationService == null)
            {
                throw new System.ArgumentNullException(nameof(navigationService));
            }

            return await navigationService.NavigateAsync(SceneUrl, KeepLoaded, RememberCurrent);
        }
    }

    [DataContract]
    public class NavigateBack : NavigationButtonAction
    {
        public override async Task<bool> Handle(ISceneNavigationService navigationService)
        {
            if (navigationService == null)
            {
                throw new System.ArgumentNullException(nameof(navigationService));
            }

            return await navigationService.GoBackAsync(RememberCurrent);
        }
    }

    [DataContract]
    public class NavigateForward : NavigationButtonAction
    {
        public override async Task<bool> Handle(ISceneNavigationService navigationService)
        {
            if (navigationService == null)
            {
                throw new System.ArgumentNullException(nameof(navigationService));
            }

            return await navigationService.GoForwardAsync(RememberCurrent);
        }
    }
}