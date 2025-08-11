using UnityEngine;

[CreateAssetMenu(fileName = "NewModelOption", menuName = "Character/Model Option")]
public class ModelOption : ScriptableObject
{
    public string modelName;
    public GameObject modelPrefab;
    public Sprite previewIcon;
}
