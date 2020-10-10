
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Tower : Structure
{
    //================================ Variables

    private Elements tower_element;

    private float radius;


    //================================ Methods

    public Tower(Elements e)
    {
        tower_element = e;        
    }

    void awake()
    {
       
    }

    void Start()
    { 

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

            print("urhtguirho");
        
    }


    //================================ Accessors
    public void SetAttackRadius(float radius)
    {
        this.radius = radius;
    }



}
