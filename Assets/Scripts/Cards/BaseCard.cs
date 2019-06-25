using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ExecCard(GameObject playerCaller)
    {
        Debug.Assert(false, "Oh boy, this card script are virtual, do not use it!");
    }
}
