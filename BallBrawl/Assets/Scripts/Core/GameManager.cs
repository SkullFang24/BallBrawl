using UnityEngine;

public class GameManager : MonoBehaviour
{
    internal static GameManager Instance;
    internal static bool IsGamePlaying { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad( gameObject );
        SetIsGamePlaying(true);
    }

    private void Start()
    {
    }

    public static void SetIsGamePlaying( bool value )
    {
        IsGamePlaying = value;
    }
}
