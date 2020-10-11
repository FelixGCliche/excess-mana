using Harmony;
using Script.Character;
using Script.Character.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Game;

[Findable(Tags.Player)]
public class Player : Character
{
  [SerializeField] private PlayerAttack playerAttack;

  private InputAction moveInputs;
  private InputAction fireInput;
  private InputAction interactInput;
  private InputAction buildInput;
  private InputAction fireElementInput;
  private InputAction waterElementInput;
  private InputAction windElementInput;
  private InputAction earthElementInput;

  StructureHandler structureHandler;
  private PlayerInventory inventory;

  private Elements currentSpellElement;

  public Elements CurrentSpellElement => currentSpellElement;

  public InputAction[] ElementInputs => new[]
  {
    fireElementInput,
    earthElementInput,
    windElementInput,
    waterElementInput
  };

  private new void Awake()
  {
    base.Awake();

    moveInputs = Finder.Inputs.Actions.Game.Move;
    fireInput = Finder.Inputs.Actions.Game.Fire;
    interactInput = Finder.Inputs.Actions.Game.Interact;
    buildInput = Finder.Inputs.Actions.Game.Interact;
    fireElementInput = Finder.Inputs.Actions.Game.FireElement;
    waterElementInput = Finder.Inputs.Actions.Game.WaterElement;
    windElementInput = Finder.Inputs.Actions.Game.WindElement;
    earthElementInput = Finder.Inputs.Actions.Game.EarthElement;
  }

  // Start is called before the first frame update
  void Start()
  {
    health = baseHealth;
    inventory = new PlayerInventory();
    currentSpellElement = Elements.FIRE;

    structureHandler = gameObject.GetComponent<StructureHandler>();
    transform.position = new Vector3(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    UpdateElement();
    if (fireInput.triggered)
      Attack();

    Mover.Move(moveInputs.ReadValue<Vector2>());

    if (buildInput.triggered)
      structureHandler.BuildTower(gameObject.transform, Elements.WIND);
  }

  protected override void Kill()
  {
  }

  public void Attack()
  {
    switch (currentSpellElement)
    {
      case Elements.FIRE:
        playerAttack.FireAttack(transform.position);
        break;
      case Elements.EARTH:
        playerAttack.EarthAttack(transform.position);
        break;
      case Elements.WIND:
        playerAttack.WindAttack(transform.position);
        break;
      case Elements.WATER:
        playerAttack.WaterAttack(transform.position);
        break;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Rune rune = other.GetComponentInParent<Rune>();
    if (rune != null)
    {
      Elements runeElement = rune.GetElement();
      RuneSize runeSize = rune.GetSize();
      inventory.AddRune(1, runeElement, runeSize);
      rune.PickUp();
    }
  }

  private void UpdateElement()
  {
    if (fireElementInput.triggered)
      currentSpellElement = Elements.FIRE;
    else if (earthElementInput.triggered)
      currentSpellElement = Elements.EARTH;
    else if (windElementInput.triggered)
      currentSpellElement = Elements.WIND;
    else if (waterElementInput.triggered)
      currentSpellElement = Elements.WATER;
  }
}