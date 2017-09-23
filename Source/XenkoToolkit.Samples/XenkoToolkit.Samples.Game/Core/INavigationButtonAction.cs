using System.Threading.Tasks;

namespace XenkoToolkit.Samples.Core
{
    public interface INavigationButtonAction
    {
        Task<bool> Handle(ISceneNavigationService navigationService);
    }
}