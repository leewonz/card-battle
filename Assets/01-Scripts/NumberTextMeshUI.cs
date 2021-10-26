using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberTextMeshUI : MonoBehaviour
{
    TextMeshProUGUI textUI;

    [SerializeField]
    int firstVal;
    [TextArea(3, 6)]
    public string firstValFormat;
    public bool isUsingMaxValue;
    [SerializeField]
    int secondVal;
    [TextArea(3, 6)]
    public string secondValFormat;
    public string divider;

    public int FirstVal
    {
        get
        {
            return firstVal;
        }
        set
        {
            firstVal = value;
            Refresh();
        }
    }

    public int SecondVal
    {
        get
        {
            return secondVal;
        }
        set
        {
            secondVal = value;
            Refresh();
        }
    }

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        if (firstValFormat.Equals("")) { firstValFormat = "{0}"; }
        if (secondValFormat.Equals("")) { secondValFormat = "{0}"; }
    }
    private void Start()
    {
        Refresh();
    }
    void Refresh()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        if(isUsingMaxValue)
        {
            textUI.text = 
                string.Format(
                    "{0}{1}",
                    string.Format(firstValFormat, firstVal),
                    string.Format(secondValFormat, divider + secondVal));
        }
        else
        {
            textUI.text =
                string.Format(
                    "{0}",
                    string.Format(firstValFormat, firstVal));
        }
    }
    public void Activate()
    {
        textUI.enabled = false;
    }

    public void Deactivate()
    {
        textUI.enabled = true;
    }
}
