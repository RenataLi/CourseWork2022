using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _scoresText;

    public void UpdateHealth(string newHealth)
    {
        _healthText.text = newHealth;
    }
    
    public void UpdateScores(string newScores)
    {
        _scoresText.text = newScores;
    }
    
}
