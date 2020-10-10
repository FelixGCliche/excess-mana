using Harmony;
using Script.Character;
using Script.Character.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private PlayerAttack playerAttack;
    
    private InputAction moveInputs;
    private bool Fire => Finder.Inputs.Actions.Game.Fire.triggered;
    private bool Interact => Finder.Inputs.Actions.Game.Interact.triggered;
    private bool Build => Finder.Inputs.Actions.Game.Interact.triggered;

    StructureHandler structureHandler;
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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateElement();
        if (Fire)
            Attack();
        
        Mover.Move(moveInputs.ReadValue<Vector2>());

        if (Build)
            structureHandler.BuildTower(gameObject.transform, Elements.WIND);
        
    }

    protected override void Kill()
    {
        
    }
    
    public void Attack()
    {
        switch (currentSpellElement)
        {
            case Elements.FIRE :
                playerAttack.FireAttack(transform.position);
                break;
            case Elements.EARTH :
                playerAttack.EarthAttack(transform.position);
                break;
            case Elements.WIND :
                playerAttack.WindAttack(transform.position);
                break;
            case Elements.WATER :
                playerAttack.WaterAttack(transform.position);
                break;
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
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        //     currentSpellElement = Elements.FIRE;
        // else if (Input.GetKeyDown(KeyCode.Alpha2))
        //     currentSpellElement = Elements.EARTH;
        // else if (Input.GetKeyDown(KeyCode.Alpha3))
        //     currentSpellElement = Elements.WIND;
        // else if (Input.GetKeyDown(KeyCode.Alpha4))
        //     currentSpellElement = Elements.WATER;
    }
}
