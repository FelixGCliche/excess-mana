using System.Collections;
using System.Collections.Generic;
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
    public List<Tower> tower_list = new List<Tower>();

    public int active_towers;

    public Tower temp;
    //================================ Methods

    public void Start()
    {
        current_tower_count = 0;
        XmlDocument doc = new XmlDocument();
        doc.Load("Assets/Script/Util/default_value.xml");

        foreach (XmlNode node in doc.DocumentElement.ChildNodes)
        {
            default_value_dictionnary.Add(node.Attributes["name"]?.InnerText, node.InnerText);
        }
    }

    public void Update()
    {
        
    }

    public Elements GetTowerElement(int tower_id)
    {
        return tower_list[tower_id].GetCurrentElement();
    }

    public void TestAdd(int tower_id, int quantity, Elements e, RuneSize s)
    {
        tower_list[tower_id].inventory.AddRune(quantity, e, s);
        if (tower_list[tower_id].CheckRuneQuantity(s))
            LevelUpTower(tower_id, quantity, e, s);
    }

    public int GetRequiredRunes(int tower_id, RuneSize rune_size)
    {
        return tower_list[tower_id].RuneQuantityNumber(rune_size);
    }
    /*
    public void Test(int tower_id, RuneSize runesize, int rune_number)
    {
        switch( runesize)
        {
            case RuneSize.SMALL:
                tower_list[tower_id].current_small_runes += rune_number;
                break;

            case RuneSize.MEDIUM:
                tower_list[tower_id].current_medium_runes += rune_number;
                break;

            case RuneSize.LARGE:
                tower_list[tower_id].current_large_runes += rune_number;
                break;
        }
    }*/

    public void SwitchRUneTYpeByLevel(int tower_id)
    {
        tower_list[tower_id].GetCurrentLevel();
    }

    public int CheckIsSelectedTower()
    {
        foreach(Tower t in tower_list)
        {
            if (t.is_selected)
            {
                return t.id;
            }
        }
        return 9999;
    }

    public void LevelUpTower(int tower_id, int quantity, Elements e, RuneSize s)
    {
        int tempLevel = tower_list[tower_id].GetCurrentLevel();
        if(tempLevel < tower_list[tower_id].max_level)
        {
            tempLevel++;
            tower_list[tower_id].SetCurrentLevel(tempLevel);
            tower_list[tower_id].inventory.RemoveRune(quantity, e, s);
        }

    }

    public void SetLifeTo1(int tower_id)
    {
        tower_list[tower_id].TakeDmage(99f,Elements.FIRE);
    }

    public void RepairTower(int tower_id)
    {
        if(tower_list[tower_id].current_life <= tower_list[tower_id].default_life)
        {
            StartCoroutine(tower_list[tower_id].DoRepair(10));
        }
        else 
        {
            tower_list[tower_id].current_life = tower_list[tower_id].default_life;
        }
    }

    public void BuildTower(Transform t, Elements element)
    {
        switch(element)
        {
            case Elements.EARTH:
                print("Earth");
                temp = Instantiate(earth_tower, t.position, t.rotation);
                initialise_tower(temp, t, element,current_tower_count);
                break;

            case Elements.FIRE:
                print("fire");
                temp = Instantiate(fire_tower, t.position, t.rotation);
                initialise_tower(temp, t, element, current_tower_count);
                break;

            case Elements.WATER:
                print("Water");
                temp = Instantiate(water_tower, t.position, t.rotation);
                initialise_tower(temp, t, element, current_tower_count);
                break;

            case Elements.WIND:
                print("AIR");
                temp = Instantiate(air_tower, t.position, t.rotation);
                initialise_tower(temp, t,element, current_tower_count);
                break;

            default:

                break;
        }
        tower_list.Add(temp);

        current_tower_count++;
    }

    public void initialise_tower(Tower tower, Transform transform, Elements e, int id)
    {
        tower.SetCurrentElement(e);
        tower.SetId(id);

        tower.SetCurrentLife(GetDictionnaryFloatValue("MAX_TOWER_LIFE"));
        tower.SetCurrentLevel(GetDictionnaryIntValue("default_level"));
        tower.SetCurrentExp(GetDictionnaryIntValue("default_exp"));
        tower.SetCurrentRuneNumber(GetDictionnaryIntValue("default_rune_number"));
        tower.SetAttackRadius(GetDictionnaryFloatValue("default_attack_radius"));

        tower.SetFireCountdown(GetDictionnaryFloatValue("default_fire_countdown"));
        tower.SetFireRate(GetDictionnaryFloatValue("default_fire_rate"));

        tower.max_level = GetDictionnaryIntValue("MAX_TOWER_LEVEL");

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
