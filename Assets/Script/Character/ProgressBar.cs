using System;
using UnityEditor;
using UnityEngine;

namespace Script.Character
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject bar;

        public void UpdateProgressBar(float percentage)
        {
            bar.transform.localScale = new Vector2(percentage, 1f);
        }
    }
}