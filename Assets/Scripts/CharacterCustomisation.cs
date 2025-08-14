using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomisation : MonoBehaviour
{
    [SerializeField] private Transform modelSlot;
    [SerializeField] private Transform earSlot;
    [SerializeField] private Transform eyeSlot;
    [SerializeField] private Transform tailSlot;
    [SerializeField] private Transform hornSlot;

    private GameObject currentModel;
    private GameObject currentEar;
    private GameObject currentEye;
    private GameObject currentTail;
    private GameObject currentHorn;

    [HideInInspector] public int CurrentModelIndex = 0;
    [HideInInspector] public int CurrentEarIndex = 0;
    [HideInInspector] public int CurrentEyeIndex = 0;
    [HideInInspector] public int CurrentTailIndex = 0;
    [HideInInspector] public int CurrentHornIndex = 0;

    [HideInInspector] public List<ModelOption> ModelOptions = new List<ModelOption>();
    public List<ModelParts> PartOptions = new List<ModelParts>();

    public void LoadModelData()
    {
        ModelOptions.Clear();
        PartOptions.Clear();
        for (int i = 0; i < PartOptions.Count; i++)
        {
            PartOptions[i].EarOptions.Clear();
            PartOptions[i].EyeOptions.Clear();
            PartOptions[i].TailOptions.Clear();
            PartOptions[i].HornOptions.Clear();
        }

        foreach (var modelAsset in GameManager.Instance.CharacterModelAssets)
        {
            if (modelAsset is ModelOption model)
            {
                ModelOptions.Add(model);
                PartOptions.Add(new ModelParts());
            }
        }

        foreach (var modelPartAsset in GameManager.Instance.CharacterModelAssets)
        {
            if (modelPartAsset is ModelPartOption part)
            {
                for (int i = 0; i < ModelOptions.Count; i++)
                {
                    if (part.associatedModel == ModelOptions[i])
                    {
                        if (part.modelPartType == ModelPartType.Ear)
                            PartOptions[i].EarOptions.Add(part);
                        else if (part.modelPartType == ModelPartType.Eye)
                            PartOptions[i].EyeOptions.Add(part);
                        else if (part.modelPartType == ModelPartType.Tail)
                            PartOptions[i].TailOptions.Add(part);
                        else if (part.modelPartType == ModelPartType.Horn)
                            PartOptions[i].HornOptions.Add(part);
                        break;
                    }
                }
            }
        }
    }

    public void SetModel(GameObject modelPrefab)
    {
        if (currentModel != null)
            Destroy(currentModel);
        currentModel = Instantiate(modelPrefab, modelSlot.position, modelSlot.rotation, modelSlot);
    }

    public void SetEar(GameObject earPrefab)
    {
        if (currentEar != null)
            Destroy(currentEar);
        currentEar = Instantiate(earPrefab, earSlot.position, earSlot.rotation, earSlot);
    }

    public void SetEye(GameObject eyePrefab)
    {
        if (currentEye != null)
            Destroy(currentEye);
        currentEye = Instantiate(eyePrefab, eyeSlot.position, eyeSlot.rotation, eyeSlot);
    }

    public void SetTail(GameObject tailPrefab)
    {
        if (currentTail != null)
            Destroy(currentTail);
        currentTail = Instantiate(tailPrefab, tailSlot.position, tailSlot.rotation, tailSlot);
    }

    public void SetHorn(GameObject hornPrefab)
    {
        if (currentHorn != null)
            Destroy(currentHorn);
        currentHorn = Instantiate(hornPrefab, hornSlot.position, hornSlot.rotation, hornSlot);
    }

    public void SetWholeCharacter(int modelIndex, int earIndex, int eyeIndex, int tailIndex, int hornIndex)
    {
        CurrentModelIndex = modelIndex;
        CurrentEarIndex = earIndex;
        CurrentEyeIndex = eyeIndex;
        CurrentTailIndex = tailIndex;
        CurrentHornIndex = hornIndex;
        UpdateModel();
    }

    public void UpdateModel()
    {
        SetModel(ModelOptions[CurrentModelIndex].modelPrefab);
        SetEar(PartOptions[CurrentModelIndex].EarOptions[CurrentEarIndex].partPrefab);
        SetEye(PartOptions[CurrentModelIndex].EyeOptions[CurrentEyeIndex].partPrefab);
        SetTail(PartOptions[CurrentModelIndex].TailOptions[CurrentTailIndex].partPrefab);
        SetHorn(PartOptions[CurrentModelIndex].HornOptions[CurrentHornIndex].partPrefab);
    }
}

public class ModelParts
{
    [HideInInspector] public List<ModelPartOption> EarOptions = new List<ModelPartOption>();
    [HideInInspector] public List<ModelPartOption> EyeOptions = new List<ModelPartOption>();
    [HideInInspector] public List<ModelPartOption> TailOptions = new List<ModelPartOption>();
    [HideInInspector] public List<ModelPartOption> HornOptions = new List<ModelPartOption>();
}
