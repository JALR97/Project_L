using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float turnSmoothTime = 0.1f; **Never used?
    
    //Componentes
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Transform thirdPersonCamera;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Interactor _interactor;
    
    //variables de balance
    [SerializeField] private float speed = 20f;
    
    //variables de trabajo
    private Vector3 moveDirection;
    private float horizontalInput;
    private float verticalInput;
    
    //Funciones
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
    }
    void MovePlayer() {
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg + thirdPersonCamera.eulerAngles.y ;            
            
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        Vector3 move = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        Vector3 cameraForward = thirdPersonCamera.transform.forward;
        cameraForward.y = 0f;  // Mantener el movimiento en el plano horizontal
        cameraForward.Normalize();

        Vector3 movement = (horizontalInput * thirdPersonCamera.transform.right + verticalInput * cameraForward).normalized;
        movement.y = 0f;
        rb.velocity = movement * (speed * Time.deltaTime);
    }
}
