using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject projectile;
	public float playerRange;
	public float attackRange;
	public float direction;
	private GameObject player;
	private Rigidbody2D rb;

	private float health = 40;
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
		calculateDirection ();
		if (playerInRange && canAttack) {
			GameObject attack = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
			BulletController attackController = attack.GetComponent<BulletController> ();
			attackController.direction = direction;
			attack.transform.position = transform.position;
			Cooldown = Time.time + AttackSpeed;
		}
	}

	void Hit(float damage) {
		health -= damage;
		Debug.Log ("Damage!!! "+ health);
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public void collide(GameObject col){
		if (col.tag == "PlayerBullet") {
			Hit (col.GetComponent<BulletController>().damage);
			Destroy (col);
			//Debug.Log (col.gameObject);
		}
		if (col.tag == "EnemeyBullet") {
			Destroy (col);
		}
	}

	void calculateDirection() {
		float angle  = (Mathf.Atan2((transform.position.x - player.transform.position.x), (transform.position.y - player.transform.position.y)) * 180 / Mathf.PI) + 180;

		if (angle > 45 && angle < 135) {
			direction = 4; // right!!!
		}else if (angle > 135 && angle < 225) {
			direction = 3; // down!!!
		}else if (angle > 225 && angle < 315) {
			direction = 2; // left!!!
		}else if (angle > 315 || angle < 45) {
			direction = 1; // up
		}
	}
}
