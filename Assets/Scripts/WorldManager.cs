using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 m_prevMousePos = new Vector2();
    bool m_clicked = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            m_prevMousePos = mousePos;
            m_clicked = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            m_clicked = false;
        }

        if(m_clicked)
        {
            Vector3 pos = Camera.main.transform.position;
            pos += (Vector3)(m_prevMousePos - (Vector2)mousePos);
            Camera.main.transform.position = pos;
        }
    }
}
