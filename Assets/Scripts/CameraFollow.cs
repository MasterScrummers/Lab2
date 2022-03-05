using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject; //the object the camera will be following (Mario in this case)
    public Vector2 followOffset; //distance followObject can move before camera moves
    public float speed = 3f; //Default Camera speed

    private Vector2 threshold; //Screen boundary box
    private Rigidbody2D rb; //Rigidbody2D reference of followObject

    // Start is called before the first frame update
    void Start()
    {
        threshold = calculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 follow = followObject.transform.position;
        float xDiff = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDiff = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newCamPos = transform.position;
        if (Mathf.Abs(xDiff) >= threshold.x)
        {
            newCamPos.x = follow.x;
        }

        if (Mathf.Abs(yDiff) >= threshold.y)
        {
            newCamPos.y = follow.y;
        }

        transform.position = Vector3.MoveTowards(transform.position, newCamPos, speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect; //camera aspect ratio
        Vector2 camDimensions = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);

        camDimensions.x -= followOffset.x;
        camDimensions.y -= followOffset.y;

        return camDimensions;
    }

    //Displays Boundary in SceneView 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
