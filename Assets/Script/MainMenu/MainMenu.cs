using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.MainMenu
{
    class MainMenu : MonoBehaviour
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

        public void QuitGame()
        {
            Application.Quit();
        }

        public void GoToSettings()
        {
            mainMenu.enabled = false;
            settingsMenu.enabled = true;
        }

        public void GoToGame()
        {
            SceneManager.LoadScene(GameScene.name);
            SceneManager.UnloadSceneAsync(MenuScene.name);
        }
    }
}
