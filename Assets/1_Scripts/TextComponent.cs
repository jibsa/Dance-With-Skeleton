using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComponent : MonoBehaviour
{
    [SerializeField]
    Text heartText;
    [SerializeField]
    Text timeText;

    private void Start()
    {
        InvokeRepeating("UpdateHeart", 0f, 1f);
        InvokeRepeating("UpdateTime", 0f, 1f);
    }
    void UpdateHeart()
    {
        heartText.text = string.Format("♪ {0}", GameManager.Instance.CurrentUser.heart);
    }
    void UpdateTime()
    {
        if (GameManager.Instance.CurrentUser.heart == 15)
        {
                timeText.text = string.Format("남은 시간(초) : 0");
        }
        else
        {
                timeText.text = string.Format("남은 시간(초) : {0}", 60 - (int)GameManager.Instance.CurrentUser.time);
        }
    }

}
