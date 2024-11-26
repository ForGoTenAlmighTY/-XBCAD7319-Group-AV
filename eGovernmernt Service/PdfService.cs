using eGovernmernt_Service.Models;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace eGovernmernt_Service
{
    public class PdfService
    {
        // Reusable PDF generation method
        private byte[] GeneratePdf<T>(IEnumerable<T> data, string title, string[] headers)
        {
            using var memoryStream = new MemoryStream();
            var writer = new PdfWriter(memoryStream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf, PageSize.A4.Rotate()); // Use landscape orientation for wide tables
            document.SetMargins(20, 20, 20, 20); // Adjust margins

            // Add Title
            document.Add(new Paragraph(title)
                .SetFontSize(18)
                .SetTextAlignment(TextAlignment.CENTER));

            // Create Table
            var table = new Table(UnitValue.CreatePercentArray(headers.Length)).UseAllAvailableWidth(); // Full-width table
            foreach (var header in headers)
            {
                table.AddHeaderCell(new Cell().Add(new Paragraph(header)));
            }

            foreach (var item in data)
            {
                foreach (var property in item.GetType().GetProperties())
                {
                    table.AddCell(new Cell().Add(new Paragraph(property.GetValue(item)?.ToString() ?? string.Empty)));
                }
            }

            // Add Table to Document
            document.Add(table);
            document.Close();

            return memoryStream.ToArray();
        }

        // Individual methods for specific tables
        public byte[] GenerateTrafficRegistrationApplicationsPdf(IEnumerable<TrafficRegistrationApplication> data)
        {
            return GeneratePdf(data, "Traffic Registration Applications Report",
                new[] { "Application ID", "ID Number", "Full Names", "Home Address", "Contact Number", "Copy Of Vehicle", "Proof Of Residence", "ID Copy", "Status" });
        }

        public byte[] GenerateTrafficLicenceApplicationsPdf(IEnumerable<TrafficLicenceApplication> data)
        {
            return GeneratePdf(data, "Traffic Licence Applications Report",
                new[] { "Application ID", "ID Number", "Full Names", "Home Address", "Contact Number", "Licence Type", "Proof Of Residence", "ID Copy", "Eye Test", "Status" });
        }

        public byte[] GenerateHealthApplicationsPdf(IEnumerable<HealthApplication> data)
        {
            return GeneratePdf(data, "Health Applications Report",
                new[] { "Application ID", "ID Number", "Full Names", "Home Address", "Contact Number", "Medical History", "Proof Of Residence", "ID Copy", "Status" });
        }

        public byte[] GenerateSocialApplicationsPdf(IEnumerable<SocialApplication> data)
        {
            return GeneratePdf(data, "Social Applications Report",
                new[] { "Application ID", "ID Number", "Full Names", "Type Of Grant", "Proof Of Income", "Proof Of Residence", "ID Copy","Status" });
        }

        public byte[] GenerateHomeAffairsApplicationsPdf(IEnumerable<HomeAffairsApplication> data)
        {
            return GeneratePdf(data, "Home Affairs Applications Report",
                new[] { "Application ID", "Full Names", "Type Of Application", "Home Address", "Contact Number", "Date Of Birth", "Birth Certificate", "Status" });
        }

    }
}
