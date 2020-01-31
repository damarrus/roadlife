using System.Reflection;

namespace MadaoEcs {
    public class EcsSystemMethodInfo {

        public short NodeTypeId;
        public int Priority;
        public EcsSystem Source;
        public FastMethodInfo Method;

        public EcsSystemMethodInfo(short nodeTypeId, int priority, EcsSystem source, MethodInfo method) {
            NodeTypeId = nodeTypeId;
            Priority = priority;
            Source = source;
            Method = new FastMethodInfo(method);
        }
    }
}
