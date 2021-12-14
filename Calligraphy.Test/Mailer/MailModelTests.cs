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
using Calligraphy.Mailer.Settings;

namespace Calligraphy.Test.Mailer
{
    public class MailModelTests
    {
        // Test an instantiation of the mailer request class
        //[Fact]
        //public void CreateMailerRequestConstructor()
        //{
        //    // Arrange
        //    string email = "trsiatnblacklafleur@hotmail.ca";
        //    string subject = "test";
        //    string body = "this is a test";
        //    //string filePath = "‪TestFiles\\23784.png";
        //    //using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
        //    //var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
        //    List<IFormFile> attachtments = new List<IFormFile>();
        //    //attachtments.Add(formFile);

        //    // Act
        //    MailRequest request = new MailRequest(email, subject, body, attachtments);

        //    // Assert
        //    Assert.Equal(typeof(MailRequest), request.GetType());
        //}

        // Test the instantiation of a MailerSettings Class
        //[Fact]
        //public void CreateMailerSettingsConstructor()
        //{
        //    //Arrange
        //    string mail = "Mail1";
        //    string displayName = "display1";
        //    string password = "password1";
        //    string host = "host1";
        //    int port = 1;

        //    // Act
        //    MailSettings settings = new MailSettings(mail, displayName, password, host, port);

        //    //Assert
        //    Assert.Equal(mail, settings.mail);
        //}
    }
}
