using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public NumberTextMeshUI healthPointText;
    public NumberTextMeshUI attackPointText;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthPointText(int value, int maxValue)
    {
        healthPointText.FirstVal = value;
        healthPointText.SecondVal = maxValue;
    }

    public void SetAttackPointText(int value, int attackCount)
    {
        attackPointText.FirstVal = value;
        attackPointText.SecondVal = attackCount;
    }
}
