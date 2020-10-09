using UnityEngine;

public class Player : Character
{
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode space = KeyCode.Space;

    bool upKeyDown;
    bool leftKeyDown;
    bool downKeyDown;
    bool rightKeyDown;
    bool spaceKeyDown;

    public StructureHandler s;


    // Start is called before the first frame update
    void Start()
    {
        upKeyDown = false;
        leftKeyDown = false;
        downKeyDown = false;
        rightKeyDown = false;
        spaceKeyDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeyState();
        CheckForMovement();

        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");

            s.test();
            s.BuildTower(gameObject.transform);

        }
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
}
