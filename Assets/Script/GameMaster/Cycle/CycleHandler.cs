using System;
using Script.GameMaster.WaveSpawner;
using UnityEngine;
using UnityEngine.UI;

public class CycleHandler : MonoBehaviour
{
    [SerializeField] private float dayDuration = 300f;
    [SerializeField] private float timeBetweenWaves = 30f;
    [SerializeField] private int waveAmount = 5;
    [SerializeField] private SpriteRenderer nightOverlay;
    [SerializeField] private float nightOverlayFullOpacity = 0.5f;
    [SerializeField] private float nightOverlaySwitchDuration = 30f;
    [SerializeField] private Text timerText;
    [SerializeField] private WaveSpawner waveSpawner;
    
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
                UpdateNightOverlay();
            }
        }
        else if (cycleState == CycleState.DAY)
        {
            BeginNight();
        }
        else if (waveNumber < waveAmount)
        {
            StartWave();
        }
        else if (HaveAllEnemyBeenSlain())
        {
            BeginDay();
        }
        else
        {
            cycleTimer = 0;
        }
        UpdateTimerText();
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
        waveSpawner.StartWave();
        cycleTimer = timeBetweenWaves;
        waveNumber++;
    }

    private bool HaveAllEnemyBeenSlain()
    {
        //TODO
        return true;
    }

    private void UpdateNightOverlay()
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

    private void UpdateTimerText()
    {
        float seconds = cycleTimer % 60f;
        if (Math.Abs(seconds - 60f) < 0.5f)
            seconds = 0;
        float minutes = (cycleTimer - seconds) / 60f;
        
        if (cycleState == CycleState.DAY)
        {
            timerText.text = "Time left before night : " + minutes.ToString("0") + ":" + seconds.ToString("00");
        }
        else
        {
            timerText.text = "Time left before next wave : " + minutes + ":" + (int)seconds;
        }
            
    }
}
