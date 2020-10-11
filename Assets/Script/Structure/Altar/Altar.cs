using Game;
using Harmony;

public class Altar : Structure
{
    private GameController gameController;

    
    // Start is called before the first frame update
    void Start()
    {
        gameController = Finder.GameController;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected new void Destroy()
    {
        Finder.ElementHandler.DeactivateElement(currentElement);
        areAllElementDeactivated();
        base.Destroy();
    }

    private void areAllElementDeactivated()
    {
        if (!Finder.ElementHandler.GetIsEarthActivated() && 
            !Finder.ElementHandler.GetIsFireActivated() && 
            !Finder.ElementHandler.GetIsWaterActivated() && 
            !Finder.ElementHandler.GetIsWindActivated() );
        AltarsAreDestoyed();
    }

    void AltarsAreDestoyed()
    {
        gameController.AltarsAreDestoyed();
    }
}


