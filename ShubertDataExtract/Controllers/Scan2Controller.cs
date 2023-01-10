using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ShubertDataExtract.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Scan2Controller : Controller
    {


        //Hello this is a change

        [HttpGet("scan_in_success")]
        public IActionResult ScanInSuccess()
        {

            var doc = new HtmlDocument();
            doc.LoadHtml(scan_in_success);

            var firstElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font/text()[1]");
            var rowElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font/text()[3]");
            var seatElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[1]/font/text()[5]");
            var Seat = new String(seatElement.InnerText.Where(Char.IsDigit).ToArray());
            var allowElement = doc.DocumentNode.SelectSingleNode("/html/body/p/b[2]/font");
            var fifthElement = doc.DocumentNode.SelectSingleNode("/html/body/p/font[1]");
            var SixthElement = doc.DocumentNode.SelectSingleNode("/html/body/p/font[2]");
            var SeventhElement = doc.DocumentNode.SelectSingleNode("/html/body/p/font[3]");
            var eventNameElement = doc.DocumentNode.SelectSingleNode("/html/body/p/font[4]");
            var eventPlaceElement = doc.DocumentNode.SelectSingleNode("/html/body/p/font[5]");
            var eventPlaceandDate = doc.DocumentNode.SelectSingleNode("/html/body/p/b[3]/font");
            var eventPlaceCityElement = Regex.Match(eventPlaceandDate.InnerText, @"([A-Za-z ])+([A-Za-z ]+)").Value;
            string stringWithDate = eventPlaceandDate.InnerText;
            Match match = Regex.Match(stringWithDate, @"\d{2}\/\d{2}\/\d{2}");
            string date = match.Value;
            var dateTime = DateTime.ParseExact(date, "MM/dd/yy", CultureInfo.CurrentCulture);


            var eventDateElement = dateTime.ToString();

            var response = new
            {
                first = firstElement.InnerText,
                row = rowElement.InnerText,
                seat = Seat,
                allow = allowElement.InnerText,
                fifth = fifthElement.InnerText,
                Six = SixthElement.InnerText,
                Seven = SeventhElement.InnerText,
                eventName = eventNameElement.InnerText,
                eventPlace = eventPlaceElement.InnerText,
                eventPlaceCity = eventPlaceCityElement,
                eventDate = eventDateElement
            };



            return Ok(response);
        }
        string scan_in_success = @"<html>
  <body bgcolor='#FFFFFF'>
    <p align='center'>
      <b>
        <font face='Times New Roman' color=spv_font_color1 size='+20'>ORCHO <br>
          <font face='Times New Roman' color='#660033' size='5'> row: </font>H <br>
          <font face='Times New Roman' color='#660033' size='5'>seat: </font>22&nbsp;
      </b>
      </font>
      <br>
      <br>
      <b><font face='Times New Roman' color='#FF0000' size='+20'>Allow Entry</b>
      </font>
      <br>
      <br>
      <font face='Times New Roman' color='#FF007A' size='6'> (PH) </b></font>
      <br>
      <font face='Times New Roman' color='#000000' size='6'>The Phantom of the Opera</font>
      <br>
      <font face='Times New Roman' color='#000099' size='6'>Majestic Theatre</font>
      <br>
      <font face='Times New Roman' color='#000000' size='4'>NY City Area</font>
      <br>
      <font face='Times New Roman' color='#000099' size='6'>12/30/22 8:00 PM (E)</font>
      <br>
      <br>
      <b>
        <font face='Times New Roman' color=spv_font_color1 size='7'>SE UNKNOWN 11/29/22</b></font>
      <br>
      <b>
        <font face='Times New Roman' color=spv_font_color1 size='3'> (922749711995)
      </b>
      </font>
    </p>
</html>";
    }
}
