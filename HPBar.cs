using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _gauge;

    public void GaugeAmount(float amount)
    {
        _gauge.fillAmount = amount;
    }
}
