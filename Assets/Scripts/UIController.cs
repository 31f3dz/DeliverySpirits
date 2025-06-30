using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TimeController timeCnt; // TimeControllerスクリプトの情報
    public TextMeshProUGUI timeText; // TimeTextオブジェクトに付いているTMPのコンポーネントの情報

    // Start is called before the first frame update
    void Start()
    {
        timeCnt = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState == GameState.playing)
        {
            // 切り上げたdisplayTimeをstringに変換してtextに差し替え
            timeText.text = Mathf.Ceil(timeCnt.displayTime).ToString();
        }
    }
}
