using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    Text score;
    [SerializeField]
    Text songName;
    [SerializeField]
    Text heartText;
    [SerializeField]
    Text timeText;
    [SerializeField]
    GameObject musicPanel;
    [SerializeField]
    private GameObject gameOverPanel = null;

    bool isSetting = false;

    private List<MusicPanel> musicList = new List<MusicPanel>();

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("GameOver", -1) != -1)
        {
            gameOverPanel.SetActive(true);
            GameOverManager.Instance.SetGameOverUI();
        }
    }

    private void Start()
    {
        InvokeRepeating("TimeText", 0f, 0.5f);
        InvokeRepeating("Heart", 0f, 1f);
        CreatePanels();
    }

    public void UpdateScore()
    {
        songName.text = GameManager.Instance.stageName;
        score.text = string.Format("{0}", score);
    }

    //도전 티켓 텍스트 업데이트
    public void Heart()
    {
        Debug.Log(GameManager.Instance.CurrentUser.heart);
        heartText.text = string.Format("♪ {0}", GameManager.Instance.CurrentUser.heart);
    }

    //도전 티켓 시간 텍스트
    public void TimeText()
    {
        Debug.Log(GameManager.Instance.CurrentUser.time);
        if (GameManager.Instance.CurrentUser.heart == 15)
        {
            timeText.text = string.Format("남은 시간(초) : 0");
        }
        else
        {
            timeText.text = string.Format("남은 시간(초) : {0}", 60 - (int)GameManager.Instance.CurrentUser.time);
        }
    }

    public void Setting(GameObject setting)
    {
        isSetting = isSetting ? false : true;
        setting.SetActive(isSetting);
    }

    public void OpenUI(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void CloseUI(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void CreatePanels()
    {
        GameObject panel = null;
        MusicPanel panelComponent = null;

        foreach (MusicData music in GameManager.Instance.musicList)
        {
            panel = Instantiate(musicPanel.gameObject, musicPanel.transform.parent);
            panelComponent = panel.GetComponent<MusicPanel>();
            panelComponent.SetValue(music);
            panel.SetActive(true);
            musicList.Add(panelComponent);
        }

    }

    public void Quit()
    {
        Application.Quit();
    }
}
