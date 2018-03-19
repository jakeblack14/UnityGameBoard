using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterButtonBehavior : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private Button pb;
    public Sprite newSprite;
    public Sprite clickedSprite;
    private Sprite oldSprite;

    void Start()
    {
        pb = GetComponent<Button>();
        pb.onClick.AddListener(TaskOnClick);
        oldSprite = pb.image.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        oldSprite = pb.image.sprite;
        pb.image.sprite = newSprite;
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
