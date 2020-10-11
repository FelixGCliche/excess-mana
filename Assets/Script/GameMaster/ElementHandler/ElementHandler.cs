using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

[Findable(Tags.ElementHandler)]
public class ElementHandler : MonoBehaviour
{

    private bool IsFireActivated = true;
    private bool IsWaterActivated = true;
    private bool IsWindActivated = true;
    private bool IsEarthActivated = true;

    private Player player;

    public bool GetIsFireActivated() { return IsFireActivated; }
    public bool GetIsWaterActivated() { return IsWaterActivated; }
    public bool GetIsWindActivated() { return IsWindActivated; }
    public bool GetIsEarthActivated() { return IsEarthActivated; }

    public void DeactivateElement(Elements elementToDeactivate)
    {
        switch (elementToDeactivate)
        {
            case Elements.FIRE:
                IsFireActivated = false;
                break;
            case Elements.WATER:
                IsWaterActivated = false;
                break;
            case Elements.WIND:
                IsWindActivated = false;
                break;
            case Elements.EARTH:
                IsEarthActivated = false;
                break;
        }
        InformPlayerThatElementIsDeactivated(elementToDeactivate);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubscribePlayer(Player player) 
    {
        this.player = player;
    }

    private void InformPlayerThatElementIsDeactivated(Elements element) 
    {
        //player.LearnThatElementIsDeactivated(element);
    }
}
