
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuSushi : MonoBehaviour
{
    public float speed = 1f;
    private float positionOnScreen = 0f;

    void Update()
    {
        positionOnScreen += speed * Time.deltaTime;
        if (positionOnScreen > Screen.height)
        {
            positionOnScreen = 0f;
        }
        transform.position = new Vector3(0, positionOnScreen, 0);
    }

    void Start()
    {
        //create a square that goes from the top to the bottom of the screen indefintely
        GameObject square = new GameObject("StartMenuSushi");
        square.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = square.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1));
        spriteRenderer.color = Color.white;
        RectTransform rt = square.transform as RectTransform;
        rt.localScale = new Vector3(Screen.height / (float)Screen.width, 1, 1);
        rt.anchoredPosition = new Vector2(0, 0.5f);
        rt.position = new Vector3(0, Screen.height / 2, 0);
    }
}
