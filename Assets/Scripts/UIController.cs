using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TimeController timeCnt; // TimeControllerスクリプトの情報
    public TextMeshProUGUI timeText; // TimeTextオブジェクトに付いているTMPのコンポーネントの情報

    public GameObject gameOverPanel; // ゲームオーバーUIを参照

    // ステージポイント関連
    int currentPoint; // UIが管理しているポイント
    public TextMeshProUGUI pointText;

    // 宅配ボックスのUI関連
    public Image boxImage; // 対象コンポーネント
    public Sprite[] boxPics; // BOXの絵
    int currentBoxNum; // UIが把握している宅配BOX番号

    public GameObject resultPanel;
    public string currentStageName; // ステージ名を入力
    public TextMeshProUGUI thisScoreText; // 現在スコアのUI
    public TextMeshProUGUI highScoreText; // ハイスコアのUI
    public TextMeshProUGUI[] boxTexts; // ボックスの個別成績の文字列の配列

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
            // 右クリックして選択したBOX番号がUIの把握しているBOX番号と違うなら
            if (currentBoxNum != Shooter.boxNum)
            {
                currentBoxNum = Shooter.boxNum; // 最新のBOX番号に更新
                // UIが把握している最新のBOX番号に対応した絵を対象のImageコンポーネントのspriteに代入
                boxImage.sprite = boxPics[currentBoxNum];
            }

            if (currentPoint != GameController.stagePoints)
            {
                currentPoint = GameController.stagePoints;
                pointText.text = currentPoint.ToString();
            }

            // 切り上げたdisplayTimeをstringに変換してtextに差し替え
            timeText.text = Mathf.Ceil(timeCnt.displayTime).ToString();

            if (timeText.text == "0")
            {
                // ゲームステータスをtimeoverにする
                GameController.gameState = GameState.timeover;
                // 過去のハイスコアを取得
                int highScore = PlayerPrefs.GetInt(currentStageName);

                // もし現在スコアが過去スコアを上回ったら
                if (GameController.stagePoints > highScore)
                {
                    highScore = GameController.stagePoints;
                    PlayerPrefs.SetInt(currentStageName, highScore);
                }

                // ThisTimesScoreへの表示
                thisScoreText.text = "This Time's Score " + currentPoint.ToString();
                // HighScoreへの表示
                highScoreText.text = "High Score " + highScore.ToString();

                // 各ボックスの成功率を表示
                for (int i = 0; i < boxTexts.Length; i++)
                {
                    float successRate;
                    // 分母が0だと計算できないので成功率を強制的に0にする
                    if (Shooter.shootCounts[i] == 0) successRate = 0;
                    else
                    {
                        // Rateの計算 ※分子・分母両方ともintの場合は、どちらか片方をfloatにキャストしておけば成立
                        successRate = ((float)Post.successCounts[i] / Shooter.shootCounts[i]) * 100f;
                    }

                    boxTexts[i].text = "Box" + (i+1) + " " + Post.successCounts[i] + " / " + Shooter.shootCounts[i] + " success rate " + successRate.ToString("F1") + "%";
                }

                resultPanel.SetActive(true);

                // カーソルの復活
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                GameController.gameState = GameState.end;
            }
        }
        else if (GameController.gameState == GameState.gameover)
        {
            gameOverPanel.SetActive(true);

            // カーソルの復活
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            GameController.gameState = GameState.end;
        }
    }
}
