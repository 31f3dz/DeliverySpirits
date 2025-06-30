using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    public float dangerSpeed = 10.0f; // 迫ってくるスピード

    // Start is called before the first frame update
    void Start()
    {
        // 10秒後に消滅
        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // もしもゲームステータスがtimeoverならコライダーをオフ→即消滅
        if (GameController.gameState == GameState.timeover)
        {
            GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject);
        }

        // 自分の座標を前方向(transform.forward)に対してdangerSpeedの係数をかけながら変化していく
        transform.position += transform.forward * dangerSpeed * Time.deltaTime;
    }
}
