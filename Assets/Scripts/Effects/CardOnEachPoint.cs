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
            player.GetCardHolder.SetActive(false);
            player.GetCardHolder.SetActive(true);
            player.LockPlayer(true);
        }
    }

    public override void OnPointStayed()
    {
        if (Active)
        {
            Player player = Target.GetComponent<Player>();
            player.GetCardHolder.SetActive(true);
            player.LockPlayer(true);
        }
        Active = false;
    }
}