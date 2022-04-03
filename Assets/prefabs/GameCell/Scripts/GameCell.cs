using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell : MonoBehaviour
{
    public Sprite LitSprite;
    public Sprite UnLitSprite;
    [HideInInspector]
    public int XPosition;

    [HideInInspector]
    public int YPosition;

    [HideInInspector]
    public bool NeedsToggle;

    bool state;

SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        NeedsToggle = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //create a State propery (true = lit; false = unlit)
    public bool State
    {
        get { return state; }   // get method

        set
        {
            state = value;
            if (state == true)  //true = lit
                spriteRenderer.sprite = LitSprite;
            else
                spriteRenderer.sprite = UnLitSprite;
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log(XPosition + "," + YPosition);
        NeedsToggle = true;
    }

}
