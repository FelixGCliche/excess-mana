﻿using System.Collections;
using Harmony;
using Script.Character;
using Script.Character.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using  Game;
using System.Collections.Generic;

public class Player : Character
{
    [SerializeField] private PlayerAttack playerAttack;

    [SerializeField] private Transform spriteTransform;

    private InputAction moveInputs;
    private bool Fire => Finder.Inputs.Actions.Game.Fire.triggered;
    private bool Interact => Finder.Inputs.Actions.Game.Interact.triggered;
    private bool Build => Finder.Inputs.Actions.Game.Build.triggered;
    private bool IsFireElement => Finder.Inputs.Actions.Game.FireElement.triggered;
    private bool IsWaterElement => Finder.Inputs.Actions.Game.WaterElement.triggered;
    private bool IsWindElement => Finder.Inputs.Actions.Game.WindElement.triggered;
    private bool IsEarthElement => Finder.Inputs.Actions.Game.EarthElement.triggered;

    public StructureHandler structureHandler;
    private PlayerInventory inventory;

    private Elements currentSpellElement;
    private ElementHandler elementHandler;

    private AudioSource deathSource;
    private AudioSource runSource;
    private AudioSource attackedSource;
    private AudioSource fireAttackSource;
    private AudioSource waterAttackSource;
    private AudioSource airAttackSource;
    private AudioSource earthAttackSource;

    private bool isRunning;
    
    public Grid grid;

    List<double> validated_x = new List<double>();
    List<double> validated_y = new List<double>();

    Vector3 v;
    Vector3 v2;
    Vector3 v3;
    Vector3 v4;



    private new void Awake()
    { 
        base.Awake();

        deathSource = GameObject.Find("PlayerDeathSource").gameObject.GetComponent<AudioSource>();
        runSource = GameObject.Find("PlayerRunSource").gameObject.GetComponent<AudioSource>();
        attackedSource = GameObject.Find("PlayerAttackedSource").gameObject.GetComponent<AudioSource>();
        fireAttackSource = GameObject.Find("PlayerFireAttackSource").gameObject.GetComponent<AudioSource>();
        waterAttackSource = GameObject.Find("PlayerWaterAttackSource").gameObject.GetComponent<AudioSource>();
        airAttackSource = GameObject.Find("PlayerAirAttackSource").gameObject.GetComponent<AudioSource>();
        earthAttackSource = GameObject.Find("PlayerEarthAttackSource").gameObject.GetComponent<AudioSource>();
        
        moveInputs = Finder.Inputs.Actions.Game.Move;
        elementHandler = Finder.ElementHandler;
        grid = FindObjectOfType<Grid>();

        
    }

