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
    public int health = 3;
    public int maxEnemies = 5;
    private int score = 0;
    private int curMaxEnemies = 1;
    private int curEnemies = 0;
    void Start()
    {
        scoreUI.text = "Score: " + score.ToString();
        healthUI.text = "Health: " + health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        while (curEnemies < curMaxEnemies){
            createEnemy(enemyBaseCopy);
            curEnemies = curEnemies + 1;
        }
        Ray ray = player.ViewportPointToRay(0.5f * Vector2.one);
        RaycastHit hit;
        bool enemyHit = Physics.Raycast(ray, out hit);
        if (enemyHit && Input.GetMouseButtonDown(0)) {
            Destroy(hit.transform.gameObject);
            curEnemies = curEnemies - 1;
            score = score + 1;
            scoreUI.text = "Score: " + score.ToString();
        }
        curMaxEnemies = Mathf.Min(Mathf.FloorToInt((score + 5)/(5)), maxEnemies);
       
    }

    private void createEnemy(GameObject enemyBaseCopy)
    {
        GameObject enemyClone = Instantiate(enemyBaseCopy, new Vector3(0, 0, 0), Quaternion.Euler(0, Random.Range(1, 360), 0));
        enemyClone.transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
        enemyClone.transform.GetChild(0).GetComponent<EnemyMovement>().enabled = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        print("Collision");
        if (collision.gameObject.tag == "Enemies")
        {
            Destroy(collision.gameObject);
            curEnemies = curEnemies - 1;
            health = health - 1;
            healthUI.text = "Health: " + health.ToString();
            print(health);
            if (health == 0)
            {
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            }
        }
    }
}

