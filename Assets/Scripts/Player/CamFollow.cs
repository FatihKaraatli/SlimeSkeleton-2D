using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject playerTarget;
    public GameObject player;
    public Vector2 imageBordersLeftUp;
    public Vector2 imageBordersRightDown;

    void Update()
    {
        Vector3 playerTargetPos = playerTarget.transform.position;
        Vector3 camHolderPos = this.gameObject.transform.position;
        if (camHolderPos.x <= imageBordersLeftUp.x || camHolderPos.x >= imageBordersRightDown.x)
        {
            
        }
        else
        {
            this.gameObject.transform.position = new Vector3(playerTargetPos.x, camHolderPos.y, camHolderPos.z);
        }
        if (player.transform.position.x >= imageBordersLeftUp.x && player.transform.position.x <= imageBordersRightDown.x)
        {
            this.gameObject.transform.position = new Vector3(playerTargetPos.x, camHolderPos.y, camHolderPos.z);
        }
    }
}
