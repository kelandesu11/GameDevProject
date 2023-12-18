using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    private Animator myAnim;
    private PlayerController playCon;
    private Rigidbody2D playRig;
    
    private float playSpeed;
    private float timer;
    private float animTime = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        playCon = GetComponent<PlayerController>();
        playRig = GetComponent<Rigidbody2D>();
        playSpeed = playCon.speed;

        timer = float.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if ((playRig.velocity.x != 0 || playRig.velocity.y != 0) && Input.GetButtonDown("Sprint"))
        {
            Debug.Log("Left shift was pressed");
            playCon.enabled = false;
            timer = animTime;
            StartCoroutine(dashRoutine());
        }

        if (timer <= 0)
        {
            playCon.enabled = true;
            timer = float.MaxValue;
        }
    }
    
    private IEnumerator dashRoutine()
    {
        myAnim.SetBool("RUN", false);
        playSpeed *= 2.5f;
        myAnim.SetBool("DASH", true);
        //yield return new WaitForSeconds(0.5f);
        myAnim.SetBool("DASH", false);
        playSpeed = 10f;
        myAnim.SetBool("RUN", true);
        //yield return new WaitForSeconds(1f);
        yield break;
    }
}
