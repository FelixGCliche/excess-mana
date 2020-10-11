using UnityEngine;

namespace Game
{
  public class Mover : MonoBehaviour
  {
    [SerializeField] [Min(0.0f)] private float speed = 5.0f;
    
    private Transform parentTransform;

    private void Awake()
    {
      parentTransform = transform.parent;
    }

    public void Move(Vector2 direction)
    {
      parentTransform.Translate(direction * (speed * Time.deltaTime));
    }
  }
}