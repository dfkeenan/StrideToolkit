using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Input;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.UI.Controls;
using SiliconStudio.Xenko.UI.Events;
using SiliconStudio.Core;
using XenkoToolkit.Samples.Core;

namespace XenkoToolkit.Samples
{
    public class MenuNavigation : StartupScript
    {
        public UIPage MenuPage { get; set; }

        public Dictionary<string, string> MenuScenes { get; } = new Dictionary<string, string>();

        private bool isNavigating = false;

        public override void Start()
        {
            MenuPage?.RootElement.AddHandler(ButtonBase.ClickEvent, MenuItemClickHandler);
        }

        private async void MenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            if(!isNavigating)
            {
                isNavigating = true;

                if (e.Source is ButtonBase button && MenuScenes.TryGetValue(button.Name, out var sceneName))
                {
                    var navService = Game.Services.GetService<ISceneNavigationService>();

                    await navService.NavigateAsync(sceneName);
                }
                isNavigating = false;
            }           

            
        }
    }
}
