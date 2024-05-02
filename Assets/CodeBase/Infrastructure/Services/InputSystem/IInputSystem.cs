using UnityEngine;

public interface IInputSystem
{
    public Vector3 DirectionAxis { get; }
    public Vector2 MousePosition { get; }
    public bool IsSelection { get; }
    public bool IsSetTarget { get; }
}

public interface IInitializationInput : IInputSystem
{
    public void EnableSystem();
}