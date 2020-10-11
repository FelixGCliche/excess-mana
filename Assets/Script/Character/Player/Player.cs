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
    
    private new void Awake()
    { 
        base.Awake();

        moveInputs = Finder.Inputs.Actions.Game.Move;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        inventory = new PlayerInventory();
        currentSpellElement = Elements.FIRE;

        structureHandler = gameObject.GetComponent<StructureHandler>();
        transform.position = new Vector3(0,0,0);
       inventory.AddRune(10, Elements.WIND, RuneSize.SMALL);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateElement();
        if (Fire)
            Attack();

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
            structureHandler.BuildTower(gameObject.transform, currentSpellElement);
        }

        if(Interact)
        {
            Debug.Log("Interact");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position,
                Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue()));

            Debug.Log(hit.collider.name);
            //RuneSizeSwitch(RuneSize.SMALL, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            //RuneSizeSwitch(RuneSize.MEDIUM, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
            // RuneSizeSwitch(RuneSize.LARGE, structureHandler.GetTowerElement(structureHandler.CheckIsSelectedTower()));
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
            inventory.AddRune(1, runeElement, runeSize);
            rune.PickUp();
        }
    }

    private void UpdateElement()
    {
        if (IsFireElement)
            currentSpellElement = Elements.FIRE;
        else if (IsEarthElement)
            currentSpellElement = Elements.EARTH;
        else if (IsWindElement)
            currentSpellElement = Elements.WIND;
        else if (IsWaterElement)
            currentSpellElement = Elements.WATER;
    }
}
