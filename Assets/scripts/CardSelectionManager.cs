using UnityEngine;
using System;
using System.Collections.Generic;

public class CardSelectionManager : MonoBehaviour
{
    [Header("Card UI Elements")]
    public GameObject cardSelectionPanel;
    public CardUI[] cardSlots;

    [Header("Card Pool")]
    public List<CardData> availableCards;

    private Action onComplete;

    public void ShowCardSelection(Action onCompleteCallback)
    {
        onComplete = onCompleteCallback;
        cardSelectionPanel.SetActive(true);

        List<CardData> randomCards = GetRandomUniqueCards(3);

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

    private List<CardData> GetRandomUniqueCards(int count)
    {
        List<CardData> cards = new List<CardData>();
        List<CardData> pool = new List<CardData>(availableCards);

        for (int i = 0; i < count; i++)
        {
            if (pool.Count == 0) break;

            float totalWeight = 0;
            foreach (var c in pool) totalWeight += c.weight;

            float rand = UnityEngine.Random.Range(0, totalWeight);
            float cumulative = 0f;

            for (int j = 0; j < pool.Count; j++)
            {
                cumulative += pool[j].weight;
                if (rand <= cumulative)
                {
                    cards.Add(pool[j]);
                    pool.RemoveAt(j); // ❗ลบออก ป้องกันซ้ำ
                    break;
                }
            }
        }

        return cards;
    }
}