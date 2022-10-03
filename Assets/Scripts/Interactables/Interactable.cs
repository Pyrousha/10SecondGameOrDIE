using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private InventoryManager.ItemTypes itemNeeded;
    [SerializeField] private UnityEvent onInteractSuccess;
    private bool completed = false;
    [SerializeField] private bool consumeItemOnUse;

    public void OnInteracted(InventoryManager.ItemTypes item)
    {
        if(item == itemNeeded)
        {
            if(completed == false)
            {
                onInteractSuccess.Invoke();
                completed = true;
                if (consumeItemOnUse)
                    InventoryManager.Instance.ConsumeItem();
            }
        }
    }
}
