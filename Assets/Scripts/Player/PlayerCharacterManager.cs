using System.IO;
using UnityEngine;

public class PlayerCharacterManager : CharacterCustomisation
{
    private CharacterData characterData;

    private string characterDataPath = Application.dataPath + "/CharacterData.json";

    private void Start()
    {
        LoadModelData();
        LoadCharacterData();
    }

    public void SetNewPlayerModel(int modelIndex, int earIndex, int eyeIndex, int tailIndex, int hornIndex)
    {
        SetWholeCharacter(modelIndex, earIndex, eyeIndex, tailIndex, hornIndex);
        SaveCharacterData();
    }

    #region Save & Load Character Data

    private void LoadCharacterData()
    {
        if (!File.Exists(characterDataPath))
        {
            characterData = new CharacterData();
            SaveCharacterData();
        }
        else
        {
            string json = File.ReadAllText(characterDataPath);
            characterData = JsonUtility.FromJson<CharacterData>(json);
            SetWholeCharacter(characterData.ModelIndex, characterData.EarIndex, characterData.EyeIndex, characterData.TailIndex, characterData.HornIndex);
        }
    }

    private void SaveCharacterData()
    {
        if(File.Exists(characterDataPath))
        {
            File.Delete(characterDataPath);
        }

        characterData.ModelIndex = CurrentModelIndex;
        characterData.EarIndex = CurrentEarIndex;
        characterData.EyeIndex = CurrentEyeIndex;
        characterData.TailIndex = CurrentTailIndex;
        characterData.HornIndex = CurrentHornIndex;
        string json = JsonUtility.ToJson(characterData);
        File.WriteAllText(characterDataPath, json);
    }

    #endregion
}

public class CharacterData
{
    public int ModelIndex = 0;
    public int EarIndex = 0;
    public int EyeIndex = 0;
    public int TailIndex = 0;
    public int HornIndex = 0;
}
