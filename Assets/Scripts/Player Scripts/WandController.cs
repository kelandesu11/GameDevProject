using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 screenPoint;
    private Vector2 wandDir;
    private float aimAngle;

    public GameObject spell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*-------------------------*/
        /*  ---  Mouse Point  ---  */
        /*-------------------------*/
        mousePos = Input.mousePosition;
        screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        wandDir = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        aimAngle = Mathf.Atan2(wandDir.y, wandDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
        
        /*-------------------------*/
        /*  ---  Cast Spells  ---  */
        /*-------------------------*/
        if (Input.GetButtonDown("Fire2"))
        {
            GameObject g = Instantiate(spell);
            g.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, 0);
        }
    }
}
