using Harmony;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Script.HUD
{
  public class ElementSelector : MonoBehaviour
  {
    private Player player;
    private InputAction[] elementSelectInputs;

    private Button btnFireElement;
    private Button btnEarthElement;
    private Button btnWindElement;
    private Button btnWaterElement;

    Elements currentElement;

    private void Awake()
    {
      var buttons = GetComponentsInChildren<Button>();
      btnFireElement = buttons.WithName(GameObjects.FireSelect);
      btnEarthElement = buttons.WithName(GameObjects.EarthSelect);
      btnWindElement = buttons.WithName(GameObjects.WindSelect);
      btnWaterElement = buttons.WithName(GameObjects.WaterSelect);

      elementSelectInputs = new[]
      {
        Finder.Inputs.Actions.Game.FireElement,
        Finder.Inputs.Actions.Game.EarthElement,
        Finder.Inputs.Actions.Game.WindElement,
        Finder.Inputs.Actions.Game.WaterElement
      };

      for (int i = 0; i < elementSelectInputs.Length; i++)
      {
        SetButtonTextFromBinding(buttons[i], elementSelectInputs[i]);
      }
      
      currentElement = 0;
    }

    private void Update()
    {
      for (int i = 0; i < elementSelectInputs.Length; i++)
      {
        if (elementSelectInputs[i].triggered) currentElement = (Elements) i;
      }

      ShowSelectedElement();
    }

    private void ShowSelectedElement()
    {
      switch (currentElement)
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