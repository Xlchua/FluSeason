using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float moveSpeed = 8f;

    public float bulletSpawnInterval = 0.2f;

    [SerializeField]
    public GameObject BasicBullet;

    private bool isBulletSpawning = false;

    private IEnumerator fireCoroutine;

    private Rigidbody rb;

    Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        fireCoroutine = FireCoroutine();
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
        line.startWidth = 0.4f;
        line.endWidth = 0.4f;
        line.SetPositions(endpoints);
    }

    void StartShooting()
    {
        StartCoroutine(fireCoroutine);
        Debug.Log("Starting Shooting");
        isBulletSpawning = true;
    }

    void StopShooting()
    {
        StopCoroutine(fireCoroutine);
        Debug.Log("Stopping Shooting");
        isBulletSpawning = false;
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            Instantiate(BasicBullet, this.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(bulletSpawnInterval);
        }
    }


}
