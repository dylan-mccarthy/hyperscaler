using Godot;
using System;

public partial class PopUpMenuItem : Control
{
	private Label _label;
	private TextEdit _textEdit;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_label = GetNode<Label>("Panel/Label");
		_textEdit = GetNode<TextEdit>("Panel/TextEdit");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddLabel(string text)
	{
		_label.Text = text;
	}
	public void AddTextEdit(string text)
	{
		_textEdit.Text = text;
	}

	public string[] GetValue()
	{
		return new string[] { _label.Text, _textEdit.Text };
	}
}
