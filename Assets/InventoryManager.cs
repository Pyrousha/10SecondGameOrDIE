using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private Transform playerInteractTransform;
    [SerializeField] private Image inventorySlotImage;
    [SerializeField] private LayerMask grabbaleLayer;
    [SerializeField] private LayerMask interactLayer;
    private GrabbableID currEquippedItem;

    public enum ItemTypes
    {
        Sword,
        WateringCan,
        Fruit
    }

    private void Update()
    {
        if (InputHandler.Instance.Grab.down)
        {
            Collider2D hitItem = Physics2D.OverlapCircle(playerInteractTransform.position, 0.5f, grabbaleLayer);
            if ((hitItem != null) && (hitItem.GetComponent<GrabbableID>().CanPickUp)) //Hit an interactable
            {
                if (currEquippedItem != null)
                {
                    //put equipped item back on ground
                    currEquippedItem.PutDown(playerInteractTransform.position);
                }

                //pick up new item
                currEquippedItem = hitItem.GetComponent<GrabbableID>();
                inventorySlotImage.sprite = currEquippedItem.Sprite;
                inventorySlotImage.color = Color.white;

                currEquippedItem.PickedUp();
            }
            else
            {
                if (currEquippedItem != null) //put equipped on ground
                {
                    //put equipped item back on ground
                    currEquippedItem.PutDown(playerInteractTransform.position);

                    inventorySlotImage.color = Color.clear;

                    currEquippedItem = null;
                }
            }
        }

        if (InputHandler.Instance.Interact.down)
        {
            Collider2D hitInteract = Physics2D.OverlapCircle(playerInteractTransform.position, 0.5f, interactLayer);
            if (hitInteract != null) //Hit an interactable
            {
                hitInteract.GetComponent<Interactable>().OnInteracted(currEquippedItem.ID);
            }
        }
    }

    public void ConsumeItem()
    {
        //put equipped item back on ground
        //currEquippedItem.transform.position = playerInteractTransform.position;
        //currEquippedItem.SetActive(true);

        inventorySlotImage.color = Color.clear;

        currEquippedItem.Consumed();

        currEquippedItem = null;
    }
}
