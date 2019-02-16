using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Diagnostics;
using PdfSharp.Drawing;
using PdfSharp;
using PdfSharp.Pdf;
using System.Windows.Forms;
using BAL;

namespace view
{
    class PdfMaker
    {
        List<string> data;
        PdfDocument document;
        string billNo;
        sellPipe sp;
        addData ad;
        Alert alert;
        public PdfMaker()
        {
            this.data = null;
            this.sp = null;
            ad = new addData();
            this.alert = new Alert();
        }

        public void createPdf(string billNo, sellPipe sp)
        {

            this.sp = sp;
            this.data = new List<string>();
            this.data.Add(this.sp.PipeName);//0
            this.data.Add(this.sp.PipeSize);//1
            this.data.Add(this.sp.Quantity);//2
            this.data.Add(this.sp.PricePerPipe);//3
            this.data.Add(this.sp.TotalPrice);//4
            this.data.Add(this.sp.CustomerName);//5
            this.data.Add(this.sp.CustomerPhoneNumber);//6
            this.data.Add(this.sp.CustomerEmail);//7
            this.data.Add(this.sp.PaidAmount);//8
            this.data.Add(this.sp.DueAmount);//9
            this.data.Add(this.sp.Discount);//10
            this.data.Add(this.sp.UserName);//11
            this.data.Add(this.sp.TotalPrize);//12
            this.data.Add(billNo);//13
            this.billNo = billNo;
            if (!this.ad.dataAddToDb(this.data))
            {
                MessageBox.Show("Check Internet Connection\n\nOr Report to admin");
            }
            else
            {
                MakePdf();
            }
           
        }

        public void MakePdf()
        {
            this.document = new PdfDocument();
            XPen line = new XPen(XColors.Black, 2);

            document.Info.Title = "Afsar & Sons pipeShop billNo: " + billNo;

            // Create an empty page

            PdfPage page = new PdfPage();
            page.Size = PageSize.A4;
            page = document.AddPage();

            // Get an XGraphics object for drawing

            XGraphics gfx = XGraphics.FromPdfPage(page);
            // Create a font
            XFont font = new XFont("Arial", 15, XFontStyle.BoldItalic);
            XFont fontH = new XFont("Arial", 12, XFontStyle.BoldItalic);
            XFont fontData = new XFont("Arial", 8, XFontStyle.Bold);
            XFont underLine = new XFont("Arial", 9, XFontStyle.Regular);
            // Draw the text.
            gfx.DrawString("Afsar & Sons pipeShop", font, XBrushes.Black,

          new XRect(page.Width / 4, 30, page.Width, 30),

           XStringFormats.Center);


            gfx.DrawString("Date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt"), underLine, XBrushes.Black,

                      new XRect((page.Width / 4) - 25, 45, page.Width, 45),

                       XStringFormats.Center);
            gfx.DrawString("Bill No: " + billNo, underLine, XBrushes.Red,

                      new XRect((page.Width / 4) - 35, 53, page.Width, 50),

                       XStringFormats.Center);

            gfx.DrawLine(line, 30, 95, page.Width - 30, 95);


            gfx.DrawString("Pipe Information:", fontH, XBrushes.Chocolate,

             new XRect(30, 110, page.Width, 110),

              XStringFormats.TopLeft);


            gfx.DrawLine(line, 30, 124, 140, 124);


            gfx.DrawString("Pipe Name:", fontData, XBrushes.Black,

          new XRect(40, 130, page.Width, 120),

           XStringFormats.TopLeft);

            gfx.DrawString(data[0], fontData, XBrushes.Black,

          new XRect(340, 130, page.Width, 120),

           XStringFormats.TopLeft);

            gfx.DrawString("Pipe Size:", fontData, XBrushes.Black,

          new XRect(40, 150, page.Width, 125),

           XStringFormats.TopLeft);

            gfx.DrawString(data[1], fontData, XBrushes.Black,

          new XRect(340, 150, page.Width, 120),

           XStringFormats.TopLeft);

            gfx.DrawString("Quantity:", fontData, XBrushes.Black,

          new XRect(40, 170, page.Width, 130),

           XStringFormats.TopLeft);

