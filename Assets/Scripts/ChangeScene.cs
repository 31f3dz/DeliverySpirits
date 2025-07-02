using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    public void Load()
    {
        GameController.stagePoints = 0;

        for (int i = 0; i < Post.successCounts.Length; i++)
        {
            Post.successCounts[i] = 0;
            Shooter.shootCounts[i] = 0;
        }

        SceneManager.LoadScene(sceneName);
    }
}
