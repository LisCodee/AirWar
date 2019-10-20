using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public float HP = 1000;
    public float maxHP = 1000;

    //玩家头部位置
    public Transform headTF;

    public void Damage(float amount)
    {
        HP -= amount;
        Debug.Log("受伤");
        if(HP <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        print("玩家死亡");
    }
}
