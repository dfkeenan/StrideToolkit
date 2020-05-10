using System.Threading.Tasks;

namespace StrideToolkit.Engine.Navigation.Components
{
    public interface INavigationButtonAction
    {
        Task<bool> Handle(ISceneNavigationService navigationService);
    }
}