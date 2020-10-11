
using System;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] private Elements element = Elements.FIRE;
    [SerializeField] private RuneSize size = RuneSize.SMALL;
    
    [SerializeField] private int smallRuneValue = 1;
    [SerializeField] private int mediumRuneValue = 5;
    [SerializeField] private int largeRuneValue = 25;

    public int Value
    {
        get
        {
            switch (size)
            {
                case RuneSize.SMALL:
                    return smallRuneValue;
                case RuneSize.MEDIUM:
                    return mediumRuneValue;
                case RuneSize.LARGE:
                    return largeRuneValue;
                default:
                    throw new Exception("The rune size you are trying to fetch doesn't exist");
            }
        }
    }

    public Elements Element => element;
    public RuneSize Size => size;

    public bool IsInWall => Physics2D.OverlapCircle(transform.position, 0.5f).tag == "Walls";

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
