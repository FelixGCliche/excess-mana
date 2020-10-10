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

    private SceneBundleLoader loader;

    private void Awake()
    {
      loader = Finder.SceneBundleLoader;
      DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
      DOTween.SetTweensCapacity(200, 125);

      // Should be placed in home menu
      LoadGameScenes();
    }

    private IEnumerator Start()
    {
      yield return loader.Load(gameScenes);
    }

    public Coroutine LoadGameScenes()
    {
      return loader.Load(gameScenes);
    }

    public Coroutine UnloadGameScenes()
    {
      return loader.Unload(gameScenes);
    }
  }
}