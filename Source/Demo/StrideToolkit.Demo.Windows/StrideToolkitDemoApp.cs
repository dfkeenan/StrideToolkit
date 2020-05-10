using Stride.Engine;

namespace StrideToolkit.Demo
{
    class StrideToolkitDemoApp
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
