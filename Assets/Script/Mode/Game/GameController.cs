using Harmony;
using TMPro;
using UnityEngine;

namespace Game
{
  [Findable(Tags.GameController)]
  public class GameController : MonoBehaviour
  {
    private InputActions.GameActions gameInputs;

    private MainController main;

    public static int nbDaysSurvived;
    
    private void Awake()
    {
      gameInputs = Finder.Inputs.Actions.Game;
    }

    private void Start()
    {
      main = Finder.MainController;
      gameInputs.Enable();
    }

    private void Update()
    {
      if (gameInputs.Exit.triggered) ApplicationExtensions.Quit();
    }

    public void PlayerIsDead()
    {
      main.LoadGameOverScenes();
      
      main.UnloadGameScenes();
    }
  }
}