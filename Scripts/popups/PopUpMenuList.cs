using Godot;
using System;
using System.Collections.Generic;

public partial class PopUpMenuList : Control
{
    private VBoxContainer _vContainer;
    private HBoxContainer _header;
    private List<HBoxContainer> _rows;
    public override void _Ready()
    {
        _vContainer = GetNode<VBoxContainer>("Panel/VBoxContainer");
        _header = GetNode<HBoxContainer>("Panel/VBoxContainer/Header");
        _rows = new List<HBoxContainer>();
    }

    public override void _Process(double delta)
    {
    }
}