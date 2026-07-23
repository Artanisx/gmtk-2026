using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementPatrol : MonoBehaviour
{
   [SerializeField] private Transform[] _waypoints;
   private NavMeshAgent _agent;

   private int _waypointIndex;
   private int WaypointIndex
   {
      get => _waypointIndex;
      set
      {
         _waypointIndex = value;
         
         if (value >= _waypoints.Length)
         {
            _waypointIndex = 0;
         }
      }
   }

   private void Awake() => _agent = GetComponent<NavMeshAgent>();

   private void Update()
   {
      Patrol();
   }

   public void Patrol()
   {
      var npcPosition = new Vector3(transform.position.x, 0f, transform.position.z);
      var targetPosition = new Vector3(_waypoints[WaypointIndex].position.x, 0f, _waypoints[WaypointIndex].position.z);
      
      if (npcPosition != targetPosition)
      {
         _agent.SetDestination(_waypoints[WaypointIndex].position);
         return;
      }

      WaypointIndex++;
   }
}
