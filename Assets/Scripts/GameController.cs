using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    timeover, // ゲームクリア
    playing,
    gameover,
    end,
}

public class GameController : MonoBehaviour
{
    public static GameState gameState; // 自作したGameState型のstatic変数
    public static int stagePoints; // ステージの得点

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.playing;
        stagePoints = 0; // ポイントリセット
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
