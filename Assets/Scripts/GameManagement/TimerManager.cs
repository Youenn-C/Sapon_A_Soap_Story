using System.Collections;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    [Header("Mangament"), Space(5)]
    [SerializeField] private bool _timerMode;
    [SerializeField] private bool _timeOn;

    [Header("Timer Mode"), Space(5)]
    [SerializeField, Range(0,300)] private float _timeRemaining = 3f; // Temps initial en secondes

    [Header("Chrono Mode"), Space(5)]
    [SerializeField] private float _timeChrono;

    [Header("Common"), Space(5)]
    [SerializeField] private TMP_Text _timeText; // Référence à TextMesh Pro pour afficher le temps

    private void Awake()
    {
        // Singleton pour assurer qu'il n'y a qu'une seule instance de TimerManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_timeOn)
        {
            if (_timerMode)
            {
                if (_timeRemaining > 0)
                {
                    _timeRemaining -= Time.deltaTime; // Décrémente le temps
                    UpdateTimerUI(_timeRemaining);
                }
                else
                {
                    _timeRemaining = 0;
                    UpdateTimerUI(_timeRemaining);
                    // Vous pouvez ajouter ici la logique à exécuter quand le temps est écoulé
                }
            }
            else
            {
                _timeChrono += Time.deltaTime;
                UpdateTimerUI(_timeChrono);
            }
        }
    }

    private void UpdateTimerUI(float time)
    {
        if (_timeText != null)
        {
            int centiseconds = Mathf.FloorToInt((time - Mathf.Floor(time)) * 100);
            int seconds = Mathf.FloorToInt(time % 60);
            int minutes = Mathf.FloorToInt((time / 60) % 60);
            int hours = Mathf.FloorToInt(time / 3600);

            _timeText.text = string.Format("{0:D2} : {1:D2} : {2:D2} : {3:D2}", hours, minutes, seconds, centiseconds);
        }
    }

    public float Get_timeRemaining()
    {
        return _timeRemaining;
    }

    public void Reset()
    {
        if (_timeRemaining != 0) _timeRemaining = 0f;
        if (_timeChrono != 0) _timeChrono = 0f;
    }

    public void AddTimeToTimer(float _tempsBonus)
    {
        _timeRemaining += _tempsBonus;
    }

    public void TimeOnOff()
    {
        _timeOn = !_timeOn;
    }
}
