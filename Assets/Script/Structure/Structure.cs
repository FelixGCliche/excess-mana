using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Util;
using UnityEngine;


public class Structure : MonoBehaviour
{
    [SerializeField] [Range(0, 1000)] protected float baseHealth = 100f;

    [SerializeField] protected Elements current_element;

    [SerializeField] protected HealthBar healthBar;

    private float current_life;

    void Start()
    {
        current_life = baseHealth;
    }

    protected IEnumerator DoRepair(float health)
    {
        for (; ; )
        {
            Repair(health);
            yield return new WaitForSeconds(.1f);
        }
    }

    protected void Repair(float health)
    {
        SetCurrentLife(health);
    }

    protected void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        SetCurrentLife(damage);
    }

    public void TakeDamage(float damageAmount, Elements damageElement)
    {
        baseHealth -= DamageCalculator.CalculateDamage(damageAmount, damageElement, current_element);
        if (baseHealth <= 0)
            Destroy();
        else
            healthBar.AdjustHealthBar(baseHealth / 100);
    }

    public void SetCurrentLife(float life)
    {
        this.current_life = life;
    }

    public float GetCurrentLife()
    {
        return current_life;
    }

    public void SetCurrentElement(Elements e)
    {
        this.current_element = e;
    }

    public Elements GetCurrentElement()
    {
        return this.current_element;
    }
}
