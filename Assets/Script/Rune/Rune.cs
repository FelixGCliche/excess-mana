
using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] private Elements element = Elements.FIRE;
    [SerializeField] private RuneSize size = RuneSize.SMALL;

    public void PickUp()
    {
        Destroy(gameObject);
    }

    public Elements GetElement()
    {
        return element;
    }

    public RuneSize GetSize()
    {
        return size;
    }
}
