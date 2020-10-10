using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttacks : MonoBehaviour
{
    [SerializeField] private GameObject tower_wind_projectile;

    public void WindAttack(Transform t)
    {
        Instantiate(tower_wind_projectile, t.position, t.rotation);
    }


}
