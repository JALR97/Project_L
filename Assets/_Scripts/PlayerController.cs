using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedPlayer = 10f;
    float horizontalInput;
    float verticalInput;
    public float turnSmoothTime = 0.1f;
    Rigidbody rb;
    Vector3 moveDirection;

    public Transform thirdPersonCamera;
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
            
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg + thirdPersonCamera.eulerAngles.y ;            
            

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 move = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            Vector3 cameraForward = thirdPersonCamera.transform.forward;
            cameraForward.y = 0f;  // Mantener el movimiento en el plano horizontal
            cameraForward.Normalize();

            Vector3 movement = (horizontalInput * thirdPersonCamera.transform.right + verticalInput * cameraForward).normalized;
            movement.y = 0f;
            rb.velocity = movement * speedPlayer* Time.deltaTime;
        }

    }
}
