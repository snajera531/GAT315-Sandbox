using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StringUI : MonoBehaviour
{
    public TMP_Text label = null;
    public StringData data = null;

    private void OnValidate()
    {
        if (data != null)
        {
            name = data.name;
            label.text = name;
        }
    }

    void Update()
    {
        label.text = data.value;
    }
}