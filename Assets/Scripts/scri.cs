using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scri : MonoBehaviour
{
    private float maxY, minY, floatTimer, speed;
    public float coinBackSpeed = 6f;
    public float coinLeftSpeed = 3f;
    public bool coinHit;
    private Transform savePos;

    // Start is called before the first frame update
    void Start()
    {
        maxY = transform.position.y + 0.3f;
        minY = transform.position.y ;
        speed = -0.0004f;
        //player = GameObject.FindGameObjectWithTag("player");
        //savePos = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        savePos = GameObject.FindWithTag("Player").transform;
        floatTimer = Time.deltaTime;
        transform.Rotate(0, 0.2f, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
        if (transform.position.y <= minY)
        {
            speed *= -1;
        }
        else if (transform.position.y >= maxY)
        {
            speed *= -1;
        }

        if (coinHit)
        {
            //transform.Translate(Vector3.back * coinUpSpeed * Time.deltaTime);
            //transform.Translate(Vector3.left * coinUpSpeed * Time.deltaTime);
            
            //transform.Translate(Vector3.back * coinBackSpeed * Time.deltaTime, savePos);
            //transform.Translate(Vector3.left * coinUpSpeed * Time.deltaTime, Space.World);
            transform.Translate(Vector3.left * coinLeftSpeed * Time.deltaTime, savePos);
        }
        //if (floatTimer > 0.4f)
        //{
        //    speed *= -1;
        //    floatTimer = 0;
        //}
    }
}
