using UnityEngine;

namespace Game
{
  public class ColliderStimuli2D : MonoBehaviour
  {
    public event StimuliEventHandler OnDestroyed;

#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(!GetComponent<Collider2D>().isTrigger, "ColliderStimuli2D needs a collider, not a trigger");
    }

#endif

    private void OnDestroy()
    {
      NotifyDestroyed();
    }

    private void NotifyDestroyed()
    {
      if (OnDestroyed != null) OnDestroyed(transform.parent.gameObject);
    }
  }
}