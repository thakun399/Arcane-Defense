using UnityEngine;
using System;
using System.Collections.Generic;

public class CardSelectionManager : MonoBehaviour
{
    [Header("Card UI Elements")]
    public GameObject cardSelectionPanel;
    public CardUI[] cardSlots; // 3 ใบใน UI

    [Header("Card Pool")]
    public List<CardData> availableCards;

    private Action onComplete;

    public void ShowCardSelection(Action onCompleteCallback)
    {
        onComplete = onCompleteCallback;
        cardSelectionPanel.SetActive(true);

        List<CardData> randomCards = GetRandomCards(3);

        for (int i = 0; i < cardSlots.Length; i++)
        {
            cardSlots[i].SetCard(randomCards[i]);
            cardSlots[i].onCardSelected = OnCardSelected;
        }
    }

    private void OnCardSelected(CardData selectedCard)
    {
        Debug.Log($"Selected card: {selectedCard.cardName}");
        GameManager.Instance.ApplyBuffs(selectedCard.effects);

        cardSelectionPanel.SetActive(false);
        onComplete?.Invoke();
    }

    private List<CardData> GetRandomCards(int count)
    {
        List<CardData> result = new List<CardData>();
        float totalWeight = 0f;
        foreach (var c in availableCards) totalWeight += c.weight;

        for (int i = 0; i < count; i++)
        {
            float rand = UnityEngine.Random.Range(0, totalWeight);
            float cumulative = 0f;

            foreach (var card in availableCards)
            {
                cumulative += card.weight;
                if (rand <= cumulative)
                {
                    result.Add(card);
                    break;
                }
            }
        }

        return result;
    }
}

