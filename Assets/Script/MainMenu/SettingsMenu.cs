using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.MainMenu
{
    class SettingsMenu : MonoBehaviour
    {
        [SerializeField] Scene GameScene;
        [SerializeField] Scene SettingsScene;
        [SerializeField] Scene MenuScene;

        Canvas settingsMenu;
        Canvas mainMenu;

        public void Start()
        {
            settingsMenu = GameObject.Find("SettingsMenu").gameObject.GetComponent<Canvas>();
            mainMenu = GameObject.Find("MainMenu").gameObject.GetComponent<Canvas>();
        }

        public void GoToMainMenu()
        {
            mainMenu.enabled = true;
            settingsMenu.enabled = false;
        }
    }
}
