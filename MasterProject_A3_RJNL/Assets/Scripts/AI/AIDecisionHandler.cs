using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    /// <summary>
    /// Makes the decisions for the ai
    /// </summary>
    public class AIDecisionHandler : MonoBehaviour
    {

        bool decisionMade = false;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<AIPlayerConeDetector>().onPlayerDetected += OnPlayerDetected;
        }

        private void OnPlayerDetected(Vector3 postition)
        {
            MakeDecision(postition);
            
        }

        public void MakeDecision(Vector3 postition)
        {
            if (!decisionMade)
            {
                var buttons = FindObjectOfType<Alarm.AlarmContainer>().alarmButtons;
                var distanceToPlayer = Vector3.Distance(postition, transform.position);
                foreach (var item in buttons)
                {
                    var distanceToButton = Vector3.Distance(item.transform.position, transform.position);
                    

                    if (distanceToButton < distanceToPlayer)
                    {
                        // ga naar knop
                        GetComponent<AINavigationSystem>().SetCurrentWayPoint(item.transform.position);
                        GetComponent<GuardState>().SetState(AIState.SoundingAlarm);
                        item.GetComponentInChildren<AIDistanceChecker>().AddAI(gameObject);
                        decisionMade = true;
                        return;
                    }
                }

                GetComponent<GuardState>().SetState(AIState.Attacking);
                Debug.Log("SetState");
                decisionMade = true;
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}