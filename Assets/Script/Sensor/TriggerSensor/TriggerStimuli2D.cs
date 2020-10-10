using System;
using Harmony;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Game
{
  public sealed class TriggerStimuli2D : MonoBehaviour
  {
    [SerializeField] private TriggerShape2D shape = TriggerShape2D.Circle;
    [SerializeField] [Range(0.0f, 100.0f)] private float size = 1.0f;

    public event StimuliEventHandler OnDestroyed;

    private void Awake()
    {
      CreateCollider();
      SetSensorLayer();
    }

    private void OnDestroy()
    {
      NotifyDestroyed();
    }

    private void CreateCollider()
    {
      switch (shape)
      {
        case TriggerShape2D.Circle:
          CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
          circleCollider.isTrigger = true;
          circleCollider.radius = size / 2;
          break;
        case TriggerShape2D.Square:
          BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
          boxCollider.isTrigger = true;
          boxCollider.size = Vector2.one * size;
          break;
        default:
          throw new Exception("Unknown shape named \"" + shape + "\"");
      }
    }

    private void SetSensorLayer()
    {
      gameObject.layer = Layers.Sensor;
    }

    private void NotifyDestroyed()
    {
      if (OnDestroyed != null) OnDestroyed(transform.parent.gameObject);
    }
  }
}