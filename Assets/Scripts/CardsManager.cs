using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    GameObject m_templateCard = null;

    public int CardCount = 6;
    public GameObject Player = null;

    void Start()
    {
        m_templateCard = transform.Find("TemplateCard").gameObject;
        m_templateCard.SetActive(false);

        GenerateCards(CardCount);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    void GenerateCards(int count)
    {
        while (transform.childCount - 1 < count)
        {
            int cardID = Random.Range(1, 6);
            GameObject newCard = Instantiate(m_templateCard, transform);
            newCard.name = cardID.ToString();
            newCard.GetComponent<Button>().onClick.AddListener(() => CardSelected(newCard.name));
            newCard.AddComponent<BaseCard>();
            newCard.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CardSelected(string cardID)
    {
        if (Player != null)
        {
            Player playerScript = Player?.GetComponent<Player>();
            if (playerScript.GetCurrentState() == global::Player.State.ChoosingCard)
            {
                GameObject card = transform.Find(cardID).gameObject;
                playerScript.ChooseCard(card);
                Destroy(card);
            }
        }
    }
}
