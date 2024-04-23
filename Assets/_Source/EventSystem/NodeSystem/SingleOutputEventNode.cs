using XNode;

namespace EventSystem.NodeSystem
{
    public class SingleOutputEventNode : EventNode
    {
        [Output] public EventNode nextNode;
        
        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            var fromNodeSingle = from.node as SingleOutputEventNode;
            var fromNodeDouble = from.node as DoubleOutputEventNode;
            var toNode = (EventNode)to.node;

            if (fromNodeSingle)
            {
                toNode.previousNode = fromNodeSingle;
            }
			else if (fromNodeDouble)
            {
                toNode.previousNode = fromNodeDouble;
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            var portNode = (SingleOutputEventNode)port.node;
            if (port.direction == NodePort.IO.Input)
            {
                portNode.previousNode = default;
                return;
            }

            portNode.nextNode = default;
        }
    }
}