using UnityEngine;
using System.Collections;

public class EnemyRanged : EnemyController {
	public GameObject projectile;
	public float playerAttackRange;
	public float attackRange;
	protected bool playerInRange = false;

	protected float Cooldown;
	protected int AttackSpeed = 1;

	
	// Update is called once per frame
	public override void FixedUpdate () {
		playerInRange = (xDifPlayer > - playerAttackRange && xDifPlayer < playerAttackRange) && (yDifPlayer > -playerAttackRange && yDifPlayer < playerAttackRange);
		xDifPlayer = player.transform.position.x - transform.position.x;
		yDifPlayer = player.transform.position.y - transform.position.y;

		if (path !=null) {
			if (path.vectorPath.Count - currentWaypoint < 3 && playerInRange) {

				if (Time.time >= Cooldown) {
					Attack ();
				}
				return;
			}else{
				animation.SetBool ("isShooting", false);
			}
		}
		base.FixedUpdate ();
	}

	public override void createNewPath () {
		if (currentWaypoint >= path.vectorPath.Count) {
			if (!playerInRange) {
				seeker.StartPath (transform.position, player.transform.position, OnPathComplete);
			}
		return;
		}
	}

	void Attack() {
		bool playerInRange = (xDifPlayer > -attackRange && xDifPlayer < attackRange) && (yDifPlayer > -attackRange && yDifPlayer < attackRange);
		calculateDirection ();
		if (playerInRange) {
			au_s.PlayOneShot(enemyShoot, 0.5f);
			animation.SetBool ("isShooting", true);
			Vector3 bulletStartLocation = transform.position;
			bulletStartLocation.y += 0.32f;
			GameObject attack = Instantiate (projectile, bulletStartLocation, Quaternion.identity) as GameObject;
			BulletController attackController = attack.GetComponent<BulletController> ();
			attackController.direction = direction;
			Cooldown = Time.time + AttackSpeed;
		}
	}
}
