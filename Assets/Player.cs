using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    Camera cam;

    Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.z = Input.GetAxisRaw("Vertical");
        movementDirection.Normalize();

        animator.SetFloat("velocity", movementDirection.magnitude);

        movementDirection = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * movementDirection;

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 720.0f);


        }

        transform.position = new Vector3(transform.position.x, 0.395f, transform.position.z);

        controller.Move(movementDirection * Time.deltaTime * 5.0f);


    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if(rb != null)
        {
            Vector3 force = hit.gameObject.transform.position - transform.position;
            force.Normalize();
            rb.AddForceAtPosition(force * 1.0f, transform.position, ForceMode.Impulse);
        }
    }
}
