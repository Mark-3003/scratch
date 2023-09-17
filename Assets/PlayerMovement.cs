using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float turnSpeed;
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        float r = Input.GetAxisRaw("TurnInput") * turnSpeed * Time.deltaTime;

        Vector3 vec = x * -transform.up + y * transform.right;
        rb.position = rb.position + new Vector2(vec.x, vec.y);
        rb.rotation = rb.rotation + r;
    }
}
