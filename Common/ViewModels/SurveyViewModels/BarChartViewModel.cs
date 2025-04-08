namespace Common.ViewModels.SurveyViewModels
{
    public class BarChartViewModel
    {
        public string Title { get; set; }
        public string ValueYField { get; private set; }
        public string ValueXField { get; private set; }
        public string Unit { get; set; }
        public List<ColumnViewModel> Columns { get; set; }
        public BarChartViewModel()
        {
            Title = string.Empty;
            ValueYField = "Value";
            ValueXField = "Label";
            Unit = "Unit";
            Columns = new List<ColumnViewModel>();
        }
    }
    public class ColumnViewModel
    {
        public string Label { get; set; }
        public double Value { get; set; }
        public ColumnViewModel()
        {
            Label = string.Empty;
        }
    }
}
