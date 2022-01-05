using System;

namespace AwsS3.Application.Entities.Dtos
{
    public class StatusResponse
    {
        public DateTime Started { get; set; }
        public string Server { get; set; }
        public string OsVersion { get; set; }
    }
}
