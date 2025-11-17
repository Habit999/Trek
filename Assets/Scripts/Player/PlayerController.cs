using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    private PlayerDeviceInput playerDeviceInput;
    private PlayerMovement playerMovement;
    private PlayerCamera playerCamera;
    private PlayerInteraction playerInteraction;
    [HideInInspector] public PlayerInventory Inventory;
    private PlayerStatManager playerStatManager;
    [HideInInspector] public PlayerCharacterManager CharacterManager;

    private float maxHealth;
    private float health;

    private Camera mainCamera;

    private bool canObserve;

    private void Awake()
    {
        playerDeviceInput = GetComponent<PlayerDeviceInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();
        playerInteraction = GetComponent<PlayerInteraction>();
        Inventory = GetComponent<PlayerInventory>();
        CharacterManager = GetComponent<PlayerCharacterManager>();
        playerStatManager = GetComponent<PlayerStatManager>();

        mainCamera = Camera.main;
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void OnEnable()
    {
        EnableMoveInput();

        playerStatManager.OnVitalityChanged += SetMaxHealth;

        playerInteraction.OnItemCollected += Inventory.AddItem;
    }

    private void OnDisable()
    {
        DisableMoveInput();

        playerStatManager.OnVitalityChanged -= SetMaxHealth;

        playerInteraction.OnItemCollected -= Inventory.AddItem;
    }

    private void Update()
    {
        if (canObserve) playerInteraction.Observe();
    }

    private void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        DisableMoveInput();
        Debug.Log("Player has died.");
    }

    public void SetMainCameraActive(bool active)
    {
        mainCamera.gameObject.SetActive(active);
    }

    public void EnableMoveInput()
    {
        canObserve = true;

        playerDeviceInput.OnMove += playerMovement.Move;
        playerDeviceInput.OnLook += playerCamera.Look;

        playerDeviceInput.OnStartSprint += playerMovement.StartSprint;
        playerDeviceInput.OnStopSprint += playerMovement.StopSprint;

        playerDeviceInput.OnStartJump += playerMovement.StartJump;
        playerDeviceInput.OnStopJump += playerMovement.StopJump;

        playerDeviceInput.OnStartCrouch += playerMovement.StartCrouch;
        playerDeviceInput.OnStopCrouch += playerMovement.StopCrouch;

        playerDeviceInput.OnInteract += playerInteraction.TryInteract;
    }

    public void DisableMoveInput()
    {
        canObserve = false;

        playerDeviceInput.OnMove -= playerMovement.Move;
        playerDeviceInput.OnLook -= playerCamera.Look;

        playerDeviceInput.OnStartSprint -= playerMovement.StartSprint;
        playerDeviceInput.OnStopSprint -= playerMovement.StopSprint;

        playerDeviceInput.OnStartJump -= playerMovement.StartJump;
        playerDeviceInput.OnStopJump -= playerMovement.StopJump;

        playerDeviceInput.OnStartCrouch -= playerMovement.StartCrouch;
        playerDeviceInput.OnStopCrouch -= playerMovement.StopCrouch;

        playerDeviceInput.OnInteract -= playerInteraction.TryInteract;
    }
}
