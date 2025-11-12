using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI descriptionText;
   // public Image cardImage;
    public Button selectButton;

    private CardData cardData;
    public Action<CardData> onCardSelected;

    public void SetCard(CardData data)
    {
        cardData = data;
        cardNameText.text = data.cardName;
        descriptionText.text = data.description;
        //cardImage.sprite = data.cardImage;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => onCardSelected?.Invoke(cardData));
    }
}