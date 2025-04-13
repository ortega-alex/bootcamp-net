namespace UniversitiApiBackend.Models.DataModels
{
    public class UserTokents
    {
        public int Id { get; set; }
        public string Tocken { get; set; }
        public string UserName { get; set; }
        public TimeSpan Validity { get; set; }
        public String RefreshToken { get; set; }
        public string EmailId { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }

    }
}
