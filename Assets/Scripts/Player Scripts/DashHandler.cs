using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHandler : MonoBehaviour
{
    private Rigidbody2D playRig;
    private SpriteRenderer playSpr;
    private PlayerController playCon;
    private Animator myAnim;

    private float timer;
    private float animTime = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        playRig = GetComponentInParent<Rigidbody2D>();
        playSpr = GetComponentInParent<SpriteRenderer>();
        playCon = GetComponentInParent<PlayerController>();
        myAnim = GetComponent<Animator>();

        timer = float.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if ((playRig.velocity.x != 0 || playRig.velocity.y != 0) && Input.GetButtonDown("Sprint"))
        {
            if (playRig.velocity.x >= 0)
            {
                transform.localScale = new Vector3(1, 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 0, 0);
            }

            playSpr.enabled = false;
            
        }
    }
}
