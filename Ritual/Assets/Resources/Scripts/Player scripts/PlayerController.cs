using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    protected float moveHorizontal, moveVertical;
	public float direction; //1=down 2=left 3=up 4=right
    private Rigidbody2D playerRigidbody;

    public static bool moveAllow;
    public float maxSpeed;

    void Start()
    {
		direction = 1;
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

		// only triggered when moving ! :< 

		if(moveHorizontal < 0){
			direction = 2;
		}else if(moveHorizontal > 0){
			direction = 4;
		}else if(moveVertical < 0){
			direction = 3;
		}else if(moveVertical > 0){
			direction = 1;
		}


    }
}
