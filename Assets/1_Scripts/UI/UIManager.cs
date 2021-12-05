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
    Text wowText;
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
        
        CreatePanels();
    }

    public void GameOverScore()
    {
        songName.text = GameManager.Instance.stageName;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        score.text = string.Format("Score : {0}", score);
    }

    //���� Ƽ�� �ؽ�Ʈ ������Ʈ
    public void Heart()
    {
        Debug.Log(GameManager.Instance.CurrentUser.heart);
        heartText.text = string.Format("�� {0}", GameManager.Instance.CurrentUser.heart);
    }

    //���� Ƽ�� �ð� �ؽ�Ʈ
    public void TimeText()
    {
        Debug.Log(GameManager.Instance.CurrentUser.time);
        if (GameManager.Instance.CurrentUser.heart == 15)
        {
            timeText.text = string.Format("���� �ð�(��) : 0");
        }
        else
        {
            timeText.text = string.Format("���� �ð�(��) : {0}", 60 - (int)GameManager.Instance.CurrentUser.time);
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
