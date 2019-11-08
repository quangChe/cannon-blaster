using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public Object[] levels;
    public TextAsset loadedLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        levels = Resources.LoadAll("levels", typeof(TextAsset));
    }

    public void LoadLevel(int levelNumber)
    {
        loadedLevel = (TextAsset)levels[levelNumber - 1];
    }
}
