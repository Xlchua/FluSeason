using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float moveSpeed = 8f;


    private Rigidbody2D rb;
    Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * moveSpeed;
    }


    void Look()
    {
        Vector3 mousePos = Input.mousePosition;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(new Vector3(0f,0f, angle));
    }


}
