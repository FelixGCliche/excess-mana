using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float size = 1.0f;

    public Vector2 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);

        Vector3 result = new Vector2
            (
            (float)xCount * size,
            (float)yCount * size
            );

        result += transform.position;
        return result;

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for(float x = -25; x < 25; x+= size)
        {
            for(float y = -25; y < 25; y+= size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, y, 0));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
