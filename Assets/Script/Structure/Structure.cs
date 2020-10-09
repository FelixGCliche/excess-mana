using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Structure : MonoBehaviour
{
    //================================ Variables
    [SerializeField] protected Vector2 spawnPosition;

    [SerializeField] protected Elements current_element;
    [SerializeField] protected StructureState current_state;

    [SerializeField] protected Sprite sprite;
    [SerializeField] protected float current_life;

    [SerializeField] protected float time_to_build;
    [SerializeField] protected float time_to_repair;
    [SerializeField] protected float time_to_upgrade;

    [SerializeField] protected int current_level;
    [SerializeField] protected int current_exp;
    [SerializeField] protected int exp_required;

    [SerializeField] protected int current_rune_number;

    [SerializeField] protected float time_between_attacks;

    [SerializeField] protected SpriteRenderer spriteRenderer;

    //================================ Methods
    
    void awake()
    {

    }

    void Start()
    {
        current_state = StructureState.Idle;
    }

    void Update()
    {

    }

    protected void Init()
    {
        
    }

    protected void Build()
    {
        transform.position = spawnPosition;
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
        current_state = StructureState.Repairing;
        SetCurrentLife(health);
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

    protected void Attack()
    {
        current_state = StructureState.Attacking;
        //find ebject by tag ennemy
        //draw debug line
        //apply damages
    }

    protected void LevelUp()
    {
        int temp = GetCurrentLevel();
        SetCurrentLevel(temp++);
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

    public int GetCurrentLevel()
    {
        return current_level;
    }

    public void SetSpawnPosition(Vector2 position)
    {
        this.spawnPosition = position;
    }

    public void SetDefaultState()
    {
        this.current_state = StructureState.Idle;
    }
    



}
