using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoSingleton<InputManager>
{
    public enum ClickType { MouseUp, MouseDown}

    GameObject selectedCard;
    public GameObject SelectedCard
    {
        get;
        set;
    }

    private void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Click(ClickType.MouseDown);
        //}
        //else if(Input.GetMouseButtonUp(0))
        //{
        //    Click(ClickType.MouseUp);
        //}
    }

    public bool IsInputAvailable()
    {
        return CommandManager.Instance.state == CommandManager.State.Empty;
    }

    public void Click(ClickType clickType)
    {
        if (IsInputAvailable())
        {
            RaycastHit hit = ShootRay();

            Debug.Log("click " + clickType + " " );
            if (hit.collider == null)
            {
                return; 
            }
            if (hit.collider.TryGetComponent<IClickable>(out IClickable clickable))
            {
                switch (clickType)
                {
                    case ClickType.MouseDown:
                        clickable.OnClickDown();
                        break;
                    case ClickType.MouseUp:
                        clickable.OnClickUp();
                        break;
                }
            }
        }
    }

    public RaycastHit ShootRay()
    {
        RaycastHit hit;
        Physics.Raycast(
            Camera.main.ScreenPointToRay(Input.mousePosition),
            out hit,
            Con.RaycastDefaults.maxDistance,
            LayerMask.GetMask(Con.Layers.clickable));
        return hit;
    }

    public void SlotUp(CharacterSlot characterSlot)
    {
        //if(IsInputAvailable())
        //{
        //    if (SelectedCard)
        //    {
        //        Hand hand = GameObject.FindGameObjectWithTag(Con.Tags.player).GetComponent<Hand>();

        //        characterSlot.cardObj = selectedCard;
        //        hand.Remove(selectedCard);
        //        characterSlot.SummonCharacter(selectedCard);
        //        characterSlot.cardObj.GetComponent<Card>().Summon();
        //    }
        //}
    }
}

public interface IClickable
{
    public void OnClickUp();
    public void OnClickDown();
}
