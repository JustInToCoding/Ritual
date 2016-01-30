using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject projectile;
	public float playerRange;
	public float attackRange;
	public float direction;
	private GameObject player;
	private Rigidbody2D rb;

	private int health = 40;
	private float speed = 0.5f;
	private float Cooldown;
	private int projectileSpeed = 4;
	private int AttackSpeed = 1;
	private bool canAttack = true;

	private float xDifPlayer;
	private float yDifPlayer;
	private Vector2 target;


	// Use this for initialization
	void Start () {
		direction = 1;
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool playerInRange = (xDifPlayer > -playerRange && xDifPlayer < playerRange) && (yDifPlayer > -playerRange && yDifPlayer < playerRange);

		target = player.transform.position - transform.position;
		xDifPlayer = player.transform.position.x - transform.position.x;
		yDifPlayer = player.transform.position.y - transform.position.y;
		if (!playerInRange) {
			calculateDirection ();
			rb.velocity = target * speed;
		} else {
			rb.velocity = Vector2.zero;
		}

		if (Time.time >= Cooldown) {
			Attack ();
		}
	}

	void Attack() {
		bool playerInRange = (xDifPlayer > -attackRange && xDifPlayer < attackRange) && (yDifPlayer > -attackRange && yDifPlayer < attackRange);
		if (playerInRange && canAttack) {
			GameObject attack = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
			BulletController attackController = attack.GetComponent<BulletController> ();
			attackController.direction = direction;
			attack.transform.position = transform.position;
			Cooldown = Time.time + AttackSpeed;
		}
	}

	void calculateDirection() {
		//float angle = Vector2.Angle (transform.position, player.transform.position);
		//Vector3 cross = Vector3.Cross (transform.position, player.transform.position);
		float angle  = (Mathf.Atan2((transform.position.x - player.transform.position.x), (transform.position.y - player.transform.position.y)) * 180 / Mathf.PI) + 180;
		//if (cross.z > 0) {
		//	angle = 360 - angle;
		//}
		Debug.Log ("angle: "+ angle);
		if (angle > 45 && angle < 135) {
			Debug.Log ("4: right");
			direction = 4; // right!!!
		}else if (angle > 135 && angle < 225) {
			Debug.Log ("3: down");
			direction = 3; // down!!!
		}else if (angle > 225 && angle < 315) {
			Debug.Log ("2: left");
			direction = 2; // left!!!
		}else if (angle > 315 || angle < 45) {
			Debug.Log ("1: up");
			direction = 1; // up
		}
	}
}
