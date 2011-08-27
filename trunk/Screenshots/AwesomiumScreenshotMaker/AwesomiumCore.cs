using AwesomiumSharp;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker
{
    public class AwesomiumCore
    {
        public static void Initialze()
        {
            WebCore.Initialize(new WebCoreConfig { CustomCSS = "::-webkit-scrollbar { visibility: hidden; }" });
        }

        public static void Shutdown()
        {
            WebCore.Shutdown();
        }
    }
}