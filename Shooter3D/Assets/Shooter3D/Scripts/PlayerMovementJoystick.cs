using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovementJoystick : MonoBehaviour
{
    public float moveSpeed = 5f;
    public VariableJoystick joystick; // gắn từ inspector

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

        // Quay nhân vật theo hướng di chuyển nếu cần
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, 500 * Time.fixedDeltaTime);
        }
    }
}
