
public class EquipmentSlot : ItemSlot
{
	public EquipmentTypes EquipmentType;

	protected override void OnValidate()
	{
		base.OnValidate();
		gameObject.name = EquipmentType.ToString() + " Slot";
	}
}
