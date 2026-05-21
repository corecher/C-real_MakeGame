using UnityEngine;

public class EnemyState : MonoBehaviour 
{
    public int hp=10;
    public int damage=10;
    public void GetDamage(int damage)
    {
        hp-=damage;
        if(hp<=0)
        {
            Destroy(gameObject);
        }
    }
}
