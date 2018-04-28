using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Xenko.Engine;

namespace XenkoToolkit.Engine
{
    internal readonly struct ScriptDelegateWatcher
    {
        private readonly ScriptComponent script;

        public ScriptDelegateWatcher(Delegate delgate)
        {
            if (delgate == null)
            {
                throw new ArgumentNullException(nameof(delgate));
            }

            var invocationList = delgate.GetInvocationList();

            if (invocationList.Length == 1 && invocationList[0].Target is ScriptComponent scriptComponent)
            {
                script = scriptComponent;
            }
            else
            {
                script = null;
            }

        }

        public bool IsActive => script == null || (script.Entity != null && (script.SceneSystem?.SceneInstance?.Contains(script.Entity) == true));
    }
}
