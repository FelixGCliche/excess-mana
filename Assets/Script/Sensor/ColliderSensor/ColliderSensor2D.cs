using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Game
{
  [RequireComponent(typeof(Collider2D))]
  public class ColliderSensor2D : MonoBehaviour, ISensor<GameObject>
  {
    private Transform parentTransform;
    private new Collider2D collider;
    private readonly List<GameObject> sensedObjects;
    private ulong dirtyFlag;

    public event SensorEventHandler<GameObject> OnSensedObject;
    public event SensorEventHandler<GameObject> OnUnsensedObject;

    public IReadOnlyList<GameObject> SensedObjects => sensedObjects;
    public ulong DirtyFlag => dirtyFlag;

    public ColliderSensor2D()
    {
      sensedObjects = new List<GameObject>();
      dirtyFlag = ulong.MinValue;
    }

    private void Awake()
    {
      parentTransform = transform.parent ?? transform;
      collider = GetComponent<Collider2D>();

      Debug.Assert(!collider.isTrigger, "ColliderSensor2D need a collider, not a trigger.");
    }

    private void OnEnable()
    {
      collider.enabled = true;
    }

    private void OnDisable()
    {
      collider.enabled = false;
      ClearSensedObjects();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      var otherParentTransform = other.transform.parent ?? other.transform;
      if (!IsSelf(otherParentTransform))
      {
        var stimuli = other.collider.GetComponent<ColliderStimuli2D>();
        if (stimuli != null)
        {
          stimuli.OnDestroyed += RemoveSensedObject;
          AddSensedObject(otherParentTransform.gameObject);
        }
      }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
      var otherParentTransform = other.transform.parent ?? other.transform;
      if (!IsSelf(otherParentTransform))
      {
        var stimuli = other.collider.GetComponent<ColliderStimuli2D>();
        if (stimuli != null)
        {
          stimuli.OnDestroyed -= RemoveSensedObject;
          RemoveSensedObject(otherParentTransform.gameObject);
        }
      }
    }

    public ISensor<T> For<T>()
    {
      return new ColliderSensor2D<T>(this);
    }

    public ISensor<T> ForNothing<T>()
    {
      return new EmptyColliderSensor2D<T>();
    }

    private void AddSensedObject(GameObject obj)
    {
      if (!sensedObjects.Contains(obj))
      {
        sensedObjects.Add(obj);
        dirtyFlag++;
        NotifySensedObject(obj);
      }
    }

    private void RemoveSensedObject(GameObject obj)
    {
      if (sensedObjects.Contains(obj))
      {
        sensedObjects.Remove(obj);
        dirtyFlag++;
        NotifyUnsensedObject(obj);
      }
    }

    private void ClearSensedObjects()
    {
      sensedObjects.Clear();
      dirtyFlag++;
    }

    private bool IsSelf(Transform otherParentTransform)
    {
      return parentTransform == otherParentTransform;
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

  [SuppressMessage("ReSharper", "DelegateSubtraction")]
  public sealed class ColliderSensor2D<T> : ISensor<T>
  {
    private readonly ColliderSensor2D sensor;
    private SensorEventHandler<T> onSensedObject;
    private SensorEventHandler<T> onUnsensedObject;

    private readonly List<T> sensedObjects;
    private ulong dirtyFlag;

    public IReadOnlyList<T> SensedObjects
    {
      get
      {
        if (IsDirty()) UpdateSensor();

        return sensedObjects;
      }
    }

    public event SensorEventHandler<T> OnSensedObject
    {
      add
      {
        if (onSensedObject == null || onSensedObject.GetInvocationList().Length == 0)
          sensor.OnSensedObject += OnSensedObjectInternal;
        onSensedObject += value;
      }
      remove
      {
        if (onSensedObject != null && onSensedObject.GetInvocationList().Length == 1)
          sensor.OnSensedObject -= OnSensedObjectInternal;
        onSensedObject -= value;
      }
    }

    public event SensorEventHandler<T> OnUnsensedObject
    {
      add
      {
        if (onUnsensedObject == null || onUnsensedObject.GetInvocationList().Length == 0)
          sensor.OnUnsensedObject += OnUnsensedObjectInternal;
        onUnsensedObject += value;
      }
      remove
      {
        if (onUnsensedObject != null && onUnsensedObject.GetInvocationList().Length == 1)
          sensor.OnUnsensedObject -= OnUnsensedObjectInternal;
        onUnsensedObject -= value;
      }
    }

    public ColliderSensor2D(ColliderSensor2D sensor)
    {
      this.sensor = sensor;
      sensedObjects = new List<T>();
      dirtyFlag = sensor.DirtyFlag;

      UpdateSensor();
    }

    private bool IsDirty()
    {
      return sensor.DirtyFlag != dirtyFlag;
    }

    private void UpdateSensor()
    {
      sensedObjects.Clear();

      foreach (var obj in sensor.SensedObjects)
      {
        var otherComponent = obj.GetComponentInChildren<T>();
        if (otherComponent != null) sensedObjects.Add(otherComponent);
      }

      dirtyFlag = sensor.DirtyFlag;
    }

    private void OnSensedObjectInternal(GameObject obj)
    {
      var otherComponent = obj.GetComponentInChildren<T>();
      if (otherComponent != null && !sensedObjects.Contains(otherComponent))
      {
        sensedObjects.Add(otherComponent);
        NotifySensedObject(otherComponent);
      }

      dirtyFlag = sensor.DirtyFlag;
    }

    private void OnUnsensedObjectInternal(GameObject obj)
    {
      var otherComponent = obj.GetComponentInChildren<T>();
      if (otherComponent != null && sensedObjects.Contains(otherComponent))
      {
        sensedObjects.Remove(otherComponent);
        NotifyUnsensedObject(otherComponent);
      }

      dirtyFlag = sensor.DirtyFlag;
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

  public sealed class EmptyColliderSensor2D<T> : ISensor<T>
  {
    private readonly List<T> sensedObjects;

    public IReadOnlyList<T> SensedObjects => sensedObjects;

    public event SensorEventHandler<T> OnSensedObject;
    public event SensorEventHandler<T> OnUnsensedObject;

    public EmptyColliderSensor2D()
    {
      sensedObjects = new List<T>();
    }
  }
}