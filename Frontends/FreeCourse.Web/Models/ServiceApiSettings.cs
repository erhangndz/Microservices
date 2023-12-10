namespace FreeCourse.Web.Models
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public string PhotoStockUri { get; set; }
        public Catalog Catalog { get; set; }
    }

    public class Catalog
    {

        public string Path { get; set; }

    }

}
