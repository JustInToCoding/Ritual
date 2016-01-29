using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject projectile;
	public float playerRange;
	public float attackRange;
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
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		float time = 2;
		bool playerInRange = (xDifPlayer > -playerRange && xDifPlayer < playerRange) && (yDifPlayer > -playerRange && yDifPlayer < playerRange);

		target = player.transform.position - transform.position;
		xDifPlayer = player.transform.position.x - transform.position.x;
		yDifPlayer = player.transform.position.y - transform.position.y;
		if (!playerInRange) {
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
			Vector2 projectileVelocity = target * projectileSpeed;
			GameObject attack = Instantiate (projectile);
			attack.transform.position = transform.position;
			attack.GetComponent<Rigidbody2D>().velocity = projectileVelocity;
			Cooldown = Time.time + AttackSpeed;
		}
	}
}
