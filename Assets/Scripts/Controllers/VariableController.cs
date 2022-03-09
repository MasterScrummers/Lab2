using UnityEngine;

public class VariableController : MonoBehaviour
{
    public int score = 0;
    public int coins = 0;

    const int max_lives = 10;
    public int lives  = 10;

    public void ResetScore()
    {
        score = 0;
    }

    public int ResetLives()
    {
        lives = max_lives;
        return lives;
    }

    public int DecrementLife()
    {
        lives--;
        if (lives == 0)
        {
            Debug.Log("We're out of lives!");
        }

        return lives;
    }
}
