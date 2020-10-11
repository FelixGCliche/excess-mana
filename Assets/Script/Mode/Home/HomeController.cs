using Harmony;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
  public class HomeController : MonoBehaviour
  {
    private MainController main;
    private InputActions.MenuActions menuInputs;
    
    private Button btnPlay;
    private Button btnSettings;
    private Button btnQuit;

    private void Awake()
    {
      main = Finder.MainController;
      menuInputs = Finder.Inputs.Actions.Menu;

      var buttons = GetComponentsInChildren<Button>();
      btnPlay = buttons.WithName(GameObjects.PlayButton);
      btnSettings = buttons.WithName(GameObjects.SettingsButton);
      btnQuit = buttons.WithName(GameObjects.QuitButton);
    }

    private void Start()
    {
      menuInputs.Enable();
      btnPlay.Select();
    }

    private void OnEnable()
    {
      btnPlay.onClick.AddListener(StartGame);
      btnSettings.onClick.AddListener(StartSettings);
      btnQuit.onClick.AddListener(MainController.QuitApplication);
    }

    private void OnDisable()
    {
      btnPlay.onClick.RemoveListener(StartGame);
      btnSettings.onClick.RemoveListener(StartSettings);
      btnSettings.onClick.RemoveListener(MainController.QuitApplication);
    }

    private void StartGame()
    {
      main.LoadGameScenes();
      main.UnloadHomeScenes();
    }

    private void StartSettings()
    {
      main.LoadSettingsScenes();
      main.UnloadHomeScenes();
    }
  }
}