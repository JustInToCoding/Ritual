using UnityEngine;
using System.Collections;

public class EnemyBoss : EnemyController {
	public GameObject spawnableEnemy;
	private float spawnDistance = 2;
	private bool canFire = false;
	private bool canNormalAttack = true;
	private bool canSpawnEnemies = true;
	private bool canSpawnBullets = true;
	private bool canMove = true;
	private int bulletsSpawned = 0;
	private int normalAttackPerformed = 0;
	private Vector2 startLocation;
	private float destinationX;
	private float destinationY;

	// Use this for initialization
	public override void Start () {
		//base.Start ();
		direction = 1;
		speed = 1;
		player = GameObject.FindGameObjectWithTag("Player");
		canAttack = false;
		startLocation = transform.position;
		StartCoroutine (AttackTimer(Random.Range(5f, 7f)));
	}

	public void FixedUpdate () {
		calculateDirection ();
		Move ();
		if (canAttack) {
			Attack ();
		}

	}

	void Move () {
		if (canMove) {
			destinationX = Random.Range (startLocation.x - 3, startLocation.x + 3);
			destinationY = Random.Range (startLocation.y - 3, startLocation.y + 3);
			StartCoroutine(moveTimer(Random.Range(2f, 5f)));
		}
		float step = speed * Time.deltaTime;
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D> ();
		rb.transform.position = Vector3.MoveTowards (transform.position, new Vector3(destinationX, destinationY, 0), step);
		canMove = false;
		//todo: normal movement..
	}

	public override void Attack() {
		canMove = false;
		stopWalking ();
		if (canNormalAttack) {
			canNormalAttack = false;
			Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D> ();
			Vector2 walkDir = new Vector2 (0, 1);
			if (direction == 1 || direction == 3) {
				walkDir = new Vector2 (1, 0);
			}
			//Debug.Log (walkDir * speed * Time.deltaTime);
			rb.velocity = walkDir * speed;
			StartCoroutine (normalAttackTimer(0.2f)); //time that seperates bullets
		} else if (canSpawnEnemies) {
			SpawnMultipleEnemies ();
			StartCoroutine (spawnEnemiesTimer(Random.Range(10f, 20f)));
			canSpawnEnemies = false;
		}else if (canSpawnBullets) {
			SpawnBulletsInCircle ();
			StartCoroutine (spawnBulletsTimer(Random.Range(15f, 20f)));
			canSpawnBullets = false;
		}
		canAttack = false;
		StartCoroutine (AttackTimer(Random.Range(3f,5f)));
	}

	void stopWalking() {
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0); 
	}

	void NormalAttack () {
		GameObject attack = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		BulletController attackController = attack.GetComponent<BulletController> ();
		attackController.direction = direction;
	}

	void SpawnMultipleEnemies () {
		float degrees = 360;
		while (degrees > 0) {
			// calculate x position
			float x = transform.position.x + (Mathf.Sin(degrees) * spawnDistance);
			float y = transform.position.y + (Mathf.Cos(degrees) * spawnDistance);
			Vector2 location = new Vector2 (x, y);
			Instantiate (spawnableEnemy, location, transform.rotation);
			degrees -= 60;
		}
	}

	void SpawnBulletsInCircle () {
		if (bulletsSpawned < 18) {
			SpawnBullets (bulletsSpawned++);
			StartCoroutine (bulletTimer (0.1f));
		}
	}

	void SpawnBullets (int adjustment) {
		float degrees = 360 - adjustment;
		while (degrees > 0 - adjustment) {
			float x = transform.position.x + (Mathf.Sin (degrees) * spawnDistance);
			float y = transform.position.y + (Mathf.Cos(degrees) * spawnDistance);
			Vector2 location = new Vector2 (x, y);
			GameObject spawnedBullet = Instantiate (projectile, location, transform.rotation) as GameObject;
			degrees -= 18;
			spawnedBullet.GetComponent<Rigidbody2D> ().velocity = ((location - new Vector2(transform.position.x, transform.position.y)).normalized) * spawnedBullet.GetComponent<BulletController> ().speed;
		}
	}

	public IEnumerator bulletTimer(float seconds) {
		yield return new WaitForSeconds (seconds);
		SpawnBulletsInCircle();
	}

	public IEnumerator normalAttackTimer(float seconds) {
		yield return new WaitForSeconds (seconds);
		if (normalAttackPerformed < 3) {
			normalAttackPerformed++;
			NormalAttack ();
			StartCoroutine (normalAttackTimer (seconds));
		} else {
			stopWalking ();
			StartCoroutine (normalAttackRefresh(Random.Range(2f, 5f)));
		}
	}

	public IEnumerator normalAttackRefresh (float seconds) {
		yield return new WaitForSeconds (seconds);
		normalAttackPerformed = 0;
		canNormalAttack = true;
	}

	public IEnumerator spawnEnemiesTimer(float seconds) {
		yield return new WaitForSeconds (seconds);
		canSpawnEnemies = true;
	}

	public IEnumerator spawnBulletsTimer(float seconds) {
		yield return new WaitForSeconds (seconds);
		canSpawnBullets = true;
	}

	public IEnumerator AttackTimer (float seconds) {
		yield return new WaitForSeconds (seconds);
		canAttack = true;
		canMove = true;
	}

	public IEnumerator moveTimer (float seconds) {
		yield return new WaitForSeconds (seconds);
		canMove = true;
	}
}
