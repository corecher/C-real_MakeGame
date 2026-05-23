using Unity.Mathematics;
using UnityEngine;

public class EnemyState : MonoBehaviour,IState
{
    public int hp=10;
    public int damage=10;
    public GameObject explosionEffect;
    public void GetDamage(int damage)
    {
        hp-=damage;
        if(hp<=0)
        {
            Instantiate(explosionEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
