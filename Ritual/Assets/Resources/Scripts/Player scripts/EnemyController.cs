using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyController : MonoBehaviour {
	public float playerSearchRange = 10f;
	public float direction;
    public GameObject Death;
	public Animator animation;
	protected GameObject player;
    public AudioSource au_s;
    public AudioClip enemyHit, enemyDeath, enemyShoot;

	protected float health = 20;
	protected float speed = 2;
	protected float spriteSize = 0.3f;
	protected bool walking = false;
	float seperation = 1f;

	protected float xDifPlayer;
	protected float yDifPlayer;

	protected Seeker seeker;
	public Path path;
	public float nextWaypointDistance = 0.02f;
	protected int currentWaypoint = 0;


	// Use this for initialization
	public virtual void Start () {
		direction = 1;
        au_s = GetComponent<AudioSource>();
		player = GameObject.FindGameObjectWithTag("Player");
		animation = GetComponent<Animator> ();
		seeker = GetComponent<Seeker> ();
	}

	public virtual void FixedUpdate () {
		bool playerFound = Vector3.Distance (transform.position, player.transform.position) < playerSearchRange;
		walking = false;
		calculateDirection ();
		Animate ();
		if (playerFound && path == null) {
			seeker.StartPath (transform.position, player.transform.position, OnPathComplete);
		}

		if (path == null)
		{
			//We have no path to move after yet
			return;
		}

		createNewPath ();

		if (currentWaypoint > path.vectorPath.Count) {
			seeker.StartPath (transform.position, player.transform.position, OnPathComplete);
			return;
		}
		//Direction to the next waypoint
		Vector3 dir = ( path.vectorPath[ currentWaypoint ] - transform.position).normalized;
		dir = addSeperation (dir);
		dir *= speed * Time.fixedDeltaTime;
		this.gameObject.transform.Translate( dir );
		walking = true;
		Animate ();
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance(path.vectorPath[ currentWaypoint ], transform.position ) < nextWaypointDistance)
		{
			currentWaypoint++;
			return;
		}
	}

	Vector3 addSeperation (Vector3 dir) {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = 0; i < enemies.Length; i++) {
			if (Vector3.Distance(enemies[i].transform.position, transform.position) < seperation) {
				dir += transform.position - enemies[i].transform.position;
			}
		}
		return dir;
	}

	public virtual void createNewPath () {
		if (currentWaypoint >= path.vectorPath.Count) {
			seeker.StartPath (transform.position, player.transform.position, OnPathComplete);
			//Debug.Log( "End Of Path Reached" );
			return;
		}
	}

	void Hit(float damage) {
		health -= damage;
		if (health <= 0) {
            Instantiate(Death);
			Destroy (gameObject);
		}
	}

	public void collide(GameObject col){
		if (col.tag == "PlayerBullet") {
            au_s.PlayOneShot(enemyHit, 0.1f);
			Hit (col.GetComponent<BulletController>().damage);
			Destroy (col);
			//Debug.Log (col.gameObject);
		}
		if (col.tag == "EnemeyBullet") {
			Destroy (col);
		}
	}

	protected void calculateDirection() {
		float angle  = (Mathf.Atan2((transform.position.x - player.transform.position.x), (transform.position.y - player.transform.position.y)) * 180 / Mathf.PI) + 180;

		if (angle > 45 && angle < 135) {
			direction = 4; // right
		}else if (angle > 135 && angle < 225) {
			direction = 3; // down
		}else if (angle > 225 && angle < 315) {
			direction = 2; // left
		}else if (angle > 315 || angle < 45) {
			direction = 1; // up
		}
	}

	public virtual void Animate () {
		Vector3 temp = this.transform.localScale;

		if (walking) {
			if (direction == 4) {
				temp.x = spriteSize;
				animation.SetBool ("isWalking", true);
			} else if (direction == 3) {
				animation.SetBool ("isWalking", true);
			} else if (direction == 2) {
				temp.x = -spriteSize;
				animation.SetBool ("isWalking", true);
			} else if (direction == 1) {
				animation.SetBool ("isWalking", true);
			}
			this.transform.localScale = temp;
		} else {
			animation.SetBool ("isWalking", false);
		}
	}

	public void OnPathComplete ( Path p ) {
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}
	}
}
