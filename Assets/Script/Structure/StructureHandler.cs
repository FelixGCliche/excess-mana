using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using UnityEngine;

public class StructureHandler : MonoBehaviour
{
    //================================ Variables

    public Structure altair;
    public Structure tower;
    public Tower to;

    public int current_tower_count;

    public Dictionary<string, string> default_value_dictionnary = new Dictionary<string, string>();
    // int result = Int32.Parse(input);
    //  Console.WriteLine(result);

    //================================ Methods

    public void Start()
    {

        XmlDocument doc = new XmlDocument();
        doc.Load("Assets/Script/Util/default_value.xml");

        foreach (XmlNode node in doc.DocumentElement.ChildNodes)
        {
            default_value_dictionnary.Add(node.Attributes["name"].InnerText, node.InnerText);
        }

        /*
        foreach (KeyValuePair<string, string> entry in default_value_dictionnary)
        {
            print(entry.Key);
            print(entry.Value);
        }*/


    }

    public void BuildTower(Transform t, Elements element)
    {
        switch(element)
        {
            case Elements.EARTH:
                print("Earth");
                break;

            case Elements.FIRE:
                print("fire");
                break;

            case Elements.WATER:
                print("Water");
                break;

            case Elements.WIND:
                print("AIR");
                break;

            default:

                break;
        }
    
        //[TODO] - a changer
        Structure new_structure = Instantiate(to, t.position, t.rotation);
        new_structure.SetSpawnPosition(t.position);
    }

    public void test()
    {
        print("erere");
    }
}
