
using Script.Character;

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
        
    }

    protected override void Kill()
    {
        Destroy(gameObject);
    }
}
