using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenkoToolkit.Collections;

namespace XenkoToolkit.Engine.Navigation.Components
{
    public class SceneNavigationService : StartupScript, ISceneNavigationService
    {

        public Scene StartScene { get; set; }
        public bool KeepStartSceneLoaded { get; set; }
        public bool IsNavigating { get; private set; }

        private readonly List<SceneHistoryItem> back = new List<SceneHistoryItem>();
        private readonly List<SceneHistoryItem> forward = new List<SceneHistoryItem>();
        private SceneHistoryItem currentItem = default(SceneHistoryItem);


        public override void Start()
        {
            Game.Services.AddService<ISceneNavigationService>(this);

            if (StartScene != null)
            {
                var navTo = new SceneHistoryItem
                {
                    Scene = StartScene,
                    KeepLoaded = KeepStartSceneLoaded,
                };

                if (!Content.TryGetAssetUrl(StartScene,out navTo.AssetName) && KeepStartSceneLoaded)
                {
                    Log.Warning("Start Scene must be an Asset.");
                }

                Navigate(navTo, false);
            }
        }

        public override void Cancel()
        {
            Game.Services.RemoveService<ISceneNavigationService>();

            ClearHistory();

            if (currentItem.Scene != null)
            {
                SceneSystem.SceneInstance.RootScene.Children.Remove(currentItem.Scene);

                if(currentItem.Scene != StartScene)
                {
                    Content.Unload(currentItem.Scene);
                }
            }

            currentItem = default(SceneHistoryItem);
        }

        public void ClearHistory()
        {
            foreach (var scene in back.Select(s => s.Scene).Where(s => s != null && s != StartScene))
            {
                Content.Unload(scene);
            }

            back.Clear();

            foreach (var scene in forward.Select(s => s.Scene).Where(s => s != null && s != StartScene))
            {
                Content.Unload(scene);
            }

            forward.Clear();
        }

        public async Task<bool> NavigateAsync(string url, bool keepLoaded = false, bool rememberCurrent = true)
        {
            if (IsNavigating) return false;

            IsNavigating = true;

            if (!Content.Exists(url)) return false;

            var navTo = new SceneHistoryItem
            {
                Scene = await Content.LoadAsync<Scene>(url),
                AssetName = url,
                KeepLoaded = keepLoaded,
            }; 

            Navigate(navTo, rememberCurrent);

            IsNavigating = false;
            return true;
        }

        private void Navigate(SceneHistoryItem navTo, bool rememberCurrent)
        {
            if(currentItem.Scene != null)
            {
                SceneSystem.SceneInstance.RootScene.Children.Remove(currentItem.Scene);

                if (!currentItem.KeepLoaded)
                {
                    Content.Unload(currentItem.Scene);
                    currentItem.Scene = null;
                }

                if (rememberCurrent)
                {
                    back.Push(currentItem);
                }              
            }

            SceneSystem.SceneInstance.RootScene.Children.Add(navTo.Scene);

            currentItem = navTo;
        }

        public bool CanGoBack => back.Count > 0;

        public bool CanGoForward => forward.Count > 0;

        

        public async Task<bool> GoBackAsync(bool rememberCurrent = true)
        {
            if (IsNavigating) return false;

            IsNavigating = true;

            if (!CanGoBack)
            {
                IsNavigating = false;
                return false;
            }

            var navTo = back.Pop();

            if (navTo.Scene == null)
            {
                navTo.Scene = await Content.LoadAsync<Scene>(navTo.AssetName);
            }

            SceneSystem.SceneInstance.RootScene.Children.Remove(currentItem.Scene);

            if (!currentItem.KeepLoaded)
            {
                Content.Unload(currentItem.Scene);
                currentItem.Scene = null;
            }

            if (rememberCurrent)
            {
                forward.Push(currentItem);
            }

            SceneSystem.SceneInstance.RootScene.Children.Add(navTo.Scene);

            currentItem = navTo;
            IsNavigating = false;
            return true;
        }

        public async Task<bool> GoForwardAsync(bool rememberCurrent = true)
        {
            if (IsNavigating) return false;

            IsNavigating = true;

            if (!CanGoForward) return false;

            var navTo = forward.Pop();

            if (navTo.Scene == null)
            {
                navTo.Scene = await Content.LoadAsync<Scene>(navTo.AssetName);
            }

            SceneSystem.SceneInstance.RootScene.Children.Remove(currentItem.Scene);

            if (!currentItem.KeepLoaded)
            {
                Content.Unload(currentItem.Scene);
                currentItem.Scene = null;
            }

            if (rememberCurrent)
            {
                back.Push(currentItem);
            }

            SceneSystem.SceneInstance.RootScene.Children.Add(navTo.Scene);

            currentItem = navTo;
            IsNavigating = false;
            return true;
        }

        private struct SceneHistoryItem
        {
            public Scene Scene;
            public string AssetName;
            public bool KeepLoaded;
        }
    }
}