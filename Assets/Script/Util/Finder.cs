namespace Harmony
{
  public static partial class Finder
  {
    private static SceneBundleLoader findableSceneBundleLoader = null;

    public static SceneBundleLoader SceneBundleLoader
    {
      get
      {
        if (!findableSceneBundleLoader)
          findableSceneBundleLoader = FindWithTag<SceneBundleLoader>(Tags.MainController);

        return findableSceneBundleLoader;
      }
    }

    private static Player findablePlayer = null;
    public static Player Player
    {
      get
      {
        if (!findablePlayer)
          findablePlayer = FindWithTag<Player>(Tags.Player);

        return findablePlayer;
      }
    }
  }
}