using UnityEngine;
using UnityEditor;

public class CardOnEachPointEffect : BaseEffect
{
    private GameObject m_animationObj = null;
    private bool KillAfterAnim = false;

    public CardOnEachPointEffect(GameObject target, GameObject animationObj) : base(target)
    {
        m_animationObj = animationObj;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnPointSteped()
    {
        if (Active && !KillAfterAnim)
        {
            Player player = Target.GetComponent<Player>();
            m_animationObj.SetActive(false);
            m_animationObj.SetActive(true);
            player.LockPlayer(true);
        }
    }

    public override void OnPointStayed()
    {
        if (Active && !KillAfterAnim)
        {
            Player player = Target.GetComponent<Player>();
            m_animationObj.SetActive(true);
            player.LockPlayer(true);
        }
        KillAfterAnim = true;
    }
}