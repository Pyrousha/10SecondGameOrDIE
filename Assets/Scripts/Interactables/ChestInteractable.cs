using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : MonoBehaviour
{
    private GrabbableID storedItem;
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnInteracted()
    {
        if(storedItem != null)
        {
            if(InventoryManager.Instance.CurrEquippedItem == null)
            {
                //take item out
                InventoryManager.Instance.EquipItem(storedItem);
                storedItem = null;

                anim.SetTrigger("Open");
            }
            else
            {
                //Make item invisible and take out of inventory
                GrabbableID currEquipped = InventoryManager.Instance.CurrEquippedItem;
                currEquipped.PutInChest(InventoryManager.Instance.PlayerInteractPos);

                //Equip the item in chest
                InventoryManager.Instance.EquipItem(storedItem);

                //Place prev held item in chest
                storedItem = currEquipped;

                anim.SetTrigger("Open");
                anim.SetTrigger("Close");
            }
        }
        else
        {
            //Make item invisible and take out of inventory
            GrabbableID currEquipped = InventoryManager.Instance.CurrEquippedItem;
            if (currEquipped == null)
                return;

            currEquipped.PutInChest(InventoryManager.Instance.PlayerInteractPos);

            //Equip the item in chest
            InventoryManager.Instance.EquipItem(null);

            //Place prev held item in chest
            storedItem = currEquipped;

            anim.SetTrigger("Close");
        }
    }
}
