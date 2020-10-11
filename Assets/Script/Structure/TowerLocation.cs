using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerLocation : MonoBehaviour
{
    [SerializeField] private Tower airTower;
    [SerializeField] private Tower earthTower;
    [SerializeField] private Tower fireTower;
    [SerializeField] private Tower waterTower;

    private InputAction moveInputs;
    private InputAction aimInput;
    private bool build => Finder.Inputs.Actions.Game.Interact.triggered;

    private Tower tower;

    private void Awake()
    {
        aimInput = Finder.Inputs.Actions.Game.Aim;
    }

    void Update()
    {
        if (build)
        {
            Ray ray = Camera.main.ScreenPointToRay(aimInput.ReadValue<Vector2>());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetInstanceID() == transform.GetInstanceID())
                {
                    Build();
                }
            }
        }
    }

    void Build()
    {
        if (tower != null)
        {
            tower.Upgrade();
        }
        else 
        {
            Vector3 position = transform.position;
            position.y = position.y + 0.5f;
            switch (Finder.Player.currentSpellElement) 
            {
                case Elements.FIRE:
                    tower = Instantiate(fireTower, position, transform.rotation);
                    break;
                case Elements.WATER:
                    tower = Instantiate(waterTower, position, transform.rotation);
                    break;
                case Elements.WIND:
                    tower = Instantiate(airTower, position, transform.rotation);
                    break;
                case Elements.EARTH:
                    tower = Instantiate(earthTower, position, transform.rotation);
                    break;
            }
        }
    }
}
