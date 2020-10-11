using Script.Character;
using Script.Character.Player;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;


    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private KeyCode space = KeyCode.Space;
    [SerializeField] private KeyCode e = KeyCode.E;
    [SerializeField] private KeyCode q = KeyCode.Q;


    bool upKeyDown ;
    bool leftKeyDown;
    bool downKeyDown;
    bool rightKeyDown;
    bool spaceKeyDown;

    public StructureHandler structureHandler;
    private PlayerInventory inventory;

    private Elements currentSpellElement;
    
    private new void Awake()
    { 
        base.Awake();
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

        inventory.AddRune(10, Elements.WIND, RuneSize.SMALL);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateElement();
        if (Input.GetKeyDown(attackKey))
            Attack();
        UpdateKeyState();
        CheckForMovement();

        if (Input.GetKeyDown("space"))
        {
            structureHandler.BuildTower(gameObject.transform, Elements.WIND);
        }

        if(Input.GetKeyDown("q"))
        {
            inventory.AddRune(10, Elements.WIND, RuneSize.SMALL);

        }

        if (Input.GetKeyDown("e"))
        {
            // structureHandler.LevelUpTower(structureHandler.CheckIsSelectedTower());

            // structureHandler.SetLifeTo1(structureHandler.CheckIsSelectedTower());
            // structureHandler.RepairTower(structureHandler.CheckIsSelectedTower());

            structureHandler.TestAdd(structureHandler.CheckIsSelectedTower(), inventory.GetRuneQuantity(Elements.WIND, RuneSize.SMALL), Elements.WIND, RuneSize.SMALL);
            inventory.RemoveRune(structureHandler.GetRequiredRunes(structureHandler.CheckIsSelectedTower()), Elements.WIND, RuneSize.SMALL);
            Debug.Log("Inventoory player reste : " + inventory.GetRuneQuantity(Elements.WIND, RuneSize.SMALL));
        }

        Debug.Log(inventory.GetRuneQuantity(Elements.WIND, RuneSize.SMALL));
    }

    private void CheckForMovement()
    {
        Vector2 movement = Vector2.zero;

        if (upKeyDown && !downKeyDown)
            movement += Vector2.up;
        else if(!upKeyDown && downKeyDown)
            movement += Vector2.down;

        if (leftKeyDown && !rightKeyDown)
            movement += Vector2.left;
        else if (!leftKeyDown && rightKeyDown)
            movement += Vector2.right;

        if (movement.x != 0 && movement.y != 0)
        {
            movement.x *= 0.7f;
            movement.y *= 0.7f;
        }

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
