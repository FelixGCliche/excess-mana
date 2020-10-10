
using Script.Character;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;
        movement += Vector2.right;

        transform.Translate(Time.deltaTime * 1 * movement);

    }

    protected override void Kill()
    {
        Destroy(gameObject);
    }
}
