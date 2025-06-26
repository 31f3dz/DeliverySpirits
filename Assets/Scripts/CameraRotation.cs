using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 3.0f; // マウス感度

    // 上下の角度上限
    public float minVerticalAngle = -60.0f;
    public float maxVerticalAngle = 60.0f;

    // 左右の角度上限
    public float minHorizontalAngle = -90.0f;
    public float maxHorizontalAngle = 90.0f;

    // プレイ中のカメラの角度
    float verticalRotation = 0;
    float horizontalRotation = 0;

    // プレイ開始時のカメラの左右の角度
    float initialY = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 画面中心にカーソルをロック
        Cursor.visible = false; // カーソルを非表示

        Vector3 angles = transform.eulerAngles; // プレイ開始時のカメラの角度
        initialY = angles.y; // あらためてY軸の基準はカメラの値による
        verticalRotation = angles.x; // カメラの初期の角度(上下)を入れておく
        horizontalRotation = 0; // 明確に初期の角度の計算値も0
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // その時のマウスの動きに応じた数値を積み重ね
        horizontalRotation += mouseX;
        // 最大・最小に絞り込みはされる
        horizontalRotation = Mathf.Clamp(horizontalRotation, minHorizontalAngle, maxHorizontalAngle);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        // 基準としている角度に対してmin～maxの間でのマウス移動の積み重ね
        float yRotation = initialY + horizontalRotation;

        // カメラの角度を決定する
        transform.rotation = Quaternion.Euler(verticalRotation, yRotation, 0);
    }
}
