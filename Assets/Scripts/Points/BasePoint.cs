using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePoint : MonoBehaviour
{
    public List<GameObject> NextPoints = new List<GameObject>();
    public List<GameObject> PrevPoints = new List<GameObject>();
    public int CurrentId = ++m_lastCellId;

    static private int m_lastCellId = 0;

    private bool m_acceptable = false;

    // Start is called before the first frame update
    void Start()
    {
        SetAceptable(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsAcceptable()
    {
        return m_acceptable;
    }

    public void SetAceptable(bool acceplable)
    {
        m_acceptable = acceplable;
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        Color color;
        if (m_acceptable)
        {
            color = new Color(0, 255, 0);
        }
        else
        {
            color = new Color(255, 0, 0);
        }
        spr.color = color;
    }

    public bool FindWay(int toPointID, int stepsLimitation, List<GameObject> outputWay)
    {
        if (stepsLimitation == 0)
        {
            return false;
        }
        outputWay.Add(gameObject);
        if(toPointID == CurrentId && stepsLimitation == 1)
        {
            return true;
        }
        else if (stepsLimitation < 2)
        {
            outputWay.Clear();
            return false;
        }
        for (int i = 0; i < NextPoints.Count; i++)
        {
            List<GameObject> tempPointList = new List<GameObject>();
            if (NextPoints[i].GetComponent<BasePoint>().FindWay(toPointID, stepsLimitation - 1, tempPointList))
            {
                outputWay.AddRange(tempPointList);
                return true;
            }
        }
        return false;
    }

    static List<GameObject> m_highlightedPoints = new List<GameObject>();
    public void HighlightAceptableForSteps(int steps)
    {
        RemoveHightlightAceptable();
        FindPointByDistance(steps, m_highlightedPoints);
        foreach(var point in m_highlightedPoints)
        {
            point.GetComponent<BasePoint>().SetAceptable(true);
        }
    }

    public void RemoveHightlightAceptable()
    {
        foreach (var point in m_highlightedPoints)
        {
            point.GetComponent<BasePoint>().SetAceptable(false);
        }
        m_highlightedPoints.Clear();
    }

    public void FindPointByDistance(int steps, List<GameObject> output)
    {
        if(steps == 0)
        {
            output.Add(gameObject);
            return;
        }
        else
        {
            foreach(var chil in NextPoints)
            {
                chil.GetComponent<BasePoint>().FindPointByDistance(steps - 1, output);
            }
        }
    }
}
