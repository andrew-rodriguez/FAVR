using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton : MonoBehaviour
{
    [SerializeField] private GameObject _infoText;
    [SerializeField] private GameObject _trendingText;
    [SerializeField] private GameObject _reviewsText;
    

    void Start()
    {
        _infoText.SetActive(false);
    }


    public void ButtonClicked()
    {
        _trendingText.SetActive(false);
        _reviewsText.SetActive(false);
        _infoText.SetActive(true);
    }
}