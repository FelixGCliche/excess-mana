using UnityEngine;

public class Player : Character
{
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode rightKey = KeyCode.D;

    bool upKeyDown;
    bool leftKeyDown;
    bool downKeyDown;
    bool rightKeyDown;

    // Start is called before the first frame update
    void Start()
    {
        upKeyDown = false;
        leftKeyDown = false;
        downKeyDown = false;
        rightKeyDown = false;
    }

    // Update is called once per frame
    void Update()
    {
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
}
