using Harmony;
using Script.Character;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using  Game;
using System.Collections.Generic;
using Script.Menu;
using UnityEngine.Serialization;

[Findable(Tags.Player)]
public class Player : Character
{

    [SerializeField]
    private ProgressBar manaBar;
    
    [SerializeField] private PlayerAttack playerAttack;

    [SerializeField] private Transform spriteTransform;

    private PlayerInventory inventory;

    [FormerlySerializedAs("gameOverMenu")] [SerializeField] private GameOverController gameOverController;
    
    private InputAction moveInputs;
    
    private bool Fire => Finder.Inputs.Actions.Game.Fire.triggered;
    private bool IsFireElement => Finder.Inputs.Actions.Game.FireElement.triggered;
    private bool IsWaterElement => Finder.Inputs.Actions.Game.WaterElement.triggered;
    private bool IsWindElement => Finder.Inputs.Actions.Game.WindElement.triggered;
    private bool IsEarthElement => Finder.Inputs.Actions.Game.EarthElement.triggered;

    public Elements currentSpellElement;
    private ElementHandler elementHandler;

    private AudioSource deathSource;
    private AudioSource runSource;
    private AudioSource attackedSource;
    private AudioSource fireAttackSource;
    private AudioSource waterAttackSource;
    private AudioSource airAttackSource;
    private AudioSource earthAttackSource;

    private bool isRunning;

    public PlayerInventory Inventory => inventory;
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
        inventory = new PlayerInventory();
    }

    // Start is called before the first frame update
    private void Start()
    {
        health = baseHealth;
        currentSpellElement = Elements.FIRE;

        transform.position = new Vector3(0,0,0);

        StartCoroutine(DoRegenLife(1));
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateManaBar();
        UpdateElement();
        CollectRune();
        
        if (Fire)
            Attack();

        if (baseHealth <= 0)
            Kill();
        
        if (moveInputs.ReadValue<Vector2>() != Vector2.zero && !isRunning)
        {
            StartCoroutine(PlayRunSound());
        }

        Mover.Move(moveInputs.ReadValue<Vector2>());
        ReflectPlayerSprite();
    }

    private void ReflectPlayerSprite()
    {
        if (moveInputs.ReadValue<Vector2>().x < 0)
        {
            spriteTransform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInputs.ReadValue<Vector2>().x > 0)
        {
            spriteTransform.localScale = new Vector3(-1, 1, 1);
        }
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


    private void UpdateManaBar()
    {
        manaBar.UpdateProgressBar(playerAttack.AttackCooldownPercentage);
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

    public void RegenLife(int life_amount)
    {
        if (health < baseHealth)
        {
            health += life_amount;
        }
        else
        {
            health = baseHealth;
        }
        healthBar.UpdateProgressBar(health / baseHealth);
    }

    public IEnumerator DoRegenLife(int life_amount)
    {
        for(; ; )
        {
            RegenLife(life_amount);
            yield return new WaitForSeconds(1);
        }
    }

    private void CollectRune()
    {
        ISensor<Rune> runeSensor = ColliderSensor.For<Rune>();
        if (runeSensor.SensedObjects.Count > 0)
        {
            Debug.Log("Collecting");
            foreach (Rune rune in runeSensor.SensedObjects)
            {
                inventory.Collect(rune.Element, rune.Value);
                rune.PickUp();
            }
        }
    }

}
