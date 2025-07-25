using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MinLane = -1;
    const int MaxLane = 1;
    const float LaneWidth = 3.0f;

    CharacterController controller;
    // Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;

    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;

    // Start is called before the first frame update
    void Start()
    {
        // 必要なコンポーネントを自動取得
        controller = GetComponent<CharacterController>();
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームステータスがplayingのときのみ左右に動かせる
        if (GameController.gameState == GameState.playing)
        {
            if (Input.GetKeyDown(KeyCode.A)) MoveToLeft();
            if (Input.GetKeyDown(KeyCode.D)) MoveToRight();
            if (Input.GetKeyDown("space")) Jump();
        }

        // 徐々に加速しZ方向に常に前進させる
        float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
        moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

        // X方向は目標のポジションまでの差分の割合で速度を計算
        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;

        // 重力分の力を毎フレーム追加
        moveDirection.y -= gravity * Time.deltaTime;

        // 移動実行
        // controller.Move(moveDirection * Time.deltaTime); // 前進するだけならこれでOK
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        // 移動後接地していたらY方向の速度はリセットする
        if (controller.isGrounded) moveDirection.y = 0;

        // 速度が0以上なら走っているフラグをtrueにする
        // animator.SetBool("run", moveDirection.z > 0.0f);
    }

    // 左のレーンに移動を開始
    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }

    // 右のレーンに移動を開始
    public void MoveToRight()
    {
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;

            // ジャンプトリガーを設定
            // animator.SetTrigger("Jump");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Danger"))
        {
            controller.Move(new Vector3(0, 5, 0));
            controller.transform.Rotate(Random.Range(-45, 45), Random.Range(-45, 45), Random.Range(-45, 45)); // 回転させる
            GameController.gameState = GameState.gameover;
            Destroy(gameObject, 3.0f);
        }
    }
}
