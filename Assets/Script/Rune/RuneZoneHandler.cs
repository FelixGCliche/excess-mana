using UnityEngine;

public class RuneZoneHandler : MonoBehaviour
{ 
    [SerializeField] private RuneZone smallFireRuneZone;
    [SerializeField] private RuneZone smallEarthRuneZone;
    [SerializeField] private RuneZone smallWindRuneZone;
    [SerializeField] private RuneZone smallWaterRuneZone;
    [SerializeField] private RuneZone mediumFireRuneZone;
    [SerializeField] private RuneZone mediumEarthRuneZone;
    [SerializeField] private RuneZone mediumWindRuneZone;
    [SerializeField] private RuneZone mediumWaterRuneZone;
    [SerializeField] private RuneZone largeFireRuneZone;
    [SerializeField] private RuneZone largeEarthRuneZone;
    [SerializeField] private RuneZone largeWindRuneZone;
    [SerializeField] private RuneZone largeWaterRuneZone;

    public void ResetAllRuneZones()
    {
        smallFireRuneZone.ResetRunes();
        smallEarthRuneZone.ResetRunes();
        smallWindRuneZone.ResetRunes();
        smallWaterRuneZone.ResetRunes();
        mediumFireRuneZone.ResetRunes();
        mediumEarthRuneZone.ResetRunes();
        mediumWindRuneZone.ResetRunes();
        mediumWaterRuneZone.ResetRunes();
        largeFireRuneZone.ResetRunes();
        largeEarthRuneZone.ResetRunes();
        largeWindRuneZone.ResetRunes();
        largeWaterRuneZone.ResetRunes();
    }
    
}