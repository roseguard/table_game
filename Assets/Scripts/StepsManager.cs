﻿using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StepsManager : MonoBehaviour
{
    GameObject m_templateDice = null;

    public int DiceCount = 6;

    void Start()
    {
        m_templateDice = transform.GetChild(0).gameObject;
        m_templateDice.SetActive(false);

        GenerateDices(DiceCount);
    }

    private void OnDestroy()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    void GenerateDices(int count)
    {
        while(transform.childCount - 1 < count)
        {
            int diceValue = Random.Range(1, 6);
            GameObject newDice = Instantiate(m_templateDice, transform);
            GameObject textObj = newDice.transform.Find("Text (TMP)").gameObject;
            textObj.GetComponent<TextMeshProUGUI>().text = diceValue.ToString();
            newDice.name = diceValue.ToString();
            newDice.GetComponent<Button>().onClick.AddListener(() => DiceSelected(newDice.name));
            newDice.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DiceSelected(string diceValue)
    {

        Player playerScript = PlayersManager.Instance.GetCurrentPlayer();
        if (playerScript.GetCurrentState() == global::Player.State.ChoosingSteps)
        {
            playerScript.MoveOnSteps(diceValue);
            Destroy(transform.Find(diceValue).gameObject);
            GenerateDices(DiceCount + 1);
        }
    }
}
