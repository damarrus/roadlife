namespace MadaoEcs {

    /* Copypaste to every new Event
    
#region Ecs framework magic
public static short TypeId;
public override short EventTypeId => TypeId;
#endregion 
     
    */

    public abstract class Event {
        public abstract short EventTypeId { get; }
        public bool IsCanceled;
    }

    public class NodeAddedEvent : Event {

        public short NodeId;
        public NodeAddedEvent(short nodeId) {
            NodeId = nodeId;
        }

        #region Ecs framework magic
        public static short TypeId;

        public override short EventTypeId => TypeId;
        #endregion

    }

    public class NodeRemoveEvent : Event {

        public short NodeId;
        public NodeRemoveEvent(short nodeId) {
            NodeId = nodeId;
        }

        #region Ecs framework magic
        public static short TypeId;

        public override short EventTypeId => TypeId;
        #endregion
    }
}
