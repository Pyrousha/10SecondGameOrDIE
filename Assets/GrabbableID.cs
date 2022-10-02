using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableID : MonoBehaviour
{
    private Vector3 spawnPosition;
    public enum itemState
    {
        untouched,
        moved,
        equipped,
        inGrave,
        buried,
        consumed
    }

    private itemState state = itemState.untouched;

    private void Start()
    {
        transform.parent = null;
        DontDestroyOnLoad(gameObject);

        spawnPosition = transform.position;
    }

    [SerializeField] private InventoryManager.ItemTypes id;
    public InventoryManager.ItemTypes ID => id;

    public void ChangeState(itemState newState)
    {
        state = newState;
    }

    public void OnDeath()
    {
        switch (state)
        {
            case itemState.untouched:
                {
                    //No need to do anything
                    break;
                }
            case itemState.moved:
                {
                    state = itemState.buried;
                    break;
                }
            case itemState.equipped:
                {
                    state = itemState.inGrave;
                    break;
                }
            case itemState.inGrave:
                {
                    ResetPosition();
                    break;
                }
            case itemState.buried:
                {
                    ResetPosition();
                    break;
                }
            case itemState.consumed:
                {
                    ResetPosition();
                    break;
                }
        }
    }

    public void ResetPosition()
    {
        transform.position = spawnPosition;
    }
}
