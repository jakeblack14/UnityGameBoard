using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterButtonBehavior : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private Button pb;

    public bool isAlien;
    public Sprite[] hoverSprite;
    public Sprite[] clickedSprite;

    private Sprite currentHoverSprite; //hover background
    private Sprite currentClickedSprite;
    private Sprite oldSprite;

    void Start()
    {
        pb = GetComponent<Button>();
        pb.onClick.AddListener(TaskOnClick);
        oldSprite = pb.image.sprite;

        if(GameBoardData.IsAlien)
        {
            isAlien = true;
        }

        if(isAlien)
        {
            currentHoverSprite = hoverSprite[1];
            currentClickedSprite = clickedSprite[1];
        }
        else
        {
            currentHoverSprite = hoverSprite[0];
            currentClickedSprite = clickedSprite[0];
        }
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