    public void PlaceTowerNear(Vector2 nearPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(nearPoint);
       

         v = new Vector3 ( -10, 10, 0 );
         v2 = new Vector3 ( -1, 10, 0 );
         v3 = new Vector3 ( -1, 2, 0 );
         v4 = new Vector3 ( -10, 2, 0 );

        if(finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y  )
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

         v = new Vector3(1, 10, 0);
         v2 = new Vector3(10, 10, 0);
         v3 = new Vector3(1, 2, 0);
         v4 = new Vector3(10, 2, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(-10, -1, 0);
        v2 = new Vector3(-2, -1, 0);
        v3 = new Vector3(-2, -10, 0);
        v4 = new Vector3(-10, -10, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(1, -1, 0);
        v2 = new Vector3(10, 1, 0);
        v3 = new Vector3(1, -10, 0);
        v4 = new Vector3(10, -10, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(-15, 3, 0);
        v2 = new Vector3(-12, 3, 0);
        v3 = new Vector3(-12, -2, 0);
        v4 = new Vector3(-15, -3, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(12, 3, 0);
        v2 = new Vector3(15, 3, 0);
        v3 = new Vector3(12, -2, 0);
        v4 = new Vector3(15, -2, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(-20, 2, 0);
        v2 = new Vector3(-18, 2, 0);
        v3 = new Vector3(-20, -1, 0);
        v4 = new Vector3(-18, -1, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(18, 2, 0);
        v2 = new Vector3(20, 2, 0);
        v3 = new Vector3(-18, -1, 0);
        v4 = new Vector3(-20, -1, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }


        v = new Vector3(-3, 15, 0);
        v2 = new Vector3(3, 15, 0);
        v3 = new Vector3(-3, 12, 0);
        v4 = new Vector3(3, 12, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(-3, -12, 0);
        v2 = new Vector3(3, -12, 0);
        v3 = new Vector3(-3, -15, 0);
        v4 = new Vector3(3, -15, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(-2, -21, 0);
        v2 = new Vector3(2, 21, 0);
        v3 = new Vector3(-2, 18, 0);
        v4 = new Vector3(2, 18, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(-2, -18, 0);
        v2 = new Vector3(2, -18, 0);
        v3 = new Vector3(-2, -21, 0);
        v4 = new Vector3(2, -21, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(-15, 15, 0);
        v2 = new Vector3(-12, 15, 0);
        v3 = new Vector3(-15, 11, 0);
        v4 = new Vector3(-12, 11, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(11, 15, 0);
        v2 = new Vector3(15, 15, 0);
        v3 = new Vector3(12, 11, 0);
        v4 = new Vector3(15, 11, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(12, -11, 0);
        v2 = new Vector3(15, -11, 0);
        v3 = new Vector3(12, -15, 0);
        v4 = new Vector3(15, -15, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        v = new Vector3(-14, -11, 0);
        v2 = new Vector3(-12, -11, 0);
        v3 = new Vector3(-15, -14, 0);
        v4 = new Vector3(-12, -14, 0);

        if (finalPosition.x >= v.x && finalPosition.x <= v2.x && finalPosition.y >= v4.y && finalPosition.y <= v2.y)
        {
            structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);
        }

        Debug.DrawLine(v,v2,Color.green, 5.0f);


    }

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        inventory = new PlayerInventory();
        currentSpellElement = Elements.FIRE;

        structureHandler = gameObject.GetComponent<StructureHandler>();
        transform.position = new Vector3(0,0,0);
       inventory.AddRune(100, Elements.WIND, RuneSize.SMALL);
       inventory.AddRune(100, Elements.WIND, RuneSize.MEDIUM);
       inventory.AddRune(100, Elements.WIND, RuneSize.LARGE);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateElement();
        if (Fire)
            Attack();

        if (moveInputs.ReadValue<Vector2>() != Vector2.zero && !isRunning)
        {
            StartCoroutine(PlayRunSound());
        }
            


        Mover.Move(moveInputs.ReadValue<Vector2>());
        if (moveInputs.ReadValue<Vector2>().x < 0)
        
        /*
        if (Input.GetKeyDown("space"))
        {
            spriteTransform.localScale = new Vector3(1,1,1);
        }
        else if (moveInputs.ReadValue<Vector2>().x > 0)
        {
            spriteTransform.localScale = new Vector3(-1, 1, 1);
        }
        /*
        if(Input.GetKeyDown("q"))
        {
            inventory.AddRune(10, Elements.WIND, RuneSize.SMALL);
            inventory.AddRune(10, Elements.WIND, RuneSize.MEDIUM);
            inventory.AddRune(10, Elements.WIND, RuneSize.LARGE);

        }*/
        /*
        if (Input.GetKeyDown("e"))
        {

            /* structureHandler.TestAdd(structureHandler.CheckIsSelectedTower(), inventory.GetRuneQuantity(Elements.WIND, RuneSize.SMALL), Elements.WIND, RuneSize.SMALL);
             inventory.RemoveRune(structureHandler.GetRequiredRunes(structureHandler.CheckIsSelectedTower(),RuneSize.SMALL), Elements.WIND, RuneSize.SMALL);
             

            RuneSizeSwitch(RuneSize.SMALL, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            RuneSizeSwitch(RuneSize.SMALL, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            RuneSizeSwitch(RuneSize.SMALL, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            Debug.Log("Inventoory player reste : " + inventory.GetRuneQuantity(Elements.WIND, RuneSize.SMALL));
        }*/

        if (Build)
        {
            Vector2 a = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            PlaceTowerNear(a);
        }

        if(Interact)
        {
            Debug.Log("Interact");

            if (structureHandler.GetCUrrentLife(structureHandler.CheckIsSelectedTower()) != 100)
            {
                structureHandler.RepairTower(structureHandler.CheckIsSelectedTower());
            }
            else
            {
                RuneSizeSwitch(RuneSize.SMALL, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
                RuneSizeSwitch(RuneSize.MEDIUM, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
                RuneSizeSwitch(RuneSize.LARGE, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            }
        }
    }

    public void RuneSizeSwitch(RuneSize r, Elements e)
    {
        structureHandler.TestAdd(structureHandler.CheckIsSelectedTower(), inventory.GetRuneQuantity(e, r), e, r);
        inventory.RemoveRune(structureHandler.GetRequiredRunes(structureHandler.CheckIsSelectedTower(),r), e, r);
    }

    protected override void Kill()
    {
        deathSource.Play();
    }
    
    public void Attack()
    {
        if (playerAttack.IsReadyToAttack())
        {
            switch (currentSpellElement)
            {
                case Elements.FIRE:
                    playerAttack.FireAttack(transform.position);
                    fireAttackSource.Play();
                    break;
                case Elements.EARTH:
                    playerAttack.EarthAttack(transform.position);
                    earthAttackSource.Play();
                    break;
                case Elements.WIND:
                    playerAttack.WindAttack(transform);
                    airAttackSource.Play();
                    break;
                case Elements.WATER:
                    playerAttack.WaterAttack(transform.position);
                    waterAttackSource.Play();
                    break;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rune rune = other.GetComponentInParent<Rune>();
        if (rune != null)
        {
            Elements runeElement = rune.GetElement();
            RuneSize runeSize = rune.GetSize();
            inventory.AddRune(1, runeElement, runeSize);
            rune.PickUp();
        }
    }

    private void UpdateElement()
    {
        if (IsFireElement && elementHandler.GetIsFireActivated())
        {
            currentSpellElement = Elements.FIRE;
            fireAttackSource.Play();
        }
        else if (IsEarthElement && elementHandler.GetIsEarthActivated())
        {
            currentSpellElement = Elements.EARTH;
            earthAttackSource.Play();
        }
        else if (IsWindElement && elementHandler.GetIsWindActivated())
        {
            currentSpellElement = Elements.WIND;
            airAttackSource.Play();
        }        
        else if (IsWaterElement && elementHandler.GetIsWaterActivated())
        {
            currentSpellElement = Elements.WATER;
            waterAttackSource.Play();
        }    
    }

    public void PlayAttackedSound()
    {
        attackedSource.Play();
    }

    IEnumerator PlayRunSound()
    {
        isRunning = true;
        while (moveInputs.ReadValue<Vector2>() != Vector2.zero)
        {
            runSource.Play();
            yield return new WaitForSeconds(0.3f);
        }

        isRunning = false;
    }

    public void LearnThatElementIsDeactivated(Elements element)
    {
        if (currentSpellElement == element)
        {
            if (elementHandler.GetIsFireActivated())
                currentSpellElement = Elements.FIRE;
            else if (elementHandler.GetIsEarthActivated())
                currentSpellElement = Elements.EARTH;
            else if (elementHandler.GetIsWindActivated())
                currentSpellElement = Elements.WIND;
            else if (elementHandler.GetIsWaterActivated())
                currentSpellElement = Elements.WATER;
            else
                currentSpellElement = Elements.NONE;
        }
    }
}
