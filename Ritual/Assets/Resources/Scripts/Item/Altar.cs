using UnityEngine;
using System.Collections;

public class Altar : MonoBehaviour
{
    private int itemsNeeded;
    public static int sacrificedItems;
    private CircleCollider2D cc;
    public GameObject particleFX;
    public GameObject particleBossFX;
    public GameObject boss;
    private Vector3 startpos;
    // Use this for initialization
    void Start()
    {
        startpos = this.transform.position;
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

    void SpawnBoss()
    {
        GameObject spawnFX = Instantiate(particleBossFX);
        GameObject spawnBoss = Instantiate(boss, startpos, Quaternion.identity) as GameObject;
        StartCoroutine(waitToSpawnBoss());
    }

    public IEnumerator deleteFX(GameObject Go)
    {
        yield return new WaitForSeconds(4f);
        Destroy(Go.gameObject);
        Destroy(GameObject.Find("Pentagram"));
        Destroy(this.gameObject);
        SpawnBoss();

    }

    public IEnumerator waitToSpawnBoss()
    {
        yield return new WaitForSeconds(3f);
        
    }
}
