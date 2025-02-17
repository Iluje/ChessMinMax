using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryTest : MonoBehaviour
{
    public Dictionary<string, int> Fruit;
    public string FruitName;
    
    private void Start()
    {
        Fruit = new Dictionary<string, int>()
        {
            { "Pomme", 3 },
            { "Poire", 2 },
            { "Abricot", 1 },
        };

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (FruitName == "Pomme")
            {
               Debug.Log("Prix de la " + Fruit["Pomme"]); 
            }
            if (FruitName == "Poire")
            {
                Debug.Log("Prix de la " + Fruit["Poire"]); 
            }
            if (FruitName == "Abricot")
            {
                Debug.Log("Prix de la " + Fruit["Abricot"]); 
            }
        }
    }
}
