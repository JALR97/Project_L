using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedPlayer = 10f;
    float horizontalInput;
    float verticalInput;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity = 1.0f;
    Rigidbody rb;
    Vector3 moveDirection;

    public Transform camera;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        if( moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 move = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            rb.AddForce(move.normalized  * speedPlayer * Time.deltaTime, ForceMode.Force);
        }

    }
}
