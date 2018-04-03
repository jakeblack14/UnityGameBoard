using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterButtonBehavior : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private Button pb;

    //public bool isAlien;
    public Sprite hoverSprite;
    public Sprite clickedSprite;

    //private Sprite currentHoverSprite; //hover background
    //private Sprite currentClickedSprite;
    private Sprite oldSprite;

    //private bool isActivated = false;

    void Start()
    {
        pb = GetComponent<Button>();
        pb.onClick.AddListener(TaskOnClick);
        oldSprite = pb.image.sprite; 
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        oldSprite = pb.image.sprite;
        pb.image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pb.image.sprite = oldSprite;
    }

    public void TaskOnClick()
    {
        pb.image.sprite = clickedSprite;
        oldSprite = pb.image.sprite;
    }
}
