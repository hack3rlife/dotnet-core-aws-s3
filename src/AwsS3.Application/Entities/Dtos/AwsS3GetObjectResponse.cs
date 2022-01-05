using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace AwsS3.Application.Entities.Dtos
{
    public class AwsS3GetObjectResponse
    {
        public byte[] ContentBody { get; set; }
        public string ContentType { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

    }
}
