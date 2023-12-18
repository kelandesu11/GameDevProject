using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int playerHP;
    private int enemyHP;
    private int speed = 10;

    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.Find("PC").GetComponent<PlayerController>().playerHP;
        transform.position += Vector3.forward * speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject g = col.gameObject;
        if (g.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }

        if (g.name == "PC")
        {
            playerHP -= damage;
            Destroy(gameObject);
        }
    }
}
