using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class hiscoreData
{
    public int[] hiscoreArray;
    public bool newHiscore;
    public int hiScoreLocation;

    public hiscoreData()
    {
        hiscoreArray = new int[10];
        newHiscore = false;
        hiScoreLocation = -1;
    }
    public void addHiscoreData(int[] data, int scoreInt)
    {
        newHiscore = false;
        for(int x = 0; x < hiscoreArray.Length; x++)
        {
            if (scoreInt > hiscoreArray[x])
            {
                shiftarray(hiscoreArray, x);
                hiscoreArray[x] = scoreInt;
                hiScoreLocation = x + 1;
                newHiscore = true;
                break;
            }
        }

    }

    public static void saveScores(string scoresPath, hiscoreData hiscores)
    {
        FileStream scoreFile;

        if (File.Exists(scoresPath))
        {
            scoreFile = File.OpenWrite(scoresPath);
        }
        else
        {
            scoreFile = File.Create(scoresPath);
        }
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(scoreFile, hiscores);
        scoreFile.Close();
    }
    public static hiscoreData loadScores(string scoresPath)
    {
        FileStream scoreFile;

        if (File.Exists(scoresPath))
        {
            scoreFile = File.OpenRead(scoresPath);
        }
        else
        {
            Debug.LogError("File not found");
            return new hiscoreData();
        }
        BinaryFormatter bf = new BinaryFormatter();
        hiscoreData hiscores = (hiscoreData)bf.Deserialize(scoreFile);
        scoreFile.Close();
        return hiscores;
    }

    private int[] shiftarray(int[] array, int x)
    {
        if(x == array.Length-1)
        {
            return array;
        }
        else
        {
            shiftarray(array, x + 1);
            array[x + 1] = array[x];
            return array;
        }
    }
}
