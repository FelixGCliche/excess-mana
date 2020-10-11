using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Util;
using UnityEngine;


public class Structure : MonoBehaviour
{
    [SerializeField] [Range(0, 1000)] protected float initialHealth = 100;

    [SerializeField] protected Elements currentElement;

    [SerializeField] protected HealthBar healthBar;

    protected float currentHealth;

    void Start()
    {
        currentHealth = initialHealth;
    }

    protected void Repair(float health)
    {
        SetCurrentLife(currentHealth + health);
        if (currentHealth > initialHealth)
        {
            currentHealth = initialHealth;
        }
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
        initialHealth -= DamageCalculator.CalculateDamage(damageAmount, damageElement, currentElement);
        if (initialHealth <= 0)
            Destroy();
        else
            healthBar.AdjustHealthBar(initialHealth / 100);
    }

    public void SetCurrentLife(float life)
    {
        this.currentHealth = life;
    }

    public float GetCurrentLife()
    {
        return currentHealth;
    }

    public void SetCurrentElement(Elements e)
    {
        this.currentElement = e;
    }

    public Elements GetCurrentElement()
    {
        return this.currentElement;
    }
}
