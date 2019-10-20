using UnityEngine;
using System.Collections;

/// <summary>
/// 子弹
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 攻击力
    /// </summary>
    [HideInInspector]
    public float atk;
    /// <summary>
    /// 射线out对象
    /// </summary>
    [HideInInspector]
    public RaycastHit hit;
    /// <summary>
    /// 攻击有效距离
    /// </summary>
    [HideInInspector]
    public float atkDistance;
    /// <summary>
    /// 射线层级
    /// </summary>
    public LayerMask layer;

    public void Init(float atk,float distance)
    {
        this.atk = atk;
        atkDistance = distance;
        moveSpeed = 100;
        CalculateTargetPoint();
    }

    public Vector3 targetPos;
    //通过射线计算击中物体
    private void CalculateTargetPoint()
    { 

        if (Physics.Raycast(transform.position, transform.forward, out hit, atkDistance, layer))
        {
            targetPos = hit.point;
            Debug.Log("击中物体");
        }
        else
        {
            targetPos = this.transform.position + this.transform.forward * atkDistance;
            Debug.Log("没有击中");
        } 
    }

    private void Update()
    { 
        Movement(); 
        if ((transform.position - targetPos).sqrMagnitude < 0.1f)
        { //到达目标点
            //销毁子弹
            Destroy(this.gameObject);
            //生成特效
            GenerateContactEffect();
        }
    }
    /// <summary>
    /// 子弹飞行速度
    /// </summary>
    public float moveSpeed = 100;
    private void Movement()
    { 
        this.transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }
    //生成接触特效
    private void GenerateContactEffect()
    { // Resources/ContactEffects/xxxx
        //根据  受击物体标签（ hit.collider.tag） 创建相应特效
        //规定：读取的资源必须放置在 Resources 文件夹内
        //GameObject go = Resources.Load<GameObject>("ContactEffects/xx");

        if (hit.collider == null) return;

        //特效命名规则：Effects+标签
        string prefabName = "ContactEffects/Effects" + hit.collider.tag;
        GameObject go = Resources.Load<GameObject>(prefabName);
        if (go)
        {
            Debug.Log("击中" + hit.collider.tag);
            Instantiate(go, targetPos + hit.normal *0.01f, Quaternion.LookRotation(hit.normal));
        }
    }
}
