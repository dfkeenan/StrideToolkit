using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.UI.Controls;
using StrideToolkit.Engine.Navigation.Components;
using Stride.Core;

namespace StrideToolkit.Engine.Navigation.Components
{
    [Display("Navigation Button Handler")]
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

        private void Button_Click(object sender, Stride.UI.Events.RoutedEventArgs e)
        {           
            var navService = Game.Services.GetService<ISceneNavigationService>();

            if(navService != null && !navService.IsNavigating)
            {
                Script.AddTask(() => ButtonAction?.Handle(navService));
            }

        }

        public override void Update()
        {
            // Do stuff every new frame
        }

        public override void Cancel()
        {
            if (Page?.RootElement.FindName(ButtonName) is Button button)
            {
                button.Click -= Button_Click;

            }
        }
    }
}
