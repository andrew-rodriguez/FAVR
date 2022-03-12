using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrendingButton : MonoBehaviour
{
    [SerializeField] private GameObject _infoText;
    [SerializeField] private GameObject _trendingText;
    [SerializeField] private GameObject _reviewsText;


    void Start()
    {
        _trendingText.SetActive(false);
    }


    public void ButtonClicked()
    {
        _infoText.SetActive(false);
        _reviewsText.SetActive(false);
        _trendingText.SetActive(true);
    }
}
