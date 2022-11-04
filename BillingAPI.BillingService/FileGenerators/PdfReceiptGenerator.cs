using BillingAPI.BillingService.Interfaces;
using BillingAPI.BillingService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace BillingAPI.BillingService.FileGenerators
{
    public class PdfReceiptGenerator
    {
        public byte[] GenerateReceipt(ReceiptDTO receipt)
        {
            StringBuilder htmlContent = BuildHtmlContent(receipt);

            var pdfDocument = PdfGenerator.GeneratePdf(
                htmlContent.ToString(),
                SetConfigurationOptions());


            MemoryStream stream = new MemoryStream();
            pdfDocument.Save(stream, false);

            return stream.ToArray();
        }

        private PdfGenerateConfig SetConfigurationOptions()
        {
            return new PdfGenerateConfig
            {
                PageOrientation = PdfSharpCore.PageOrientation.Portrait,
                PageSize = PdfSharpCore.PageSize.A4,
                MarginBottom = 20,
                MarginLeft = 20,
            };
        }
        private StringBuilder BuildHtmlContent(ReceiptDTO receipt)
        {
            StringBuilder htmlContent =
                new StringBuilder($"<!DOCTYPE html><html lang = \"en\"><head><style style='font-size:14px'></style></head><body>");

            htmlContent.Append("<p>" + receipt.CompanyName + "</p>");
            htmlContent.Append("<p>" + receipt.CompanyAddress + "</p>");
            htmlContent.Append("<p> Order number: " + receipt.OrderNumber + "</p>");
            htmlContent.Append("<p> Amount: " + receipt.Amount + "</p>");

            htmlContent.Append("</body></html>");
            return htmlContent;
        }
    }
}
