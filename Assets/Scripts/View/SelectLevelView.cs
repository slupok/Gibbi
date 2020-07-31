using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelView : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    //public List<SceneAsset> Scenes = new List<SceneAsset>();
    public int SceneCount;
    public GameObject Togle;

    [Header("Levels Area")]
    [SerializeField]private ToggleGroup ToggleGroup;
    [SerializeField]private RectTransform RectLevelView;
    private string _startSceneName;
    private int _startScene;
    public void Awake()
    {
        if (PlayerInfo == null)
        {
            return;
        }
        for (int i = 0; i < SceneCount; i++)
        {
            var lvl = Instantiate(Togle,RectLevelView).GetComponent<ToggleLevel>();
            lvl.SetLevel(i);
            lvl.SetToggleGroup(ToggleGroup);
            lvl.SelectLevelView = this;
            if(i <= PlayerInfo.LevelCount)
                lvl.SetInteactable(true);
            else
                lvl.SetInteactable(false);
        }
        float h = (SceneCount / 4 + 1) * 90;
        RectLevelView.sizeDelta = new Vector2(RectLevelView.rect.width,h);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetPlayer(PlayerInfo player)
    {
        PlayerInfo = player;
    }

    public void SetScene(int value)
    {
        _startScene = value;
    }
    public void Load()
    {
        PlayerInfo.CurrentLvl = _startScene;
        //SceneManager.LoadScene(Scenes[_startScene].name);
        SceneManager.LoadScene(_startScene+1);
    }

    public void MainMenuOpen()
    {
        SceneManager.LoadScene(0);
    }
    

}
