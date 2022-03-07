using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(ControllerFinder))]
public class SecretPipe : MonoBehaviour
{
    public string destination;
    private bool isColliding = false;

    public bool pressUp = false;
    public bool pressDown = true;
    public bool pressLeft = false;
    public bool pressRight = false;
    private bool isPressing;

    public Vector3 setPlayerPosition;

    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Update()
    {
        isPressing = pressUp && Input.GetAxisRaw("Vertical") == 1;
        isPressing = isPressing || pressDown && Input.GetAxisRaw("Vertical") == -1;
        isPressing = isPressing || pressRight && Input.GetAxisRaw("Horizontal") == 1;
        isPressing = isPressing || pressLeft && Input.GetAxisRaw("Horizontal") == -1;
        if (isColliding && isPressing)
        {
            DoStatic.LoadScene(destination);
            GetComponent<ControllerFinder>().gameController.GetComponent<SceneController>().ChangeScene(destination, setPlayerPosition);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}
