using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePoint : MonoBehaviour
{
    public List<GameObject> NextPoints = new List<GameObject>();
    public List<GameObject> PrevPoints = new List<GameObject>();
    public int CurrentId = ++m_lastCellId;

    static private int m_lastCellId = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool FindWay(int toPointID, int stepsLimitation, List<GameObject> outputWay)
    {
        if(stepsLimitation == 0)
        {
            return false;
        }
        outputWay.Add(gameObject);
        if(toPointID == CurrentId)
        {
            return true;
        }
        else if(stepsLimitation < 2)
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
}
