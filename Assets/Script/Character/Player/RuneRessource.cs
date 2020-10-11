namespace Script.Character.Player
{
  public struct RuneRessource
  {
    private Elements element;
    private int total;

    public Elements Element => element;
    public int Total => total;

    public RuneRessource(Elements element)
    {
      this.element = element;
      total = 0;
    }

    public bool CanPay(int cost)
    {
      if (total < cost) return false;
      total -= cost;
      return true;
    }

    public void Collect(int cost)
    {
      total += cost;
    }
  }
}