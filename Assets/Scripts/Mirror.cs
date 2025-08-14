using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mirror : Interactable
{
    [SerializeField] private GameObject mirrorCamera;
    [SerializeField] private GameObject customisationUI;
    [SerializeField] private Transform model;
    [SerializeField] private float rotationSpeed;

    [Space(10)]

    [SerializeField] private TextMeshProUGUI modelIndexText;
    [SerializeField] private TextMeshProUGUI earIndexText;
    [SerializeField] private TextMeshProUGUI eyeIndexText;
    [SerializeField] private TextMeshProUGUI tailIndexText;
    [SerializeField] private TextMeshProUGUI hornIndexText;

    private PlayerController playerController;

    private CharacterCustomisation characterCustomisation;

    private float modelRotation;

    private void Awake()
    {
        characterCustomisation = GetComponent<CharacterCustomisation>();

        mirrorCamera.SetActive(false);
        customisationUI.SetActive(false);
    }

    private void Start()
    {
        characterCustomisation.LoadModelData();

        modelRotation = model.rotation.eulerAngles.y;
    }

    public override void Interact(PlayerController interactor)
    {
        playerController = interactor;
        playerController.DisableMoveInput();
        playerController.SetMainCameraActive(false);
        mirrorCamera.SetActive(true);
        customisationUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetModel();
        UpdateIndexUI();
    }

    public void ApplyChanges()
    {
        playerController.CharacterManager.SetNewPlayerModel(
            characterCustomisation.CurrentModelIndex,
            characterCustomisation.CurrentEarIndex,
            characterCustomisation.CurrentEyeIndex,
            characterCustomisation.CurrentTailIndex,
            characterCustomisation.CurrentHornIndex
        );
        CloseMirror();
    }

    public void CloseMirror()
    {
        playerController.EnableMoveInput();
        mirrorCamera.SetActive(false);
        playerController.SetMainCameraActive(true);
        customisationUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SetModel()
    {
        characterCustomisation.SetWholeCharacter(
            characterCustomisation.CurrentModelIndex, 
            characterCustomisation.CurrentEarIndex, 
            characterCustomisation.CurrentEyeIndex, 
            characterCustomisation.CurrentTailIndex, 
            characterCustomisation.CurrentHornIndex
        );
    }

    private void ResetPartIndexes()
    {
        characterCustomisation.CurrentEarIndex = 0;
        characterCustomisation.CurrentEyeIndex = 0;
        characterCustomisation.CurrentTailIndex = 0;
        characterCustomisation.CurrentHornIndex = 0;
    }

    #region UI Callbacks

    public void RotateModelLeft()
    {
        modelRotation += rotationSpeed * Time.deltaTime * 100;
        model.rotation = Quaternion.Euler(0, modelRotation, 0);
    }

    public void RotateModelRight()
    {
        modelRotation -= rotationSpeed * Time.deltaTime * 100;
        model.rotation = Quaternion.Euler(0, modelRotation, 0);
    }

    private void UpdateIndexUI()
    {
        modelIndexText.text = (characterCustomisation.CurrentModelIndex + 1).ToString();
        earIndexText.text = (characterCustomisation.CurrentEarIndex + 1).ToString();
        eyeIndexText.text = (characterCustomisation.CurrentEyeIndex + 1).ToString();
        tailIndexText.text = (characterCustomisation.CurrentTailIndex + 1).ToString();
        hornIndexText.text = (characterCustomisation.CurrentHornIndex + 1).ToString();
    }

    public void NextModel()
    {
        characterCustomisation.CurrentModelIndex++;
        if (characterCustomisation.CurrentModelIndex >= characterCustomisation.ModelOptions.Count)
            characterCustomisation.CurrentModelIndex = 0;

        ResetPartIndexes();
        SetModel();
        UpdateIndexUI();
    }
    public void PreviousModel()
    {
        characterCustomisation.CurrentModelIndex--;
        if (characterCustomisation.CurrentModelIndex < 0)
            characterCustomisation.CurrentModelIndex = characterCustomisation.ModelOptions.Count - 1;

        ResetPartIndexes();
        SetModel();
        UpdateIndexUI();
    }

    public void NextEar()
    {
        characterCustomisation.CurrentEarIndex++;
        if (characterCustomisation.CurrentEarIndex >= characterCustomisation.PartOptions[characterCustomisation.CurrentModelIndex].EarOptions.Count)
            characterCustomisation.CurrentEarIndex = 0;
        SetModel();
        UpdateIndexUI();
    }

    public void PreviousEar()
    {
        characterCustomisation.CurrentEarIndex--;
        if (characterCustomisation.CurrentEarIndex < 0)
            characterCustomisation.CurrentEarIndex = characterCustomisation.PartOptions[characterCustomisation.CurrentModelIndex].EarOptions.Count - 1;
        SetModel();
        UpdateIndexUI();
    }

    public void NextEye()
    {
        characterCustomisation.CurrentEyeIndex++;
        if (characterCustomisation.CurrentEyeIndex >= characterCustomisation.PartOptions[characterCustomisation.CurrentModelIndex].EyeOptions.Count)
            characterCustomisation.CurrentEyeIndex = 0;
        SetModel();
        UpdateIndexUI();
    }

    public void PreviousEye()
    {
        characterCustomisation.CurrentEyeIndex--;
        if (characterCustomisation.CurrentEyeIndex < 0)
            characterCustomisation.CurrentEyeIndex = characterCustomisation.PartOptions[characterCustomisation.CurrentModelIndex].EyeOptions.Count - 1;
        SetModel();
        UpdateIndexUI();
    }

    public void NextTail()
    {
        characterCustomisation.CurrentTailIndex++;
        if (characterCustomisation.CurrentTailIndex >= characterCustomisation.PartOptions[characterCustomisation.CurrentModelIndex].TailOptions.Count)
            characterCustomisation.CurrentTailIndex = 0;
        SetModel();
        UpdateIndexUI();
    }

    public void PreviousTail()
    {
        characterCustomisation.CurrentTailIndex--;
        if (characterCustomisation.CurrentTailIndex < 0)
            characterCustomisation.CurrentTailIndex = characterCustomisation.PartOptions[characterCustomisation.CurrentModelIndex].TailOptions.Count - 1;
        SetModel();
        UpdateIndexUI();
    }

    public void NextHorn()
    {
        characterCustomisation.CurrentHornIndex++;
        if (characterCustomisation.CurrentHornIndex >= characterCustomisation.PartOptions[characterCustomisation.CurrentModelIndex].HornOptions.Count)
            characterCustomisation.CurrentHornIndex = 0;
        SetModel();
        UpdateIndexUI();
    }

    public void PreviousHorn()
    {
        characterCustomisation.CurrentHornIndex--;
        if (characterCustomisation.CurrentHornIndex < 0)
            characterCustomisation.CurrentHornIndex = characterCustomisation.PartOptions[characterCustomisation.CurrentModelIndex].HornOptions.Count - 1;
        SetModel();
        UpdateIndexUI();
    }
    #endregion
}
