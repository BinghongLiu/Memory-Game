using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainImageScript : MonoBehaviour
{
    [SerializeField] private GameObject cover;
    [SerializeField] private GameControllerScript gameController;
    [SerializeField] private GameObject theCard;

    public void OnMouseDown()
    {
        if (cover.activeSelf && gameController.canFlip)
        {
            cover.SetActive(false);
            gameController.cardFlipped(this);
        }
    }

    private int _spriteId;
    public int spriteId
    {
        get { return _spriteId; }
    }

    public void ChangeSprite(int id,Sprite image)
    {
        _spriteId = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void Close()
    {
        cover.SetActive(true);
    }

    public void Remove()
    {
        theCard.SetActive(false);
    }
}
