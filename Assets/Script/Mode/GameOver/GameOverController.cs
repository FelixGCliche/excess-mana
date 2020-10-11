using System;
using System.Collections;
using Game;
using UnityEngine;
using Harmony;
using Script.Util;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Menu
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI statText;

        private MainController mainController;

        private void Start()
        {
            mainController = Finder.MainController;
            statText.SetText(statText.text + PlayerStats.Days.ToString() + " Days.");
        }

        public void GoToMenu()
        {
            mainController.LoadHomeScenes();
            mainController.UnloadGameOverScenes();
        }
        
        public void GoToGame()
        {
            mainController.LoadGameScenes();
            mainController.UnloadGameOverScenes();
        }
    }
}