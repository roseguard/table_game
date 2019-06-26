using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject CurrentPoint = null;
    public int _TargetID = 0;
    public int _TargetSteps = 0;

    private List<GameObject> m_wayPoints = new List<GameObject>();
    private int m_moveOnSteps = 0;
    private State m_currentState = State.ChoosingSteps;
    private Vector2 m_clickPos = Vector2.zero;
    public float PlayerSpeed = 1f;

    public enum State
    {
        Idle,
        ChoosingCard,
        ChoosingSteps,
        ChoosingDirection,
        Moving,
        Waiting,
        Count
    }

    // Start is called before the first frame update
    void Start()
    {
        m_wayPoints.Clear();
        //CurrentPoint.GetComponent<BasePoint>().FindWay(_TargetID, _TargetSteps, m_wayPoints);
        //Debug.Log(m_wayPoints.Count);
    }

    void Update()
    {
        if (m_currentState == State.ChoosingDirection)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = mousePos;
            if (Input.GetMouseButtonDown(0))
            {
                m_clickPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0) && Vector2.Distance(Input.mousePosition, m_clickPos) < 0.01f)
            {  
                List<RaycastHit2D> hitted = new List<RaycastHit2D>();
                Physics2D.Raycast(mousePos2D, Vector2.zero, new ContactFilter2D(), hitted);
                for (int i = 0; i < hitted.Count; i++)
                {
                    if (hitted[i].collider.tag == "Point")
                    {
                        GameObject gm = hitted[i].collider.gameObject;
                        BasePoint targetPoint = gm.GetComponent<BasePoint>();
                        if (targetPoint.IsAcceptable())
                        {
                            int targetID = targetPoint.CurrentId;
                            BasePoint currentPoint = CurrentPoint.GetComponent<BasePoint>();
                            m_wayPoints.Clear();
                            bool valid = currentPoint.FindWay(targetID, m_moveOnSteps+1, m_wayPoints);
                            if (valid && m_wayPoints.Count > 0)
                            {
                                Debug.Log(m_wayPoints.Count);
                                m_currentState = State.Moving;
                            }
                            else
                            {
                                m_wayPoints.Clear();
                            }
                        }
                    }
                }
            }
        }
        else if (m_currentState == State.Moving)
        {
            Vector2 pos = transform.position;
            Vector2 targetPos = m_wayPoints[0].transform.position;
            pos = pos + (targetPos - pos).normalized * Time.deltaTime*PlayerSpeed;
            if (Vector2.Distance(pos, targetPos) < 0.1f)
            {
                CurrentPoint = m_wayPoints[0];
                m_wayPoints.Remove(CurrentPoint);
            }
            transform.position = pos;
            if(m_wayPoints.Count <= 0)
            {
                m_currentState = State.ChoosingCard;
                CurrentPoint.GetComponent<BasePoint>().RemoveHightlightAceptable();
            }
        }
    }

    public void MoveOnSteps(string steps)
    {
        if (m_currentState == State.ChoosingSteps)
        {
            m_moveOnSteps = int.Parse(steps);
            CurrentPoint.GetComponent<BasePoint>().HighlightAceptableForSteps(m_moveOnSteps);
            m_currentState = State.ChoosingDirection;
        }
    }

    public void ChooseCard(GameObject card)
    {
        if (m_currentState == State.ChoosingCard)
        {
            card.GetComponent<BaseCard>().ExecCard(gameObject);
            m_currentState = State.ChoosingSteps;
        }
    }

    public State GetCurrentState()
    {
        return m_currentState;
    }
}
