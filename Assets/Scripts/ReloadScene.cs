﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //if (GameCore.BoardManager.againstNetwork)
        //{
        //    if (GameCore.BoardManager.playerGoingFirst)
        //    {
        //        GameCore.BoardManager.waitForNetwork = true;
        //    }
        //    else
        //    {
        //        GameCore.BoardManager.waitForNetwork = false;
        //    }
        //}
    }
}
