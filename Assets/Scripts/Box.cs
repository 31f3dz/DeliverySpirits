using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float deleteTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Physics.gravity(重力の値)をAddForceを使って余分に付加している ※2倍にしている
        GetComponent<Rigidbody>().AddForce(Physics.gravity, ForceMode.Acceleration);
    }
}
