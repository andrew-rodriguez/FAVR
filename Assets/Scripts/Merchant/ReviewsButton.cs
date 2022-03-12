using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewsButton : MonoBehaviour
{
    [SerializeField] private GameObject _infoText;
    [SerializeField] private GameObject _trendingText;
    [SerializeField] private GameObject _reviewsText;
    

    void Start()
    {
        _reviewsText.SetActive(false);
    }


    public void ButtonClicked()
    {
        _infoText.SetActive(false);
        _trendingText.SetActive(false);
        _reviewsText.SetActive(true);
    }
}
