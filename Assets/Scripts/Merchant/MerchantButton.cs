using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantButton : MonoBehaviour
{
    [SerializeField] private GameObject _infoButton;
    [SerializeField] private GameObject _trendingButton;
    [SerializeField] private GameObject _reviewsButton;
    [SerializeField] private GameObject _recommendButton;

    private bool _toggle = false;
    private Vector3 _lowerLimit;
    private Vector3 _upperLimit;


    // Start is called before the first frame update
    void Start()
    {
        _infoButton.SetActive(_toggle);
        _trendingButton.SetActive(_toggle);
        _reviewsButton.SetActive(_toggle);
        _recommendButton.SetActive(_toggle);

        _lowerLimit = this.transform.position;
        _upperLimit = new Vector3(_lowerLimit.x, _lowerLimit.y + 2.0f, _lowerLimit.z);
    }


    void Update()
    {
        if (_infoButton.activeSelf || _trendingButton.activeSelf || _reviewsButton.activeSelf)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _upperLimit, 0.1f);
        }
        else if (!_infoButton.activeSelf && !_trendingButton.activeSelf && !_reviewsButton.activeSelf)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _lowerLimit, 0.1f);
        }
    }


    public void ButtonClicked()
    {
        _toggle = !_toggle;
        _infoButton.SetActive(_toggle);
        _trendingButton.SetActive(_toggle);
        _reviewsButton.SetActive(_toggle);
        _recommendButton.SetActive(_toggle);
    }
}
