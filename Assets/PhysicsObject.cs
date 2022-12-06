using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    protected Vector2 velocity;
    public float gravityModifier = 1f;

    protected Rigidbody2D rb2d;

    protected const float minMoveDistance = 0.001f;


    protected ContactFilter2D contactFilter;
    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        contactFilter.useTriggers = false;
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 move = Vector2.up * deltaPosition.y;

        Movement(move);
    }
    void Movement(Vector2 move)
    {
        float distance = move.magnitude;
        if (distance > minMoveDistance)
        {

        }

        rb2d.position = rb2d.position + move;
    }
}
