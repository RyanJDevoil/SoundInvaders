using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyBaseCopy;
    public Camera player;
    public Text scoreUI;
    public int maxEnemies = 5;
    private int score = 0;
    private int curMaxEnemies = 1;
    private int curEnemies = 0;
    void Start()
    {
        //createEnemy(enemyBaseCopy);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            SetCursorLock(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorLock(false);
        }
#endif
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
            scoreUI.text = score.ToString();
        }
        curMaxEnemies = Mathf.Min(Mathf.FloorToInt((score + 5)/(5)), maxEnemies);
        
    }

    private void createEnemy(GameObject enemyBaseCopy)
    {
        GameObject enemyClone = Instantiate(enemyBaseCopy, new Vector3(0, 0, 0), Quaternion.Euler(0, Random.Range(1, 360), 0));
        enemyClone.transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
        enemyClone.transform.GetChild(0).GetComponent<EnemyMovement>().enabled = true;
    }
    private void SetCursorLock(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

