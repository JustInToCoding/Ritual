using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    protected float moveHorizontal, moveVertical;
	public float direction; //1=down 2=left 3=up 4=right
    private Rigidbody2D playerRigidbody;
	private GameController gameController;
	private Animator animator;

    public static bool moveAllow;
    public float maxSpeed;

	public int amountOfLives;

	void Start()
    {
		animator = GetComponent<Animator> ();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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

		Vector3 temp = this.transform.localScale;


		if (moveHorizontal < 0) {
			direction = 2;
			temp.x = -0.3f;
			animator.SetBool ("isWalking", true);
		} else if (moveHorizontal > 0) {
			direction = 4;
			temp.x = 0.3f;
			animator.SetBool ("isWalking", true);

		} else if (moveVertical < 0) {
			direction = 3;
			animator.SetBool ("isWalking", true);
		} else if (moveVertical > 0) {
			direction = 1;
			animator.SetBool ("isWalking", true);
		} else {
			animator.SetBool ("isWalking", false);
		}

		this.transform.localScale = temp;
    }

	void removeLive() {
		if(amountOfLives != 0){
			amountOfLives -= 1;
			gameController.RemoveLive();

			if(amountOfLives == 0 ){
				gameController.GameOver();
			}
		}
	}

	//when collider hits player check who is colliding and react
	void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Enemy"){
		//	removeLive ();
		//}
	}

	public void hit (GameObject col) {
		if (col.tag == "EnemyBullet") {
			removeLive ();
			Destroy (col);
		}else if (col.GetComponent<EnemyMelee>()) {
			removeLive ();
		}
	}

}
