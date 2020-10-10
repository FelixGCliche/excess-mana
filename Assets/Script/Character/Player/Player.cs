using Harmony;
using Script.Character;
using Script.Character.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private KeyCode space = KeyCode.Space;

    bool upKeyDown ;
    bool leftKeyDown;
    bool downKeyDown;
    bool rightKeyDown;
    bool spaceKeyDown;

    private InputAction moveInputs;
    private InputAction aimInput;
    private bool fire;
    private bool interact;
    private bool build;

    StructureHandler structureHandler;
    private PlayerInventory inventory;

    private Elements currentSpellElement;
    
    private new void Awake()
    {
        base.Awake();

        moveInputs = Finder.Inputs.Actions.Game.Move;
        aimInput = Finder.Inputs.Actions.Game.Aim;
        fire = Finder.Inputs.Actions.Game.Fire.triggered;
        interact = Finder.Inputs.Actions.Game.Interact.triggered;
        build = Finder.Inputs.Actions.Game.Interact.triggered;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        inventory = new PlayerInventory();
        currentSpellElement = Elements.FIRE;
        
        upKeyDown = false;
        leftKeyDown = false;
        downKeyDown = false;
        rightKeyDown = false;
        spaceKeyDown = false;

        structureHandler = gameObject.GetComponent<StructureHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateElement();
        if (fire)
            Attack();
        // UpdateKeyState();
        CheckForMovement();

        if (build)
            structureHandler.BuildTower(gameObject.transform, Elements.WIND);
        
    }

    private void CheckForMovement()
    {
        Vector2 movement = Vector2.zero;

        // if (upKeyDown && !downKeyDown)
        //     movement += Vector2.up;
        // else if(!upKeyDown && downKeyDown)
        //     movement += Vector2.down;
        //
        // if (leftKeyDown && !rightKeyDown)
        //     movement += Vector2.left;
        // else if (!leftKeyDown && rightKeyDown)
        //     movement += Vector2.right;
        //
        // if (movement.x != 0 && movement.y != 0)
        // {
        //     movement.x *= 0.7f;
        //     movement.y *= 0.7f;
        // }

        
        transform.Translate(Time.deltaTime * speed * movement);

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
    
    #region Input

    private void UpdateElement()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentSpellElement = Elements.FIRE;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            currentSpellElement = Elements.EARTH;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            currentSpellElement = Elements.WIND;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            currentSpellElement = Elements.WATER;
    }
    
    private void UpdateKeyState()
    {
        UpdateUpKeyState();
        UpdateLeftKeyState();
        UpdateDownKeyState();
        UpdateRightKeyState();
        UpdateSpaceKeyState();
    }

    private void UpdateUpKeyState()
    {
        if (!upKeyDown && Input.GetKeyDown(upKey))
            upKeyDown = true;
        else if (upKeyDown && Input.GetKeyUp(upKey))
            upKeyDown = false;
    }

    private void UpdateLeftKeyState()
    {
        if (!leftKeyDown && Input.GetKeyDown(leftKey))
            leftKeyDown = true;
        else if (leftKeyDown && Input.GetKeyUp(leftKey))
            leftKeyDown = false;
    }

    private void UpdateDownKeyState()
    {
        if (!downKeyDown && Input.GetKeyDown(downKey))
            downKeyDown = true;
        else if (downKeyDown && Input.GetKeyUp(downKey))
            downKeyDown = false;
    }

    private void UpdateRightKeyState()
    {
        if (!rightKeyDown && Input.GetKeyDown(rightKey))
            rightKeyDown = true;
        else if (rightKeyDown && Input.GetKeyUp(rightKey))
            rightKeyDown = false;
    }

    private void UpdateSpaceKeyState()
    {
        if (!spaceKeyDown && Input.GetKeyDown(space))
            spaceKeyDown = true;
        else if (spaceKeyDown && Input.GetKeyUp(space))
            spaceKeyDown = false;
    }
    
    #endregion
}
