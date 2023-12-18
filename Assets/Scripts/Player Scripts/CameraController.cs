using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTran;
    // Start is called before the first frame update
    void Start()
    {
        playerTran = GameObject.Find("PC").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTran.position.x, playerTran.position.y, -10);
    }
}
