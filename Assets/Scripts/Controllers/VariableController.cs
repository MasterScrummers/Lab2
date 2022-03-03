using UnityEngine;

public class VariableController : MonoBehaviour
{
    public int score = 0;
    public int coins = 0;
    public int lives { get; private set; } = 10;

    public void ResetScore()
    {
        score = 0;
    }

    public void DecrementLife()
    {
        lives--;
        if (lives == 0)
        {
            Debug.Log("We're out of lives!");
        }
    }
}
