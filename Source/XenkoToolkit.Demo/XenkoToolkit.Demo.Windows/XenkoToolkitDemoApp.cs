using Xenko.Engine;

namespace XenkoToolkit.Demo
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
