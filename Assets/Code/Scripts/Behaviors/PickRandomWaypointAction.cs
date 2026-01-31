using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Unity.Behavior
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "PickRandomWaypoint", story: "[Agent] pick random [Waypoints]", category: "Action", id: "faef26efab8109701d39f57a5e40c768")]
    internal partial class PickRandomWaypointAction : Action
    {
        [SerializeReference] public BlackboardVariable<NavMeshAgent> Agent;
        [SerializeReference] public BlackboardVariable<List<GameObject>> Waypoints;
        [SerializeReference] public BlackboardVariable<float> Speed = new(3f);
        [SerializeReference] public BlackboardVariable<float> WaypointWaitTime = new(1.0f);
        [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new(0.2f);
        [SerializeReference] public BlackboardVariable<string> AnimatorSpeedParam = new("SpeedMagnitude");

        private NavMeshAgent m_NavMeshAgent;
        private Animator m_Animator;

        [CreateProperty] private Vector3 m_CurrentTarget;
        [CreateProperty] private float m_OriginalStoppingDistance = -1f;
        [CreateProperty] private float m_OriginalSpeed = -1f;
        private float m_CurrentSpeed;
        [CreateProperty] private int m_CurrentPatrolPoint = 0;

        protected override Status OnStart()
        {
            if (Agent.Value == null)
            {
                LogFailure("No agent assigned.");
                return Status.Failure;
            }

            if (Waypoints.Value == null || Waypoints.Value.Count == 0)
            {
                LogFailure("No waypoints to patrol assigned.");
                return Status.Failure;
            }

            Initialize();
            MoveToRandomWaypoint();
            return Status.Running;
        }
        protected override Status OnUpdate()
        {
            if (Agent.Value == null || Waypoints.Value == null)
            {
                return Status.Failure;
            }

            float distance = GetDistanceToWaypoint();
            bool destinationReached = distance <= DistanceThreshold;

            if (destinationReached && (m_NavMeshAgent == null || !m_NavMeshAgent.pathPending))
            {
                m_CurrentSpeed = 0;
                return Status.Success;
            }
            else if (m_NavMeshAgent == null)
            {
                m_CurrentSpeed = SimpleMoveTowardsLocation(Agent.Value.transform, m_CurrentTarget, Speed, distance, 1f);
            }

            UpdateAnimatorSpeed();

            return Status.Running;
        }

        protected override void OnEnd()
        {
            UpdateAnimatorSpeed(0f);

            if (m_NavMeshAgent != null)
            {
                if (m_NavMeshAgent.isOnNavMesh)
                {
                    m_NavMeshAgent.ResetPath();
                }
                m_NavMeshAgent.speed = m_OriginalSpeed;
                m_NavMeshAgent.stoppingDistance = m_OriginalStoppingDistance;
            }
        }

        protected override void OnDeserialize()
        {
            m_NavMeshAgent = Agent.Value.GetComponentInChildren<NavMeshAgent>();
            if (m_NavMeshAgent != null)
            {
                if (m_OriginalSpeed >= 0f)
                    m_NavMeshAgent.speed = m_OriginalSpeed;
                if (m_OriginalStoppingDistance >= 0f)
                    m_NavMeshAgent.stoppingDistance = m_OriginalStoppingDistance;

                m_NavMeshAgent.Warp(Agent.Value.transform.position);
            }

            int patrolPoint = m_CurrentPatrolPoint - 1;
            Initialize();
            m_CurrentPatrolPoint = patrolPoint;
        }
        private void Initialize()
        {
            m_Animator = Agent.Value.GetComponentInChildren<Animator>();
            m_NavMeshAgent = Agent.Value.GetComponentInChildren<NavMeshAgent>();
            if (m_NavMeshAgent != null)
            {
                if (m_NavMeshAgent.isOnNavMesh)
                {
                    m_NavMeshAgent.ResetPath();
                }

                m_OriginalSpeed = m_NavMeshAgent.speed;
                m_NavMeshAgent.speed = Speed.Value;
                m_OriginalStoppingDistance = m_NavMeshAgent.stoppingDistance;
                m_NavMeshAgent.stoppingDistance = DistanceThreshold;
            }

            UpdateAnimatorSpeed(0f);
        }

        private float GetDistanceToWaypoint()
        {
            if (m_NavMeshAgent != null)
            {
                return m_NavMeshAgent.remainingDistance;
            }

            Vector3 targetPosition = m_CurrentTarget;
            Vector3 agentPosition = Agent.Value.transform.position;
            agentPosition.y = targetPosition.y;
            return Vector3.Distance(agentPosition, targetPosition);
        }
        private void MoveToRandomWaypoint()
        {
            m_CurrentPatrolPoint = Random.Range(0, Waypoints.Value.Count);

            m_CurrentTarget = Waypoints.Value[m_CurrentPatrolPoint].transform.position;
            if (m_NavMeshAgent != null)
            {
                m_NavMeshAgent.SetDestination(m_CurrentTarget);
            }
        }

        private void UpdateAnimatorSpeed(float explicitSpeed = -1f)
        {
            UpdateAnimatorSpeed(m_Animator, AnimatorSpeedParam, m_NavMeshAgent, m_CurrentSpeed, explicitSpeed: explicitSpeed);
        }

        private float SimpleMoveTowardsLocation(Transform agentTransform, Vector3 targetLocation, float speed, float distance, float slowDownDistance = 0.0f,
            float minSpeedRatio = 0.1f)
        {
            if (agentTransform == null)
            {
                return 0f;
            }

            Vector3 agentPosition = agentTransform.position;
            float movementSpeed = speed;

            // Slowdown
            if (slowDownDistance > 0.0f && distance < slowDownDistance)
            {
                float ratio = distance / slowDownDistance;
                movementSpeed = Mathf.Max(speed * minSpeedRatio, speed * ratio);
            }

            Vector3 toDestination = targetLocation - agentPosition;
            toDestination.y = 0.0f;

            if (toDestination.sqrMagnitude > 0.0001f)
            {
                toDestination.Normalize();

                // Apply movement
                agentPosition += toDestination * (movementSpeed * Time.deltaTime);
                agentTransform.position = agentPosition;

                // Look at the target
                agentTransform.forward = toDestination;
            }

            return movementSpeed;
        }
        private bool UpdateAnimatorSpeed(Animator animator, string speedParameterName, NavMeshAgent navMeshAgent, float currentSpeed, float minSpeedThreshold = 0.1f,
            float explicitSpeed = -1f)
        {
            if (animator == null || string.IsNullOrEmpty(speedParameterName))
            {
                return false;
            }

            float speedValue = 0;
            if (explicitSpeed >= 0)
            {
                speedValue = explicitSpeed;
            }
            else if (navMeshAgent != null)
            {
                speedValue = navMeshAgent.velocity.magnitude;
            }
            else
            {
                speedValue = currentSpeed;
            }

            if (speedValue <= minSpeedThreshold)
            {
                speedValue = 0;
            }

            animator.SetFloat(speedParameterName, speedValue);
            return true;
        }
    }
}