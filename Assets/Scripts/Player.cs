using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject CurrentPoint = null;
    public int _TargetID = 0;
    public int _TargetSteps = 0;

    private List<GameObject> m_wayPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_wayPoints.Clear();
        CurrentPoint.GetComponent<BasePoint>().FindWay(_TargetID, _TargetSteps, m_wayPoints);
        Debug.Log(m_wayPoints.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_wayPoints.Count > 0)
        {
            Vector2 pos = transform.position;
            Vector2 targetPos = m_wayPoints[0].transform.position;
            pos = pos + (targetPos - pos).normalized * Time.deltaTime;
            if(Vector2.Distance(pos, targetPos) < 0.1f)
            {
                CurrentPoint = m_wayPoints[0];
                m_wayPoints.Remove(CurrentPoint);
            }
            transform.position = pos;
        }
    }
}
