using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameView : MonoBehaviour
{
    
    public GameProxy GameProxy;
    public SelectLevelView SelectLevelView;
    public Text Name;
    public void StartClick()
    {
        if (GameProxy == null || SelectLevelView == null || Name == null)
        {
            return;
        }
        Close();
        GameProxy.EntryGame(Name.text);
        SelectLevelView.SetPlayer(GameProxy.Player);
        SelectLevelView.Open();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    
}
