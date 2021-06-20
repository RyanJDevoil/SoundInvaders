using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject enemyBaseCopy;
    public Camera player;
    public Text scoreUI;
    public Text healthUI;
    public Text ammoUI;
    public Text hiScoreUI;
    public AudioSource gunSound;
    public AudioSource reloadSound;
    public AudioSource pain1Sound;
    public AudioSource pain2Sound;
    public AudioSource deathSound;

    public int maxAmmo = 5;
    public int health = 3;
    public int maxEnemies = 5;

    private int score = 0;
    private int curMaxEnemies = 1;
    private int curEnemies = 0;
    private int curAmmo;
    private hiscoreData hiscores;

    private bool reloading = false;
    private bool dying = false;

    private string logPath = "gameLog.txt";
    private string scoresPath = "scores.dat";

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        hiscores = hiscoreData.loadScores(scoresPath);
        print(string.Join(", ", hiscores.hiscoreArray));

        StreamWriter logger = new StreamWriter(logPath, true);
        logger.WriteLine("Game start on " + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        logger.Close();

        curAmmo = maxAmmo;

        scoreUI.text = "Score: " + score.ToString();
        healthUI.text = "Health: " + health.ToString();
        ammoUI.text = "Ammo: " + curAmmo.ToString();
        hiScoreUI.text = "HiScore: " + (hiscores.hiscoreArray[0]).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            if (Input.GetMouseButtonDown(0) && curAmmo > 0)
            {
                gunSound.Play();
                curAmmo += -1;
                ammoUI.text = "Ammo: " + curAmmo.ToString();
                Ray ray = player.ViewportPointToRay(0.5f * Vector2.one);
                RaycastHit hit;
                bool enemyHit = Physics.Raycast(ray, out hit);
                if (enemyHit)
                {
                    Destroy(hit.transform.gameObject.transform.parent.gameObject);
                    curEnemies = curEnemies - 1;

                    score = score + 1;
                    scoreUI.text = "Score: " + score.ToString();
                    if (score > hiscores.hiscoreArray[0])
                    {
                        hiScoreUI.text = "HiScore: " + score.ToString();
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0) && curAmmo == 0 && reloading == false)
            {
                ammoUI.text = "Reloading...";
                StartCoroutine(reload());
            }
        }
        while (curEnemies < curMaxEnemies){
            curEnemies = curEnemies + 1;
            createEnemy(enemyBaseCopy);
        }
        curMaxEnemies = Mathf.Min(Mathf.FloorToInt((score + 5)/(5)), maxEnemies);
       
    }

    private void createEnemy(GameObject enemyBaseCopy)
    {
        float spawnAngle = Random.Range(0, 360);
        float playerAngle = player.transform.localEulerAngles.y;
        float relToPlayer = spawnAngle - playerAngle;
        if (relToPlayer < 0)
        {
            relToPlayer += 360;
        }

        GameObject enemyClone = Instantiate(enemyBaseCopy, new Vector3(0, 0, 0), Quaternion.Euler(0, spawnAngle, 0));
        enemyClone.transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
        enemyClone.transform.GetChild(0).GetComponent<EnemyMovement>().enabled = true;
        enemyClone.transform.GetChild(0).GetComponent<EnemyMovement>().relToPlayer = relToPlayer;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemies" && !dying)
        {
            string relEnemySpawnPos = (collision.gameObject.GetComponent<EnemyMovement>().relToPlayer).ToString();
            string enemiesOnScreen = curMaxEnemies.ToString();
            string curScore = score.ToString();
            StreamWriter logger = new StreamWriter(logPath, true);
            logger.WriteLine(relEnemySpawnPos + "," + curMaxEnemies + "," + curScore);
            logger.Close();

            Destroy(collision.gameObject.transform.parent.gameObject);
            curEnemies = curEnemies - 1;

            health = health - 1;
            switch (health)
            {
                case 1:
                    pain2Sound.Play();
                    break;
                case 0:
                    StartCoroutine(death());
                    break;
                default:
                    pain1Sound.Play();
                    break;
            }
            healthUI.text = "Health: " + health.ToString();
        }
    }
    private IEnumerator reload()
    {
        reloading = true;
        reloadSound.Play();
        yield return new WaitWhile(() => reloadSound.isPlaying);
        curAmmo = maxAmmo;
        ammoUI.text = "Ammo: " + curAmmo.ToString();
        reloading = false;
    }

    private IEnumerator death()
    {
        dying = true;
        deathSound.Play();
        yield return new WaitWhile(() => deathSound.isPlaying);

        StreamWriter logger = new StreamWriter(logPath, true);
        hiscores.addHiscoreData(hiscores.hiscoreArray, score);
        if (hiscores.newHiscore)
        {
            hiscoreData.saveScores(scoresPath, hiscores);
            print("SAVED");
        }
        print(string.Join(", ", hiscores.hiscoreArray));

        logger.WriteLine("");
        logger.Close();

        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

    }
    //private void saveScores()
    //{
    //    FileStream scoreFile;

    //    if (File.Exists(scoresPath))
    //    {
    //        scoreFile = File.OpenWrite(scoresPath);
    //    }
    //    else
    //    {
    //        scoreFile = File.Create(scoresPath);
    //    }
    //    BinaryFormatter bf = new BinaryFormatter();
    //    bf.Serialize(scoreFile, hiscores);
    //    scoreFile.Close();
    //}
    //private void loadScores()
    //{
    //    FileStream scoreFile;

    //    if (File.Exists(scoresPath))
    //    {
    //        scoreFile = File.OpenRead(scoresPath);
    //    }
    //    else
    //    {
    //        Debug.LogError("File not found");
    //        return;
    //    }
    //    BinaryFormatter bf = new BinaryFormatter();
    //    hiscores = (hiscoreData)bf.Deserialize(scoreFile);
    //    scoreFile.Close();
    //    print("Done loading");
    //}
}

