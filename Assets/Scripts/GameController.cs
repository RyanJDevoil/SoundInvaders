using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyBaseCopy;
    public Camera player;
    public Text scoreUI;
    public Text healthUI;
    public Text ammoUI;
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
    private bool reloading = false;
    private bool dying = false;
    void Start()
    {
        curAmmo = maxAmmo;
        scoreUI.text = "Score: " + score.ToString();
        healthUI.text = "Health: " + health.ToString();
        ammoUI.text = "Ammo: " + curAmmo.ToString();
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
                    Destroy(hit.transform.gameObject);
                    curEnemies = curEnemies - 1;
                    score = score + 1;
                    scoreUI.text = "Score: " + score.ToString();
                }
            }
            else if (Input.GetMouseButtonDown(0) && curAmmo == 0 && reloading == false)
            {
                ammoUI.text = "Reloading...";
                StartCoroutine(reload());
            }
        }
        while (curEnemies < curMaxEnemies){
            createEnemy(enemyBaseCopy);
            curEnemies = curEnemies + 1;
        }
        curMaxEnemies = Mathf.Min(Mathf.FloorToInt((score + 5)/(5)), maxEnemies);
       
    }

    private void createEnemy(GameObject enemyBaseCopy)
    {
        GameObject enemyClone = Instantiate(enemyBaseCopy, new Vector3(0, 0, 0), Quaternion.Euler(0, Random.Range(1, 360), 0));
        enemyClone.transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
        enemyClone.transform.GetChild(0).GetComponent<EnemyMovement>().enabled = true;
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
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

    }
    void OnCollisionEnter(Collision collision)
    {
        print("Collision");
        if (collision.gameObject.tag == "Enemies")
        {
            Destroy(collision.gameObject);
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
            print(health);
            //if (health == 0)
            //{

            //    SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            //}
        }
    }
}

