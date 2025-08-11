using System.Collections.Generic;
using UnityEngine;

public class Mirror : Interactable
{
    [SerializeField] private GameObject mirrorCamera;
    [SerializeField] private GameObject customisationUI;

    private CharacterCustomisation characterCustomisation;

    private int currentModelIndex = 0;
    private int currentEarIndex = 0;
    private int currentEyeIndex = 0;
    private int currentTailIndex = 0;
    private int currentHornIndex = 0;

    private void Awake()
    {
        characterCustomisation = GetComponent<CharacterCustomisation>();

        mirrorCamera.SetActive(false);
        customisationUI.SetActive(false);
    }

    public override void Interact(PlayerController interactor)
    {
        interactor.DisableMoveInput();
        mirrorCamera.SetActive(true);
        customisationUI.SetActive(true);
    }

    public void CloseMirror()
    {
        mirrorCamera.SetActive(false);
        customisationUI.SetActive(false);
    }

    private void UpdateModel()
    {
        characterCustomisation.SetWholeCharacter(currentModelIndex, currentEarIndex, currentEyeIndex, currentTailIndex, currentHornIndex);
    }

    #region UI Callbacks
    public void NextModel()
    {
        currentModelIndex++;
        if (currentModelIndex >= characterCustomisation.ModelOptions.Count)
            currentModelIndex = 0;
        UpdateModel();
    }
    public void PreviousModel()
    {
        currentModelIndex--;
        if (currentModelIndex < 0)
            currentModelIndex = characterCustomisation.ModelOptions.Count - 1;
        UpdateModel();
    }

    public void NextEar()
    {
        currentEarIndex++;
        if (currentEarIndex >= characterCustomisation.EarOptions.Count)
            currentEarIndex = 0;
        UpdateModel();
    }

    public void PreviousEar()
    {
        currentEarIndex--;
        if (currentEarIndex < 0)
            currentEarIndex = characterCustomisation.EarOptions.Count - 1;
        UpdateModel();
    }

    public void NextEye()
    {
        currentEyeIndex++;
        if (currentEyeIndex >= characterCustomisation.EyeOptions.Count)
            currentEyeIndex = 0;
        UpdateModel();
    }

    public void PreviousEye()
    {
        currentEyeIndex--;
        if (currentEyeIndex < 0)
            currentEyeIndex = characterCustomisation.EyeOptions.Count - 1;
        UpdateModel();
    }

    public void NextTail()
    {
        currentTailIndex++;
        if (currentTailIndex >= characterCustomisation.TailOptions.Count)
            currentTailIndex = 0;
        UpdateModel();
    }

    public void PreviousTail()
    {
        currentTailIndex--;
        if (currentTailIndex < 0)
            currentTailIndex = characterCustomisation.TailOptions.Count - 1;
        UpdateModel();
    }

    public void NextHorn()
    {
        currentHornIndex++;
        if (currentHornIndex >= characterCustomisation.HornOptions.Count)
            currentHornIndex = 0;
        UpdateModel();
    }

    public void PreviousHorn()
    {
        currentHornIndex--;
        if (currentHornIndex < 0)
            currentHornIndex = characterCustomisation.HornOptions.Count - 1;
        UpdateModel();
    }
    #endregion
}
