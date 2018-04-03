using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButtonBehaviorPlayer2 : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    private Button pb;

    //public bool isAlien;
    public Sprite[] hoverSprite;
    public Sprite[] clickedSprite;

    private Sprite currentHoverSprite; //hover background
    private Sprite currentClickedSprite;
    private Sprite oldSprite;

    //private bool isActivated = false;

    void Start()
    {
        pb = GetComponent<Button>();
        pb.onClick.AddListener(TaskOnClick);
        oldSprite = pb.image.sprite;

        if(!GameCore.BoardManager.againstNetwork)
        {
            //local game
            if(GameBoardData.LocalGamePlayer1IsAlien)
            {
                currentHoverSprite = hoverSprite[0];
                currentClickedSprite = clickedSprite[0];
            }
            else
            {
                currentHoverSprite = hoverSprite[1];
                currentClickedSprite = clickedSprite[1];
            }
        }
        else
        {
            if (GameBoardData.NetworkGameLocalPlayerIsAstronaut)
            {
                Debug.Log("local player is an astro");
                currentHoverSprite = hoverSprite[0];
                currentClickedSprite = clickedSprite[0];
            }
            else
            {
                Debug.Log("local player is an alien");
                currentHoverSprite = hoverSprite[1];
                currentClickedSprite = clickedSprite[1];
            }
        }
    }

    void Update()
    {
        //if (GameCore.BoardManager.againstNetwork)
        //{
        //    if (GameBoardData.NetworkGameLocalPlayerIsAstronaut)
        //    {
        //        currentHoverSprite = hoverSprite[0];
        //        currentClickedSprite = clickedSprite[0];
        //    }
        //    else
        //    {
        //        currentHoverSprite = hoverSprite[1];
        //        currentClickedSprite = clickedSprite[1];
        //    }
        //}
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        oldSprite = pb.image.sprite;
        pb.image.sprite = currentHoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pb.image.sprite = oldSprite;
    }

    public void TaskOnClick()
    {
        pb.image.sprite = currentClickedSprite;
        oldSprite = pb.image.sprite;
    }
}
