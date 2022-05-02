using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Text _healthText;

    public void UpdateHealth(string newHealth)
    {
        _healthText.text = newHealth;
    }
}
