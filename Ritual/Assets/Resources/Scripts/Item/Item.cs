using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

    private BoxCollider2D bc;

	// Use this for initialization
	void Start () {
        bc = this.GetComponent<BoxCollider2D>(); 

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Pickup()
    {
        // destroy the gameobject on pickup
        Destroy(this.gameObject);
    }

    public void OnCollision2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Pickup();
        }
    }
}
