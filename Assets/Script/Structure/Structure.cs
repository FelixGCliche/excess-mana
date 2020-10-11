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

    [SerializeField] protected ProgressBar healthBar;

    protected float currentHealth;

    private AudioSource createSource;
    private AudioSource repairSource;
    private AudioSource upgradeSource;
    private AudioSource destroySource;

    //================================ Methods

    void Start()
    {
        createSource = GameObject.Find("TowerCreateSource").gameObject.GetComponent<AudioSource>();
        repairSource = GameObject.Find("TowerRepairSource").gameObject.GetComponent<AudioSource>();
        upgradeSource = GameObject.Find("TowerUpgradeSource").gameObject.GetComponent<AudioSource>();
        destroySource = GameObject.Find("TowerDestroySource").gameObject.GetComponent<AudioSource>();
        
        createSource.Play();
    }

    protected void Repair(float health)
    {
        SetCurrentLife(currentHealth + health);
        if (currentHealth > initialHealth)
        {
            currentHealth = initialHealth;
        }
    }

    protected void Repair(float health)
    {
        repairSource.Play();
        current_state = StructureState.Repairing;
        SetCurrentLife(health);
    }

    protected void Destroy()
    {
        destroySource.Play();
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
            healthBar.UpdateProgressBar(baseHealth / 100);
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
