using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speedPlayer = 10f;
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;
    Vector3 moveDirection;

    public Transform cameraDirection;
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
        moveDirection = cameraDirection.forward * verticalInput + cameraDirection.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * speedPlayer * 10f, ForceMode.Force);
    }
}
