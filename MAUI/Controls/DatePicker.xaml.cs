namespace MAUI.Controls;

public partial class DatePicker : ContentView
{
	public static readonly BindableProperty DateProperty =
		BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(DatePicker), DateTime.Today);

    public static readonly BindableProperty MinimumDateProperty =
       BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(DatePicker), new DateTime(2000, 1, 1));

    public static readonly BindableProperty MaximumDateProperty =
       BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(DatePicker), new DateTime(2100, 12, 31));

    public DateTime Date
    {
        get => (DateTime)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }

    public static readonly BindableProperty DateFormatProperty =
       BindableProperty.Create(nameof(DateFormat), typeof(string), typeof(DatePicker), "D");

    public string DateFormat
    {
        get => (string)GetValue(DateFormatProperty);
        set => SetValue(DateFormatProperty, value);
    }
    public DateTime MinimumDate
    {
        get => (DateTime)GetValue(MinimumDateProperty);
        set => SetValue(MinimumDateProperty, value);
    }

    public DateTime MaximumDate
    {
        get => (DateTime)GetValue(MaximumDateProperty);
        set => SetValue(MaximumDateProperty, value);
    }


    // Propiedad TextColor para aplicar tu estilo de color
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(DatePicker), Colors.Black);

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public DatePicker()
	{
		InitializeComponent();
	}
}