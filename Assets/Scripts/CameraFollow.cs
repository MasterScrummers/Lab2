using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject; //the object the camera will be following (Mario in this case)
    public GameObject leftBoundary; //
    public Vector2 followOffset; //distance followObject can move before camera moves
    public float speed = 3f; //Default Camera speed

    private Vector2 threshold; //Screen boundary box
    private Vector3 boundaryPos = Vector3.zero; //Position of left boundary
    private Rigidbody2D rb; //Rigidbody2D reference of followObject
    private Vector2 camDimensions;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 followObjPos = followObject.transform.position;
        followObjPos.y = 2;
        followObjPos.z = -10;
        transform.position = followObjPos;

        threshold = calculateThreshold();
        Rect aspect = Camera.main.pixelRect; //camera aspect ratio
        camDimensions = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        boundaryPos.x = transform.position.x - camDimensions.x;
        leftBoundary.transform.position = boundaryPos;
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

        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed; //changes cam speed to player speed if that speed is greater than the deafult
        if (newCamPos.x > transform.position.x || newCamPos.y != transform.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, newCamPos, moveSpeed * Time.deltaTime);
            boundaryPos.x = transform.position.x - camDimensions.x;
            leftBoundary.transform.position = boundaryPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 calculateThreshold()
    {
        Rect aspect2 = Camera.main.pixelRect; //camera aspect ratio
        Vector2 camDimensionsOffsett = new Vector2(Camera.main.orthographicSize * aspect2.width / aspect2.height, Camera.main.orthographicSize);

        camDimensionsOffsett.x -= followOffset.x;
        camDimensionsOffsett.y -= followOffset.y;

        return camDimensionsOffsett;
    }

    //Displays Boundary in SceneView 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
