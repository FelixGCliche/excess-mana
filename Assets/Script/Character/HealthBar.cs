using System;
using UnityEditor;
using UnityEngine;

namespace Script.Character
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject bar;

        public void AdjustHealthBar(float healthPercentage)
        {
            bar.transform.localScale = new Vector2(healthPercentage, 1f);
        }
    }
}