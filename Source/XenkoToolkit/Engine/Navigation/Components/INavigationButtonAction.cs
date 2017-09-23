using System.Threading.Tasks;

namespace XenkoToolkit.Engine.Navigation.Components
{
    public interface INavigationButtonAction
    {
        Task<bool> Handle(ISceneNavigationService navigationService);
    }
}