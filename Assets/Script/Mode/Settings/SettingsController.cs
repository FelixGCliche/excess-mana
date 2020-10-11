using Harmony;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
  public class SettingsController : MonoBehaviour
  {
    private MainController main;
    private InputActions.MenuActions menuInputs;
    
    private Button btnReturn;

    private void Awake()
    {
      main = Finder.MainController;
      menuInputs = Finder.Inputs.Actions.Menu;
      
      btnReturn = GetComponentsInChildren<Button>().WithName(GameObjects.BackButton);
    }

    private void Start()
    {
      menuInputs.Enable();
      btnReturn.Select();
    }

    private void OnEnable()
    {
      btnReturn.onClick.AddListener(Return);
    }

    private void OnDisable()
    {
      btnReturn.onClick.RemoveListener(Return);
    }

    private void Return()
    {
      main.LoadHomeScenes();
      main.UnloadSettingsScenes();
    }
  }
}