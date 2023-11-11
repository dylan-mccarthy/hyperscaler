using Godot;
using System;
using System.Windows.Markup;

public partial class CreateItemPopUp : Panel
{
	[Export]
	public VBoxContainer ValuesContiner {get; set;}
	[Export]
	public string[] Values {get; set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach(string valueName in Values){
			var hBoxContainer = new HBoxContainer();
			// Create a new label
			Label label = new Label();
			label.Text = valueName;
			// Add Editable Field
			LineEdit lineEdit = new LineEdit();
			// Add to container
			hBoxContainer.AddChild(label);
			hBoxContainer.AddChild(lineEdit);
			ValuesContiner.AddChild(hBoxContainer);

		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
