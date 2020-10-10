using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle : MonoBehaviour
{
    [SerializeField] private float dayDuration = 300f;
    [SerializeField] private float timeBetweenWaves = 30f;
    [SerializeField] private int waveAmount = 5;
    [SerializeField] private SpriteRenderer nightOverlay;
    [SerializeField] private float nightOverlayFullOpacity = 0.5f;
    [SerializeField] private float nightOverlaySwitchDuration = 60f;
    
    private CycleState cycleState;
    private float cycleTimer;
    private int waveNumber;
    private Color nightOverlayColor;
    
    // Start is called before the first frame update
    void Start()
    {
        nightOverlayColor = nightOverlay.color;
        nightOverlayColor.a = nightOverlayFullOpacity;
        nightOverlay.color = nightOverlayColor;
        BeginDay();
    }

    // Update is called once per frame
    void Update()
    {
        cycleTimer -= Time.deltaTime;
        if (cycleTimer > 0)
        {
            if (cycleState == CycleState.DAY)
            {
                if (cycleTimer > dayDuration - nightOverlaySwitchDuration)
                {
                    float overlayProgression = (dayDuration - cycleTimer) / nightOverlaySwitchDuration;
                    nightOverlayColor.a = Mathf.Lerp(nightOverlayFullOpacity, 0, overlayProgression);
                    nightOverlay.color = nightOverlayColor;
                }
                else if (cycleTimer < nightOverlaySwitchDuration)
                {
                    float overlayProgression = cycleTimer / nightOverlaySwitchDuration;
                    nightOverlayColor.a = Mathf.Lerp(nightOverlayFullOpacity, 0, overlayProgression);
                    nightOverlay.color = nightOverlayColor;
                }
            }
        }
        else if (cycleState == CycleState.DAY)
        {
            BeginNight();
        }
        else if (waveNumber <= waveAmount)
        {
            StartWave();
        }
        else if (HaveAllEnemyBeenSlain())
        {
            BeginDay();
        }
    }

    private void BeginDay()
    {
        cycleState = CycleState.DAY;
        cycleTimer = dayDuration;
    }
    
    private void BeginNight()
    {
        cycleState = CycleState.NIGHT;
        waveNumber = 0;
        StartWave();
    }

    private void StartWave()
    {
        cycleTimer = timeBetweenWaves;
        waveNumber++;
    }

    private bool HaveAllEnemyBeenSlain()
    {
        //TODO
        return true;
    }
}
