using UnityEngine;

[CreateAssetMenu(fileName = "NewModelPartOption", menuName = "Character/Model Part Option")]
public class ModelPartOption : ScriptableObject
{
    public ModelOption associatedModel;
    public ModelPartType modelPartType;
    public string partName;
    public GameObject partPrefab;
}

public enum ModelPartType
{
    Ear,
    Eye,
    Tail,
    Horn
};