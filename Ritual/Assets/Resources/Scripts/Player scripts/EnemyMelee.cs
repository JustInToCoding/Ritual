using UnityEngine;
using System.Collections;

public class EnemyMelee : EnemyController {

	// Use this for initialization
	public override void Start () {
		base.Start ();
		playerRange = 1;
		attackRange = 1;
	}
	
	// Update is called once per frame
	public override void Attack () {
		bool playerInRange = (xDifPlayer > -attackRange && xDifPlayer < attackRange) && (yDifPlayer > -attackRange && yDifPlayer < attackRange);
		if (playerInRange && canAttack) {
			player.GetComponent<PlayerController>().hit (gameObject);
			Cooldown = Time.time + AttackSpeed;
		}
	}
}
