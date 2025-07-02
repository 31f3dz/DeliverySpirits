using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] boxPrefabs; // 宅配物のプレハブを格納
    Transform player; // プレイヤーの位置情報
    Camera cam; // カメラの情報

    public static int boxNum; // 配列の何番目のボックスが選択されているか
    public float shootSpeed = 100f; // 投げたときの力
    public float upSpeed = 30f; // 投げたときの上向きの力

    bool startShoot; // シュート可能かどうかのフラグ

    // 各ボックスの投擲数
    public static int[] shootCounts = { 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShootEnabled", 0.5f);

        // プレイヤー情報の取得
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // カメラ情報の取得(MainCameraタグがついているカメラ情報は簡単に参照可)
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState != GameState.playing) return;

        if (Input.GetMouseButtonDown(0)) // もしも左クリックが押されたら
        {
            if (startShoot) Shoot(); // フラグがONならシュートするメソッド
        }

        // 生成対象のBoxの切り替え
        if (Input.GetMouseButtonDown(1))
        {
            boxNum++;
            if (boxPrefabs.Length == boxNum) boxNum = 0;
        }
    }

    void ShootEnabled()
    {
        startShoot = true; // シュート可能にする
    }

    void Shoot()
    {
        // プレイヤーが消滅していなければ
        if (player != null)
        {
            // 配列の中から指定された番号のBOXを生成
            GameObject box = Instantiate(boxPrefabs[boxNum], player.position, Quaternion.identity);

            // 生成したBOXのRigidbodyを取得
            Rigidbody rbody = box.GetComponent<Rigidbody>();

            // 生成したBOXのAddForceの力でシュート ※カメラの角度を考慮した方向にシュート 飛び出す元の位置はPlayerの位置
            rbody.AddForce(new Vector3(
                cam.transform.forward.x * shootSpeed,
                cam.transform.forward.y + upSpeed,
                cam.transform.forward.z * shootSpeed
            ), ForceMode.Impulse);

            shootCounts[boxNum]++;
        }
    }
}
