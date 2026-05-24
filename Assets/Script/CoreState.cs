using UnityEngine;

public class CoreState : MonoBehaviour,IState
{
    public int hp = 100;
    public CoreManager coreManager;
    public void GetDamage(int damage)
    {
        hp-=damage;
        if(hp<=0)
        {   
            coreManager.GameOver(false);
        }
    }
}
