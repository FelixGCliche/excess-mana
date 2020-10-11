using System;
using Harmony;
using Script.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
  public class CycleStats : MonoBehaviour
  {
    [SerializeField] private CycleHandler cycleHandler;
    [SerializeField] private string DayCountFormat = "DAY\t{0}";
    [SerializeField] private string DayTimerHeaderFormat = "Daytime left";
    [SerializeField] private string WaveHeaderFormat = "Waves remaining";
    [SerializeField] private string WaveCountFormat = "{0} of {1}";
    [SerializeField] private float criticalDayPercentage = 0.25f;

    private Text title;
    private Text header;
    private Text data;

    private void Awake()
    {
      var textAreas = GetComponentsInChildren<Text>();
      title = textAreas.WithName(GameObjects.Title);
      header = textAreas.WithName(GameObjects.Header);
      data = textAreas.WithName(GameObjects.Data);
      
      title.text = String.Format(DayCountFormat, cycleHandler.Day);
      header.text = DayTimerHeaderFormat;
      data.text = FormatTimerText(cycleHandler.CycleTimer);
      
      Debug.Log(data);
    }

    private void Update()
    {
      title.text = String.Format(DayCountFormat, cycleHandler.Day);
      
      if (!cycleHandler.IsDay)
      {
        header.text = WaveHeaderFormat;
        data.text = String.Format(WaveCountFormat, cycleHandler.WaveNumber, cycleHandler.WaveAmount);
      }
      else
      {
        header.text = DayTimerHeaderFormat;
        data.text = FormatTimerText(cycleHandler.CycleTimer);
      }

      if (cycleHandler.IsDay && cycleHandler.DayPercentage < criticalDayPercentage)
        data.color = Color.red;
      else
        data.color = Color.white;
        
      
    }
    

    private string FormatTimerText(float cycleTimer)
    {
      float seconds = cycleTimer % 60f;
      if (Math.Abs(seconds - 60f) < 0.5f)
        seconds = 0;
      float minutes = (cycleTimer - seconds) / 60f;
        
      return minutes.ToString("0") + ":" + seconds.ToString("00");
    }
  }
}