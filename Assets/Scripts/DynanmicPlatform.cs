using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynanmicPlatform : MonoBehaviour
{
    public float speed = 1;
    public float distance = 5;
    private PlayerController character;

    public Vector3 moveDirection;
    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.localPosition;
    }

    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.x = startingPos.x + (Mathf.Sin(Time.time * speed) * distance);
        transform.position = newPos;
    }

}
