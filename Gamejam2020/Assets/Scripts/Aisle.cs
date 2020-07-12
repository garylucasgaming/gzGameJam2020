using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aisle : MonoBehaviour
{
    public bool demolished = false;

    SpriteRenderer spriteRenderer;
    public Color demolitionWarningColor;

    public float demolitionTimeSeconds;

    public Sprite demolishedSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Demolish()
    {
        //Warn player that the shelf is being demolished
        spriteRenderer.color = demolitionWarningColor;

        yield return new WaitForSeconds(demolitionTimeSeconds);

        spriteRenderer.sprite = demolishedSprite;
        spriteRenderer.color = Color.white;
        yield return null;
    }
}
