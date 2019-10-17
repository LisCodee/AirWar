using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieControl))]
[RequireComponent(typeof(ZombieStatus))]
public class ZombieAI : MonoBehaviour
{
    /// <summary>
    /// 敌人状态
    /// </summary>
    public enum State
    {
        /// <summary>
        /// 攻击状态
        /// </summary>
        Attack,
        /// <summary>
        /// 死亡状态
        /// </summary>
        Death,
        /// <summary>
        /// 寻路状态
        /// </summary>
        Pathfinding
    }
    private Animator anim;              //加载丧尸动画
    private ZombieControl control;  //加载丧尸马达
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        control = GetComponent<ZombieControl>();
    }
    public State state = State.Pathfinding;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Pathfinding:
                Pathfinding();
         
                break;
            case State.Attack:
                Attack();
                
                break;
        }
    }
    private float atkTime;
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float atkInterval = 3;

    /// <summary>
    /// 攻击延迟时间
    /// </summary>
    public float delay = 0.3f;

    private void Attack()
    {
        //朝向玩家
        control.LookRotation(PlayerStatus.Instance.transform.position);
        //限制攻击频率
        //播放攻动画
        if (atkTime <= Time.time)
        {
            anim.SetBool("hit", true);
            //希望动画播放到某一时刻再执行攻击行为
            Invoke("Hit", delay);
            atkTime = Time.time + atkInterval;
        }

        if (!anim.GetBool("hit") && !anim.GetBool("run"))
        {
            //如果其他动画没有播放  再  播放闲置动画
            anim.SetBool("hit", false);
        }
    }
    /// <summary>
    /// 寻路
    /// </summary>
    private void Pathfinding()
    {
        //播放跑步动画
        anim.SetBool("run", true);
        //调用马达寻路功能  如果到达终点，修改状态为 state 攻击
        if (!control.Pathfinding()) state = State.Attack;
    }
    private void Hit()
    {
        anim.SetBool("hit", false);
    }
 
}
