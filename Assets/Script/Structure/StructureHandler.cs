using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using UnityEngine;

public class StructureHandler : MonoBehaviour
{
    //================================ Variables

    public Structure altair;

    public Tower air_tower;
    public Tower earth_tower;
    public Tower fire_tower;
    public Tower water_tower;

    public int current_tower_count;

    public Dictionary<string, string> default_value_dictionnary = new Dictionary<string, string>();

    //================================ Methods

    public void Start()
    {

        XmlDocument doc = new XmlDocument();
        doc.Load("Assets/Script/Util/default_value.xml");

        foreach (XmlNode node in doc.DocumentElement.ChildNodes)
        {
            default_value_dictionnary.Add(node.Attributes["name"]?.InnerText, node.InnerText);
        }
    }

    public void BuildTower(Transform t, Elements element)
    {
        switch(element)
        {
            case Elements.EARTH:
                print("Earth");
                Tower new_earth_tower = Instantiate(earth_tower, t.position, t.rotation);
                initialise_tower(new_earth_tower,t);
                break;

            case Elements.FIRE:
                print("fire");
                Tower new_fire_tower = Instantiate(fire_tower, t.position, t.rotation);
                initialise_tower(new_fire_tower,t);
                break;

            case Elements.WATER:
                print("Water");
                Tower new_water_tower = Instantiate(water_tower, t.position, t.rotation);
                initialise_tower(new_water_tower,t);
                break;

            case Elements.WIND:
                print("AIR");
                Tower new_air_tower = Instantiate(air_tower, t.position, t.rotation);
                initialise_tower(new_air_tower,t);
                break;

            default:

                break;
        }
    }

    public void initialise_tower(Tower tower, Transform transform)
    {
        tower.SetCurrentLife(GetDictionnaryFloatValue("MAX_TOWER_LIFE"));
        tower.SetCurrentLevel(GetDictionnaryIntValue("default_level"));
        tower.SetCurrentExp(GetDictionnaryIntValue("default_exp"));
        tower.SetCurrentRuneNumber(GetDictionnaryIntValue("default_rune_number"));
        tower.SetAttackRadius(GetDictionnaryFloatValue("default_attack_radius"));

        tower.SetFireCountdown(GetDictionnaryFloatValue("default_fire_countdown"));
        tower.SetFireRate(GetDictionnaryFloatValue("default_fire_rate"));

        tower.SetSpawnPosition(transform.position);
        tower.SetCurrentState(StructureState.Idle);
    }


    private float GetDictionnaryFloatValue(string key)
    {
        return float.Parse(default_value_dictionnary[key]);
    }

    private int GetDictionnaryIntValue(string key)
    {
        return int.Parse(default_value_dictionnary[key]);
    }
}
