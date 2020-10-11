
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Rune : MonoBehaviour
{
    [SerializeField] private Elements element = Elements.FIRE;
    [SerializeField] private RuneSize size = RuneSize.SMALL;

    public bool IsInWall()
    {
        return Physics2D.OverlapCircle(transform.position, 0.5f).tag == "Walls";
    }

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
