using UnityEngine;
using System.Collections;

public class Altar : MonoBehaviour
{
    private int itemsNeeded;
    public int heldItems;
    private CircleCollider2D cc;

    // Use this for initialization
    void Start()
    {
        itemsNeeded = 5;
        heldItems = 0;
        cc = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartRitual()
    {
        // Start the ritual when 3 items are collected at the altar
        // level.SpawnEnemies = true;
        // GameObject Boss = Instantiate (Resources.Load("Resources/Prefabs/Bosses/Boss#1"), transform.position, transform.rotation);
        // Reset();
        Debug.Log("Ritual started");

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            heldItems += HeldItems.heldSacrificialItems;

            // Start the particle system and start the ritual
            if (heldItems >= itemsNeeded)
                HeldItems.heldSacrificialItems = 0;
                StartRitual();
        }
    }
}
