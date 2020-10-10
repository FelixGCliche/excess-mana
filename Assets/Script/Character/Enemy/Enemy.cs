using UnityEngine;
using Script.Util;
using Script.Character;

public class Enemy : Character
{
    [SerializeField] EnemyDirections initialDirection = InitialDirections.NONE;


    const int ENEMY_SPEED = 15;
    const int ENEMY_HEALTH = 25;

    void Start()
    {
        
    }


    void Update()
    {
        Move();
        
        
    }

    void Move()
    {

        switch (initialDirection)
        {
            case EnemyDirections.RIGHT:
                transform.Translate(Time.deltaTime * speed * Vector2.right);
                break;
            case EnemyDirections.LEFT:
                transform.Translate(Time.deltaTime * speed * Vector2.left);
                break;
            case EnemyDirections.UP:
                transform.Translate(Time.deltaTime * speed * Vector2.up);
                break;
            case EnemyDirections.DOWN:
                transform.Translate(Time.deltaTime * speed * Vector2.down);
                break;
            case EnemyDirections.FOLLOW_TARGET:
                FollowTarget();
                break;
        }
    }

    void FollowTarget(GameObject target)
    {
        Vector2 targetPosition = target.transform.position;
        Vector2 direction;

        direction.x = transform.position.x - targetPosition.x;
        direction.y = transform.position.y - targetPosition.y;

        transform.Translate(Time.deltaTime * speed * direction);
    }

    void Attack()
    {

    }

    bool IsDetectingTarget()
    {

        return false;
    }

    protected override void Kill()
    {
        //Play sound
        Destroy(gameObject);
    }
}
