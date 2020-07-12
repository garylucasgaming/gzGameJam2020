using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aisle : MonoBehaviour
{
    public bool demolished = false;

    public Color demolitionWarningColor;
    public Color demolishedColor;

    public float demolitionTimeSeconds;

    public Sprite regularSprite;

    public float itemDeletionRange;



    private void Awake()
    {
    }

    public void ResetVisuals()
    {
        SetColors(Color.white);
    }

    private void DestroyItemsInRange()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, itemDeletionRange);

        foreach(var col in cols)
        {
            if (col.gameObject.GetComponent<Item>() != null)
            {
                Destroy(col.gameObject);
            }
        }
        
    }

    public IEnumerator Demolish()
    {
        //Warn player that the shelf is being demolished
        SetColors(demolitionWarningColor);

        yield return new WaitForSeconds(demolitionTimeSeconds);

        SetColors(demolishedColor);
        demolished = true;

        DestroyItemsInRange();

        yield return null;
    }

    public void SetColors(Color color)
    {
        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in spriteRenderers)
        {
            sr.color = color;
        }
    }
}
