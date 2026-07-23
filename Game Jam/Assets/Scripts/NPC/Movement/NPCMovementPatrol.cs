using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCMovementPatrol : MonoBehaviour
{
   [SerializedDictionary("Hour", "Waypoint")]
   [SerializeField] private SerializedDictionary<int, Transform> _scheduledWaypoints;
   [SerializeField] private float _maxPatrollingTime;
   [SerializeField] private float _patrolRadius = 1f;
   private NavMeshAgent _agent;
   private bool _isPatrolling;
   private float _currentPatrollingTime;

   private void Awake()
   {
      _agent = GetComponent<NavMeshAgent>();
      EventManager.TimeHasChanged.AddListener(ScheduledPatrol);
   }

   private void Update()
   {
      CheckForPatrolAround();
   }
   
   public void ScheduledPatrol(int hour)
   {
      if (!_scheduledWaypoints.ContainsKey(hour))
      {
         return;
      }
      
      StopPatrolling();
      _agent.SetDestination(_scheduledWaypoints[hour].position);
   }

   private void CheckForPatrolAround()
   {
      if (_isPatrolling)
      {
         _currentPatrollingTime += Time.deltaTime;
         return;
      }

      if (!_scheduledWaypoints.ContainsKey(TimeManager.Instance.CurrentHour))
      {
         return;
      }
      
      var npcPosition = new Vector3(transform.position.x, 0f, transform.position.z);
      var targetPosition = new Vector3(_scheduledWaypoints[TimeManager.Instance.CurrentHour].position.x, 0f, _scheduledWaypoints[TimeManager.Instance.CurrentHour].position.z);
      
      if (npcPosition != targetPosition)
      {
         return;
      }

      _isPatrolling = true;
      
      StartCoroutine(nameof(PatrolAroundWaypoint));
   }
   
   public IEnumerator PatrolAroundWaypoint()
   {
      if (_currentPatrollingTime >= _maxPatrollingTime)
      {
         StopPatrolling();
         yield break;
      }
      
      var npcPosition = new Vector3(transform.position.x, 0f, transform.position.z);
      var radius = new Vector3(Mathf.Sin(Random.Range(-360, 360f) * Mathf.Deg2Rad) * _patrolRadius, 0f, Mathf.Cos(Random.Range(-360, 360f) * Mathf.Deg2Rad) * _patrolRadius);
      var radiusPosition = new Vector3(transform.position.x + radius.x, 0f, transform.position.z + radius.z);
      
      if (npcPosition != radiusPosition)
      {
         _agent.SetDestination(radiusPosition);
      }

      yield return new WaitForSeconds(5f);
      StartCoroutine(nameof(PatrolAroundWaypoint));
   }

   private void StopPatrolling()
   {
      _currentPatrollingTime = 0;
      _isPatrolling = false;
   }
}
