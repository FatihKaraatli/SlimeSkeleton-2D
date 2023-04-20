using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class GameplayControl : MonoBehaviour
{
    public GameObject firstFloorEnemiesParentObject;
    public GameObject firstFloorTransportBlockObject;
    public GameObject firstFloorExitSignLight;

    public GameObject secondFloorEnemiesParentObject;
    public GameObject secondFloorBossCrewParentObject;
    public GameObject secondFloorTransportBlockObject;
    public GameObject secondFloorExitSignLight;

    public GameObject thirdFloorEnemiesParentObject;
    public GameObject thirdFloorBossCrewParentObject;

    public GameObject player;
    public GameObject cam;
    public Transform secondFloorBossEntryPos;
    public Transform thirdFloorBossEntryPos;
    public Timer cameraGoingBossMovementTimer;
    public Timer cameraBackToPlayerBossMovementTimer;

    public GameObject portal;

    public AudioSource backgroundAudioSource;
    public AudioClip mainMusic;
    public AudioClip secondFloorBossMusic;
    public AudioClip thirdFloorBossMusic;
    public AudioClip endgameMusic;

    private Vector3 camFirstPos;
    private bool camMovementControl = false;
    private bool camGoToBoss = false;
    private bool camWait = false;

    private bool secondFloorCodeStopControl = false;
    private bool thirdFloorCodeStopControl = false;
    private bool secondFloorBossCodeStopControl = false;
    private bool thirdFloorCodeBossStopControl = false;
    private bool secondFloorCrewControl = false;
    private bool thirdFloorCrewControl = false;
  
    void Update()
    {
        if (firstFloorEnemiesParentObject.transform.childCount == 0)
        {
            if (firstFloorTransportBlockObject != null) firstFloorTransportBlockObject.GetComponent<FadeEffect>().enabled = true;
            firstFloorExitSignLight.SetActive(true);
        }
        if (!secondFloorCodeStopControl)
        {
            secondFloorCodeStopControl = ChildCountControl(secondFloorEnemiesParentObject, secondFloorCodeStopControl);
            if (secondFloorCodeStopControl)
            {
                backgroundAudioSource.clip = secondFloorBossMusic;
                backgroundAudioSource.Play();
                secondFloorCrewControl = true;
                thirdFloorCrewControl = false;
            }
        }
        if (!secondFloorBossCodeStopControl)
        {
            secondFloorBossCodeStopControl = ChildCountControl(secondFloorBossCrewParentObject, secondFloorTransportBlockObject, secondFloorBossCodeStopControl);
        }
        if (!thirdFloorCodeStopControl)
        {
            thirdFloorCodeStopControl = ChildCountControl(thirdFloorEnemiesParentObject, thirdFloorCodeStopControl);
            if (thirdFloorCodeStopControl)
            {
                backgroundAudioSource.clip = thirdFloorBossMusic;
                backgroundAudioSource.Play();
                secondFloorCrewControl = false;
                thirdFloorCrewControl = true;
            }
        }
        if (!thirdFloorCodeBossStopControl)
        {
            thirdFloorCodeBossStopControl = ChildCountControl(thirdFloorBossCrewParentObject, null ,thirdFloorCodeBossStopControl);
        }


        if (camMovementControl)
        {
            if(secondFloorCrewControl) BossCrewControl(secondFloorBossEntryPos, secondFloorBossCrewParentObject);
            else if(thirdFloorCrewControl) BossCrewControl(thirdFloorBossEntryPos, thirdFloorBossCrewParentObject);
        }
    }

    public bool ChildCountControl(GameObject obj, bool stopControl)
    {
        if (obj.transform.childCount == 0)
        {
            GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (var bullet in allBullets)
            {
                bullet.SetActive(false);
            }
            camFirstPos = cam.transform.position;
            camMovementControl = true;
            camGoToBoss = true;
            player.GetComponent<PlayerMovement>().enabled = false;
            cam.GetComponent<CamFollow>().enabled = false;
            stopControl = true;
        }
        return stopControl;
    }
    public bool ChildCountControl(GameObject obj, GameObject blockObj, bool stopControl)
    {
        if (obj.transform.childCount == 0)
        {
            if (blockObj != null) 
            {
                backgroundAudioSource.clip = mainMusic;
                backgroundAudioSource.Play();
                secondFloorExitSignLight.SetActive(true);
                backgroundAudioSource.Play();
                blockObj.GetComponent<FadeEffect>().enabled = true;
            }
            else
            {
                backgroundAudioSource.clip = endgameMusic;
                backgroundAudioSource.Play();
                portal.SetActive(true);
            }
            stopControl = true;
        }
        return stopControl;
    }
    public void BossCrewControl(Transform bossCrewPos, GameObject bossCrew)
    {
        if (camGoToBoss)
        {
            cameraGoingBossMovementTimer.UpdateTimer();

            if (cameraGoingBossMovementTimer.Done() && !camWait)
            {
                bossCrew.SetActive(true);
                cameraGoingBossMovementTimer.ResetTimer(6);
                camWait = true;
            }
            if (camWait)
            {
                cameraGoingBossMovementTimer.UpdateTimer();
                if (cameraGoingBossMovementTimer.Done())
                {
                    camGoToBoss = false;
                    camWait = false;
                    cameraGoingBossMovementTimer.ResetTimer();
                }
            }
            else
            {
                cam.transform.position = Vector3.Lerp(camFirstPos, bossCrewPos.position, cameraGoingBossMovementTimer.NormalizedTime);
            }
        }
        else
        {
            cameraBackToPlayerBossMovementTimer.UpdateTimer();
            cam.transform.position = Vector3.Lerp(bossCrewPos.position, camFirstPos, cameraBackToPlayerBossMovementTimer.NormalizedTime);

            if (cameraBackToPlayerBossMovementTimer.Done())
            {
                player.GetComponent<PlayerMovement>().enabled = true;
                cam.GetComponent<CamFollow>().enabled = true;
                camMovementControl = false;
                cameraBackToPlayerBossMovementTimer.ResetTimer();
            }
        }
    }
    
}
