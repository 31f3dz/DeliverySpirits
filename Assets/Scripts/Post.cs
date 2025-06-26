using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PostType
{
    box1,
    box2,
    box3,
}

public class Post : MonoBehaviour
{
    public PostType type; // 自作した列挙型を扱う変数 自分のタイプを決める
    bool posted; // 配達済みかどうかのフラグ

    public int getPoint = 50; // ポイント
    public TextMeshProUGUI pointText;

    public PointUI pointUI; // ポイントを表示するUIコントローラークラス

    void OnTriggerEnter(Collider other)
    {
        if (!posted)
        {
            switch (type)
            {
                case PostType.box1:
                    if (other.gameObject.CompareTag("Box1"))
                        // 宅配完了の処理
                        PostComp();
                    break;
                case PostType.box2:
                    if (other.gameObject.CompareTag("Box2"))
                        // 宅配完了の処理
                        PostComp();
                    break;
                case PostType.box3:
                    if (other.gameObject.CompareTag("Box3"))
                        // 宅配完了の処理
                        PostComp();
                    break;
            }
        }
    }

    void PostComp()
    {
        posted = true;

        // エフェクトそのものの座標より少し上に表示
        Vector3 showPos = transform.position + (Vector3.up * 1.5f);
        // 表示対象オブジェクトのテキスト内容を設定したスコアと同じになるように
        pointText.text = "+" + getPoint.ToString() + "Point!";

        // 表示メソッドを発動
        pointUI.Show(showPos);

        GameController.stagePoints += getPoint; // ステージポイント加算

        Destroy(transform.parent.gameObject, 1.0f);
    }
}
