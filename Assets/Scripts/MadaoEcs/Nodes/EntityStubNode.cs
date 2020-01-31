using System.Collections.Generic;

namespace MadaoEcs {
    public class EntityStubNode : Node {

        public EntityStubComponent EntityStubComponent;

        #region ecs framework magic
        public static short TypeId;
        public override short NodeTypeId => TypeId;

        public static Dictionary<ulong, Node> ENTITIES_WITH_NODE = new Dictionary<ulong, Node>();

        public override Dictionary<ulong, Node> GetEntitiesWithNode() {
            return ENTITIES_WITH_NODE;
        }
        #endregion
    }
}
