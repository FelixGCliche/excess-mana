using System;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace Game
{
  public sealed class TriggerSensor2D : MonoBehaviour, ISensor<GameObject>
  {
    [SerializeField] private TriggerShape2D shape = TriggerShape2D.Circle;
    [SerializeField] [Range(0.0f, 100.0f)] private float size = 1.0f;

    private Transform parentTransform;
    private new Collider2D collider;
    private readonly List<GameObject> sensedObject;
    private ulong dirtyFlag;

    public event SensorEventHandler<GameObject> OnSensedObject;
    public event SensorEventHandler<GameObject> OnUnsensedObject;

    public IReadOnlyList<GameObject> SensedObjects => sensedObject;
    public ulong DirtyFlag => dirtyFlag;

    public TriggerSensor2D()
    {
      sensedObject = new List<GameObject>();
      dirtyFlag = ulong.MinValue;
    }

    private void Awake()
    {
      parentTransform = transform.parent ?? transform;

      CreateCollider();
      SetSensorLayer();
    }

    private void OnEnable()
    {
      collider.enabled = true;
      collider.isTrigger = true;
    }

    private void OnDisable()
    {
      collider.enabled = false;
      collider.isTrigger = false;
      ClearSensedObject();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      Transform otherParentTransform = other.transform.parent ?? other.transform;

      if (!IsSelf(otherParentTransform))
      {
        TriggerStimuli2D stimuli = other.GetComponent<TriggerStimuli2D>();
        if (stimuli != null)
        {
          stimuli.OnDestroyed += RemoveSensedObject;
          AddSensedObject(otherParentTransform.gameObject);
        }
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      Transform otherParentTransform = other.transform.parent ?? other.transform;

      if (!IsSelf(otherParentTransform))
      {
        TriggerStimuli2D stimuli = other.GetComponent<TriggerStimuli2D>();
        if (stimuli != null)
        {
          stimuli.OnDestroyed -= RemoveSensedObject;
          RemoveSensedObject(otherParentTransform.gameObject);
        }
      }
    }

    public ISensor<T> For<T>()
    {
      return new TriggerSensor2D<T>(this);
    }

    public ISensor<T> ForNothing<T>()
    {
      return new EmptyTriggerSensor2D<T>();
    }

    private void CreateCollider()
    {
      switch (shape)
      {
        case TriggerShape2D.Circle:
          CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
          circleCollider.isTrigger = true;
          circleCollider.radius = size / 2;
          collider = circleCollider;
          break;
        case TriggerShape2D.Square:
          BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
          boxCollider.isTrigger = true;
          boxCollider.size = Vector2.one * size;
          collider = boxCollider;
          break;
        default:
          throw new Exception("Unknown shape named \"" + shape + "\"");
      }
    }

    private void SetSensorLayer()
    {
      gameObject.layer = Layers.Sensor;
    }

    private void AddSensedObject(GameObject obj)
    {
      if (!sensedObject.Contains(obj))
      {
        sensedObject.Add(obj);
        dirtyFlag++;
        NotifySensedObject(obj);
      }
    }

    private void RemoveSensedObject(GameObject obj)
    {
      if (!sensedObject.Contains(obj))
      {
        sensedObject.Remove(obj);
        dirtyFlag++;
        NotifyUnsensedObject(obj);
      }
    }

    private void ClearSensedObject()
    {
      sensedObject.Clear();
      dirtyFlag++;
    }

    private bool IsSelf(Transform otherPrentTransform)
    {
      return parentTransform == otherPrentTransform;
    }

    private void NotifySensedObject(GameObject obj)
    {
      if (OnSensedObject != null) OnSensedObject(obj);
    }

    private void NotifyUnsensedObject(GameObject obj)
    {
      if (OnUnsensedObject != null) OnUnsensedObject(obj);
    }
  }

  public sealed class TriggerSensor2D<T> : ISensor<T>
  {
    private readonly TriggerSensor2D triggerSensor;
    private SensorEventHandler<T> onSensedObject;
    private SensorEventHandler<T> onUnsensedObject;

    private readonly List<T> sensedObjects;
    private ulong dirtyFlag;

    public event SensorEventHandler<T> OnSensedObject
    {
      add
      {
        if (onSensedObject == null || onSensedObject.GetInvocationList().Length == 0)
          triggerSensor.OnSensedObject += OnSensedObjectInternal;
        onSensedObject += value;
      }
      remove
      {
        if (onSensedObject == null || onSensedObject.GetInvocationList().Length == 0)
          triggerSensor.OnSensedObject -= OnSensedObjectInternal;
        onSensedObject -= value;
      }
    }

    public event SensorEventHandler<T> OnUnsensedObject
    {
      add
      {
        if (onUnsensedObject == null || onUnsensedObject.GetInvocationList().Length == 0)
          triggerSensor.OnUnsensedObject += OnUnsensedObjectInternal;
        onUnsensedObject += value;
      }
      remove
      {
        if (onUnsensedObject == null || onUnsensedObject.GetInvocationList().Length == 0)
          triggerSensor.OnUnsensedObject -= OnUnsensedObjectInternal;
        onUnsensedObject -= value;
      }
    }

    public IReadOnlyList<T> SensedObjects
    {
      get
      {
        if (IsDirty()) UpdateSensor();
        return sensedObjects;
      }
    }

    public TriggerSensor2D(TriggerSensor2D triggerSensor)
    {
      this.triggerSensor = triggerSensor;
      sensedObjects = new List<T>();
      dirtyFlag = triggerSensor.DirtyFlag;
    }

    private bool IsDirty()
    {
      return triggerSensor.DirtyFlag != dirtyFlag;
    }

    private void UpdateSensor()
    {
      sensedObjects.Clear();

      foreach (GameObject obj in triggerSensor.SensedObjects)
      {
        var otherComponent = obj.GetComponentInChildren<T>();
        if (otherComponent != null && !sensedObjects.Contains(otherComponent))
        {
          sensedObjects.Remove(otherComponent);
          NotifyUnsensedObject(otherComponent);
        }

        dirtyFlag = triggerSensor.DirtyFlag;
      }
    }

    private void OnSensedObjectInternal(GameObject obj)
    {
      var otherComponent = obj.GetComponentInChildren<T>();
      if (otherComponent != null && sensedObjects.Contains(otherComponent))
      {
        sensedObjects.Add(otherComponent);
        NotifySensedObject(otherComponent);
      }
      dirtyFlag = triggerSensor.DirtyFlag;
    }

    private void OnUnsensedObjectInternal(GameObject obj)
    {
      var otherComponent = obj.GetComponentInChildren<T>();
      if (otherComponent != null && sensedObjects.Contains(otherComponent))
      {
        sensedObjects.Add(otherComponent);
        NotifyUnsensedObject(otherComponent);
      }
      dirtyFlag = triggerSensor.DirtyFlag;
    }

    private void NotifySensedObject(T obj)
    {
      if (onSensedObject != null) onSensedObject(obj);
    }

    private void NotifyUnsensedObject(T obj)
    {
      if (onUnsensedObject != null) onUnsensedObject(obj);
    }
  }

  public sealed class EmptyTriggerSensor2D<T> : ISensor<T>
  {
    private readonly List<T> sensedObjects;
    
    public event SensorEventHandler<T> OnSensedObject;
    public event SensorEventHandler<T> OnUnsensedObject;
    public IReadOnlyList<T> SensedObjects => sensedObjects;

    public EmptyTriggerSensor2D()
    {
      sensedObjects = new List<T>();
    }
  }
}