using UnityEngine;
using Script.Util;
using Script.Character;

public class Enemy : Character
{
    [SerializeField] EnemyDirections initialDirection = EnemyDirections.NONE;

    const string TARGET_DETECTION_HITBOX_NAME = "TagetDetectionHitbox";
    const int ENEMY_HEALTH = 25;
    const int ATTACK_RANGE = 5;

    CircleCollider2D targetDetectionHitbox;

    void Start()
    {
        targetDetectionHitbox = GameObject.Find(TARGET_DETECTION_HITBOX_NAME).GetComponent<CircleCollider2D>();
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
                //Follow target
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

    bool IsNearTarget(GameObject target)
    {
        Vector2 targetPosition = target.transform.position;
        Vector2 direction;
        float distanceToTarget;

        direction.x = (transform.position.x - targetPosition.x) * (transform.position.x - targetPosition.x);
        direction.y = (transform.position.y - targetPosition.y) * (transform.position.y - targetPosition.y);
        distanceToTarget = Mathf.Sqrt(direction.x + direction.y);

        if (distanceToTarget <= 5)
            return true;

        return false;
    }

    void Attack(GameObject target)
    {
        
    }

    void detectTarget();


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
