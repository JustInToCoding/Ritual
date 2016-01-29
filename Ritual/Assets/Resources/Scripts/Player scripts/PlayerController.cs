using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    protected float moveHorizontal, moveVertical;

    private Rigidbody2D playerRigidbody;

    public static bool moveAllow;
    public float maxSpeed;

    void Start()
    {
        moveAllow = true;

        //Setting references to components.
        playerRigidbody = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        if (Time.timeScale > 0)
        {
            Move();
        }

    }

    void Move()
    {
        //Returns a value between -1 and 1 based on which direction is pressed in the horizontal or vertical direction.
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        if (moveAllow)
        {
            playerRigidbody.velocity = new Vector2(moveHorizontal * maxSpeed, moveVertical * maxSpeed);

        }
        else
        {
            playerRigidbody.velocity = new Vector2(0, 0);
        }
    }
}
