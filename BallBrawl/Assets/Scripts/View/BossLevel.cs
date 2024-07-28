using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossLevel : MonoBehaviour
{
    public static BossLevel instance;

    public TextMeshProUGUI levelText;
    public int bossWave;

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
        bossWave = 0;
        UpdateLevelNo();
    }

    public void addLevelNo(int wavenumber)
    {
        bossWave += wavenumber;
        UpdateLevelNo();
    }

    private void UpdateLevelNo()
    {
        levelText.text = "Wave: " + bossWave.ToString();
    }
}
