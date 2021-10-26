using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleInput : MonoBehaviour
{
    [Serializable]
    public enum ModalState
    {
        None,
        ShowDeckPile,
        ShowDrawPile,
        ShowDiscardPile
    }

    public ModalState modalState;

    public GameObject hand;
    public GameObject deckPileIcon;
    public GameObject deckPileModal;
    public GameObject drawPileIcon;
    public GameObject drawPileModal;
    public GameObject discardPileIcon;
    public GameObject discardPileModal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickDeckPile()
    {
        modalState =
            (modalState == ModalState.ShowDeckPile) ? ModalState.None : ModalState.ShowDeckPile;
        Refresh();
    }

    public void ClickDrawPile()
    {
        modalState =
            (modalState == ModalState.ShowDrawPile) ? ModalState.None : ModalState.ShowDrawPile;
        Refresh();
    }

    public void ClickDiscardPile()
    {
        modalState =
            (modalState == ModalState.ShowDiscardPile) ? ModalState.None : ModalState.ShowDiscardPile;
        Refresh();
    }

    public void ClickEndTurn()
    {
        modalState = ModalState.None;
        Refresh();
    }

    public void Refresh()
    {
        hand.SetActive(false);
        deckPileModal.SetActive(false);
        drawPileModal.SetActive(false);
        discardPileModal.SetActive(false);
        deckPileIcon.GetComponent<ButtonImage>().IsSelected = false;
        drawPileIcon.GetComponent<ButtonImage>().IsSelected = false;
        discardPileIcon.GetComponent<ButtonImage>().IsSelected = false;

        switch (modalState)
        {
            case ModalState.None:
                hand.SetActive(true);
                break;
            case ModalState.ShowDeckPile:
                deckPileModal.SetActive(true);
                deckPileIcon.GetComponent<ButtonImage>().IsSelected = true;
                break;
            case ModalState.ShowDrawPile:
                drawPileModal.SetActive(true);
                drawPileIcon.GetComponent<ButtonImage>().IsSelected = true;
                break;
            case ModalState.ShowDiscardPile:
                discardPileModal.SetActive(true);
                discardPileIcon.GetComponent<ButtonImage>().IsSelected = true;
                break;
            default:
                break;
        }
    }
}
