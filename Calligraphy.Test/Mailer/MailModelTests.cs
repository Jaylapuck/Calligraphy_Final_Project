using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Xunit;
using System.Threading.Tasks;
using Calligraphy.Mailer.Model;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Calligraphy.Test.Mailer
{
    public class MailModelTests
    {
        // Test an instantiation of the mailer request class
        [Fact]
        public void CreateMailerRequestConstructor()
        {
            // Arrange
            string email = "trsiatnblacklafleur@hotmail.ca";
            string subject = "test";
            string body = "this is a test";
            string filePath = "‪C:\\Users\\trist\\Pictures\\23784.png";
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            List<IFormFile> attachtments = new List<IFormFile>();
            attachtments.Add(formFile);

            // Act
            MailRequest request = new MailRequest(email, subject, body, attachtments);

            // Assert
            Assert.Equal(typeof(MailRequest), request.GetType());
        }
    }
}
