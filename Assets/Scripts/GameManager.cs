using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string GimmeAsciiString;

    public ScriptableObject[] CharacterModelAssets;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    [ContextMenu("Gimme Ascii")]
    private void GimmeAscii()
    {
        string value = "Hai :3";
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        foreach (var b in bytes)
        {
            Debug.Log(b);
        }
    }
}
