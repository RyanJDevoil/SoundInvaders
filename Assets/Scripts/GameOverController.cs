using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Text hiscoreText;
    public Text newHiscoreUI;
    private string scoresPath = "scores.dat";
    hiscoreData hiscores;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        hiscores = hiscoreData.loadScores(scoresPath);
        hiscoreText.text = string.Join(System.Environment.NewLine, hiscores.hiscoreArray);
        if (hiscores.newHiscore)
        {
            newHiscoreUI.text = "New No." + (hiscores.hiScoreLocation).ToString() + " high score!";
            hiscores.newHiscore = false;
            hiscoreData.saveScores(scoresPath, hiscores);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
