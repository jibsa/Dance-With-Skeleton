using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSc : MonoBehaviour
{
    [SerializeField]
    Button playButton;

    private void Awake()
    {
        //게임 플레이 버튼 클릭 시
        playButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance.CurrentUser.heart <= 0) return;
            GameManager.Instance.CurrentUser.heart--;
            GameManager.Instance.SetStageData();
            SceneManager.LoadScene("InGame");

        });

    }
}
