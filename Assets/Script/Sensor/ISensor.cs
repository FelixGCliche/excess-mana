namespace Game
{
  public interface ISensor<out T>
  {
    event SensorEventHandler<T> OnSensedObject;
    event SensorEventHandler<T> OnUnsensedObject;
  }

  public delegate void SensorEventHandler<in T>(T otherObject);
}