using UnityEngine;
using Script.Util;
using Script.Character;

public class Enemy : Character
{
    [SerializeField] int enemyAttackRange = 2;
    [SerializeField] int enemyDetectionRange = 10;
    [SerializeField] bool isInWave = false;

    GameObject target;
    Player player;

    bool isPlayerDetected = false;

    void Start()
    {
        health = baseHealth;
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();

        if (!isInWave)
            target = player.gameObject;
    }

    void Update()
    {
        if (isInWave)
        {
            if (!IsNearTarget())
                Move();
            else
                Attack();
        }
        else
        {
            isPlayerNear();

            if (isPlayerDetected)
            {
                if (IsNearTarget())
                    Attack();
                else
                    Move();
            }
        }
        
    }

    void Move()
    {
        if (isInWave)
            target = GetNewTarget();
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector2 targetPosition = target.transform.position;
        Vector2 direction;

        direction.x = transform.position.x - targetPosition.x;
        direction.y = transform.position.y - targetPosition.y;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed);
    }

    bool IsNearTarget()
    {
        if (target != null)
        {
            Vector2 targetPosition = target.transform.position;
            float distanceToTarget;

            distanceToTarget = DistanceCalculator(targetPosition);

            if (distanceToTarget <= enemyAttackRange)
                return true;
        }

        return false;
    }

    void isPlayerNear()
    {
        if (DistanceCalculator(player.transform.position) <= enemyDetectionRange)
            isPlayerDetected = true;
        else
            if (DistanceCalculator(player.transform.position) <= enemyDetectionRange * 3)
                isPlayerDetected = false;
    }

    GameObject GetNewTarget()
    {
        Structure[] structuresTargets = new Structure[1];
        structuresTargets = GetComponents<Structure>();
        
        float distanceToTarget = 0;
        float distanceToCurrentTarget = float.MaxValue;
        GameObject currentTarget = null;

        if(GameObject.FindGameObjectsWithTag("Structure").Length > 0)
        {
            foreach (GameObject structure in GameObject.FindGameObjectsWithTag("Structure"))
            {
                distanceToTarget = DistanceCalculator(structure.transform.position);

                if (distanceToTarget < distanceToCurrentTarget)
                {
                    distanceToCurrentTarget = distanceToTarget;
                    currentTarget = structure.gameObject;
                }
            }
        }
        
        if (DistanceCalculator(player.transform.position) <= distanceToCurrentTarget)
            currentTarget = player.gameObject;

        return currentTarget;
    }

    void Attack()
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
