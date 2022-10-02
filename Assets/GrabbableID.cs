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
        consumed
    }

    private itemState state = itemState.untouched;

    private void Start()
    {
        transform.parent = null;
        DontDestroyOnLoad(gameObject);

        spawnPosition = transform.position;
    }

    [SerializeField] private SpriteRenderer itemSprite;
    [SerializeField] private SpriteRenderer graveSprite;

    [SerializeField] private InventoryManager.ItemTypes id;
    public InventoryManager.ItemTypes ID => id;

    public Sprite Sprite => itemSprite.sprite;

    public bool CanPickUp => ((state != itemState.equipped) && (state != itemState.consumed));

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
                    ResetPosition();
                    break;
                }
            case itemState.equipped:
                {
                    transform.position = PlayerController.Instance.transform.position;
                    state = itemState.inGrave;

                    itemSprite.enabled = false;
                    graveSprite.enabled = true;

                    break;
                }
            case itemState.inGrave:
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

        itemSprite.enabled = true;
        graveSprite.enabled = false;

        state = itemState.untouched;
    }

    public void PickedUp()
    {
        itemSprite.enabled = false;
        graveSprite.enabled = false;

        ChangeState(itemState.equipped);
    }

    public void PutDown(Vector3 newPos)
    {
        itemSprite.enabled = true;
        graveSprite.enabled = false;

        ChangeState(itemState.moved);

        transform.position = newPos;
    }

    public void Consumed()
    {
        itemSprite.enabled = false;
        graveSprite.enabled = false;

        ChangeState(itemState.consumed);
    }
}
