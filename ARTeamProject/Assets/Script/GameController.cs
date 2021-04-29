using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{

    public GameOverScreen GameOverScreen;
    // Start is called before the first frame update
    int maxplatform = 0;

    public void GameOver() //라이프가 0이면 이 게임 오버 function 불러오면 됨.
    {
        GameOverScreen.Setup(maxplatform);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
