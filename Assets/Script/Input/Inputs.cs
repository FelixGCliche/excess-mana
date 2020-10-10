using Harmony;
using UnityEngine;

namespace Game
{
  [Findable(Tags.MainController)]
  public class Inputs : MonoBehaviour
  {
    private InputActions actions;

    public InputActions Actions => actions ?? (actions = new InputActions());
  }
}