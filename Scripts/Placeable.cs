using Godot;

public abstract partial class Placeable : Node2D
{
    [Signal]
    public delegate void NewNodePlacedEventHandler(NetworkNode placeable);

    public bool IsShadow { get; set; }

    public bool IsFocus { get; set; }
}