            gfx.DrawString(data[2], fontData, XBrushes.Black,

          new XRect(340, 170, page.Width, 120),

           XStringFormats.TopLeft);

            gfx.DrawString("Price Per unit:", fontData, XBrushes.Black,

          new XRect(40, 190, page.Width, 135),

           XStringFormats.TopLeft);

            gfx.DrawString(data[3], fontData, XBrushes.Black,

          new XRect(340, 190, page.Width, 120),

           XStringFormats.TopLeft);

            gfx.DrawString("Total Price:", fontData, XBrushes.Black,

          new XRect(40, 210, page.Width, 140),

           XStringFormats.TopLeft);

            gfx.DrawString(data[4], fontData, XBrushes.Black,

          new XRect(340, 210, page.Width, 120),

           XStringFormats.TopLeft);


            gfx.DrawString("Discount:", fontData, XBrushes.Black,

          new XRect(40, 230, page.Width, 140),

           XStringFormats.TopLeft);

            gfx.DrawString(data[10], fontData, XBrushes.Black,

          new XRect(340, 230, page.Width, 120),

           XStringFormats.TopLeft);


            gfx.DrawString("Amount To Pay:", fontData, XBrushes.Red,

         new XRect(40, 250, page.Width, 140),

          XStringFormats.TopLeft);

            gfx.DrawString(data[12], fontData, XBrushes.Red,

          new XRect(340, 250, page.Width, 120),

           XStringFormats.TopLeft);


            gfx.DrawString("Customer Information:", fontH, XBrushes.Chocolate,

             new XRect(30, 270, page.Width, 180),

              XStringFormats.TopLeft);


            gfx.DrawLine(line, 30, 284, 170, 284);

            gfx.DrawString("Customer Name:", fontData, XBrushes.Black,

              new XRect(40, 290, page.Width, 180),

               XStringFormats.TopLeft);

            gfx.DrawString(data[5], fontData, XBrushes.Black,

          new XRect(340, 290, page.Width, 120),

           XStringFormats.TopLeft);

            gfx.DrawString("Phone Number:", fontData, XBrushes.Black,

              new XRect(40, 310, page.Width, 185),

               XStringFormats.TopLeft);

            gfx.DrawString(data[6], fontData, XBrushes.Black,

          new XRect(340, 310, page.Width, 120),

           XStringFormats.TopLeft);

            gfx.DrawString("Email-address:", fontData, XBrushes.Black,

              new XRect(40, 330, page.Width, 190),

               XStringFormats.TopLeft);

            gfx.DrawString(data[7], fontData, XBrushes.Black,

          new XRect(340, 330, page.Width, 120),

           XStringFormats.TopLeft);
            gfx.DrawString("Amount paid:", fontData, XBrushes.Red,

              new XRect(40, 350, page.Width, 195),

               XStringFormats.TopLeft);

            gfx.DrawString(data[8], fontData, XBrushes.Red,

          new XRect(340, 350, page.Width, 120),

           XStringFormats.TopLeft);
            gfx.DrawString("Due Amount:", fontData, XBrushes.Red,

              new XRect(40, 370, page.Width, 180),

               XStringFormats.TopLeft);

            gfx.DrawString(data[9], fontData, XBrushes.Red,

          new XRect(340, 370, page.Width, 120),

           XStringFormats.TopLeft);

            gfx.DrawLine(line, 20, page.Height - 95, 100, page.Height - 95);

            gfx.DrawString("Signature by " + data[11], font, XBrushes.DarkCyan,

              new XRect(20, page.Height - 80, page.Width, 220),

               XStringFormats.TopLeft);


            // Save the document...

            string filename = "Bill_Report_"+billNo+".pdf";

            string path= Directory.GetCurrentDirectory();
            string pdfPath = path + "\\pdfs";

            try
            {
                if (!Directory.Exists(pdfPath))
                {
                    Directory.CreateDirectory(pdfPath);
                }
            }
            catch
            {
                this.alert.Show(alert.Error,"Folder creation error");
            }


            document.Save(pdfPath+"\\"+filename);


            // ...and start a viewer.
            Process.Start(pdfPath + "\\" + filename);
            // return filename;
        }
    }

}
