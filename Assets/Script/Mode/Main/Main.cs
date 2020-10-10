using Harmony;
using UnityEngine;
using DG.Tweening;

namespace Game
{
  public class Main : MonoBehaviour
  {
    [Header("Scenes")] 
    [SerializeField] private SceneBundle gameScenes;

    private SceneBundleLoader loader;

    private void Awake()
    {
      loader = Finder.SceneBundleLoader;
      DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
      DOTween.SetTweensCapacity(200, 125);
    }
  }
}