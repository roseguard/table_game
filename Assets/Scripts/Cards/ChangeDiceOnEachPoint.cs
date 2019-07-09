using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDiceOnEachPoint : BaseCard
{
    public GameObject AnimationObject = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void ExecCard(GameObject playerCaller)
    {
        CardOnEachPointEffect effect = new CardOnEachPointEffect(playerCaller, AnimationObject);
        playerCaller.GetComponent<Player>().AddEffect(effect);
    }
}
