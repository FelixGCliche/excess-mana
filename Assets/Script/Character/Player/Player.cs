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

    private Elements currentSpellElement;

    private bool upKeyDown;
    private bool leftKeyDown;
    private bool downKeyDown;
    private bool rightKeyDown;

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        currentSpellElement = Elements.FIRE;
        
        upKeyDown = false;
        leftKeyDown = false;
        downKeyDown = false;
        rightKeyDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateElement();
        if (Input.GetKeyDown(attackKey))
            Attack();
        UpdateKeyState();
        CheckForMovement();
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
    
    #endregion
}
