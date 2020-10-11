using System;
using Harmony;

namespace Script.Character.Player
{
  [Findable(Tags.Player)]
  public class PlayerInventory
  {
    private RuneRessource fireRuneRessources;
    private RuneRessource earthRuneRessources;
    private RuneRessource windRuneRessources;
    private RuneRessource waterRuneRessources;

    public RuneRessource FireRuneRessources => fireRuneRessources;
    public RuneRessource EarthRuneRessources => earthRuneRessources;
    public RuneRessource WindRuneRessources => windRuneRessources;
    public RuneRessource WaterRuneRessources => waterRuneRessources;

    public PlayerInventory()
    {
      fireRuneRessources = new RuneRessource(Elements.FIRE);
      earthRuneRessources = new RuneRessource(Elements.EARTH);
      windRuneRessources = new RuneRessource(Elements.WIND);
      waterRuneRessources = new RuneRessource(Elements.WATER);
    }

    public bool CanPay(Elements element, int cost)
    {
      switch (element)
      {
        case Elements.FIRE:
          return fireRuneRessources.CanPay(cost);
        case Elements.EARTH:
          return earthRuneRessources.CanPay(cost);
        case Elements.WIND:
          return windRuneRessources.CanPay(cost);
        case Elements.WATER:
          return waterRuneRessources.CanPay(cost);
        default:
          throw new Exception("The RuneRessource you are trying to fetch doesn't exist");
      }
    }
  }
}