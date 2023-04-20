using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CountDownTimer;
using TMPro;

public class SkillTree : MonoBehaviour
{
    public GameObject board;
    public Timer boardCreateTimer;
    public Timer cameraMovementTimer;
    public Timer skillsPressMovementTimer;
    public float rotSpeed;
    public Transform boardPos;
    public Transform skillsSmallPosition;
    public GameObject[] skills;
    public Transform[] skillsFirstPosAndScales;
    public GameObject[] skillsUpgradeScreens;
    public GameObject pinkCircle;

    private bool boardCreateControl = false;
    private bool camMovementControl = false;
    private bool skillsPressMovementControl = false;
    private bool upgradeSceneControl = true;
    private Vector3 camFirstPos;
    private Vector3[] skillsSmallScales = new Vector3[5];
    private GameObject cam;
    private int skillCount;
    private GameObject player;
    private bool isOpening = true;

    public AttackCanvasControl attackCanvasControl;
    public PlayerAttack playerAttack;
    public PlayerHealth playerHealth;

    public float healthIncreaseAmount;
    public int healthIncreaseDnaCount;
    public TMP_Text currentHealthText;
    public TMP_Text nextHealthText;
    public TMP_Text priceHealthText;
    public int healthIncreaseCount;

    public float ultiTimeDecreaseAmount;
    public int ultiTimeDecreaseDnaCount;
    public TMP_Text currentUltiText;
    public TMP_Text nextUltiText;
    public TMP_Text priceUltiText;
    public int ultiIncreaseCount;

    public float punchPowerIncreaseAmount;
    public int punchPowerIncreaseDnaCount;
    public TMP_Text currentPunchText;
    public TMP_Text nextPunchText;
    public TMP_Text pricePunchText;
    public int punchIncreaseCount;

    public float dashTimeDecreaseAmount;
    public int dashTimeDecreaseDnaCount;
    public TMP_Text currentDashText;
    public TMP_Text nextDashText;
    public TMP_Text priceDashText;
    public int dashIncreaseCount;

    public float slimeBulletPowerIncreaseAmount;
    public int slimeBulletPowerIncreaseDnaCount;
    public TMP_Text currentSlimeBulletText;
    public TMP_Text nextSlimeBulletText;
    public TMP_Text priceSlimeBulletText;
    public int slimeBulletIncreaseCount;

    public AudioSource audioSourceEffects;
    public AudioClip audioClipEffects;

    public GameObject secondFloorEnemies;
    public GameObject thirdFloorEnemies;
    public Transform thirdFloorBoardPos;

    private bool isSecondFloor = true;
    public void Start()
    {
        //cam = Camera.main.transform.parent.gameObject;
        for (int i = 0; i < skillsSmallScales.Length; i++)
        {
            skillsSmallScales[i] = skillsFirstPosAndScales[i].localScale / 2.5f;
        }
        nextDashText.text = (playerAttack.dashTimer.GetTarget() - dashTimeDecreaseAmount) + " SC";
        nextUltiText.text = (playerAttack.ultiDestinationTimer.GetTarget() - ultiTimeDecreaseAmount) + " SC";
        nextSlimeBulletText.text = (playerAttack.playerFireBulletAttackPower + slimeBulletPowerIncreaseAmount) + " DMG";
        nextPunchText.text = (playerAttack.playerPunchAttackPower + punchPowerIncreaseAmount) + " DMG";
        nextHealthText.text = (playerHealth.health + healthIncreaseAmount) + " HP";

       // PinkPointEntered(GameObject.FindGameObjectWithTag("Player"));
        //PlayerPrefs.SetInt("DnaCount", 500);
    }

