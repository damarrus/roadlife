using FastMember;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MadaoEcs {
    public sealed class NodeMetaData {

        public readonly Func<Node> CreateMethod;
        public TypeAccessor Accessor;
        public int ComponentFieldsCount;
        public HashSet<short> NotFields = new HashSet<short>();

        public NodeMetaData(Type nodeType) {
            Accessor = TypeAccessor.Create(nodeType);
            CreateMethod = Expression.Lambda<Func<Node>>(Expression.New(nodeType)).Compile();
            foreach (var member in Accessor.GetMembers()) {
                if (!typeof(IComponent).IsAssignableFrom(member.Type)) continue;

                if (member.IsDefined(typeof(Not))) {
                    var componentId = (short)member.Type.GetField("TypeId", BindingFlags.Static | BindingFlags.Public).GetValue(null);
                    NotFields.Add(componentId);
                } else {
                    ComponentFieldsCount++;
                }                
            }
        }
    }
}
