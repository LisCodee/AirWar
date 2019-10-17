using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 路线类
/// </summary>
public class WayLine1
{
    public Vector3[] Points { get; set; }
    public bool IsUsable { get;set; }

    public WayLine1(int wayPointCount)
    {
        Points = new Vector3[wayPointCount];
        IsUsable = true;
    }
}
