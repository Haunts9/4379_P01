using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    int _score = 0;
    TankController _tankController;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] public GameObject[] art;
    private Color[] savedArt;
    private void Awake()
    {
        savedArt = new Color[art.Length];
        int count = 0;
        _tankController = GetComponent<TankController>();
        foreach (GameObject f in art)
        {
            savedArt[count] = f.GetComponent<Renderer>().material.color;
            count++;
        }
    }

    private void Start()
    {
        _scoreText.text = ("Score: " + _score);
    }

    public void ScoreUp(int value)
    {
        _score+= value;
        if (_scoreText != null)
        {
            _scoreText.text = ("Score: " + _score);
        }

    }
    public void RevertChangesToColor()
    {
        int count = 0;
        Debug.Log("Reset");
        foreach (GameObject f in art)
        {
            f.GetComponent<Renderer>().material.color = savedArt[count];
            count++;
        }
    }

}
