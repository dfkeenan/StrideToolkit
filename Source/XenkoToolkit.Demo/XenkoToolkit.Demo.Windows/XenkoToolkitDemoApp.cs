using SiliconStudio.Xenko.Engine;

namespace XenkoToolkit
{
    class XenkoToolkitDemoApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
