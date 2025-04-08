namespace Common.ViewModels.SurveyViewModels
{
    public class ClusteredBarChartViewModel
    {
        public string Title { get; set; }
        public string ValueYField { get; private set; }
        public string ValueXField { get; private set; }
        public List<GroupColoumnViewModel> Groups { get; set; }
        public List<string> Labels { get; set; }
        public ClusteredBarChartViewModel()
        {
            Title = string.Empty;
            ValueYField = "Value";
            ValueXField = "Label";
            Groups = new List<GroupColoumnViewModel>();
            Labels = new List<string>();
        }
    }
    public class GroupColoumnViewModel
    {
        public string Label { get; set; }
        public List<ColumnViewModel> Columns { get; set; }
        public GroupColoumnViewModel()
        {
            Label = string.Empty;
            Columns = new List<ColumnViewModel>();
        }
    }
}