    void Update()
    {
        if (boardCreateControl)
        {
            if (isOpening)
            {
                boardCreateTimer.UpdateTimer();
                var rot = board.transform.rotation;
                board.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, boardCreateTimer.NormalizedTime);
                board.transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
                if (boardCreateTimer.Done())
                {
                    board.transform.rotation = Quaternion.Lerp(board.transform.rotation, Quaternion.Euler(0, 0, 0), boardCreateTimer.NormalizedTime);
                    boardCreateControl = false;
                    camMovementControl = true;
                    boardCreateTimer.ResetTimer();
                }
            }
            else
            {
                boardCreateTimer.UpdateTimer();
                var rot = board.transform.rotation;
                board.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, boardCreateTimer.NormalizedTime);
                board.transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
                if (boardCreateTimer.Done())
                {
                    board.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), board.transform.rotation, boardCreateTimer.NormalizedTime);
                    boardCreateControl = false;
                    camMovementControl = true;
                    boardCreateTimer.ResetTimer();
                }
            }
        }
        if (camMovementControl)
        {
            if (isOpening)
            {
                cameraMovementTimer.UpdateTimer();
                cam.transform.position = Vector3.Lerp(camFirstPos, boardPos.position, cameraMovementTimer.NormalizedTime);

                if (cameraMovementTimer.Done())
                {
                    camMovementControl = false;
                    cameraMovementTimer.ResetTimer();
                    isOpening = false;
                }
            }
            else
            {
                cameraMovementTimer.UpdateTimer();
                cam.transform.position = Vector3.Lerp(boardPos.position, camFirstPos, cameraMovementTimer.NormalizedTime);

                if (cameraMovementTimer.Done())
                {
                    camMovementControl = false;
                    isOpening = true;
                    boardCreateControl = true;
                    boardCreateControl = false;
                    camMovementControl = false;
                    skillsPressMovementControl = false;
                    upgradeSceneControl = true;
                    if (isSecondFloor)
                    {
                        isSecondFloor = false;
                        this.gameObject.transform.parent.gameObject.transform.position = thirdFloorBoardPos.position;
                        pinkCircle.SetActive(true);
                        this.GetComponent<BoxCollider2D>().enabled = true;
                    }
                    cameraMovementTimer.ResetTimer();
                    this.gameObject.GetComponent<SkillTree>().enabled = false;
                }
            }
        }
        if (skillsPressMovementControl && upgradeSceneControl)
        {
            skillsPressMovementTimer.UpdateTimer();
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].transform.position = Vector3.Lerp(skillsFirstPosAndScales[i].position, skillsSmallPosition.position + new Vector3(3.65f * i, 0, 0), skillsPressMovementTimer.NormalizedTime);
                skills[i].transform.localScale = Vector3.Lerp(skillsFirstPosAndScales[i].localScale, skillsSmallScales[i] * (i == skillCount ? 1.3f : 1), skillsPressMovementTimer.NormalizedTime);
            }

            if (skillsPressMovementTimer.Done())
            {
                skillsPressMovementControl = false;
                upgradeSceneControl = false;
            }
        }
    }
    //public void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        player = other.gameObject;
    //        camFirstPos = cam.transform.position;
    //        boardCreateControl = true;

    //        this.GetComponent<BoxCollider2D>().enabled = false;
    //        other.GetComponent<PlayerMovement>().enabled = false;
    //        cam.GetComponent<CamFollow>().enabled = false;
    //        pinkCircle.SetActive(false);
    //    }
    //}
    public void PinkPointEntered(GameObject obj)
    {
        player = obj;
        cam = Camera.main.transform.parent.gameObject;
        camFirstPos = cam.transform.position;
        boardCreateControl = true;

        this.GetComponent<BoxCollider2D>().enabled = false;
        obj.GetComponent<PlayerMovement>().enabled = false;
        obj.GetComponent<PlayerAttack>().enabled = false;
        obj.GetComponent<PlayerHealth>().enabled = false;
        cam.GetComponent<CamFollow>().enabled = false;
        pinkCircle.SetActive(false);
    }

    public void DashPowerUpScreenButton()
    {
        audioSourceEffects.PlayOneShot(audioClipEffects);
        UpgradeScreen(0);
    }
    public void UltiPowerUpScreenButton()
    {
        audioSourceEffects.PlayOneShot(audioClipEffects);
        UpgradeScreen(1);
    }
    public void SlimeBulletPowerUpScreenButton()
    {
        audioSourceEffects.PlayOneShot(audioClipEffects);
        UpgradeScreen(2);
    }
    public void PunchPowerUpScreenButton()
    {
        audioSourceEffects.PlayOneShot(audioClipEffects);
        UpgradeScreen(3);
    }
    public void HealthPowerUpScreenButton()
    {
        audioSourceEffects.PlayOneShot(audioClipEffects);
        UpgradeScreen(4);
    }

    public void UpgradeScreen(int index)
    {
        skillsPressMovementControl = true;
        skillCount = index;
        for (int i = 0; i < skillsUpgradeScreens.Length; i++)
        {
            skillsUpgradeScreens[i].SetActive(i == index ? true : false);
            skills[i].transform.localScale = skillsSmallScales[1] * (i == index ? 1.3f : 1);
        }
    }

    public void DashPowerUp()
    {
        if (PlayerPrefs.GetInt("DnaCount") >= dashTimeDecreaseDnaCount && dashIncreaseCount > 0)
        {            
            audioSourceEffects.PlayOneShot(audioClipEffects);
            dashIncreaseCount--;
            
            currentDashText.text = (playerAttack.dashTimer.GetTarget() - dashTimeDecreaseAmount) + " SC";
            if (dashIncreaseCount != 0)
            {
                PlayerPrefs.SetInt("DnaCount", PlayerPrefs.GetInt("DnaCount") - dashTimeDecreaseDnaCount);
                nextDashText.text = (playerAttack.dashTimer.GetTarget() - dashTimeDecreaseAmount - dashTimeDecreaseAmount) + " SC";
                priceDashText.text = (++dashTimeDecreaseDnaCount) + " DNA";
            }
            else
            {
                PlayerPrefs.SetInt("DnaCount", PlayerPrefs.GetInt("DnaCount") - dashTimeDecreaseDnaCount);
                nextDashText.text = "-MAX-";
                priceDashText.text = "-";
            }
            playerAttack.dashTimer.ResetTimer(playerAttack.dashTimer.GetTarget() - dashTimeDecreaseAmount);
        }
    }
    public void UltiPowerUp()
    {
        if (PlayerPrefs.GetInt("DnaCount") >= ultiTimeDecreaseDnaCount && ultiIncreaseCount > 0)
        {           
            audioSourceEffects.PlayOneShot(audioClipEffects);
            ultiIncreaseCount--;
            
            currentUltiText.text = (playerAttack.ultiDestinationTimer.GetTarget() - ultiTimeDecreaseAmount) + " SC";
            if (ultiIncreaseCount != 0)
            {
                PlayerPrefs.SetInt("DnaCount", PlayerPrefs.GetInt("DnaCount") - ultiTimeDecreaseDnaCount);
                nextUltiText.text = (playerAttack.ultiDestinationTimer.GetTarget() - ultiTimeDecreaseAmount - ultiTimeDecreaseAmount) + " SC";
                priceUltiText.text = (++ultiTimeDecreaseDnaCount) + " DNA";
            }
            else
            {
                PlayerPrefs.SetInt("DnaCount", PlayerPrefs.GetInt("DnaCount") - ultiTimeDecreaseDnaCount);
                nextUltiText.text = "-MAX-";
                priceUltiText.text = "-";
            }
            playerAttack.ultiDestinationTimer.ResetTimer(playerAttack.ultiDestinationTimer.GetTarget() - ultiTimeDecreaseAmount);                   
        }
    }
    public void SlimeBulletPowerUp()
    {
        if (PlayerPrefs.GetInt("DnaCount") >= slimeBulletPowerIncreaseDnaCount && slimeBulletIncreaseCount > 0)
        {
            audioSourceEffects.PlayOneShot(audioClipEffects);
            slimeBulletIncreaseCount--;

            currentSlimeBulletText.text = (playerAttack.playerFireBulletAttackPower + slimeBulletPowerIncreaseAmount) + " DMG";
            if (slimeBulletIncreaseCount != 0)
            {
                PlayerPrefs.SetInt("DnaCount", PlayerPrefs.GetInt("DnaCount") - slimeBulletPowerIncreaseDnaCount);
                nextSlimeBulletText.text = (playerAttack.playerFireBulletAttackPower + slimeBulletPowerIncreaseAmount + slimeBulletPowerIncreaseAmount) + " DMG";
                priceSlimeBulletText.text = (++slimeBulletPowerIncreaseDnaCount) + " DNA";
            }
            else
            {
                PlayerPrefs.SetInt("DnaCount", PlayerPrefs.GetInt("DnaCount") - slimeBulletPowerIncreaseDnaCount);
                nextSlimeBulletText.text = "-MAX-";
                priceSlimeBulletText.text = "-";
            }
            playerAttack.playerFireBulletAttackPower += slimeBulletPowerIncreaseAmount;            
        }
    }
    public void PunchPowerUp()
    {
        if (PlayerPrefs.GetInt("DnaCount") >= punchPowerIncreaseDnaCount && punchIncreaseCount > 0)
        {
            audioSourceEffects.PlayOneShot(audioClipEffects);
            punchIncreaseCount--;

            currentPunchText.text = (playerAttack.playerPunchAttackPower + punchPowerIncreaseAmount) + " DMG";
            if (punchIncreaseCount != 0)
            {
                PlayerPrefs.SetInt("DnaCount", PlayerPrefs.GetInt("DnaCount") - punchPowerIncreaseDnaCount);
                nextPunchText.text = (playerAttack.playerPunchAttackPower + punchPowerIncreaseAmount + punchPowerIncreaseAmount) + " DMG";
                pricePunchText.text = (++punchPowerIncreaseDnaCount) + " DNA";
            }
            else
            {
                PlayerPrefs.SetInt("DnaCount", PlayerPrefs.GetInt("DnaCount") - punchPowerIncreaseDnaCount);
                nextPunchText.text = "-MAX-";
                pricePunchText.text = "-";
            }
            playerAttack.playerPunchAttackPower += slimeBulletPowerIncreaseAmount;
        }
    }
    public void HealthPowerUp()
    {
        if (PlayerPrefs.GetInt("DnaCount") >= healthIncreaseDnaCount && healthIncreaseCount > 0)
        {
            audioSourceEffects.PlayOneShot(audioClipEffects);
            healthIncreaseCount--;

            currentHealthText.text = (playerHealth.lifeSlider.maxValue + healthIncreaseAmount) + " HP";
            if (healthIncreaseCount != 0)
            {
                PlayerPrefs.SetInt("DnaCount", PlayerPrefs.GetInt("DnaCount") - healthIncreaseDnaCount);
                nextHealthText.text = (playerHealth.lifeSlider.maxValue + healthIncreaseAmount + healthIncreaseAmount) + " HP";          
                priceHealthText.text = (++healthIncreaseDnaCount) + " DNA";
            }
            else
            {
                PlayerPrefs.SetInt("DnaCount", PlayerPrefs.GetInt("DnaCount") - healthIncreaseDnaCount);
                nextHealthText.text = "-MAX-";
                priceHealthText.text = "-";
            }
            playerHealth.lifeSlider.maxValue += healthIncreaseAmount;
            playerHealth.health += healthIncreaseAmount;
            playerHealth.lifeSlider.value += healthIncreaseAmount;
        }
    }
    public void CloseButton()
    {
        if (isSecondFloor) secondFloorEnemies.SetActive(true);
        else thirdFloorEnemies.SetActive(true);
        audioSourceEffects.PlayOneShot(audioClipEffects);
        boardCreateControl = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerAttack>().enabled = true;
        player.GetComponent<PlayerHealth>().enabled = true;
        cam.GetComponent<CamFollow>().enabled = true;
    }

}
 