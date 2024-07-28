using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI BosslevelText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        BosslevelText.gameObject.SetActive(false);
    }

    public void changeLevelText(int wavenumber)
    {
        levelText.text = "Wave: " + wavenumber;
    }

    

    public void EnableBosslevelText(bool enable)
    {
        BosslevelText.gameObject.SetActive(enable);

    }
}
