using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float gameTime=300;
    public Text timer;
    void Update()
    {
        gameTime -= Time.deltaTime;    
        timer.text = "남은 시간 : "+gameTime+"초";
    }
}
