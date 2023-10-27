using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private TraveledDistance _distance;
    [SerializeField] private Text _score;

    private void Start()
    {
        _distance = GameObject.Find("Player").GetComponent<TraveledDistance>();
    }

    private void Update()
    {
        _score.text = _distance.Distance.ToString();
    }
}
