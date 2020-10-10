using UnityEngine;
using Script.Util;
using Script.Character;

public class Enemy : Character
{
    [SerializeField] EnemyDirections initialDirection = EnemyDirections.NONE;

    const string TARGET_DETECTION_HITBOX_NAME = "TagetDetectionHitbox";
    const int ENEMY_HEALTH = 25;
    const int ATTACK_RANGE = 5;

    GameObject target;
    Player player;

    void Start()
    {
        health = baseHealth;
        player = GetComponent<Player>();
    }


    void Update()
    {
        if (!IsNearTarget(target))
            Move();
        else
            Attack(target);
        
    }

    void Move()
    {
        target = GetNewTarget();
        FollowTarget(target);
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
        float distanceToTarget;

        distanceToTarget = DistanceCalculator(targetPosition);

        if (distanceToTarget <= 5)
            return true;

        return false;
    }

    GameObject GetNewTarget()
    {
        Structure[] structuresTargets = new Structure[1];
        structuresTargets = GetComponents<Structure>();
        
        float distanceToTarget = 0;
        float distanceToCurrentTarget = float.MaxValue;
        GameObject currentTarget = null;

        for (int i = 0; i < structuresTargets.Length; i++)
        {
            Vector2 targetPosition = structuresTargets[i].transform.position;
            distanceToTarget = DistanceCalculator(targetPosition);

            
            if (distanceToTarget < distanceToCurrentTarget)
            {
                distanceToCurrentTarget = distanceToTarget;
                currentTarget = structuresTargets[i].gameObject;
            }
        }

        return currentTarget;
    }

    void Attack(GameObject target)
    {
        
    }


    protected override void Kill()
    {
        //Play sound
        Destroy(gameObject);
    }

    float DistanceCalculator(Vector2 targetPosition)
    {
        Vector2 directionToTarget;
        float distanceToTarget;

        directionToTarget.x = (transform.position.x - targetPosition.x) * (transform.position.x - targetPosition.x);
        directionToTarget.y = (transform.position.y - targetPosition.y) * (transform.position.y - targetPosition.y);
        distanceToTarget = Mathf.Sqrt(directionToTarget.x + directionToTarget.y);

        return distanceToTarget;
    }
}
