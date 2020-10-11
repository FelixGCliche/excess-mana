using System.Collections;
using Harmony;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
  public class RuneStats : MonoBehaviour
  {
    [SerializeField] private Player player;
    
    private PlayerInventory inventory;

    private Button btnFireRune;
    private Button btnEarthRune;
    private Button btnWindRune;
    private Button btnWaterRune;

    private void Awake()
    {
      var buttons = GetComponentsInChildren<Button>();
      btnFireRune = buttons.WithName(GameObjects.FireRuneCard);
      btnEarthRune = buttons.WithName(GameObjects.EarthRuneCard);
      btnWindRune = buttons.WithName(GameObjects.WindRuneCard);
      btnWaterRune = buttons.WithName(GameObjects.WaterRuneCard);
    }

    private void Update()
    {
      inventory = player.Inventory;

      if (inventory != null)
      {
        SetButtonTextFromRuneRessource(btnFireRune, inventory.FireRuneRessources);
        SetButtonTextFromRuneRessource(btnEarthRune, inventory.EarthRuneRessources);
        SetButtonTextFromRuneRessource(btnWindRune, inventory.WindRuneRessources);
        SetButtonTextFromRuneRessource(btnWaterRune, inventory.WaterRuneRessources);
      }
    }

    private void SetButtonTextFromRuneRessource(Button btn, RuneRessource runeRessource)
    {
      Text btnText = btn.GetComponentInChildren<Text>();
      if (btnText != null) btnText.text = runeRessource.Total.ToString();
    }
  }
}