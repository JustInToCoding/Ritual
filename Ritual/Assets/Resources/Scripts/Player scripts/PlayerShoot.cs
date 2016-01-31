using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    private AudioSource au_s;
    public AudioClip playerHit, playerShoot;
    public Rigidbody2D BulletPrefab;
    //	public float BulletSpeed;
    public float AttackSpeed;
    private bool onCooldown;
    float CooldownTime;

    private Animator animator;

    void Start()
    {
        au_s = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

        if (!onCooldown)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Fire();
            }
            if (Input.GetMouseButton(0))
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        animator.SetBool("isShooting", true);
        onCooldown = true;
        //		var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //			targetPos.z = transform.position.z;
        //
        //		print("y:"+Input.mousePosition+" x:"+targetPos.x+" z:"+targetPos.z);
        //			AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //			audioSource.Play();

        //		bulletStartLocation.x -= 0.2f;

        //		bPrefab.transform.position = Vector3.MoveTowards(transform.position, targetPos, BulletSpeed * Time.deltaTime);

        //get the thing component on your instantiated object
        //		
        StartCoroutine(cooldownTimer());

        //anim.Play("cannon");


        if (onCooldown)
        {
            CooldownTime = Time.time + AttackSpeed;
        }
    }

    public void MakeBullet()
    {
        Vector3 bulletStartLocation = transform.position;
        bulletStartLocation.y += 0.2f;
        //		bulletStartLocation.x += 0.2f;
        au_s.PlayOneShot(playerShoot, 0.3f);
        Rigidbody2D bPrefab = Instantiate(BulletPrefab, bulletStartLocation, Quaternion.identity) as Rigidbody2D;


        BulletController cbController = bPrefab.GetComponent<BulletController>();

        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        //now get the variable from the script reference we just made
        float playerDirection = playerController.direction;

        cbController.direction = playerDirection;
    }

    public void StopAnimating()
    {
        animator.SetBool("isShooting", false);
    }


    public IEnumerator cooldownTimer()
    {
        yield return new WaitForSeconds(AttackSpeed);
        onCooldown = false;
    }
}


