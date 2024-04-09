namespace SitecoreSend.SDK
{
    public enum CustomFieldType
    {
        Text = 0, // Default - accepts any text value as input.
        Number = 1, // Accepts only numeric values as input.
        DateTime = 2, // Accepts only date values as input, with or without time.
        SingleSelectDropdown = 3, // Accepts only values explicitly defined in a list.
        CheckBox = 4, // Accepts only values of true or false.
    }
}