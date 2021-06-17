using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    public float spawnAngle;
    public float relToPlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Transform>().position = Vector3.MoveTowards(GetComponent<Transform>().position, target.position, 1 * Time.fixedDeltaTime);
    }
}
