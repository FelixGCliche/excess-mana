using System.Collections;
using Harmony;
using UnityEngine;
using DG.Tweening;

namespace Game
{
  [Findable(Tags.MainController)]
  public class MainController : MonoBehaviour
  {
    [Header("Scenes")] 
    [SerializeField] private SceneBundle gameScenes;
    [SerializeField] private SceneBundle homeScenes;
    [SerializeField] private SceneBundle settingsScenes;

    private SceneBundleLoader loader;

    private void Awake()
    {
      loader = Finder.SceneBundleLoader;
      DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
      DOTween.SetTweensCapacity(200, 125);
    }

    private IEnumerator Start()
    {
      yield return loader.Load(homeScenes);
    }

    public Coroutine LoadGameScenes()
    {
      return loader.Load(gameScenes);
    }

    public Coroutine UnloadGameScenes()
    {
      return loader.Unload(gameScenes);
    }

    public Coroutine LoadHomeScenes()
    {
      return loader.Load(homeScenes);
    }

    public Coroutine UnloadHomeScenes()
    {
      return loader.Unload(homeScenes);
    }

    public Coroutine LoadSettingsScenes()
    {
      return loader.Load(settingsScenes);
    }

    public Coroutine UnloadSettingsScenes()
    {
      return loader.Unload(settingsScenes);
    }

    public static void QuitApplication()
    {
      ApplicationExtensions.Quit();
    }
  }
}