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

    [HideInInspector] public List<ModelOption> ModelOptions = new List<ModelOption>();
    [HideInInspector] public List<ModelPartOption> EarOptions = new List<ModelPartOption>();
    [HideInInspector] public List<ModelPartOption> EyeOptions = new List<ModelPartOption>();
    [HideInInspector] public List<ModelPartOption> TailOptions = new List<ModelPartOption>();
    [HideInInspector] public List<ModelPartOption> HornOptions = new List<ModelPartOption>();

    private void Start()
    {
        LoadModelData();
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
        SetModel(ModelOptions[modelIndex].modelPrefab);
        SetEar(EarOptions[earIndex].partPrefab);
        SetEye(EyeOptions[eyeIndex].partPrefab);
        SetTail(TailOptions[tailIndex].partPrefab);
        SetHorn(HornOptions[hornIndex].partPrefab);
    }

    private void LoadModelData()
    {
        ModelOptions.Clear();
        EarOptions.Clear();
        EyeOptions.Clear();
        TailOptions.Clear();
        HornOptions.Clear();

        foreach (var modelAsset in GameManager.Instance.CharacterModelAssets)
        {
            if(modelAsset is ModelOption model)
            {
                ModelOptions.Add(model);
            }
        }

        foreach (var modelPartAsset in GameManager.Instance.CharacterModelAssets)
        {
            if (modelPartAsset is ModelPartOption part)
            {
                if (part.modelPartType == ModelPartType.Ear)
                    EarOptions.Add(part);
                else if (part.modelPartType == ModelPartType.Eye)
                    EyeOptions.Add(part);
                else if (part.modelPartType == ModelPartType.Tail)
                    TailOptions.Add(part);
                else if (part.modelPartType == ModelPartType.Horn)
                    HornOptions.Add(part);
            }
        }
    }
}
