using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip[] footsteps;

    Animator animator;
    CharacterController controller;
    Camera cam;
    GameObject ball;
    Transform ballAttachment;
    int footstep;
    AudioSource source;

    Vector3 movementDirection;
    bool canPickupBall;
    bool hasBall;

    float dropTimer_default;
    float dropTimerCurrent;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        source = GetComponent<AudioSource>();
        footstep = 0;

        dropTimerCurrent = 0.0f;
        dropTimer_default = 0.25f;
        
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        ballAttachment = GameObject.Find("BallAttachment").transform;
        canPickupBall = false;
        hasBall = false;
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

        controller.Move(movementDirection * Time.deltaTime * 5.0f);
        transform.position = new Vector3(transform.position.x, 0.395f, transform.position.z);

        if(Input.GetKeyDown(KeyCode.E) && hasBall)
        {
            animator.SetBool("hasBall", false);
            hasBall = false;
            canPickupBall = true;
            dropTimerCurrent = dropTimer_default;
            ball.GetComponent<SphereCollider>().isTrigger = false;
        }

        if(!hasBall && dropTimerCurrent > 0.0f)
        {
            dropTimerCurrent -= Time.deltaTime;
        }
        else if(!hasBall)
        {
            dropTimerCurrent = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.E) && canPickupBall && dropTimerCurrent <= 0.0f)
        {
            ball.transform.position = Vector3.zero;
            animator.SetBool("hasBall", true);
            hasBall = true;
            canPickupBall = false;
            ball.GetComponent<SphereCollider>().isTrigger = true;
        }
        

        if(hasBall)
        {
            ball.transform.position = ballAttachment.transform.position;
            ball.transform.rotation = ballAttachment.transform.rotation;

        }

    }

    public void Footstep()
    {
        footstep = Random.Range(0, 3);
        source.clip = footsteps[footstep];
        source.Play();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb != null)
        {
            Vector3 force = hit.gameObject.transform.position - transform.position;
            force.Normalize();
            rb.AddForceAtPosition(force * 1.0f, transform.position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball") && !hasBall)
        {
            canPickupBall = true;
            ball = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball") && !hasBall && dropTimerCurrent <= 0.0f)
        {
            canPickupBall = false;
            ball = null;
        }
    }
}
