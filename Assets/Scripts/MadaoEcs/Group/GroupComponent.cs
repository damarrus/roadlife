namespace MadaoEcs {

    /* COPY TO EVERY NEW GROUP COMPONENT

#region Ecs framework magic
public CTOR(ulong groupId, bool isGroupOwner) : base(groupId, isGroupOwner) {
}

private static string COMPONENT_NAME;

public static short TypeId;

public override short GetComponentTypeId() {
    return TypeId;
}

public override IComponent Clone() {
    return new CTOR(GroupId, IsGroupOwner);
}

public override string GetComponentName() {
    return COMPONENT_NAME;
}
#endregion

    */

    public abstract class GroupComponent : IComponent {

        [Visual]
        public bool IsGroupOwner { get; set; }
        [Visual]
        public ulong GroupId { get; set; }

        public GroupComponent(ulong groupId, bool isGroupOwner) {
            GroupId = groupId;
            IsGroupOwner = isGroupOwner;
        }

        public abstract IComponent Clone();
        public abstract short GetComponentTypeId();
        public abstract string GetComponentName();

        public GroupComponent CloneAsChild() {
            var clone = (GroupComponent)Clone();
            clone.IsGroupOwner = false;
            return clone;
        }
    }
}
