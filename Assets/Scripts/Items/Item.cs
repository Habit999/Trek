using UnityEngine;

public class Item : MonoBehaviour, ICollectable, IInteractable
{
    public int MaxStackSize;

    public void Collect()
    {
        gameObject.SetActive(false);
    }

    public void Interact(PlayerController interactor)
    {
        interactor.Inventory.AddItem(this);
        Collect();
    }

    public virtual void UseItem()
    {
        // Default Item Use
    }
}
