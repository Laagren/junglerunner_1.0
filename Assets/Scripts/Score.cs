using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score;
    
    public Text scoreText;
    private scri coin;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.isTrigger && other.gameObject.tag == "Coin")
        {
            
        }

        if (other.tag == "Coin")
        {
            score += 10;
            scoreText.text = score.ToString();
            //coin = other.GetComponent<scri>();
            coin = other.GetComponent<scri>();
            coin.coinHit = true;
            //coin.Coin
            //other.GetComponents<CoinScript>();
            //Destroy(other.gameObject);
            //Debug.Log("scoreee");
        }
    }

}
