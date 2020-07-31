using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public GameObject LinePrefab;
    List<GameObject> _lines = new List<GameObject>();

    

    public void RemoveOldLine()
    {
        foreach (var line in _lines)
        {
            Destroy(line);
        }
        _lines.Clear();
        
    }

    public void CalcLine(Vector3 startPos, Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = new Ray(startPos,direction);
        var line = Physics.Raycast(ray, out hit, 300, 1 << 8 | 1 << 11 | 1 << 12);
        Vector3 hitPos = hit.point;
        if (!line)
        {
            hitPos = startPos + direction * 300;
        }
        DrawLine(startPos, hitPos);
        if (line)
        {
            if (hit.collider.GetComponent<Wall>() != null || hit.collider.GetComponent<Burrow>() != null)
                return;
            CalcLine(hitPos, Vector3.Reflect(direction, hit.normal));
        }
    }

    public void DrawLine(Vector3 startPos, Vector3 endPos)
    {
        var go = Instantiate(LinePrefab, Vector3.zero, Quaternion.identity);
        LineRenderer line = go.GetComponent<LineRenderer>();
        _lines.Add(go);
        line.SetPosition(0,startPos);
        line.SetPosition(1,endPos);
    }
    
    
}
