using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NextPoint = null;

    public GameObject GetFinalBasePoint()
    {
        BasePoint point = NextPoint.GetComponent<BasePoint>();
        if (point == null)
        {
            return NextPoint.GetComponent<Subpoint>().GetFinalBasePoint();
        }
        else
        {
            return point.gameObject;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
