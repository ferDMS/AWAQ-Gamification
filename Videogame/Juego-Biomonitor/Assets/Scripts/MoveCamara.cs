using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamara : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    public float speed;
    public float minXLimit; // Límite mínimo en el eje X
    public float maxXLimit; // Límite máximo en el eje X

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveLeft = false;
        moveRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }

    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    public void PointerDownRight()
    {
        moveRight = true;
    }

    public void PointerUpRight()
    {
        moveRight = false;
    }

    public void MovePlayer()
    {
        if (moveLeft)
        {
            horizontalMove = -speed;
        }
        else if (moveRight)
        {
            horizontalMove = speed;
        }
        else
        {
            horizontalMove = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontalMove, rb.velocity.y);
        // Limitar la posición en el eje X
        float clampedX = Mathf.Clamp(transform.position.x, minXLimit, maxXLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
