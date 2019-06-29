using UnityEngine;
using UnityEditor;

public class CardOnEachPointEffect : BaseEffect
{
    public CardOnEachPointEffect(GameObject target) : base(target)
    {
    }

    public override void OnPointSteped()
    {
        if (Active)
        {
            Player player = Target.GetComponent<Player>();
            player.CardsHolder.GetComponent<CardsManager>().GenerateOneMoreCard();
        }
    }

    public override void OnPointStayed()
    {
        Active = false;
    }
}