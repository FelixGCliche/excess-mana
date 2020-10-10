using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Util;
using UnityEngine;


public class Structure : MonoBehaviour
{
    //================================ Variables
    [SerializeField] [Range(0, 1000)] protected float baseHealth = 100f;

    [SerializeField] protected Vector3 spawnPosition;

    [SerializeField] protected Elements current_element;
    [SerializeField] protected StructureState current_state;

    [SerializeField] protected float current_life;

    [SerializeField] protected float default_time_to_build;
    [SerializeField] protected float default_time_to_repair;
    [SerializeField] protected float default_time_to_upgrade;

    [SerializeField] protected int current_level;
    [SerializeField] protected int current_exp;
    [SerializeField] protected int exp_required;

    [SerializeField] protected int current_rune_number;

    [SerializeField] protected float time_between_attacks;

    [SerializeField] private HealthBar healthBar;


    //================================ Methods

    void Start()
    {
        current_state = StructureState.Idle;
    }

    protected void Build()
    {
        transform.position = spawnPosition;
    }

    protected IEnumerator DoRepair(float health)
    {
        for (; ; )
        {
            Debug.Log("repair");

            yield return new WaitForSeconds(1.0f);
        }
    }

    protected void Repair(float health)
    {
        current_life += health;
    }

    protected void Upgrade()
    {
        LevelUp();
        //[TODO] Asset upgrade
    }
    protected void Destroy()
    {
        //[TODO] sprite
        current_state = StructureState.Destroyed;
    }

    protected void TakeDamage(float damage)
    {
        SetCurrentLife(damage);
    }

    protected void LevelUp()
    {
        int temp = GetCurrentLevel();
        SetCurrentLevel(temp++);
    }

    public void TakeDmage(float damageAmount, Elements damageElement)
    {
        current_life -= DamageCalculator.CalculateDamage(damageAmount, damageElement, current_element);
        if (current_life <= 0)
            Destroy();
        else
            healthBar.AdjustHealthBar(current_life / baseHealth);
    }

    //================================ Accessors

    public void SetCurrentLife(float life)
    {
        this.current_life = life;
    }

    public void SetCurrentLevel(int level)
    {
        this.current_level = level;
    }

    public void SetCurrentExp(int exp)
    {
        this.current_exp = exp;
    }

    public void SetCurrentRuneNumber(int number)
    {
        this.current_rune_number = number;
    }

    public int GetCurrentLevel()
    {
        return current_level;
    }

    public void SetSpawnPosition(Vector2 position)
    {
        this.spawnPosition = position;
    }

    public void SetCurrentState(StructureState state)
    {
        this.current_state = state;
    }

    public void SetCurrentElement(Elements e)
    {
        this.current_element = e;
    }


    



}
