using System;
using Script.GameMaster.WaveSpawner;
using UnityEngine;
using UnityEngine.UI;

public class CycleHandler : MonoBehaviour
{
    [SerializeField] private float dayDuration = 180f;
    [SerializeField] private float timeBetweenWaves = 30f;
    [SerializeField] private int waveAmount = 1;
    [SerializeField] private SpriteRenderer nightOverlay;
    [SerializeField] private float nightOverlayFullOpacity = 0.5f;
    [SerializeField] private float nightOverlaySwitchDuration = 30f;
    [SerializeField] private Text timerText;
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private RuneZoneHandler runeZoneHandler;
    
    private CycleState cycleState;
    private float cycleTimer;
    private int waveNumber;
    private Color nightOverlayColor;
    private int day;

    public float NightDuration => CalculateNightDuration();
    
    // Start is called before the first frame update
    private void Start()
    {
        nightOverlayColor = nightOverlay.color;
        nightOverlayColor.a = nightOverlayFullOpacity;
        nightOverlay.color = nightOverlayColor;
        day = 0;
        BeginDay();
    }

    // Update is called once per frame
    private void Update()
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
        else if (waveSpawner.HaveAllEnemyBeenSlain())
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
        day++;
        cycleState = CycleState.DAY;
        cycleTimer = dayDuration;
        runeZoneHandler.ResetAllRuneZones();
        WaveDifficulty difficulty = WaveDifficulty.NORMAL;
        if (day % 5 == 0)
        {
            waveAmount++;
            difficulty = WaveDifficulty.HARD;
        }
        waveSpawner.CalculateNextNightContent(waveAmount, difficulty);
        waveSpawner.NextDay();
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
        waveNumber++;
        if (waveNumber < waveAmount)
            cycleTimer = timeBetweenWaves;
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

    private float CalculateNightDuration()
    {
        return 0;
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
        else if (waveNumber < waveAmount)
        {
            timerText.text = "Time left before next wave : " + minutes.ToString("0") + ":" + seconds.ToString("00");
        }
        else
        {
            timerText.text = "Last wave";
        }
    }
}
