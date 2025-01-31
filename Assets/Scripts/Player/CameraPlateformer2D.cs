using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlateformer2D : MonoBehaviour
{
    [Header("General Settings"), Space(5)]
    [SerializeField] private bool _isInFollowZone;
    
    [Header("Follow Settings"), Space(5)]
    [SerializeField] private  GameObject player;
    [SerializeField] private  float timeOffset;
    [SerializeField] private  Vector3 posOffset;
    [SerializeField] private Vector3 velocity;
    
    [Header("Waypoints Settings")]
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Transform _target;
    [SerializeField] private int _desPoint;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isAtLastWaypoint;
    [SerializeField] private bool _canGoToNextWaypoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _canGoToNextWaypoint = false;
        
        if (_waypoints.Length > 0)
        {
            _target = _waypoints[0];
        }
    }
    
    private void MoveToNexWaypoint(bool canGoToNextWaypoint)
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
    }

    private void Update()
    {
        if (_isInFollowZone)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
        }
        
        if (!_isInFollowZone)
        {
            if (_canGoToNextWaypoint)
            {
                MoveToNexWaypoint(_canGoToNextWaypoint);
            }
            
            if (Vector3.Distance(transform.position, _target.position) < 0.3f && !_isAtLastWaypoint)
            {
                // Changer la cible (cycle entre waypoints)
                _desPoint = (_desPoint + 1) % _waypoints.Length;
                _target = _waypoints[_desPoint];

                // Si on atteint le dernier waypoint, on marque qu'on a atteint la destination finale
                if (_desPoint == _waypoints.Length && !_isAtLastWaypoint)
                {
                    _isAtLastWaypoint = true;
                }
            }
        }   
    }
}
