using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float turnSmoothTime = 0.1f; **Never used?
    
    //Componentes
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Transform mainCameraForTPS;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Interactor _interactor;
    
    //variables de balance
    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpForce = 20f;
    //variables de trabajo
    private Vector3 moveDirection;
    private float horizontalInput;
    private float verticalInput;
    //Funciones
    private void Start() {
        mainCameraForTPS = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && playerManager.IsActive()) {
            _interactor.Interact();
        }
        
        
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        if (moveDirection.magnitude > 0 && playerManager.IsActive()) {
            MovePlayer();
        }
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }
    void MovePlayer() {
        float targetAngle;

        if (moveDirection.x != 0 && moveDirection.y != 0)
        {
            targetAngle = mainCameraForTPS.eulerAngles.y;

        }
        else
        {
            targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + mainCameraForTPS.eulerAngles.y;

        }
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);


        Vector3 cameraForward = mainCameraForTPS.transform.forward;
        cameraForward.y = 0f;  // Mantener el movimiento en el plano horizontal
        cameraForward.Normalize();

        Vector3 movement = (horizontalInput * mainCameraForTPS.transform.right + verticalInput * cameraForward).normalized;
        movement.y = 0f;
        rb.AddForce(movement * (speed * Time.deltaTime), ForceMode.Force);
    }
    
    void Jump() {
        Debug.Log("Inside jumpt");
        if (playerManager.CanJump()) {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            //playerManager.SwitchState(PlayerManager.States.JUMPING);
        }
    }
}
