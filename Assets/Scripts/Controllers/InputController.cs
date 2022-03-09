using UnityEngine;

public class InputController : MonoBehaviour
{
    public float horizontal { get; private set; } = 0;
    public float vertical { get; private set; } = 0;
    public bool jump { get; private set; } = false;
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jump = Input.GetButton("Jump");
    }
}
