using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ShubertDataExtract.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanController : ControllerBase
    {

        [HttpGet("scan_in_success")]
        public IActionResult ScanInSuccess()
        {
            UsingParttern();

            var doc = new HtmlDocument();
            doc.LoadHtml(scanInSuccessResponse4);

            var firstElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font/text()[1]");
            var rowElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font/text()[2]");
            var seatElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font/text()[3]");
            var allowElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[2]/font");
            var fifthElement = doc.DocumentNode.SelectSingleNode("/html/body/p/font[3]");
            var eventNameElement = doc.DocumentNode.SelectSingleNode("/html/body/p/font[4]");
            var eventPlaceElement = doc.DocumentNode.SelectSingleNode("/html/body/p/font[5]");
            var eventPlaceCityElement = doc.DocumentNode.SelectSingleNode("/html/body/p/font[6]");
            var eventDateElement = doc.DocumentNode.SelectSingleNode("/html/body/p/font[7]");


            //foreach (HtmlNode p in doc.DocumentNode.SelectNodes("//preceding-sibling::br")) 
            //{
            //    Console.WriteLine(p.PreviousSibling.InnerText.Trim());
            //}

            var response = new {
                first = firstElement.InnerText,
                row = rowElement.InnerText,
                seat = seatElement.InnerText,
                allow = allowElement.InnerText,
                fifth = fifthElement.InnerText,
                eventName = eventNameElement.InnerText,
                eventPlace = eventPlaceElement.InnerText,
                eventPlaceCity = eventPlaceCityElement?.InnerText,
                eventDate = eventDateElement?.InnerText
            };

         

            return Ok(response);
        }

        [HttpGet("ScanInSuccess_Data")]
        public IActionResult ScanInSuccess_Data()
        {
            string input = scanInSuccessResponse4;

            //// <font face="Times New Roman" color="#FF007A" size="6"> (PH)</b>
            ////string pattern = "<font face=\"Times New Roman\" color=\"#FF007A\" size=\"6\"> (?<data>\\w.+)";
            //Match result = new Regex(pattern).Match(input);

            //var data = result.Groups["data"].Value;

            // get the row data example
            // string parttern = @"<font face=""Times New Roman"" color=""#660033"" size=""5"">\s*row:\s*<\/font>([\s\S]*?)<br>";  // working
            // string parttern = @"<font face=""Times New Roman"" color=""#660033"" size=""5"">\s*row:\s*<\/font>([\s\S]*?)<br>";  // working

            // example 1 get the fifth element
            //string parttern = @"<font face=""Times New Roman"" color=""#FF007A"" size=""6""> (.*?)</b></font>";  // working

            string parttern = @"<font face=""Times New Roman"" color=""#FF007A"" size=""6""> \s*([\s\S]*?)<\/b>";  // working

            var match = Regex.Match(input, parttern);

            if(match.Success)
            {
                string text = match.Groups[1].Value;

               return Ok(text);
            }

            return BadRequest("DAta not found");
        }


        [HttpGet("ScanInError_Wrong_Performance")]
        public IActionResult ScanInError_Wrong_Performance()
        {
            var htmlText = signIn_error_wrong_performanceResponse;

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlText);

            var messageElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font");
            var titleElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[2]/font");
            var cityCodeElement = doc.DocumentNode.SelectSingleNode("/html/body/table/td/p/font[1]/b");
            var cityNameElement = doc.DocumentNode.SelectSingleNode("/html/body/table/td/p/font[2]");
            var eventPlaceNameElement = doc.DocumentNode.SelectSingleNode("/html/body/table/td/p/font[3]");
            var eventPlaceLocationElement = doc.DocumentNode.SelectSingleNode("html/body/table/td/p/font[4]");   ///\r\n
            var eventDateTimeElement = doc.DocumentNode.SelectSingleNode("/html/body/table/td/p/font[5]");
            var seventhTextElement = doc.DocumentNode.SelectSingleNode("/html/body/table/td/p/font[6]/text()[1]");
            var rawTextElement = doc.DocumentNode.SelectSingleNode("/html/body/table/td/p/font[6]/text()[2]");

            return Ok(new
            {
                message = messageElement?.InnerText,
                title = titleElement?.InnerText, 
                cityCode = cityCodeElement?.InnerText,
                cityName = cityNameElement?.InnerText,
                eventPlaceName = eventPlaceNameElement?.InnerText,
                eventPlaceLocation = eventPlaceLocationElement?.InnerText,
                eventDateTime = eventDateTimeElement?.InnerText,
                seventhElement = seventhTextElement?.InnerText,
                raw = rawTextElement?.InnerText
            });
        }

        string signIn_error_wrong_performanceResponse = @"<html><body bgcolor=""#FF0000""><p align=""center""><br><b><font face=""Times New Roman"" color=""#000000"" size=""+20"">Denied</b></font><br><br><b><font face=""Times New Roman"" color=""#000000"" size=""+20"">Wrong Performance</b></font><br><br><table bgcolor=""#E0E0E0"" border=""1"" width=""541""><td><p align=""center""><font face=""Times New Roman"" color=""#FF007A"" size=""6""> <b>(PH)</b></font><font face=""Times New Roman"" color=""#000000"" size=""6"">Chicago</font><br><font face=""Times New Roman"" color=""#000099"" size=""6"">Ambassador Theatre</font><br><font face=""Times New Roman"" color=""#000000"" size=""4"">NY City Area</font><br><font face=""Times New Roman"" color=""#000099"" size=""6"">04/01/23  8:00 PM (E)</font><br></b><font face=""Times New Roman"" color=""#000000"" size=""4"">ORCHC<br><font face=""Times New Roman"" color=""#660033"" size=""5""> row: </font>B<font face=""Times New Roman"" color=""#660033"" size=""3""> seat: </font>111</font></td></table><br>(941457558323)</p></html>";

        void UsingParttern()
        {
            //string input = "colors numResults=\"100\" totalResults=\"6806926\"";
            //string pattern = "totalResults=\"(?<results>\\d+?)\"";
            //Match result = new Regex(pattern).Match(input);
            //Console.WriteLine(result.Groups["results"]);

            string input = ScanInScccessResponse;
            //string pattern = "row: </font>(?<results>\\d+?)<br>";
            //string pattern = "row: </font>(?<results>\\d+?)";
            //string pattern = "row: </font>(.*)";  // working
            string pattern = " row: </font>(?<results>\\w.+)";
            Match result = new Regex(pattern).Match(input);

            var data = result.Groups["results"].Value;

            Console.WriteLine(result.Groups["results"]);
        }

        //public IActionResult ScanInError()
        //{
        //    return Ok();
        //}

        //public IActionResult ScanOutSuccess()
        //{
        //    return Ok();
        //}

        //public IActionResult ScanOutError()
        //{
        //    return Ok();
        //}

        string scanInSuccessResponse4 = @"<html><body bgcolor=""#FFFFFF""><p align=""center""><b><font face=""Times New Roman"" color=spv_font_color1 size=""+20"">ORCHO<br><font face=""Times New Roman"" color=""#660033"" size=""5""> row: </font>H<br><font face=""Times New Roman"" color=""#660033"" size=""5"">seat: </font>22&nbsp;</b> </font><br><br><b><font face=""Times New Roman"" color=""#FF0000"" size=""+20"">Allow Entry</b></font><br><br>  <font face=""Times New Roman"" color=""#FF007A"" size=""6""> (PH)</b></font><br><font face=""Times New Roman"" color=""#000000"" size=""6"">The Phantom of the Opera</font><br><font face=""Times New Roman"" color=""#000099"" size=""6"">Majestic Theatre</font><br><font face=""Times New Roman"" color=""#000000"" size=""4"">NY City Area</font><br><font face=""Times New Roman"" color=""#000099"" size=""6"">12/30/22  8:00 PM (E)</font><br><br><b><font face=""Times New Roman"" color=spv_font_color1 size=""7"">SE UNKNOWN 11/29/22</b> </font><br><b><font face=""Times New Roman"" color=spv_font_color1 size=""3"">   (922749711995)</b></font></p></html>";

        string ScanInSuccessResponse3 = @"<html><body bgcolor=\""#FFFFFF\""><p align=\""center\""><b><font face=\""Times New Roman\"" color=spv_font_color1 size=\""+20\"">ORCHO<br><font face=\""Times New Roman\"" color=\""#660033\"" size=\""5\""> row: </font>H<br><font face=\""Times New Roman\"" color=\""#660033\"" size=\""5\"">seat: </font>22&nbsp;</b> </font><br><br><b><font face=\""Times New Roman\"" color=\""#FF0000\"" size=\""+20\"">Allow Entry</b></font><br><br>  <font face=\""Times New Roman\"" color=\""#FF007A\"" size=\""6\""> (PH)</b></font><br><font face=\""Times New Roman\"" color=\""#000000\"" size=\""6\"">The Phantom of the Opera</font><br><font face=\""Times New Roman\"" color=\""#000099\"" size=\""6\"">Majestic Theatre</font><br><font face=\""Times New Roman\"" color=\""#000000\"" size=\""4\"">NY City Area</font><br><font face=\""Times New Roman\"" color=\""#000099\"" size=\""6\"">12/30/22  8:00 PM (E)</font><br><br><b><font face=\""Times New Roman\"" color=spv_font_color1 size=\""7\"">SE UNKNOWN 11/29/22</b> </font><br><b><font face=\""Times New Roman\"" color=spv_font_color1 size=\""3\"">   (922749711995)</b></font></p></html>";

        string ScanInSuccessResponse2 = @"<html><body bgcolor=\""#FFFFFF\""><p align=\""center\""><b><font face=\""Times New Roman\"" color=spv_font_color1 size=\""+20\"">ORCHO<br><font face=\""Times New Roman\"" color=\""#660033\"" size=\""5\""> row: </font>H<br><font face=\""Times New Roman\"" color=\""#660033\"" size=\""5\"">seat: </font>22&nbsp;</b> </font><br><br><b><font face=\""Times New Roman\"" color=\""#FF0000\"" size=\""+20\"">Allow Entry</b></font><br><br>  <font face=\""Times New Roman\"" color=\""#FF007A\"" size=\""6\""> (PH)</b></font><br><font face=\""Times New Roman\"" color=\""#000000\"" size=\""6\"">The Phantom of the Opera</font><br><font face=\""Times New Roman\"" color=\""#000099\"" size=\""6\"">Majestic Theatre</font><br><font face=\""Times New Roman\"" color=\""#000000\"" size=\""4\"">NY City Area</font><br><font face=\""Times New Roman\"" color=\""#000099\"" size=\""6\"">12/30/22  8:00 PM (E)</font><br><br><b><font face=\""Times New Roman\"" color=spv_font_color1 size=\""7\"">SE UNKNOWN 11/29/22</b> </font><br><b><font face=\""Times New Roman\"" color=spv_font_color1 size=\""3\"">   (922749711995)</b></font></p></html>";

        string ScanInScccessResponse = @"<html>
    <body bgcolor=""#FFFFFF"">
        <p align=""center"">
            <b>
                <font face=""Times New Roman"" color=spv_font_color1 size=""+20"">ORCHO
                    
                    <br>
                    <font face=""Times New Roman"" color=""#660033"" size=""5""> row: </font>H
                    <br>
                    <font face=""Times New Roman"" color=""#660033"" size=""5"">seat: </font>22&nbsp;
                </b>
            </font>
            <br>
            <br>
            <b>
                <font face=""Times New Roman"" color=""#FF0000"" size=""+20"">Allow Entry
                
                </b>
            </font>
            <br>
            <br>
            <font face=""Times New Roman"" color=""#FF007A"" size=""6""> (PH)
            
            </b>
        </font>
        <br>
        <font face=""Times New Roman"" color=""#000000"" size=""6"">The Phantom of the Opera</font>
        <br>
        <font face=""Times New Roman"" color=""#000099"" size=""6"">Majestic Theatre</font>
        <br>
        <font face=""Times New Roman"" color=""#000000"" size=""4"">NY City Area</font>
        <br>
        <font face=""Times New Roman"" color=""#000099"" size=""6"">12/30/22  8:00 PM (E)</font>
        <br>
        <br>
        <b>
            <font face=""Times New Roman"" color=spv_font_color1 size=""7"">SE UNKNOWN 11/29/22
            
            </b>
        </font>
        <br>
        <b>
            <font face=""Times New Roman"" color=spv_font_color1 size=""3"">   (922749711995)
            
            </b>
        </font>
    </p>
</html>";
    }
}
