using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BasicPlayerController : MonoBehaviour
{
    public int maxHP;
    public int damage;

    AudioSource myAudio;
    Transform atkBox;
    Text healthText;
    Text gameOverText;

    int currentHP;
    bool hitStun;

    public AudioClip healClip;
    public AudioClip attackClip;

    // Start is called before the first frame update
    void Start()
    {
        atkBox = transform.Find("AttackBox").transform;
        atkBox.GetComponent<SpriteRenderer>().enabled = false;
        atkBox.GetComponent<BoxCollider2D>().enabled = false;

        myAudio = GetComponent<AudioSource>();

        gameOverText = GameObject.Find("GameOver").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = "Health: " + maxHP;

        hitStun = false;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.position += new Vector3(h, v, 0) * 5 * Time.deltaTime;

        //Attack
        if(Input.GetButtonDown("Jump"))
        {
            atkBox.GetComponent<SpriteRenderer>().enabled = true;
            atkBox.GetComponent<BoxCollider2D>().enabled = true;
            myAudio.PlayOneShot(attackClip);
            Invoke("atkCleanUp", 1);
        }

            healthText.text = "Health: " + currentHP;
             

        if (currentHP <= 0)
        {
            Time.timeScale = 0;
            gameOverText.text = "YOU DIED" + "\n" + "Left-click to try again";
        }

        if (Time.timeScale <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }
        }
    }

    void atkCleanUp()
    {
        atkBox.GetComponent<SpriteRenderer>().enabled = false;
        atkBox.GetComponent<BoxCollider2D>().enabled = false;
    }

    void hitStunCleanUp()
    {
        hitStun = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if(otherGO.tag == "hurtPlayer" && !hitStun)
        {
            currentHP -= otherGO.transform.parent.GetComponent<EnemyController>().damage;
            print("Player hit by: " + otherGO.transform.parent.name + ", Player HP: " + currentHP);
            hitStun = true;
            Invoke("hitStunCleanUp", 1);
           
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGO = collision.gameObject;

        if (otherGO.tag == "Health")
        {
            if (currentHP < 100)
            {   //Heals player 
                print("You have been healed!");
                currentHP += 25;
                myAudio.PlayOneShot(healClip);
                Destroy(otherGO);
                
                if (currentHP > 100)
                {
                    currentHP = 100;
                }
            }
            else
            {
                print("You are not injured!");
            }
        }

    }

}
