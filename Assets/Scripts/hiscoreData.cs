[System.Serializable]
public class hiscoreData
{
    public int[] hiscoreArray;
    public bool newHiScore;

    public hiscoreData()
    {
        hiscoreArray = new int[10];
        newHiScore = false;
    }
    public void addHiscoreData(int[] data, int scoreInt)
    {
        newHiScore = false;
        for(int x = 0; x < hiscoreArray.Length; x++)
        {
            if (scoreInt > hiscoreArray[x])
            {
                shiftarray(hiscoreArray, x);
                hiscoreArray[x] = scoreInt;
                newHiScore = true;
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
            shiftarray(array, x + 1);
            array[x + 1] = array[x];
            return array;
        }
    }
}
