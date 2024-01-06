namespace FreeCourse.Web.Models.Orders
{
    public class AddressCreateInput
    {
        public string Province { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine { get; set; }
    }
}
