using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 丧尸生成器
/// </summary>
public class ZombieSpawn : MonoBehaviour
{
    /// <summary>
    /// 需要创建的丧尸预制件
    /// </summary>
    public GameObject[] zombieType;
    /// <summary>
    /// 最大生成丧尸数
    /// </summary>
    public int maxCount = 5;
    /// <summary>
    /// 同时创建的丧尸数
    /// </summary>
    public int startCount = 2;
    /// <summary>
    /// 当前已经生成的
    /// </summary>
    private int spawnedCount = 0;       
    public float maxDelytime = 5;
    public void GenerateZombie()
    {
        //延时加载
        Invoke("CreateZombie", Random.Range(1,maxDelytime));
    }
    private void CreateZombie()
    {
        WayLine1[] usableWayLines = SelectUsableLine();             //所有可用路线
        Debug.Log("可用路线：" + usableWayLines.Length + usableWayLines[0]);
        WayLine1 line = usableWayLines[Random.Range(0, usableWayLines.Length)];     //随机选取一条路线
        GameObject go = Instantiate(zombieType[Random.Range(0, zombieType.Length)], line.Points[0], Quaternion.identity);       //随机创建一种丧尸  
        ZombieControl control = go.GetComponent<ZombieControl>();
        control.wayline = line;
        line.IsUsable = false;
        spawnedCount++;
        go.GetComponent<ZombieStatus>().spawn = this;
    }
    private WayLine1[] lines;
    private void Start()
    {
        CreateZombie();
    }
    private void Awake()
    {
        CaculateWayLines();     //预加载路线
    }
    /// <summary>
    /// 加载所有路线，获取路点坐标
    /// </summary>
    private void CaculateWayLines()
    {
        //加载路线信息
        lines = new WayLine1[this.transform.childCount];
        for(int i = 0; i < lines.Length; i++)
        {
            Transform tf = this.transform.GetChild(i);
            lines[i] = new WayLine1(tf.childCount);
            //lines[i].Points = new Vector3[tf.childCount];
            for(int point_index = 0;point_index < lines[i].Points.Length; point_index++)
            {
                lines[i].Points[point_index] = this.transform.GetChild(i).GetChild(point_index).position;
            }
        }
    }
    /// <summary>
    /// 返回所有可用路线数组
    /// </summary>
    /// <returns></returns>
    private WayLine1[] SelectUsableLine()
    {
        List<WayLine1> result = new List<WayLine1>(lines.Length);
        foreach(var item in lines)
        {
            if (item.IsUsable)
                result.Add(item);
        }
        return result.ToArray();
    }
}
