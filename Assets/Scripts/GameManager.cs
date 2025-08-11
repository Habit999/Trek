using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
}
