using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCMovementPatrol : MonoBehaviour
{
   [SerializeField] private float _maxPatrollingTime;
   [SerializeField] private float _patrolAroundInterval = 5f;
   [SerializeField] private float _patrolRadius = 1f;

   private NavMeshAgent _agent;
   private NPCGuardController _guardController;
   private bool _isPatrolling;
   private float _currentPatrollingTime;

   private void Awake()
   {
      _agent = GetComponent<NavMeshAgent>();
      _guardController = GetComponent<NPCGuardController>();
      EventManager.TimeHasChanged.AddListener(ScheduledPatrol);
   }

   //Handles moving around a machine only if state is changed
   private void Update()
   {
      if (_guardController.npcGuard.State == CharacterState.CHECK)
      {
         CheckForPatrolAround();
      }
   }
   
   public void ScheduledPatrol(int hour)
   {
      if (!NPCScheduleManager.Instance.ScheduledWaypoints.ContainsKey(hour))
      {
         return;
      }
      
      StopPatrolling();
      _agent.SetDestination(NPCScheduleManager.Instance.ScheduledWaypoints[hour].position);
   }

   //Logic before start actually moving guard around
   private void CheckForPatrolAround()
   {
      if (_isPatrolling)
      {
         _currentPatrollingTime += Time.deltaTime;
         return;
      }

      if (!NPCScheduleManager.Instance.ScheduledWaypoints.ContainsKey(TimeManager.Instance.CurrentHour))
      {
         return;
      }
      
      var npcPosition = transform.position;
      var targetPosition = new Vector3(NPCScheduleManager.Instance.ScheduledWaypoints[TimeManager.Instance.CurrentHour].position.x, npcPosition.y, NPCScheduleManager.Instance.ScheduledWaypoints[TimeManager.Instance.CurrentHour].position.z);
      
      if (npcPosition != targetPosition)
      {
         return;
      }

      _isPatrolling = true;
      
      StartCoroutine(nameof(CheckMachine));
   }
   
   //Moves guard around
   public IEnumerator CheckMachine()
   {
      if (_currentPatrollingTime >= _maxPatrollingTime)
      {
         StopPatrolling();
         yield break;
      }
      
      var npcPosition = transform.position;
      var targetPosition = NPCScheduleManager.Instance.ScheduledWaypoints[TimeManager.Instance.CurrentHour].position;
      var radius = new Vector3(Mathf.Sin(Random.Range(-360, 360f) * Mathf.Deg2Rad) * _patrolRadius, 0f, Mathf.Cos(Random.Range(-360, 360f) * Mathf.Deg2Rad) * _patrolRadius);
      var radiusPosition = new Vector3(targetPosition.x + radius.x, targetPosition.y, targetPosition.z + radius.z);
      
      if (!_agent.SetDestination(radiusPosition))
      {
         _currentPatrollingTime = 0;
         StartCoroutine(nameof(CheckMachine));
         yield break;
      }

      yield return new WaitForSeconds(_patrolAroundInterval);
      StartCoroutine(nameof(CheckMachine));
   }

   private void StopPatrolling()
   {
      _currentPatrollingTime = 0;
      _isPatrolling = false;
   }
}
