using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    //Player Components
    private Animator myAnim;
    private SpriteRenderer myRend;
    private BoxCollider2D myCol;
    private Rigidbody2D myBod;
    private PlayerController myCon;
    
    //Other Components
    
    //Internal Variables
    public float speed = 10f;
    private float timer;
    private float animTime = 0.7f;
    
    //External Variables
    public int playerHP = 100;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myRend = GetComponent<SpriteRenderer>();
        myCol = GetComponent<BoxCollider2D>();
        myBod = GetComponent<Rigidbody2D>();
        myCon = GetComponent<PlayerController>();
        myBod.gravityScale = 0;
        timer = float.MaxValue;
        // myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        /*----------------------*/
        /*  ---  Movement  ---  */
        /*----------------------*/
        // myCam.transform.position = new Vector3(
        //     transform.position.x,
        //     transform.position.y,
        //     -10);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float hSpeed = h * speed * Time.deltaTime;
        float vSpeed = v * speed * Time.deltaTime;
        transform.position += new Vector3(hSpeed, vSpeed, 0);
        if (h > 0)
        {
            myAnim.SetBool("RUN",true);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (h < 0)
        {
            myAnim.SetBool("RUN",true);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (v != 0)
        {
            myAnim.SetBool("RUN",true);
        }
        if ((h != 0 || v != 0) && Input.GetButtonDown("Sprint"))
        {
            myCon.enabled = false;
            timer = animTime;
            StartCoroutine(DashRoutine());
        }
        else if (h == 0 && v == 0)
        {
            myAnim.SetBool("RUN",false);
        }

        if (timer <= 0)
        {
            myCon.enabled = true;
            timer = float.MaxValue;
        }
        
        /*-----------------------*/
        /*  ---  Attacking  ---  */
        /*-----------------------*/
        if (Input.GetButtonDown("Fire1"))
        {
            myAnim.SetBool("MELEE", true);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            myAnim.SetBool("RANGED", true);
        }
        else
        {
            myAnim.SetBool("MELEE", false);
            myAnim.SetBool("RANGED", false);
        }
    }

    /*---------------------------*/
    /*  ---  Dash Movement  ---  */
    /*---------------------------*/
    private IEnumerator DashRoutine()
    {
        myAnim.SetBool("RUN", false);
        speed *= 2.5f;
        myAnim.SetBool("DASH", true);
        yield return new WaitForSeconds(0.5f);
        myAnim.SetBool("DASH", false);
        speed = 10f;
        myAnim.SetBool("RUN", true);
        yield return new WaitForSeconds(1f);
    }
}
