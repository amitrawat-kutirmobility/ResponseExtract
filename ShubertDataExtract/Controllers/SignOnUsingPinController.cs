using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace ShubertDataExtract.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignOnUsingPinController : ControllerBase
    {
        [HttpGet("success_extract_geteventList")]
        public  IActionResult ExtreactSuccessDataResponse()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(successREsponse);

            var elements =  doc.DocumentNode.SelectNodes("/html/body/p/b/font/font");

            var stringArray = new List<string>();

            foreach(var element in elements)
            {
                var response = element.InnerText;

                stringArray.Add(response);
            }

            return Ok(stringArray);
        }

        [HttpGet("ExteactSuccessRawHtmlResponse")]
        public IActionResult ExteactSuccessRawHtmlResponse()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(successResponse2);

            var eventNameElement = doc.DocumentNode.SelectSingleNode("html/body/p/b/font/text()[1]");
            var messageElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b/font/text()[2]");
            var userNameElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b/font/text()[3]");

            var performanceElements = doc.DocumentNode.SelectNodes("/html/body/p/b/font/font");

            var stringArray = new List<object>();

            foreach (var element in performanceElements)
            {
                var response = element.InnerText;

                stringArray.Add(response);
            }

            return Ok(new
            {
                eventName = eventNameElement?.InnerText,
                message = messageElement?.InnerText,
                userName = userNameElement?.InnerText,
                events = stringArray
            });
        }

        [HttpGet("ExtractSuccessWithoutPerformance")]
        public IActionResult ExtractSuccessWithoutPerformance()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(successResponseWithoutPerformance2);

            var eventNameElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b/font/text()[1]");
            var messageElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b/font/text()[2]");
            var userNameElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b/font/text()[3]");

            var performanceElements = doc.DocumentNode.SelectNodes("/html/body/p/b/font/font");

            var stringArray = new List<object>();

            foreach (var element in performanceElements)
            {
                var response = element.InnerText;

                stringArray.Add(response);
            }

            return Ok(new
            {
                eventName = eventNameElement?.InnerText,
                message = messageElement?.InnerText,
                userName = userNameElement?.InnerText,
                events = stringArray
            });
        }


        [HttpGet("ShowErrorResponseTicketTakkerNotFound")]
        public IActionResult ShowErrorResponseTicketTakkerNotFound()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(errorResponseTicketTakkerNotFound);

            return Ok(new
            {
               data =  doc.DocumentNode.InnerText
            });
        }

        string errorResponseTicketTakkerNotFound = @"E
<html>
    <body bgcolor='red'>
        <font size = '20' color='yellow'>E Ticket Taker not passed to page.</font>
    </body
</html>SmeURxRIN0ub2zIEs5QUEw|Z
<html>
    <body bgcolor=""#FF0000"">
        <p align=""center"">
            <b>
                <font face=""Times New Roman"" color=""#FFFFFF"" size=""8"">Ticket Taker Not Found
                    <br>
                    <br>
                </b>
            </font>";

        string successResponseWithoutPerformance2 = @"<html><body bgcolor=""#FFFFFF""><p align=""center""><b><font face=""Times New Roman"" color=""#000000"" size=""6"">Majestic Theatre<br><br>Welcome<br>Javier Molina<br><br><font face=""Times New Roman"" color=""#660033"" size=""4"">Waitress<br>No Product-Error   </font></b></font></p></html>";

        string successResponseWithoutPeforamce = @"<html>
    <body bgcolor=""#FFFFFF"">
        <p align=""center"">
            <b>
                <font face=""Times New Roman"" color=""#000000"" size=""6"">Majestic Theatre
                    <br>
                    <br>Welcome
                    <br>Javier Molina
                    <br>
                    <br>
                    <font face=""Times New Roman"" color=""#660033"" size=""4"">Waitress
                        <br>No Product-Error   
                    </font>
                </b>
            </font>
        </p>
    </html>";


        string successResponse2 = @"<html>
    <body bgcolor=""#FFFFFF"">
        <p align=""center"">
            <b>
                <font face=""Times New Roman"" color=""#000000"" size=""6"">Majestic Theatre
                    <br>
                    <br>Welcome
                    <br>Javier Molina
                    <br>
                    <br>
                    <font face=""Times New Roman"" color=""#660033"" size=""4"">The Phantom of the Opera
                        <br>12/30/22-2:00 PM 
                    </font>
                    <br>
                    <br>
                    <font face=""Times New Roman"" color=""#660033"" size=""4"">The Phantom of the Opera
                        <br>12/30/22-8:00 PM 
                    </font>
                </b>
            </font>
        </p>
    </html>";

        string successREsponse = @"<html>
    <body bgcolor=""#FFFFFF"">
        <p align=""center"">
            <b>
                <font face=""Times New Roman"" color=""#000000"" size=""6"">Majestic Theatre
                    <br>
                    <br>Welcome
                    <br>Javier Molina
                    <br>
                    <br>
                    <font face=""Times New Roman"" color=""#660033"" size=""4"">The Phantom of the Opera
                        <br>12/30/22-2:00 PM 
                    </font>
                    <br>
                    <br>
                    <font face=""Times New Roman"" color=""#660033"" size=""4"">The Phantom of the Opera
                        <br>12/30/22-8:00 PM 
                    </font>
                </b>
            </font>
        </p>
    </html>";
    }
}
