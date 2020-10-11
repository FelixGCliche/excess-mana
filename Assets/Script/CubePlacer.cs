
using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    public Grid grid;


    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.point);
            }
        }*/
    }


    public void PlaceCubeNear(Vector2 nearPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(nearPoint);
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
    }

}
