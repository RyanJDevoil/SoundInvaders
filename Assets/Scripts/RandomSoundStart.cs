using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().time = GetComponent<AudioSource>().clip.length * Random.Range(1, 32) / 32f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
