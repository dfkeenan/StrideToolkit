using SiliconStudio.Core;
using SiliconStudio.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit
{
    public static class Module
    {
        [ModuleInitializer]
        public static void InitializeModule()
        {
            AssemblyRegistry.Register(typeof(Module).GetTypeInfo().Assembly, AssemblyCommonCategories.Assets);
        }
    }
}
