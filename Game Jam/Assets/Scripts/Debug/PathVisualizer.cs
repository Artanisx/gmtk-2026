using System;
using System.Collections.Generic;
using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        var points = new List<Vector3>();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            points.Add(child.position);
        }
        
        Gizmos.DrawLineStrip(new ReadOnlySpan<Vector3>(points.ToArray()), true);
    }
}
