using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StructureHandler : MonoBehaviour
{
    //================================ Variables

    public Structure altair;
    public Structure tower;

    public int current_tower_count;

    //================================ Methods


    public void BuildTower(Transform  t)
    {
        Instantiate(tower, t.position, t.rotation);
    }

    public void test()
    {
        print("erere");
    }
}
