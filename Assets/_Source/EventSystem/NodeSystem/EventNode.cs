using UnityEngine;
using XNode;

namespace EventSystem.NodeSystem
{
    public abstract class EventNode : Node
    {
        [Input(connectionType = ConnectionType.Override)] public EventNode previousNode;
        [field: SerializeField] public string EventName { get; private set; }
        [field: SerializeField] public Sprite EventSprite { get; private set; }
        [field: SerializeField] public string EventText { get; private set; }
        [field: SerializeField] public string FirstOptionText { get; private set; }
        [field: SerializeField] public string SecondOptionText { get; private set; }
        [field: SerializeField] public PlayerStatsData FirstOptionPlayerStatsDataChange { get; private set; }
        [field: SerializeField] public PlayerStatsData SecondOptionPlayerStatsDataChange { get; private set; }

        public override object GetValue(NodePort port)
        {
            return port;
        }
    }
}