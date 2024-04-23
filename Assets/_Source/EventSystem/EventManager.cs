using System;
using System.Collections.Generic;
using EventSystem.NodeSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace EventSystem
{
    public class EventManager : MonoBehaviour
    {
        public event Action OnEventChange;
        public EventNode CurrentEventNode { get; private set; }
        [SerializeField] private Player player;
        [SerializeField] private EventNode startEventNode;
        [SerializeField] private EventNode deathEventNode;
        [SerializeField] private List<EventNode> eventStarts;
        private readonly Stack<EventNode> _passedEvents = new();
        private readonly Stack<PlayerStatsData> _passedEventsStatsChangeData = new();

        private void Awake()
        {
            ChangeEvent(startEventNode, false);
        }

        private void SetRandomEvent()
        {
            ChangeEvent(eventStarts[Random.Range(0, eventStarts.Count)]);
        }

        private void SetDeathEvent()
        {
            ChangeEvent(deathEventNode);
        }

        private void ChangeEvent(EventNode newEvent, bool log = true)
        {
            if (log)
            {
                _passedEvents.Push(CurrentEventNode);
            }

            CurrentEventNode = newEvent;
            OnEventChange?.Invoke();
        }

        public void TurnBack()
        {
            if (_passedEvents.Count <= 0) return;

            if (player.Dead)
                player.Revive();

            player.ReversePlayerStats(_passedEventsStatsChangeData.Pop());
            ChangeEvent(_passedEvents.Pop(), false);
        }

        public void ChooseEventOption(int chosenOption)
        {
            if (player.Dead)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            var currentDoubleOutputEventNode = CurrentEventNode as DoubleOutputEventNode;
            var currentSingleOutputEventNode = CurrentEventNode as SingleOutputEventNode;

            if (chosenOption == 0)
            {
                player.ChangePlayerStats(CurrentEventNode.FirstOptionPlayerStatsDataChange);
                _passedEventsStatsChangeData.Push(CurrentEventNode.FirstOptionPlayerStatsDataChange);
                if (CheckPlayerDeath()) return;

                if (currentDoubleOutputEventNode)
                {
                    if (currentDoubleOutputEventNode.firstOptionNextNode != null)
                    {
                        ChangeEvent(currentDoubleOutputEventNode.firstOptionNextNode);
                    }
                    else
                    {
                        SetRandomEvent();
                    }

                    return;
                }
            }
            else if (chosenOption == 1)
            {
                player.ChangePlayerStats(CurrentEventNode.SecondOptionPlayerStatsDataChange);
                _passedEventsStatsChangeData.Push(CurrentEventNode.SecondOptionPlayerStatsDataChange);
                if (CheckPlayerDeath()) return;

                if (currentDoubleOutputEventNode)
                {
                    if (currentDoubleOutputEventNode.secondOptionNextNode != null)
                    {
                        ChangeEvent(currentDoubleOutputEventNode.secondOptionNextNode);
                    }
                    else
                    {
                        SetRandomEvent();
                    }

                    return;
                }
            }

            if (currentSingleOutputEventNode)
            {
                if (currentSingleOutputEventNode.nextNode != null)
                {
                    ChangeEvent(currentSingleOutputEventNode.nextNode);
                }
                else
                {
                    SetRandomEvent();
                }
            }
        }

        private bool CheckPlayerDeath()
        {
            if (!player.Dead) return false;

            SetDeathEvent();
            return true;
        }
    }
}