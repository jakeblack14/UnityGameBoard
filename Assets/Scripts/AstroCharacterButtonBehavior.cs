using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AstroCharacterButtonBehavior : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    private Button b;

    public Sprite currentHoverSprite; //hover background
    public Sprite currentClickedSprite;
    private Sprite oldSprite;

    // Use this for initialization
    void Start () {

        b = GetComponent<Button>();
        b.onClick.AddListener(TaskOnClick);
        oldSprite = b.image.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        oldSprite = b.image.sprite;
        b.image.sprite = currentHoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        b.image.sprite = oldSprite;

    }

    public void TaskOnClick()
    {
        b.image.sprite = currentClickedSprite;
        oldSprite = b.image.sprite;
    }
}
