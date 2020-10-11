using Harmony;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Script.HUD
{
  public class ElementSelector : MonoBehaviour
  {
    private Player player;

    private Button btnFireElement;
    private Button btnEarthElement;
    private Button btnWindElement;
    private Button btnWaterElement;

    private void Awake()
    {
      var buttons = GetComponentsInChildren<Button>();
      btnFireElement = buttons.WithName(GameObjects.FireSelect);
      btnEarthElement = buttons.WithName(GameObjects.EarthSelect);
      btnWindElement = buttons.WithName(GameObjects.WindSelect);
      btnWaterElement = buttons.WithName(GameObjects.WaterSelect);

      for (int i = 0; i < buttons.Length; i++)
      {
        SetButtonTextFromBinding(buttons[i], player.ElementInputs[i]);
      }
    }

    private void Update()
    {
      ShowSelectedElement();
    }

    private void ShowSelectedElement()
    {
      switch (player.CurrentSpellElement)
      {
        case Elements.FIRE:
          btnFireElement.Select();
          break;
        case Elements.EARTH:
          btnEarthElement.Select();
          break;
        case Elements.WIND:
          btnWindElement.Select();
          break;
        case Elements.WATER:
          btnWaterElement.Select();
          break;
        default:
          btnFireElement.Select();
          break;
      }
    }

    private void SetButtonTextFromBinding(Button btn, InputAction binding)
    {
      Text btnText = btn.GetComponentInChildren<Text>();
      if (btnText != null) btnText.text = binding.GetBindingDisplayString();
    }
  }
}