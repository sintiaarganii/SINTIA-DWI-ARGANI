using DinkToPdf;
using DinkToPdf.Contracts;
using SINTIA_DWI_ARGANI.Models.DTO;
using System;

namespace SINTIA_DWI_ARGANI.Services
{
    public interface IPdfService
    {
        byte[] GenerateOrderReceipt(OrderDTO order);
    }

    public class PdfService : IPdfService
    {
        private readonly IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GenerateOrderReceipt(OrderDTO order)
        {
            var htmlContent = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; }}
                    .receipt {{ width: 300px; margin: 0 auto; }}
                    .header {{ text-align: center; margin-bottom: 15px; }}
                    .title {{ font-size: 20px; font-weight: bold; }}
                    .info {{ margin-bottom: 10px; }}
                    .table {{ width: 100%; border-collapse: collapse; margin-bottom: 15px; }}
                    .table th, .table td {{ padding: 5px; border-bottom: 1px dashed #ddd; }}
                    .total {{ font-weight: bold; text-align: right; }}
                    .footer {{ text-align: center; margin-top: 20px; font-size: 12px; }}
                </style>
            </head>
            <body>
                <div class='receipt'>
                    <div class='header'>
                        <div class='title'>TOKO SINAR ABADI</div>
                        <div>Jl. Contoh No. 123, Jakarta</div>
                        <div>Telp: 021-12345678</div>
                    </div>
                    
                    <div class='info'>
                        <div>No. Order: {order.Id}</div>
                        <div>Tanggal: {order.OrderDate:dd/MM/yyyy HH:mm}</div>
                        <div>Kasir: {order.CashierName}</div>
                    </div>
                    
                    <table class='table'>
                        <tr>
                            <th>Item</th>
                            <th>Qty</th>
                            <th>Harga</th>
                            <th>Subtotal</th>
                        </tr>
                        <tr>
                            <td>{order.ProductName}</td>
                            <td>{order.Quantity}</td>
                            <td>{(order.Total / order.Quantity):C}</td>
                            <td>{order.Total:C}</td>
                        </tr>
                    </table>
                    
                    <div class='total'>TOTAL: {order.Total:C}</div>
                    
                    <div class='footer'>
                        <div>Terima kasih telah berbelanja</div>
                        <div>Barang yang sudah dibeli tidak dapat ditukar/dikembalikan</div>
                    </div>
                </div>
            </body>
            </html>";

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.Custom,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                DocumentTitle = $"Struk_Order_{order.Id}"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontSize = 9, Right = "Halaman [page] dari [toPage]", Line = true },
                FooterSettings = { FontSize = 9, Line = true, Center = "Dicetak pada [date] [time]" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return _converter.Convert(pdf);
        }
    }
}