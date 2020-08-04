using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpHeight = 4f;
    public float gravity = 9.81f;
    public float airControl = 2f;

    public Vector3 externalMoveSpeed;

    CharacterController controller;
    Vector3 input, moveDirection;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;

        input *= moveSpeed;

        if (controller.isGrounded)
        {
            moveDirection = input;
            //jump
            if(Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            } else
            {
                moveDirection.y = 0.0f;
            }
        } else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;

        
        if (!LevelManager.isGameOver)
        {
            if(transform.parent != null)
            {
                Vector3 offset = transform.parent.position - transform.position;
                controller.Move(moveDirection * Time.deltaTime + offset * Time.deltaTime);
            }
            controller.Move(moveDirection * Time.deltaTime);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DynamicPlatform"))
        {
            transform.parent = other.gameObject.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DynamicPlatform"))
        {
            transform.parent = null;
        }
    }
}
