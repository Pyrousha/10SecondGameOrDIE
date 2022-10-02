using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MimicSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprRend;
    [SerializeField] private Image img;

    // Update is called once per frame
    void Update()
    {
        img.sprite = sprRend.sprite;
        transform.localScale = sprRend.transform.localScale;
    }
}
