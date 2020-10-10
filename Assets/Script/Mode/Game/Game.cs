using Harmony;
using UnityEngine;

namespace Game
{
  public class Game : MonoBehaviour
  {
    private InputActions.GameActions gameInputs;

    private void Awake()
    {
      gameInputs = Finder.Inputs.Actions.Game;
    }

    private void Start()
    {
      gameInputs.Enable();
    }

    private void Update()
    {
      if (gameInputs.Exit.triggered) ApplicationExtensions.Quit();
    }
  }
}