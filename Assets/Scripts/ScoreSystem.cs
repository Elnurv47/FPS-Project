using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int score = 0;

    [SerializeField] private TextMeshProUGUI scoreText;

    public int Score { get => score; }

    private void Start()
    {
        Zombie.OnZombieKilled += Zombie_OnZombieKilled;
    }

    private void Zombie_OnZombieKilled()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
