using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStates : MonoBehaviour
{
    public int marioPower;
    public int score;
    public int coins;
    // Start is called before the first frame update
    void Start()
    {

        marioPower = 0;
        score = 0;
        coins = 0;
    }

    public void marioPowerUp(int powerUp)
    {
        if(marioPower < powerUp)
        {
            PowerUpSequence(powerUp);
        }
        score += 1000;
    }

    void PowerUpSequence(int powerUp)
    {

    }
}
