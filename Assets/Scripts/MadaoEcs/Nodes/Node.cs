using System.Collections.Generic;

namespace MadaoEcs {

    /* Copypaste to every new Node
    
#region ecs framework magic
public static short TypeId;
public override short NodeTypeId => TypeId;

public static Dictionary<ulong, Node> ENTITIES_WITH_NODE = new Dictionary<ulong, Node>();

public override Dictionary<ulong, Node> GetEntitiesWithNode() {
    return ENTITIES_WITH_NODE;
}
#endregion

    */

    public abstract class Node {

        public abstract short NodeTypeId { get; }

        public abstract Dictionary<ulong, Node> GetEntitiesWithNode();

        public IEntity Entity;
        public int FieldsCount = 0;
        public int NotFieldsCount = 0;

        public void SendEvent<T>(T eventInstance) where T : Event {
            Entity.SendEvent(eventInstance);
        }
    }
}
