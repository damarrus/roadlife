using MadaoEcs;

namespace MadaoEcs {
    public class EntityStubComponent : IComponent {

        private static string COMPONENT_NAME;

        public static short TypeId;

        public short GetComponentTypeId() {
            return TypeId;
        }
        public IComponent Clone() {
            return default;
        }

        public string GetComponentName() {
            return COMPONENT_NAME;
        }
    }
}
