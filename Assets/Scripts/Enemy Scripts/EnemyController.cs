using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHP;
    public int damage;

    int currentHP;
    bool hitStun;

    Animator myAnim;
    SpriteRenderer mySprite;
    BoxCollider2D atkBox;
    AudioSource myAudio;
    BasicPlayerController bpc;
    KillCounterController kcc;

    public AudioClip deathClip;
    public AudioClip hurtClip;

    Transform playerTran;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        hitStun = false;

        myAnim = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
        myAudio = GetComponent<AudioSource>();

        bpc = GameObject.Find("Player").GetComponent<BasicPlayerController>();
        playerTran = GameObject.Find("Player").transform;
        atkBox = transform.Find("AttackBox").GetComponentInChildren<BoxCollider2D>();
        kcc = GameObject.Find("KillCounter").GetComponent<KillCounterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitStun)
        {
            return;
        }
        else
        {
            Vector3 vecToPlayer = playerTran.position - transform.position;
            if (vecToPlayer.magnitude < 1.5) //Attack
            {
                myAnim.SetBool("WALK", false);
                myAnim.SetBool("ATTACK", true);
                atkBox.enabled = true;

            }
            else if (vecToPlayer.magnitude < 10) //Move towards Player
            {
                atkBox.enabled = false;
                myAnim.SetBool("WALK", true);
                myAnim.SetBool("ATTACK", false);
                transform.position += vecToPlayer.normalized * Time.deltaTime;

                /*Code for Skeleton flipping on X-Axis*/
                if (playerTran.position.x > transform.position.x)
                {
                    mySprite.flipX = false;
                }
                else if (playerTran.position.x < transform.position.x)
                {
                    mySprite.flipX = true;
                }
            }
            else if (vecToPlayer.magnitude > 10) //Idle
            {
                atkBox.enabled = false;
                myAnim.SetBool("WALK", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (hitStun)
        {
            return;
        }
        else
        {
            if (otherGO.tag == "hurtEnemy")
            {
                print(gameObject.name + " was hit by Player");
                hitStun = true;
                atkBox.enabled = false;
                myAnim.SetBool("WALK", false);
                myAnim.SetBool("ATTACK", false);

                if (currentHP <= 0)
                {
                    print(gameObject.name + " is dead");
                    kcc.addKill();
                    myAnim.SetBool("DEATH", true);
                    myAudio.PlayOneShot(deathClip);
                    Destroy(gameObject, 1);
                }
                else
                {
                    print(gameObject.name + " has been hit, currentHP: " + currentHP);
                    myAnim.SetBool("HIT", true);
                    myAudio.PlayOneShot(hurtClip);
                    //Delay, invoke HitCleanUp in 1 second
                    currentHP -= bpc.damage;
                    Invoke("hitCleanUp", 1);
                }
            }
        }
    }

    public void hitCleanUp()
    {
        hitStun = false;
        myAnim.SetBool("HIT", false);
    }
}