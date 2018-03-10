using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class RocketAnim : MonoBehaviour
    {

        Animator animator;

        BoardManager bd;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();

            bd = new BoardManager();
        }

        // Update is called once per frame
        void Update()
        {
            //if(bd.rocketPlayer1Turn)
            //{
            //    Debug.Log("Player 1 turn");
            //    animator.SetBool("Player1Turn", true);
            //    animator.SetBool("Player2Turn", false);
            //}

            //if (bd.rocketPlayer2Turn)
            //{
            //    Debug.Log("player 2 turn");
            //    animator.SetBool("Player2Turn", true);
            //    animator.SetBool("Player1Turn", false);
            //}
        }
    }
}
