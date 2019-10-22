using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatus : MonoBehaviour
{
    /// <summary>
    /// 当前血量
    /// </summary>
    public float currentHP = 100;
    /// <summary>
    /// 最大血量
    /// </summary>
    public float maxHP = 100;

    public void Damage(float amount)
    {
        //如果敌人已经死亡 则退出(防止虐尸)
        //if (currentHP <= 0) return;

        currentHP -= amount;

        if (currentHP <= 0)
            Death();
    }

    /// <summary>
    /// 死亡延迟时间
    /// </summary>
    public float deathDelay = 10;

    //敌人生成器引用  敌人创建时由生成器传递
    public ZombieSpawn spawn;
    /// <summary>
    /// 死亡
    /// </summary>
    public void Death()
    {
        //销毁当前游戏物体
        Destroy(gameObject, deathDelay);

        //播放动画
        var anim = GetComponent<Animator>();
        anim.SetTrigger("death");

        //修改状态
        GetComponent<ZombieAI>().state = ZombieAI.State.Death;

        //修改路线状态
        GetComponent<ZombieControl>().wayline.IsUsable = true;

        //需要再生成一个敌人
        spawn.GenerateZombie();
    }
}
