using Harmony;

public class Altar : Structure
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected new void Destroy()
    {
        Finder.ElementHandler.DeactivateElement(current_element);
        base.Destroy();
    }
}


