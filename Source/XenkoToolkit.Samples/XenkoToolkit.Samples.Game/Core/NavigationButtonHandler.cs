using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Input;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.UI.Controls;
using XenkoToolkit.Samples.Core;

namespace XenkoToolkit.Samples.Core
{
    public class NavigationButtonHandler : SyncScript
    {
        public UIPage Page { get; set; }

        public string ButtonName { get; set; }

        public INavigationButtonAction ButtonAction { get; set; } = new NavigateToScreen();

        public override void Start()
        {           

            Page = Page ?? this.Entity.Get<UIComponent>()?.Page;

            if (string.IsNullOrEmpty(ButtonName) || ButtonAction == null) return;

            // Initialization of the script.
            if (Page?.RootElement.FindName(ButtonName) is Button button)
            {
                button.Click += Button_Click;
                
            }
        }

        private async void Button_Click(object sender, SiliconStudio.Xenko.UI.Events.RoutedEventArgs e)
        {           
            var navService = Game.Services.GetService<ISceneNavigationService>();

            await ButtonAction?.Handle(navService);
        }

        public override void Update()
        {
            // Do stuff every new frame
        }
    }
}
