﻿using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Globalization;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ShubertDataExtract.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class Scan2Controller : Controller
    {

        [HttpGet("scan_in_success")]
        public IActionResult ScanInSuccess()
        {

            var doc = new HtmlDocument();
            doc.LoadHtml(scan_in_success);

            var firstElement = Common.GetTextByXPath(doc, "/html/body/p/b[1]/font/text()[1]");
            var rowElement = Common.GetTextByXPath(doc, "/html/body/p/b[1]/font/text()[3]");
            var seatElement = Common.GetTextByXPath(doc, "/html/body/p/b[1]/font/text()[5]");
            var Seat = new String(seatElement?.Where(Char.IsDigit).ToArray());
            var allowElement = Common.GetTextByXPath(doc, "/html/body/p/b[2]/font");
            var fifthElement = Common.GetTextByXPath(doc, "/html/body/p/font[1]");
            var SixthElement = Common.GetTextByXPath(doc, "/html/body/p/font[2]");
            var SeventhElement = Common.GetTextByXPath(doc, "/html/body/p/font[3]");
            var eventNameElement = Common.GetTextByXPath(doc, "/html/body/p/font[4]");
            var eventPlaceElement = Common.GetTextByXPath(doc, "/html/body/p/font[5]");
            var eventPlaceandDate = doc.DocumentNode.SelectSingleNode("/html/body/p/b[3]/font");
            var eventPlaceCityElement = Regex.Match(eventPlaceandDate.InnerHtml, @"([A-Za-z ])+([A-Za-z ]+)").Value;
            string stringWithDate = eventPlaceandDate.InnerHtml;
            Match match = Regex.Match(stringWithDate, @"\d{2}\/\d{2}\/\d{2}");
            string date = match.Value;
            var dateTime = DateTime.ParseExact(date, "MM/dd/yy", CultureInfo.CurrentCulture);


            var eventDateElement = dateTime.ToString();

            var response = new
            {
                first = firstElement,
                row = rowElement,
                seat = Seat,
                allow = allowElement,
                fifth = fifthElement,
                Six = SixthElement,
                Seven = SeventhElement,
                eventName = eventNameElement,
                eventPlace = eventPlaceElement,
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

        //view info
        [HttpGet("success_response")]
        public IActionResult Success_Response()
        {

            var doc = new HtmlDocument();
            doc.OptionAutoCloseOnEnd = true;
            doc.LoadHtml(success_response);

            var Title = Common.GetTextByXPath(doc, "/html/body/p/b[1]/font");
            string toGreyArea = "/html/body/table[1]/td/p[1]/font[1]";
            var rowElement = Common.GetTextByXPath(doc, xPath: $"{toGreyArea}/text()[1]");
            var VenueElement = Common.GetTextByXPath(doc, xPath: $"{toGreyArea}/text()[2]");
            var LocationElement = Common.GetTextByXPath(doc, xPath: $"{toGreyArea}/text()[3]");
            var DateandTimeElement = Common.GetTextByXPath(doc, xPath: $"{toGreyArea}/text()[4]");
            var TicketsElement = Common.GetTextByXPath(doc, xPath: $"{toGreyArea}/text()[5]");
            var OrderElement = Common.GetTextByXPath(doc, xPath: $"{toGreyArea}/text()[6]");
            var PurchasedOnElement = Common.GetTextByXPath(doc, xPath: $"{toGreyArea}/text()[7]");
            var NinthElement = Common.GetTextByXPath(doc, "//font[2]/text()");
            var ScannedInElement = Common.GetTextByXPath(doc, "/html/body/table[2]/td/p[1]/font[1]/text()[1]");
            var EventPlaceandDate = Common.GetTextByXPath(doc, "//font[@size=4]");
            var FourteenthElement = Common.GetTextByXPath(doc, "//b[1]/text()");
            var Fifteenth = Common.GetTextByXPath(doc, "//font[3]/font[1]/text()");
            var Sixteenth = Common.GetTextByXPath(doc, "//font[2]/text()");
            var Topic = Sixteenth?.Split(' ').Where(x => string.Equals(x, x.ToUpper()));
            var Row = new String(Sixteenth?.Where(Char.IsLetter).ToArray());
            var SeatNo = new String(Sixteenth?.Where(Char.IsDigit).ToArray());
            var Seventeenth = Common.GetTextByXPath(doc, "//font[3]/text()");
            var LastPart = Common.GetTextByXPath(doc, "//*").Split('(').Where(x => !string.IsNullOrWhiteSpace(x)).LastOrDefault().Split(')');

            var response = new
            {
                first = Title,
                row = rowElement,
                venue = VenueElement,
                location = LocationElement,
                dateandTime = DateandTimeElement,
                tickets = TicketsElement,
                order = OrderElement,
                purchasedOn = PurchasedOnElement,
                NinthElementn = NinthElement,
                scannedInElement = ScannedInElement,
                eventPlaceandDate = EventPlaceandDate,
                fourteen = FourteenthElement,
                fifteenth = Fifteenth,
                topic = Topic,
                seatNo = SeatNo,
                rowElement = rowElement,
                seventeenth = Seventeenth,
                lastPart = LastPart

            };



            return Ok(response);
        }
        string success_response = "<html><body bgcolor='#FFFFFF'><p align='center'><br><b><font face='Times New Roman' color='#000000' size='7'>Javier Molina Test</b></font></br><table bgcolor='#E0E0E0' width='541'><td><p align='center'><font face='Times New Roman' color='#000000' size='2'>The Phantom of the Opera<br>Majestic Theatre<br>NY City Area<br>12/30/2022 8:00 PM (E)<br><i>Tickets:</i> 2&nbsp;&nbsp;&nbsp;&nbsp;<i> Order: </i>90857490<br><i>Purchased:</i> 11/29/2022 08:46  (M1)</font></td></table><b><font face='Times New Roman' color='#000000' size='7'>ORCHO  row:H seat:22</b></font><table bgcolor='#FF0000' width='541'><td><p align='center'><font face='Times New Roman' color='#FFFFFF' size='8'>Scanned In &nbsp;12/22/2022 03:24</font></td></table><b><font face='Times New Roman' color='#000000' size='4'><br><br>SE UNKNOWN 11/29/22</b></font><br><font color='#AA0077'><b>PH</b></font><br><font face='Times New Roman' color='#000000' size='3'><font face='Times New Roman' color='#000000' size='3'><br>WESTWOOD, NJ</font><br><br><font face='Times New Roman' color='#000000' size='3'>ORCHO row:H seats:24</font><br><font face='Times New Roman' color='#FF0000' size='2'>Scanned In&nbsp; 12/01/2022 14:00</b></font><br><br><b><font face='Times New Roman' color='#000000' size='2'>(922749711995)</b></font></p></html>";

        //view info
        [HttpGet("error_denied_response")]
        public IActionResult errorDenied_response()
        {
            var doc1 = new HtmlDocument();
            doc1.OptionAutoCloseOnEnd = true;
            doc1.LoadHtml(error_denied_response1);

            var doc2 = new HtmlDocument();
            doc2.OptionAutoCloseOnEnd = true;
            doc2.LoadHtml(error_denied_response2);
            var Response = Common.GetTextByXPath(doc1, "/html/body/p/b[1]/font");
            var Message = Common.GetTextByXPath(doc1, "/html/body/p/b[2]/font");
            var Purpose = Common.GetTextByXPath(doc2, "/html/body/p/b/font");
            var Title = Common.GetTextByXPath(doc2, "//table[@bgcolor='#E0E0E0']/td/p/font/text()[1]");
            var Venue = Common.GetTextByXPath(doc2, "//table[@bgcolor='#E0E0E0']/td/p/font/text()[2]");
            var Location = Common.GetTextByXPath(doc2, "//table[@bgcolor='#E0E0E0']/td/p/font/text()[3]");
            var DateandTime = Common.GetTextByXPath(doc2, "//table[@bgcolor='#E0E0E0']/td/p/font/text()[4]");
            var Tickets = Common.GetTextByXPath(doc2, "//table[@bgcolor='#E0E0E0']/td/p/font/text()[5]");
            var Order = Common.GetTextByXPath(doc2, "//table[@bgcolor='#E0E0E0']/td/p/font/text()[6]");
            var Purchased = Common.GetTextByXPath(doc2, "//table[@bgcolor='#E0E0E0']/td/p/font/text()[7]");
            var TenthElement = Common.GetTextByXPath(doc2, "//font[2]/text()");
            var EleventhElement = Common.GetTextByXPath(doc2, "//font[@color='#FFFFFF']");
            var TwelvethElement = Common.GetTextByXPath(doc2, "//font[@size='4']");
            var ThirteenthElement = Common.GetTextByXPath(doc2, "//font[@size='3']/text()[1]");
            var FourthteenthElement = Common.GetTextByXPath(doc2, "//font[@size='3']/font[2]/text()[1]");
            var CreatedBy = Common.GetTextByXPath(doc2, "//font[@color='#FF0000']/text()[1]");
            var SixteenthElement = Common.GetTextByXPath(doc2, "//b[1]/text()[1]");
            var LastPart = Common.GetTextByXPath(doc2, "//*").Split('(').Where(x => !string.IsNullOrWhiteSpace(x)).LastOrDefault().Split(')');



            var response = new
            {
                response = Response,
                message = Message,
                purpose = Purpose,
                title = Title,
                venue = Venue,
                location = Location,
                dateandTime = DateandTime,
                tickets = Tickets,
                order = Order,
                purchased = Purchased,
                tenthElement = TenthElement,
                eleventhElement= EleventhElement,
                twelvethElement= TwelvethElement,
                thirteenthElement= ThirteenthElement,
                fourthteenthElement= FourthteenthElement,
                createdBy = CreatedBy,
                sixteenthElement = SixteenthElement,
                lastPart = LastPart
            };
            return Ok(response);
        }
        string error_denied_response1 = "<html><body bgcolor='#FF0000'><p align='center'><b><font face='Times New Roman' color='#FFFFFF' size='8'>Denied<br><br></b></font><b><font face='Times New Roman' color='#FFFFFF' size='5'>Wrong Performance</font></b></p></html>";
        string error_denied_response2 = "<html><body bgcolor='#FFFFFF'><p align='center'><br><b><font face='Times New Roman' color='#000000' size='7'>Security Check</b</font></br><table bgcolor='#E0E0E0' width='541'><td><p align='center'><font face='Times New Roman' color='#000000' size='2'>The Phantom of the Opera<br>Majestic Theatre<br>NY City Area<br>12/30/2022  8:00 PM (E   )<br><i>Tickets:</i> 2&nbsp;&nbsp;&nbsp;&nbsp;<i> Order: </i>90857494<br><i>Purchased:</i> 11/29/2022 09:41  (M1)</font></td></table><b><font face='Times New Roman' color='#000000' size='7'>ORCHO  row:G seat:15</b></font><table bgcolor='#FF0000' width='541'><td><p align='center'><font face='Times New Roman' color='#FFFFFF' size='8'>Scanned In    &nbsp;12/14/2022 13:53</font></td></table><b><font face='Times New Roman' color='#000000' size='4'><br><br>SE UNKNOWN 11/29/22</b></font><br><font color='#AA0077'><b>PH</b></font><br><font face='Times New Roman' color='#000000' size='3'><font face='Times New Roman' color='#000000' size='3'><br>WESTWOOD, NJ</font><br><br><font face='Times New Roman' color='#000000' size='3'>ORCHO row:G  seats:13</font><br><font face='Times New Roman' color='#FF0000' size='2'>Created&nbsp; 11/29/2022 09:41</b></font><br><br><b><font face='Times New Roman' color='#000000' size='2'>(964294719296)</b></font></p></html>";

    }
}