using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private Transform playerInteractTransform;
    [SerializeField] private Image inventorySlotImage;
    private GameObject currEquippedItem;

    private void Update()
    {
        if(InputHandler.Instance.Grab.down)
        {
            //Collider2D hitItem =  Physics2D.OverlapCircle(playerInteractTransform.position, 0.5f)
        }
    }
}
