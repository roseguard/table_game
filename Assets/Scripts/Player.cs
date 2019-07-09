using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject CurrentPoint = null;
    public float PlayerSpeed = 1f;
    public GameObject CardsHolder = null;
    public GameObject DiceHolder = null;

    private List<GameObject> m_wayPoints = new List<GameObject>();
    private int m_moveOnSteps = 0;
    private State m_currentState = State.ChoosingSteps;
    private Vector2 m_clickPos = Vector2.zero;
    private List<BaseEffect> m_effects = new List<BaseEffect>();
    private bool m_locked = false;
    private GameObject m_objectUnlocker = null;

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

    void Start()
    {
        m_wayPoints.Clear();
        foreach (var effect in m_effects)
        {
            effect.OnNewStep();
        }
    }

    void Update()
    {
        if(m_objectUnlocker != null && !m_objectUnlocker.activeSelf)
        {
            m_objectUnlocker = null;
            m_locked = false;
        }

        if(m_locked)
        {
            return;
        }
        CheckActiveEffects();
        foreach (var effect in m_effects)
        {
            effect.OnUpdate();
        }

        if (m_currentState == State.ChoosingDirection)
        {
            OnChoosingDirectionUpdate();
        }
        else if (m_currentState == State.Moving)
        {
            OnMovingUpdate();
        }
    }

    void OnChoosingDirectionUpdate()
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
                        bool valid = currentPoint.FindWay(targetID, m_moveOnSteps + 1, m_wayPoints);
                        if (valid && m_wayPoints.Count > 0)
                        {
                            Debug.Log(m_wayPoints.Count);
                            CurrentPoint.GetComponent<BasePoint>().SetLeaved(gameObject);
                            m_currentState = State.Moving;

                            foreach (var effect in m_effects)
                            {
                                effect.OnPointLeaved();
                            }
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

    void OnMovingUpdate()
    {
        Vector2 pos = transform.position;
        Vector2 targetPos = m_wayPoints[0].transform.position;
        pos = pos + (targetPos - pos).normalized * Time.deltaTime * PlayerSpeed;
        if (Vector2.Distance(pos, targetPos) < 0.1f)
        {
            CurrentPoint = m_wayPoints[0];
            m_wayPoints.Remove(CurrentPoint);
            if (m_wayPoints.Count > 0)
            {
                BasePoint point = CurrentPoint.GetComponent<BasePoint>();
                if (point != null)
                { 
                    point.SetSteped(gameObject);
                }

                foreach (var effect in m_effects)
                {
                    effect.OnPointSteped();
                }
            }
            else
            {
                CurrentPoint.GetComponent<BasePoint>().SetStayed(gameObject);

                foreach (var effect in m_effects)
                {
                    effect.OnPointStayed();
                }
            }
        }
        transform.position = pos;
        if (m_wayPoints.Count <= 0)
        {
            m_currentState = State.ChoosingCard;
            CurrentPoint.GetComponent<BasePoint>().RemoveHightlightAceptable();
        }
    }

    void CheckActiveEffects()
    {
        if(m_effects.Count <= 0)
        {
            return;
        }
        List<BaseEffect> notActiveEffects = new List<BaseEffect>();
        foreach(var effect in m_effects)
        {
            if(!effect.Active)
            {
                notActiveEffects.Add(effect);
            }
        }
        foreach(var effect in notActiveEffects)
        {
            m_effects.Remove(effect);
        }
    }

    public void LockPlayer(bool toLock)
    {
        m_locked = toLock;
    }

    public void UnlockOnObjectInactive(GameObject unlocker)
    {
        m_objectUnlocker = unlocker;
    }

    public void MoveOnSteps(string steps)
    {
        if (m_currentState == State.ChoosingSteps)
        {
            m_moveOnSteps = int.Parse(steps);
            CurrentPoint.GetComponent<BasePoint>().HighlightAceptableForSteps(m_moveOnSteps);
            m_currentState = State.ChoosingDirection;

            foreach (var effect in m_effects)
            {
                effect.OnDiceSelected();
            }
        }
    }

    public void ChooseCard(GameObject card)
    {
        if (m_currentState == State.ChoosingCard)
        {
            card.GetComponent<BaseCard>().ExecCard(gameObject);
            m_currentState = State.ChoosingSteps;

            foreach (var effect in m_effects)
            {
                effect.OnCardSelected();
            }
        }
    }

    public State GetCurrentState()
    {
        return m_currentState;
    }

    public void AddEffect(BaseEffect effect)
    {
        m_effects.Add(effect);
        effect.OnStart();
    }
}
