using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public int CardCount = 6;
    public GameObject Player = null;
    public List<GameObject> Prefabs = new List<GameObject>();

    void Start()
    {
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
        if(Prefabs.Count <= 0)
        {
            return;
        }
        while (transform.childCount < count)
        {
            GenerateOneMoreCard();
        }
    }

    public void GenerateOneMoreCard()
    {
        int cardID = Random.Range(0, Prefabs.Count);
        GameObject newCard = Instantiate(Prefabs[cardID], transform);
        newCard.name = cardID.ToString();
        newCard.GetComponent<Button>().onClick.AddListener(() => CardSelected(newCard.name));
        newCard.SetActive(true);
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
