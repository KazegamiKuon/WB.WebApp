namespace WB.WebApp.Models
{
    public class BillResponseData
    {
        public bool IsError { get; set; }
        public IEnumerable<BillViewModel> Bills { get; set; }
    }
}
