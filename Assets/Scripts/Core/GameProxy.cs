using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    public PlayerInfo(string name)
    {
        Name = name;
        CurrentLvl = 0;
        //Scores.Add(0);
        LevelCount = 0;
    }
    public string Name;
    
    /*заготовка на будующее
    //public List<LevelInfo> LevelsCompleted = new List<LevelInfo>();
    public List<int> Scores = new List<int>();//список доступных уровней и их счетчик*/
    
    public int CurrentLvl;//текущий уровень
    
    public int LevelCount;//кол-во доступных уровней
}
[CreateAssetMenu(menuName = "GameProxy")]
public class GameProxy : ScriptableObject
{
    public List<PlayerInfo> players = new List<PlayerInfo>();
    public PlayerInfo Player;

    public void EntryGame(string name)
    {
        Player = GetPlayer(name);
    }
    public PlayerInfo CreatePlayer(string name)
    {
        PlayerInfo player = new PlayerInfo(name);
        players.Add(player);
        return player;
    }
    public PlayerInfo GetPlayer(string name)
    {
        foreach (var player in players)
        {
            if (player.Name == name)
                return player;
        }
        return CreatePlayer(name);
    }
    
}
