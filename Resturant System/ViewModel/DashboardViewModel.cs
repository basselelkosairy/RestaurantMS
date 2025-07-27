namespace Resturant_System.ViewModel
{
    public class DashboardViewModel
    {
        public List<decimal> WeeklySales { get; set; } = new();
        public List<int> PeakOrdersByHour { get; set; } = new();
    }
}
