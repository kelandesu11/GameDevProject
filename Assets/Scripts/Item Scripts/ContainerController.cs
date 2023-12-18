using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerController : MonoBehaviour
{
    public GameObject healPrefab;

    AudioSource myAudio;

    public AudioClip breakClip;
    public AudioClip chestBreakClip;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.name == "AttackBox")
        {
            //Chest always dops item
            if (gameObject.tag == "Chest")
            {
                GameObject g = Instantiate(healPrefab);
                g.transform.position = transform.position;
                myAudio.PlayOneShot(chestBreakClip);
                Destroy(gameObject);
            }
            else
            {
                //Props sometimes drops items
                int dropChance = Random.Range(1, 5);
                if (dropChance == 1)
                {
                    GameObject g = Instantiate(healPrefab);
                    g.transform.position = transform.position;
                    myAudio.PlayOneShot(breakClip);
                    Destroy(gameObject);
                }
                else
                {
                    myAudio.PlayOneShot(breakClip);
                    Destroy(gameObject);
                }
            }

        }
    }
}
