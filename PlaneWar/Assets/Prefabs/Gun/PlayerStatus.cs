using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { private set; get; }
    private void Awake()
    {
        Instance = this;
    }

    public float HP;
    public float maxHP;

    //玩家头部位置
    public Transform headTF;

    public void Damage(float amount)
    {
        HP -= amount;
    }
}
