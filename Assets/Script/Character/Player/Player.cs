using Harmony;
using Script.Character;
using Script.Character.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using  Game;
using System.Collections.Generic;

[Findable(Tags.Player)]
public class Player : Character
{
    [SerializeField] private PlayerAttack playerAttack;

    [SerializeField] private Transform spriteTransform;

    private InputAction moveInputs;
    private bool Fire => Finder.Inputs.Actions.Game.Fire.triggered;
    private bool IsFireElement => Finder.Inputs.Actions.Game.FireElement.triggered;
    private bool IsWaterElement => Finder.Inputs.Actions.Game.WaterElement.triggered;
    private bool IsWindElement => Finder.Inputs.Actions.Game.WindElement.triggered;
    private bool IsEarthElement => Finder.Inputs.Actions.Game.EarthElement.triggered;

    private PlayerInventory playerInventory;

    public Elements currentSpellElement;
    private ElementHandler elementHandler;

    private new void Awake()
    { 
        base.Awake();

        moveInputs = Finder.Inputs.Actions.Game.Move;
        elementHandler = Finder.ElementHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        playerInventory = new PlayerInventory();
        currentSpellElement = Elements.FIRE;

        transform.position = new Vector3(0,0,0);

       playerInventory.AddRune(100, Elements.WIND, RuneSize.SMALL);
       playerInventory.AddRune(100, Elements.WIND, RuneSize.MEDIUM);
       playerInventory.AddRune(100, Elements.WIND, RuneSize.LARGE);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateElement();
        if (Fire)
            Attack();

        Mover.Move(moveInputs.ReadValue<Vector2>());
        if (moveInputs.ReadValue<Vector2>().x < 0)
        {
            spriteTransform.localScale = new Vector3(1,1,1);
        }
        else if (moveInputs.ReadValue<Vector2>().x > 0)
        {
            spriteTransform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void Kill()
    {
        
    }
    
    public void Attack()
    {
        if (playerAttack.IsReadyToAttack())
        {
            switch (currentSpellElement)
            {
                case Elements.FIRE:
                    playerAttack.FireAttack(transform.position);
                    break;
                case Elements.EARTH:
                    playerAttack.EarthAttack(transform.position);
                    break;
                case Elements.WIND:
                    playerAttack.WindAttack(transform);
                    break;
                case Elements.WATER:
                    playerAttack.WaterAttack(transform.position);
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
            playerInventory.AddRune(1, runeElement, runeSize);
            rune.PickUp();
        }
    }

    private void UpdateElement()
    {
        if (IsFireElement && elementHandler.GetIsFireActivated())
            currentSpellElement = Elements.FIRE;
        else if (IsEarthElement && elementHandler.GetIsEarthActivated())
            currentSpellElement = Elements.EARTH;
        else if (IsWindElement && elementHandler.GetIsWindActivated())
            currentSpellElement = Elements.WIND;
        else if (IsWaterElement && elementHandler.GetIsWaterActivated())
            currentSpellElement = Elements.WATER;
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
