namespace MadaoEcs {

    /* Copypaste to every new Component
    
#region Ecs framework magic
private static string COMPONENT_NAME; 

public static short TypeId;

public short GetComponentTypeId() {
    return TypeId;
}

public IComponent Clone() {
    return new ThisComponentCtor;
}

public string GetComponentName() {
    return COMPONENT_NAME;
}
#endregion
    */

    public interface IComponent {

        short GetComponentTypeId();
        string GetComponentName();
        IComponent Clone();
    }
}
