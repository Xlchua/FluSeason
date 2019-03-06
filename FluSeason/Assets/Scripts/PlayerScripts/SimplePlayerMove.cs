using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float moveSpeed = 8f;

    public float bulletSpawnInterval = 0.2f;

    public GameObject BasicBullet;

    public GameObject FastSingleBullet;

    public GameObject SpreadBullet;

    private bool isBulletSpawning = false;

    private IEnumerator fireCoroutine;

    private Rigidbody rb;

    private AudioSource audioSource;

    private string bulletType = "BasicBullet";

    Vector3 direction;

    //Audio Purposes
    public AudioClip BB_sound;

    //Used for stream bullet
    private float interval = 1.4f;
    private float x1, x2;    

    private void Awake()
    {
        x1 = interval;
        x2 = -interval;
        rb = GetComponent<Rigidbody>();
        fireCoroutine = FireCoroutine();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Look();
        //transform.Translate(direction * moveSpeed * Time.deltaTime);
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopShooting();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(!isBulletSpawning)
                StartShooting();
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * moveSpeed;
    }


    void Look()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(new Vector3(0f,0f, angle));

        DrawLine(mousePos);
    }

    void DrawLine(Vector3 mousePos)
    {
        LineRenderer line = this.GetComponent<LineRenderer>();

        mousePos.Scale(new Vector3(1, 1, 0));

        Vector3[] endpoints = new Vector3[2] { new Vector3(this.transform.position.x, this.transform.position.y), mousePos };
        line.startWidth = line.endWidth = 0.2f;
        line.SetPositions(endpoints);
    }

    void StartShooting()
    {
        StartCoroutine(fireCoroutine);
        isBulletSpawning = true;
    }

    void StopShooting()
    {
        StopCoroutine(fireCoroutine);
        isBulletSpawning = false;
    }

    //Interval for stream shooting
    private void UpdateInterval()
    {
        if(x1 <= -interval)
        {
            x1 = interval;
            x2 = -interval;
        }
        else
        {
            x1 -= 0.1f;
            x2 += 0.1f;
        }
    }

    /* --Shooting--
     * Switch statement for Bullet types
     */
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            switch (bulletType)
            {
                //Default Bullet
                case "BasicBullet":
                    Instantiate(BasicBullet, this.transform.position, this.transform.rotation);
                    break;

                //Fast single bullet
                case "FastSingleBullet":
                    Instantiate(FastSingleBullet, this.transform.position, this.transform.rotation);
                    break;

                //Spread of bullets
                case "SpreadBullet":
                    Instantiate(SpreadBullet, this.transform.position, this.transform.rotation);
                    break;

                //Dual Stream??
                case "StreamBullet":
                    Vector3 x1d = new Vector3(this.transform.position.x + x1, this.transform.position.y, this.transform.position.z);
                    Vector3 x2d = new Vector3(this.transform.position.x + x2, this.transform.position.y, this.transform.position.z);

                    GameObject x1Bullet = Instantiate(FastSingleBullet, x1d, Quaternion.Euler(new Vector3(0, 0, -90)));
                    GameObject x2Bullet = Instantiate(FastSingleBullet, x2d, Quaternion.Euler(new Vector3(0, 0, -90)));

                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    float angle = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x) * Mathf.Rad2Deg + 90;

                    x1Bullet.transform.RotateAround(this.transform.position, transform.forward, angle);
                    x2Bullet.transform.RotateAround(this.transform.position, transform.forward, angle);

                    UpdateInterval();                 
                    break;

                //All three???
                case "OPBullet":
                    Instantiate(BasicBullet, this.transform.position, this.transform.rotation);

                    Instantiate(FastSingleBullet, this.transform.position, this.transform.rotation);

                    Instantiate(SpreadBullet, this.transform.position, this.transform.rotation);

                    x1d = new Vector3(this.transform.position.x + x1, this.transform.position.y, this.transform.position.z);
                    x2d = new Vector3(this.transform.position.x + x2, this.transform.position.y, this.transform.position.z);

                    x1Bullet = Instantiate(FastSingleBullet, x1d, Quaternion.Euler(new Vector3(0, 0, -90)));
                    x2Bullet = Instantiate(FastSingleBullet, x2d, Quaternion.Euler(new Vector3(0, 0, -90)));

                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    angle = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x) * Mathf.Rad2Deg + 90;

                    x1Bullet.transform.RotateAround(this.transform.position, transform.forward, angle);
                    x2Bullet.transform.RotateAround(this.transform.position, transform.forward, angle);

                    UpdateInterval();
                    break;
            }
            PlayGunshot();
            yield return new WaitForSeconds(bulletSpawnInterval);

        }
    }

    private void PlayGunshot()
    {
        audioSource.PlayOneShot(BB_sound, 0.05f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("FastSingleUpgrade"))
        {
            bulletType = "FastSingleBullet";
            bulletSpawnInterval = 0.115f;
            Destroy(collider.gameObject);
        }

        if(collider.CompareTag("SpreadUpgrade"))
        {
            bulletType = "SpreadBullet";
            bulletSpawnInterval = 0.05f;
            Destroy(collider.gameObject);
        }

        if (collider.CompareTag("StreamUpgrade"))
        {
            bulletType = "StreamBullet";
            bulletSpawnInterval = 0.035f;
            Destroy(collider.gameObject);
        }

        if (collider.CompareTag("OPUpgrade"))
        {
            bulletType = "OPBullet";
            bulletSpawnInterval = 0.05f;
            Destroy(collider.gameObject);
        }

        if(collider.CompareTag("EnemyBullet"))
        {
            int v = collider.GetComponent<Explosion>().getDamage();
            PlayerManager.instance.addInfection(v);
            Destroy(collider.gameObject);
        }

    }
}
