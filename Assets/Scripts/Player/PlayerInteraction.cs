using System;
using System.Text;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // References
    private PlayerController player;

    // Variables
    public event Action OnStartObserving;
    public event Action OnStopObserving;

    [SerializeField] private float interactionDistance;

    private RaycastHit hitInfo;

    private bool observing;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    public void TryInteract()
    {
        if (observing)
        {
            if(hitInfo.collider.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactable.Interact(player);
            }
        }
    }

    public void Observe()
    {
        bool rayDidHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, interactionDistance);

        if (observing && !rayDidHit)
        {
            observing = false;
            OnStopObserving?.Invoke();
        }
        else if (!observing && rayDidHit)
        {
            observing = true;
            OnStartObserving?.Invoke();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Camera.main != null)
        {
            if (observing)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(Camera.main.transform.position, hitInfo.point);
                Gizmos.DrawWireSphere(hitInfo.point, 0.2f);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionDistance);
            }
        }
    }
#endif
}
