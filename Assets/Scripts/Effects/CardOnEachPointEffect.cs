using UnityEngine;
using UnityEditor;

public class CardOnEachPointEffect : BaseEffect
{
    private GameObject m_animationObj = null;
    private GameObject m_clonedAnimationObj = null;
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
            ShowGetCardPopup();
            player.LockPlayer(true);
        }
    }

    public override void OnPointStayed()
    {
        if (Active && !KillAfterAnim)
        {
            Player player = Target.GetComponent<Player>();
            ShowGetCardPopup();
            player.LockPlayer(true);
        }
        KillAfterAnim = true;
    }

    private void ShowGetCardPopup()
    {
        if (m_clonedAnimationObj != null)
        {
            Object.Destroy(m_clonedAnimationObj);
            m_clonedAnimationObj = null;
        }
        m_clonedAnimationObj = Object.Instantiate(m_animationObj, m_animationObj.transform.parent);
        m_clonedAnimationObj.SetActive(true);
    }
}