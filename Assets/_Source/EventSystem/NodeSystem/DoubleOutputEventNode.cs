using XNode;

namespace EventSystem.NodeSystem
{
    public class DoubleOutputEventNode : EventNode
    {
        [Output] public EventNode firstOptionNextNode;
        [Output] public EventNode secondOptionNextNode;

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            var fromNodeSingle = from.node as SingleOutputEventNode;
            var fromNodeDouble = from.node as DoubleOutputEventNode;
            var toNode = (EventNode)to.node;

            if (fromNodeSingle)
            {
                toNode.previousNode = fromNodeSingle;
                fromNodeSingle.nextNode = toNode;
            }
            else if (fromNodeDouble)
            {
                toNode.previousNode = fromNodeDouble;
                if (from.fieldName == "firstOptionNextNode")
                    fromNodeDouble.firstOptionNextNode = toNode;
                else
                    fromNodeDouble.secondOptionNextNode = toNode;
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            var portNode = (DoubleOutputEventNode)port.node;
            if (port.direction == NodePort.IO.Input)
            {
                portNode.previousNode = default;
                return;
            }
            
            if (port.fieldName == "firstOptionNextNode")
                portNode.firstOptionNextNode = default;
            else
                portNode.secondOptionNextNode = default;
        }
    }
}