using System.Collections.Generic;

namespace Game
{
  public interface ISensor<out T>
  {
    event SensorEventHandler<T> OnSensedObject;
    event SensorEventHandler<T> OnUnsensedObject;
    
    IReadOnlyList<T> SensedObjects { get; }
  }

  public delegate void SensorEventHandler<in T>(T otherObject);
}