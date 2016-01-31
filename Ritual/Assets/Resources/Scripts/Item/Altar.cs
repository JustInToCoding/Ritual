using UnityEngine;
using System.Collections;

public class Altar : MonoBehaviour
{
    private int itemsNeeded;
    public int sacrificedItems;
    private CircleCollider2D cc;
    public GameObject particleFX;

    // Use this for initialization
    void Start()
    {
        itemsNeeded = 5;
        sacrificedItems = 0;
        cc = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(HeldItems.heldSacrificialItems);
    }

    public void StartRitual()
    {
        // Start the ritual when 3 items are collected at the altar
        // level.SpawnEnemies = true;
        // GameObject Boss = Instantiate (Resources.Load("Resources/Prefabs/Bosses/Boss#1"), transform.position, transform.rotation);
        Reset();
        GameObject Go = Instantiate(particleFX, this.transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(deleteFX(Go));


    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (HeldItems.heldSacrificialItems >= 1)
            {
                LightIntensity.changeColour = true;
                sacrificedItems += HeldItems.heldSacrificialItems;
            }
            HeldItems.Reset();

            // Start the particle system and start the ritual
            if (sacrificedItems == itemsNeeded)
            {
                StartRitual();
            }
        }
    }

    void Reset()
    {
        sacrificedItems = 0;
        HeldItems.Reset();
    }

    public IEnumerator deleteFX(GameObject Go)
    {
        yield return new WaitForSeconds(2f);
        Destroy(Go.gameObject);

    }
}
