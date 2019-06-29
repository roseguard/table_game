using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDiceOnEachPoint : BaseCard
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ExecCard(GameObject playerCaller)
    {
        CardOnEachPointEffect effect = new CardOnEachPointEffect(playerCaller);
        playerCaller.GetComponent<Player>().AddEffect(effect);
    }
}
