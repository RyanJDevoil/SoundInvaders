[System.Serializable]
public class hiscoreData
{
    public int[] hiscores;
    public bool newHighScore;

    public hiscoreData()
    {
        hiscores = new int[10];
        newHighScore = false;
    }
    public void addHiscoreData(int[] data, int scoreInt)
    {
        for(int x = 0; x < hiscores.Length-1; x++)
        {
            if (scoreInt > hiscores[x])
            {
                shiftarray(hiscores, x);
                hiscores[x] = scoreInt;
                newHighScore = true;
                break;
            }
        }

    }

    private int[] shiftarray(int[] array, int x)
    {
        if(x == array.Length-1)
        {
            return array;
        }
        else
        {
            shiftarray(hiscores, x + 1);
            array[x + 1] = array[x];
            return array;
        }
    }
}
