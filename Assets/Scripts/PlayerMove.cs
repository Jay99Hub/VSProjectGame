using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    [HideInInspector]
    public Vector3 movementVector;
    [HideInInspector]
    public float lastHorizontalDeCoupledVector;
    [HideInInspector]
    public float lastVerticalDeCoupledVector;

    [HideInInspector]
    public float lastHorizontalCoupledVector;
    [HideInInspector]
    public float lastVerticalCoupledVector;


    [SerializeField] float speed = 3f;

    Animate animate;

    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    private void Start()
    {
        lastHorizontalDeCoupledVector = -1f;
        lastVerticalDeCoupledVector = 1f;

        lastHorizontalCoupledVector = -1f;
        lastVerticalCoupledVector = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        if(movementVector.x != 0 || movementVector.y != 0)
        {
            lastHorizontalCoupledVector = movementVector.x;
            lastVerticalCoupledVector = movementVector.y;
        }

        if(movementVector.x != 0)
        {
            lastHorizontalDeCoupledVector = movementVector.x;
        }
        if(movementVector.y != 0)
        {
            lastVerticalDeCoupledVector = movementVector.y;
        }

        animate.horizontal = movementVector.x;

        movementVector *= speed;

        rgbd2d.velocity = movementVector;
    }
}
