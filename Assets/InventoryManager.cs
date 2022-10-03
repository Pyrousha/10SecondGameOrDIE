using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private Transform playerInteractTransform;
    public Vector3 PlayerInteractPos => playerInteractTransform.position;
    [SerializeField] private Image inventorySlotImage;
    [SerializeField] private LayerMask grabbaleLayer;
    [SerializeField] private LayerMask interactLayer;
    private GrabbableID currEquippedItem;
    public GrabbableID CurrEquippedItem => currEquippedItem;
    public void SetEquippedItem(GrabbableID newItem)
    {
        currEquippedItem = newItem;
    }

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
            if (TryInteract())
                return;

            TryGrab();
        }

        if (InputHandler.Instance.Interact.down)
        {
            TryInteract();
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

    public void TryGrab()
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
            EquipItem(hitItem.GetComponent<GrabbableID>());
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

    public void EquipItem(GrabbableID newItem)
    {
        currEquippedItem = newItem; 

        if(currEquippedItem == null)
        {
            inventorySlotImage.color = Color.clear;
            return;
        }

        inventorySlotImage.sprite = currEquippedItem.Sprite;
        inventorySlotImage.color = Color.white;

        currEquippedItem.PickedUp();
    }

    private bool TryInteract()
    {
        bool toReturn = false;

        Collider2D[] hitInteract = Physics2D.OverlapCircleAll(playerInteractTransform.position, 0.25f, interactLayer);
        for(int i = 0; i< hitInteract.Length; i++)
        { 
            ChestInteractable cInteract = hitInteract[i].GetComponent<ChestInteractable>();
            if(cInteract!= null)
            {
                cInteract.OnInteracted();
                toReturn = true;
                continue;
            }

            if (currEquippedItem == null)
                continue;

            hitInteract[i].GetComponent<Interactable>().OnInteracted(currEquippedItem.ID);
            toReturn = true;
        }

        return toReturn;
    }
}

