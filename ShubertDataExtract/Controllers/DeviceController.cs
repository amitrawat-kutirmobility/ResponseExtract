using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ShubertDataExtract.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        [HttpGet("device_register_success")]
        public IActionResult Device_register_success()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(device_register_successResponse2);
            var userNameElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b/font/text()[4]");

            var input = device_register_successResponse2;

            string eventPlaceNameParttern = @"This device has been registered to\s*([\s\S]*?)<br>";

            var eventPlaceName = input.GetTextByRegexPattern(eventPlaceNameParttern);

            var macAddressPattern = @"<br>mac address: \s*([\s\S]*?)<br>";

            var macAddress = input.GetTextByRegexPattern(macAddressPattern);

            var deviceNamePattern = @"device name: \s*([\s\S]*?)<br>";
            var deviceName = input.GetTextByRegexPattern(deviceNamePattern);

            var userNamePattern = @"<br>([\s\S]*?)</b></font></p>";
            var userName = input.GetTextByRegexPattern(userNamePattern);

            var uesrName2 = userNameElement.InnerText;

            return Ok(new
            {
                eventPlaceName,
                macAddress,
                deviceName,
                userName,
                uesrName2
            });
        }

        [HttpGet("Device_register_error1")]
        public IActionResult Device_register_error()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(device_register_errorResponse);
            var messageElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font");
            var barCodeElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[2]/font");

            var message = messageElement?.InnerText?.Trim();
            var barCode = barCodeElement?.InnerText?.Trim();

            return Ok(new
            {
                message,
                barCode
            });
        }

        [HttpGet("Device_register_error2")]
        public IActionResult Device_register_error2()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(device_register_erroResponse2);
            var messageElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b/font");
            var barCodeElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[2]/font");

            var message = messageElement?.InnerText?.Trim();
            var barCode = barCodeElement?.InnerText?.Trim();

            return Ok(new
            {
                message,
                barCode
            });
        }


        [HttpGet("Device_remove_error")] 
        public IActionResult Device_Remove_Error()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(device_register_erroResponse2);

            var titleElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font");
            var barCodeElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[2]/font");


            return Ok(new
            {
                title = titleElement.InnerText,
                barCode = barCodeElement.InnerText
            });
        }

        [HttpGet("device_remove_errorResponse_device_cannot_be_deleted")]
        public IActionResult Device_remove_errorResponse_device_cannot_be_deleted()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(device_remove_errorResponse_device_cannot_be_Deleted2);

            var title1Element = doc.DocumentNode.SelectSingleNode("/html/body/p/b/font/text()[1]");
            var title2Element = doc.DocumentNode.SelectSingleNode("/html/body/p/b/font/text()[2]");

            var barCodeElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b/font/text()[3]");

            ////html/body/p/b/font/text()[1]
            // /html/body/p/b/font/text()[3]

            return Ok(new
            {
                title1 = title1Element.InnerText,
                title2 = title2Element.InnerText,
                barCode = barCodeElement?.InnerText
            });
        }

        [HttpGet("device_remove_error_TicketTakerNotFound")]
        public IActionResult device_remove_error_TicketTakerNotFound()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(device_remove_errorResponse_TicketTakerNotFound2);

            // device_remove_errorResponse_TicketTakerNotFound
            //var messageElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font/text()");
            //var barCodeElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[2]/font");

            var messageElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font");
            var barCodeElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[2]/font");

            return Ok(new
            {
                title1 = messageElement?.InnerText,
                barCode = barCodeElement?.InnerText
            });
        }

        string device_remove_errorResponse_TicketTakerNotFound2 = @"<html><body bgcolor=""#FF0000""><p align=""center""><b><font face=""Times New Roman"" color=""#FFFFFF"" size=""8"">Ticket Taker Not Found<br><br></b></font><b><font face=""Times New Roman"" color=""#FFFFFF"" size=""5"">1001530             </font></b></p></html>";

        string device_remove_errorResponse_TicketTakerNotFound = @"<html><body bgcolor=\""#FF0000\""><p align=\""center\""><b><font face=\""Times New Roman\"" color=\""#FFFFFF\"" size=\""8\"">Ticket Taker Not Found<br><br></b></font><b><font face=\""Times New Roman\"" color=\""#FFFFFF\"" size=\""5\"">1001530             </font></b></p></html>";

        string device_remove_errorResponse_device_cannot_be_Deleted2 = @"<html><body bgcolor=\""#FF0000\""><p align=\""center\""><b><font face=\""Times New Roman\"" color=\""#FFFFFF\"" size=\""8\"">Sorry&nbsp;Jennifer Flynn device cannot be deleted. <br>Device does not exist. <br>E3-21-F9-7D-A1-0B   <br><br></b></font></p></html>";

        string device_remove_errorResponse_device_cannot_be_deleted = @"<html><body bgcolor=\""#FF0000\""><p align=\""center\""><b><font face=\""Times New Roman\"" color=\""#FFFFFF\"" size=\""8\"">Sorry&nbsp;Jennifer Flynn device cannot be deleted. <br>Device does not exist. <br>E3-21-F9-7D-A1-0B   <br><br></b></font></p></html>";

        string device_remove_errorResponseTicket_Taker_Not_Found = @"<html>
    <body bgcolor=""#FF0000"">
        <p align=""center"">
            <b>
                <font face=""Times New Roman"" color=""#FFFFFF"" size=""8"">Ticket Taker Not Found
                    <br>
                    <br>
                </b>
            </font>
            <b>
                <font face=""Times New Roman"" color=""#FFFFFF"" size=""5"">1001530             </font>
            </b>
        </p>
    </html>";

        string device_register_errorResponse = @"<html><body bgcolor=\""#FF0000\""><p align=\""center\""><b><font face=\""Times New Roman\"" color=\""#FFFFFF\"" size=\""8\"">Ticket Taker Not Found<br><br></b></font><b><font face=\""Times New Roman\"" color=\""#FFFFFF\"" size=\""5\"">1001534             </font></b></p></html>";
        string device_register_erroResponse2 = @"<html>
    <body bgcolor=""#FF0000"">
        <p align=""center"">
            <b>
                <font face=""Times New Roman"" color=""#FFFFFF"" size=""8"">Sorry&nbsp;Javier Molina is not allowed to register devices
                    <br>
                    <br>
                </b>
            </font>
        </p>
    </html>";


        string device_register_successResponse2 = @"Y~STS-PDA|<html><body bgcolor=\""#FFFFFF\""><p align=\""center\""><b><font face=\""Times New Roman\"" color=\""#000000\"" size=\""8\"">This device has been registered to Majestic Theatre<br><br>mac address: E3:21:F9:7D:A1:2B<br>device name: DeviceSTS-PDA<br><br>Jennifer Flynn</b></font></p></html>";

        string device_register_successResponse =
            @"<html>
    <body bgcolor=""#FFFFFF"">
        <p align=""center"">
            <b>
                <font face=""Times New Roman"" color=""#000000"" size=""8"">This device has been registered to Majestic Theatre
                    <br>
                    <br>mac address: E3-21-F9-7D-A1-0B
                    <br>device name: DeviceSTS-PDA
                    <br>
                    <br>Jennifer Flynn
                </b>
            </font>
        </p>
    </html>";
    }
}
