using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }

    public virtual void Interact(PlayerController interactor)
    {
        Debug.Log("Interacted with " + gameObject.name + " by " + interactor.name);
    }
}
