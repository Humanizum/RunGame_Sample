using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{

    GameObject playerObj;
    Test player;
    Transform playerTransform;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Test>();
        playerTransform = playerObj.transform;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<Test>();
                playerTransform = playerObj.transform;
            }
        }

        if(playerObj != null && player != null && playerTransform != null)
           MoveCamera();
        
    }

    void MoveCamera()
    {
        //横方向だけ追従
        this.transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
    }

}