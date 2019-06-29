using UnityEngine;
using UnityEditor;

public class BaseEffect
{
    public GameObject Target = null;
    public bool Active = true;

    public BaseEffect(GameObject target)
    {
        Target = target;
    }

    public virtual void OnStart()
    {
        Debug.Log("Effect OnStart");
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnEnd()
    {
        Debug.Log("Effect OnEnd");
    }

    public virtual void OnNewStep()
    {
        Debug.Log("Effect OnNewStep");
    }

    public virtual void OnStepEnd()
    {
        Debug.Log("Effect OnStepEnd");
    }

    public virtual void OnCardSelected()
    {
        Debug.Log("Effect OnCardSelected");
    }

    public virtual void OnDiceSelected()
    {
        Debug.Log("Effect OnDiceSelected");
    }

    public virtual void OnPointSelected()
    {
        Debug.Log("Effect OnPointSelected");
    }

    public virtual void OnPointSteped()
    {
        Debug.Log("Effect OnPointSteped");
    }

    public virtual void OnPointStayed()
    {
        Debug.Log("Effect OnPointStayed");
    }

    public virtual void OnPointLeaved()
    {
        Debug.Log("Effect OnPointLeaved");
    }

    public virtual void OnCardTaken()
    {
        Debug.Log("Effect OnCardTaken");
    }
}