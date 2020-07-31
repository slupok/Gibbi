using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    
    public GameProxy GameProxy;
    private bool _isPlay;
    
    [Header("View")]
    public Transform CanvasTransform;
    public GameObject SelectLevelPrefab;
    private SelectLevelView _view;
    private bool _isActiveView = false;
    
    [Header("Music")]
    public AudioSource GameMusic;

    private void OnEnable()
    {
        if (GameProxy == null || SelectLevelPrefab == null)
        {
            return;
        }
        _isPlay = true;
        _view = Instantiate(SelectLevelPrefab, CanvasTransform).GetComponent<SelectLevelView>();
        _view.SetPlayer(GameProxy.Player);
    }
    
    public void EndGame()
    {
        if (GameProxy == null)
        {
            return;
        }
        _isPlay = false;
        if (GameProxy.Player.CurrentLvl == GameProxy.Player.LevelCount)
        {
            GameProxy.Player.LevelCount++;
        }
        _view?.Open();


    }

    public void Update()
    {
        if (!_isPlay)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isActiveView)
            {
                _view?.Close();
                _isActiveView = false;
            }
            else
            {
                _view?.Open();
                _isActiveView = true;
            }
        }
    }



}
