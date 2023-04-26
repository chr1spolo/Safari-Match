using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPoints : MonoBehaviour
{
    int displayedPoints = 0;

    public TextMeshProUGUI pointsLabel;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instace.OnPointsUpdate.AddListener(UpdatePoints);
    }

    // Update is called once per frame
    void UpdatePoints()
    {
        StartCoroutine(UpdatePointsCorroutine());
    }

    IEnumerator UpdatePointsCorroutine()
    {
        while (displayedPoints < GameManager.Instace.Points)
        {
            displayedPoints++;
            pointsLabel.text = displayedPoints.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
