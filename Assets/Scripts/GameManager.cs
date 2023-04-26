using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instace;

    private void Awake()
    {
        if (Instace == null)
        {
            Instace = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int Points = 0;

    public UnityEvent OnPointsUpdate;

    public void AddPoint(int newPoints)
    {
        Points += newPoints;
        OnPointsUpdate?.Invoke();
    }
}
