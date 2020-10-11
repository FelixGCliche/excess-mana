using System.Collections;
using Harmony;
using Script.Character;
using Script.Character.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using  Game;

public class Player : Character
{
    [SerializeField] private PlayerAttack playerAttack;
    
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

    private AudioSource deathSource;
    private AudioSource runSource;
    private AudioSource attackedSource;
    private AudioSource fireAttackSource;
    private AudioSource waterAttackSource;
    private AudioSource airAttackSource;
    private AudioSource earthAttackSource;

    private bool isRunning;
    
    public Grid grid;


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
        grid = FindObjectOfType<Grid>();

    }

    public void PlaceTowerNear(Vector2 nearPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(nearPoint);
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;.
        structureHandler.BuildTower(finalPosition, gameObject.transform, currentSpellElement);

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
        
        /*
        if (Input.GetKeyDown("space"))
        {
            structureHandler.BuildTower(gameObject.transform, currentSpellElement);
        }*/
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
            // structureHandler.LevelUpTower(structureHandler.CheckIsSelectedTower());

            // structureHandler.SetLifeTo1(structureHandler.CheckIsSelectedTower());
            // structureHandler.RepairTower(structureHandler.CheckIsSelectedTower());

            /* structureHandler.TestAdd(structureHandler.CheckIsSelectedTower(), inventory.GetRuneQuantity(Elements.WIND, RuneSize.SMALL), Elements.WIND, RuneSize.SMALL);
             inventory.RemoveRune(structureHandler.GetRequiredRunes(structureHandler.CheckIsSelectedTower(),RuneSize.SMALL), Elements.WIND, RuneSize.SMALL);
             

            RuneSizeSwitch(RuneSize.SMALL, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            RuneSizeSwitch(RuneSize.SMALL, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            RuneSizeSwitch(RuneSize.SMALL, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            Debug.Log("Inventoory player reste : " + inventory.GetRuneQuantity(Elements.WIND, RuneSize.SMALL));
        }*/

        //Debug.Log(inventory.GetRuneQuantity(Elements.WIND, RuneSize.SMALL));
        if (Build)
        {
            Vector2 a = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            PlaceTowerNear(a);
        }

        if(Interact)
        {
            Debug.Log("Interact");
          /*  RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position,
                Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue()));

            Debug.Log(hit.collider.name);*/
            RuneSizeSwitch(RuneSize.SMALL, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            RuneSizeSwitch(RuneSize.MEDIUM, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            RuneSizeSwitch(RuneSize.LARGE, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
        }

        //Debug.Log(Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue()));

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
        if (IsFireElement)
        {
            currentSpellElement = Elements.FIRE;
            fireAttackSource.Play();
        }
        else if (IsEarthElement)
        {
            currentSpellElement = Elements.EARTH;
            earthAttackSource.Play();
        }
        else if (IsWindElement)
        {
            currentSpellElement = Elements.WIND;
            airAttackSource.Play();
        }        
        else if (IsWaterElement)
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

}
