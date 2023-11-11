using Godot;

public interface IPlaceable
{
    [Signal]
    public delegate void NewNodePlacedEventHandler(NetworkNode placeable);

    public bool IsShadow { get; set; }

    public bool IsFocus { get; set; }
}