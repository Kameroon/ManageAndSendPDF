using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MediaPerf.SendPdf.Models.Models;
using MediaPerf.SendPdf.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPerf.SendPdf.Service.Manager
{
    public class PDFManager
    {

        private static Report _report = new Report();

        // -- https://forums.asp.net/t/2000508.aspx?Pdf+File+Creation+itextsharp+multiple+user+at+sametime --
        // -- https://www.codeproject.com/Articles/691723/Csharp-Generate-and-Deliver-PDF-Files-On-Demand-fr --
        // -- 
        // https://stackoverflow.com/questions/2321526/pdfptable-as-a-header-in-itextsharp  --

        /// <summary>
        /// -- async --
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="repositoryPath"></param>
        public static bool CreatePDF(string repositoryPath)
        {
            var authors = ConsolidateHelper.GetAuthors();
            var dataTable = DataTableHelper.ToDataTable<Author>(authors);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            bool result = false;

            //PdfWriter masterWriter = null;
            string fileName = string.Empty;
            DateTime fileCreationDatetime = DateTime.Now;
            string imgPath = "https://ftp.mediaperf.com/img/logo.gif";

            imgPath = @"C:\Users\Sweet Family\Desktop\logo.jpg";

            var widthPercentage = 96;

            fileName = string.Format("{0}.pdf", fileCreationDatetime.ToString(@"yyyyMMdd") + "_" + fileCreationDatetime.ToString(@"HHmmss" + ".pdf"));
            string fullPdfPath = repositoryPath + fileName;

            if (File.Exists(fullPdfPath))
            {
                File.Delete(fullPdfPath);
            }

            try
            {
                FileStream masterStream = new FileStream(fullPdfPath, FileMode.Create);
                using (Document masterDocument = new Document(PageSize.A4, 8, 8, 35, 10))
                using (PdfWriter masterWriter = PdfWriter.GetInstance(masterDocument, masterStream))
                {
                    //masterWriter.PageEvent = new MyPageHeader();

                    masterDocument.Open();

                    PdfContentByte pdfContentByte = masterWriter.DirectContent;
                    Chunk verticalPositionMark = new Chunk(new VerticalPositionMark());
                    var lineSeparator = new LineSeparator(2.0F, 96.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1);

                    #region -- Define fonts --
                    BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, false);
                    var noneFontOneWhite = new Font(baseFont, 1, Font.NORMAL, BaseColor.WHITE);
                    var boldFontEleventBlack = new Font(baseFont, 11, Font.BOLD, BaseColor.BLACK);
                    var normalFontEleventBlack = new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK);
                    var boldFontNineBlack = new Font(baseFont, 9, Font.BOLD, BaseColor.BLACK);
                    var normalFontNimeBlack = new Font(baseFont, 9, Font.NORMAL, BaseColor.BLACK);
                    var boldFontTwelveBlack = new Font(baseFont, 12, Font.BOLD, BaseColor.BLACK);
                    var normalFontTwelveBlack = new Font(baseFont, 12, Font.NORMAL, BaseColor.BLACK);
                    var boldFontTenBlack = new Font(baseFont, 10, Font.BOLD, BaseColor.BLACK);
                    var normalFontTenBlack = new Font(baseFont, 10, Font.NORMAL, BaseColor.BLACK);
                    //var TableFontmini_ARBold8 = FontFactory.GetFont("Calibri", 8, Font.BOLDITALIC, BaseColor.BLACK);
                    #endregion

                    // -- Populate dynamycs feelds --
                    _report = ConsolidateHelper.ConsolidateReport(dataTable);

                    #region -- Set Header --.
                    Image imagePath = Image.GetInstance(imgPath);
                    imagePath.ScalePercent(80f);

                    PdfPTable mtable = new PdfPTable(2);
                    mtable.WidthPercentage = 100;
                    mtable.DefaultCell.Border = Rectangle.NO_BORDER;

                    PdfPTable table = new PdfPTable(5);
                    table.WidthPercentage = 30;
                    table.TotalWidth = 30;
                    PdfPCell cell = new PdfPCell(new Phrase("2"));
                    cell.Colspan = 6;
                    //cell.Rowspan = 2;
                    cell.HorizontalAlignment = 1;
                    Paragraph para = new Paragraph();
                    para.Add(new Chunk(imagePath, 5, -40));
                    cell.AddElement(para);
                    cell.BorderColor = BaseColor.WHITE;
                    table.AddCell(cell);
                    mtable.AddCell(table);

                    table = new PdfPTable(3);
                    table.WidthPercentage = 70;
                    cell = new PdfPCell(new Phrase("Relevé de redévences", new Font(Font.FontFamily.TIMES_ROMAN,
                                        20, Font.BOLD, BaseColor.BLACK)));
                    cell.Colspan = 2;
                    cell.Padding = 2;
                    cell.PaddingTop = 5;
                    cell.PaddingBottom = 5;
                    cell.HorizontalAlignment = 1;
                    table.AddCell(cell);
                    table.AddCell(new PdfPCell(new Phrase($"              Page \n\r               { _report.CurrentPage }/{ _report.TotalPageNumber }",
                                               new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.NORMAL, BaseColor.BLACK))));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell = new PdfPCell(new Phrase("Ceci n'est pas une facture", new Font(Font.FontFamily.TIMES_ROMAN, 12,
                                                   Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.MinimumHeight = 18;
                    cell.PaddingBottom = 2;
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    mtable.AddCell(table);
                    masterDocument.Add(mtable);

                    #endregion

                    #region -- Duplicata --
                    var docFooter = new Paragraph();
                    docFooter.Font = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 42f, BaseColor.LIGHT_GRAY);
                    docFooter.Add("\n\r");
                    docFooter.Add(new Chunk("DUPLICATA"));
                    docFooter.Alignment = Element.ALIGN_RIGHT;
                    masterDocument.Add(docFooter);
                    #endregion

                    #region -- Set first line  --
                    #region -- Left --
                    PdfPCell pdfPCell = new PdfPCell();
                    PdfPTable table0 = new PdfPTable(2);
                    table0 = new PdfPTable(1);
                    table0.TotalWidth = 165;

                    pdfPCell = new PdfPCell();
                    Paragraph numRdvParagraph = new Paragraph();
                    numRdvParagraph.Add(new Phrase("   N° RdR", boldFontEleventBlack));
                    numRdvParagraph.Add(new Chunk($"     { _report.RoyaltyFeeNumber } ", normalFontEleventBlack));
                    pdfPCell.AddElement(numRdvParagraph);
                    pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    pdfPCell.VerticalAlignment = Element.ALIGN_CENTER;
                    table0.AddCell(pdfPCell);
                    table0.WriteSelectedRows(0, -1, 20, 670, pdfContentByte);

                    pdfPCell = new PdfPCell();
                    Paragraph dateParagraph = new Paragraph();
                    dateParagraph.Add(new Phrase("   Date", boldFontEleventBlack));
                    dateParagraph.Add(new Chunk($"     { _report.CurrentDate } ", normalFontEleventBlack));
                    pdfPCell.AddElement(dateParagraph);
                    pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table0.AddCell(dateParagraph);

                    PdfPTable headerRightTable = new PdfPTable(2);
                    headerRightTable = new PdfPTable(1);
                    headerRightTable.TotalWidth = 165;
                    headerRightTable.AddCell(pdfPCell);
                    headerRightTable.WriteSelectedRows(0, -1, 200, 670, pdfContentByte);
                    #endregion

                    #region -- Destinataire Right --
                    PdfPTable headerRightSubTable = new PdfPTable(1);
                    headerRightSubTable.TotalWidth = 190;
                    PdfPTable sTable30 = new PdfPTable(2);
                    pdfPCell = new PdfPCell();
                    pdfPCell.MinimumHeight = 40;
                    pdfPCell.Padding = 5;
                    Paragraph paragraph30 = new Paragraph
                        {
                            new Phrase("Destinataire\n", boldFontEleventBlack)
                        };
                    pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pdfPCell.AddElement(paragraph30);
                    sTable30.AddCell(pdfPCell);
                    headerRightSubTable.AddCell(paragraph30);

                    pdfPCell = new PdfPCell();
                    paragraph30 = new Paragraph();
                    paragraph30.Add(new Phrase($"\n\n{ _report.Destinataire } \n\n", normalFontEleventBlack));
                    paragraph30.Add(new Phrase($"{ _report.Prestataire } \n\n", normalFontEleventBlack));
                    paragraph30.Add(new Phrase($"{ _report.AdressePrestataire } \n\n\n\n\n\n\n\n", normalFontEleventBlack));
                    pdfPCell.AddElement(paragraph30);
                    paragraph30.SetLeading(2.8f, 1.2f);
                    sTable30.AddCell(pdfPCell);
                    headerRightSubTable.AddCell(paragraph30);
                    headerRightSubTable.WriteSelectedRows(0, -1, 385, 670, pdfContentByte);
                    #endregion

                    #endregion

                    #region -- Second line --
                    PdfPTable table100 = new PdfPTable(1);
                    table100.TotalWidth = 345f;

                    PdfPTable sTable = new PdfPTable(2);
                    pdfPCell = new PdfPCell();
                    Paragraph paragraph = new Paragraph
                        {
                            new Phrase($"{ _report.BfpParam1 } \n\n", boldFontEleventBlack),
                            new Phrase($"{ _report.BfpParam2 } \n\n\n\n", boldFontEleventBlack)
                        };
                    pdfPCell.AddElement(paragraph);
                    sTable.AddCell(pdfPCell);
                    table100.AddCell(paragraph);

                    pdfPCell = new PdfPCell();
                    paragraph = new Paragraph
                        {
                            new Phrase($"\n Nombre de Pv                { _report.CountPv } \n\r", normalFontEleventBlack),
                            new Phrase($" Nombre de Campagne          { _report.CountCampagne } \n\n", normalFontEleventBlack)
                        };
                    pdfPCell.AddElement(paragraph);
                    sTable.AddCell(pdfPCell);
                    table100.AddCell(paragraph);
                    paragraph.SetLeading(5.8f, 5.2f);
                    table100.WriteSelectedRows(0, -1, 20, 645, pdfContentByte);
                    #endregion

                    // -- Draw horizontal line. --
                    Paragraph firstLineSeparator = new Paragraph(new Chunk(lineSeparator));
                    firstLineSeparator.SpacingBefore = 210f;
                    masterDocument.Add(firstLineSeparator);

                    //// -- Trait de fin de prémière page --
                    //PdfContentByte contentByte = pdfContentByte;
                    //contentByte.SetLineWidth(3);
                    //contentByte.MoveTo(22, 14);
                    //contentByte.LineTo(masterDocument.PageSize.Width - 22, 14);
                    //contentByte.SetColorStroke(BaseColor.BLACK);
                    //contentByte.Stroke();

                    #region -- Generate Grid --
                    PdfPTable masterTable = new PdfPTable(dataTable.Columns.Count);
                    PdfPTable objectTable = new PdfPTable(dataTable.Columns.Count);
                    //objectTable.WidthPercentage = masterDocument.PageSize.Width - masterDocument.LeftMargin - masterDocument.RightMargin;
                    objectTable.WidthPercentage = widthPercentage;

                    // -- Add Header row for every page --
                    objectTable.HeaderRows = 1;

                    // -- Set spacing between gridView and  --
                    objectTable.SpacingBefore = 7f;

                    List<string> columnHeaders = new List<string>();

                    // -- Set columns widths --
                    float[] widths = new float[] { 12f, 12f, 60f, 12f, 30f };
                    objectTable.SetWidths(widths);

                    // -- Get columnHeaders names in the pdf file --
                    for (int column = 0; column < dataTable.Columns.Count; column++)
                    {
                        PdfPCell pdfColumnCell = new PdfPCell(new Phrase(
                                dataTable.Columns[column].ColumnName)
                            );

                        pdfColumnCell.MinimumHeight = 25;
                        pdfColumnCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                        var columnName = pdfColumnCell.Phrase[0].ToString();
                        columnHeaders.Add(columnName);

                        SetColumnAlignment(dataTable, pdfColumnCell, column);
                        pdfColumnCell.BackgroundColor = BaseColor.WHITE;

                        objectTable.AddCell(pdfColumnCell);
                    }

                    //var _objectTable = new PdfPTable(columnHeaders.Count) { WidthPercentage = 100 };
                    //_objectTable.SetWidths(GetHeaderWidths(boldFontEleventBlack, columnHeaders.ToArray()));

                    // -- Add values of DataTable in pdf file --
                    for (int row = 0; row < dataTable.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataTable.Columns.Count; col++)
                        {
                            PdfPCell pdfColumnCell = new PdfPCell(new Phrase(
                                    dataTable.Rows[row][col].ToString(),
                                    normalFontTenBlack)
                                );

                            // -- Align the pdfColumnCell in the center --
                            SetColumnAlignment(dataTable, pdfColumnCell, col);

                            objectTable.AddCell(pdfColumnCell);
                        }
                    }

                    masterDocument.Add(objectTable);

                    //PdfPCell masterCell = new PdfPCell(objectTable);
                    //masterTable.AddCell(masterCell);
                    ///
                    //masterTable.WriteSelectedRows(0, -1, 500, 300, pdfContentByte);
                    #endregion

                    #region -- Resume --
                    PdfPTable billanTable = new PdfPTable(1);
                    billanTable.WidthPercentage = widthPercentage;

                    PdfPCell billanCell = new PdfPCell();
                    string billan = $"1498 - { _report.Destinataire } - { _report.Destinataire }      Total   39       533,34";
                    billanCell = new PdfPCell(new Phrase(billan, boldFontTwelveBlack));
                    billanCell.BackgroundColor = BaseColor.LIGHT_GRAY; ;
                    billanCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    billanCell.MinimumHeight = 25;
                    billanTable.AddCell(billanCell);
                    masterDocument.Add(billanTable);
                    #endregion

                    #region -- Total --
                    PdfPTable royalFeeTotalMasterTable = new PdfPTable(1);
                    royalFeeTotalMasterTable.TotalWidth = 400;
                    PdfPTable royalFeeTotalTable = new PdfPTable(4);

                    PdfPCell firstRoyalFeeTotalCell = new PdfPCell(new Phrase("NOT Texte here", noneFontOneWhite));
                    firstRoyalFeeTotalCell.BorderColor = BaseColor.WHITE;
                    firstRoyalFeeTotalCell.Border = 5;
                    royalFeeTotalTable.AddCell(firstRoyalFeeTotalCell);
                    PdfPCell royalFeeTotalCell = new PdfPCell(new Phrase("HT", boldFontNineBlack));
                    royalFeeTotalCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    royalFeeTotalCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    royalFeeTotalCell.MinimumHeight = 20;
                    royalFeeTotalTable.AddCell(royalFeeTotalCell);

                    royalFeeTotalCell = new PdfPCell(new Phrase("TVA 20,00", boldFontNineBlack));
                    royalFeeTotalCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    royalFeeTotalCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    royalFeeTotalCell.MinimumHeight = 20;
                    royalFeeTotalTable.AddCell(royalFeeTotalCell);

                    royalFeeTotalCell = new PdfPCell(new Phrase("TTC", boldFontNineBlack));
                    royalFeeTotalCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    royalFeeTotalCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    royalFeeTotalCell.MinimumHeight = 20;
                    royalFeeTotalTable.AddCell(royalFeeTotalCell);

                    royalFeeTotalCell = new PdfPCell(new Phrase("Total du relevé", boldFontNineBlack));
                    royalFeeTotalCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    royalFeeTotalCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    royalFeeTotalCell.MinimumHeight = 20;
                    royalFeeTotalTable.AddCell(royalFeeTotalCell);

                    royalFeeTotalCell = new PdfPCell(new Phrase($"{ _report.HtMontant }", boldFontNineBlack));
                    royalFeeTotalCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    royalFeeTotalCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    royalFeeTotalCell.MinimumHeight = 20;
                    royalFeeTotalTable.AddCell(royalFeeTotalCell);

                    royalFeeTotalCell = new PdfPCell(new Phrase($"{ _report.TVA }", boldFontNineBlack));
                    royalFeeTotalCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    royalFeeTotalCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    royalFeeTotalCell.MinimumHeight = 20;
                    royalFeeTotalTable.AddCell(royalFeeTotalCell);

                    royalFeeTotalCell = new PdfPCell(new Phrase($"{ _report.TTCMontant }", boldFontNineBlack));
                    royalFeeTotalCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    royalFeeTotalCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    royalFeeTotalCell.MinimumHeight = 20;
                    royalFeeTotalTable.AddCell(royalFeeTotalCell);

                    PdfPCell royalFeeTotalMasterCell = new PdfPCell(royalFeeTotalTable);

                    royalFeeTotalMasterTable.AddCell(royalFeeTotalMasterCell);
                    royalFeeTotalMasterTable.WriteSelectedRows(0, -1, 17, 122, pdfContentByte);
                    #endregion

                    // -- Draw horizontal line. --
                    Paragraph secondLineSeparator = new Paragraph(new Chunk(lineSeparator));
                    secondLineSeparator.SpacingBefore = 15f;
                    masterDocument.Add(secondLineSeparator);

                    //// -- Define footer --
                    OnEndPage(masterWriter, masterDocument, baseFont);

                    masterDocument.Close();
                    masterStream.Close();
                    masterWriter.Close();

                    result = true;

                    stopwatch.Stop();
                    TimeSpan stopwatchElapsed = stopwatch.Elapsed;
                    //Console.WriteLine("TEMPS MIS pour générer un PDF " + Convert.ToInt32(stopwatchElapsed.TotalMilliseconds));

                    Process.Start(fullPdfPath);
                }
            }
            catch (DocumentException de)
            {
                result = false;
                throw de;
            }
            catch (IOException ioe)
            {
                result = false;
                throw ioe;
            }
            catch (Exception exception)
            {
                result = false;
                Console.WriteLine(exception.ToString());
            }
            finally
            {

            }

            return result;
        }

        #region -- Set Header and Footer --
        /// <summary>
        /// -- OK --
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        /// <param name="baseFont"></param>
        public static void OnEndPage(PdfWriter writer, Document document, BaseFont baseFont)
        {
            PdfPTable endPageTable = new PdfPTable(1);

            endPageTable.TotalWidth = 70;
            endPageTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            PdfPTable table2 = new PdfPTable(1);

            List<string> footerTextList = new List<string>()
            {
                "5 quai de Dion Bouton - 92816 Puteaux Cedex",
                "Tél 01 40 99 21 21 - Fax 01 40 99 80 30",
                "Société Anonyme au capital de 555.112.61€",
                "R.C. Nantère B 332 403 997 - TVA Intra FR1133240397",
                "Mediaperfomances"
            };

            PdfPCell cell2 = null;
            foreach (var footerText in footerTextList)
            {
                cell2 = new PdfPCell(new Phrase(footerText, new Font(baseFont, 8, Font.NORMAL, BaseColor.BLACK)));
                cell2.Border = Rectangle.NO_BORDER;
                table2.AddCell(cell2);
            }

            table2.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPCell cell = new PdfPCell(table2);
            cell.BorderColor = BaseColor.WHITE;
            endPageTable.AddCell(cell);
            endPageTable.WriteSelectedRows(0, -1, 15, 70, writer.DirectContent);
        }

        /// <summary>
        /// !!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        public static void OnHeaderPage(PdfWriter writer, Document document)
        {
            PdfContentByte pdfContentByte = writer.DirectContent;
            ColumnText ct = new ColumnText(pdfContentByte);

            pdfContentByte.BeginText();
            pdfContentByte.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12.0f);
            pdfContentByte.SetTextMatrix(document.LeftMargin, document.PageSize.Height - document.TopMargin);

            pdfContentByte.ShowText("5 quai de Dion Bouton - 92816 Puteaux Cedex \n\r");
        }
        #endregion

        #region Methods
        /// <summary>
        /// -- 
        /// </summary>
        /// <param name="font"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        private static float[] GetHeaderWidths(Font font, params string[] headers)
        {
            var total = 0;
            var columns = headers.Length;
            var widths = new int[columns];
            for (var i = 0; i < columns; ++i)
            {
                var w = font.GetCalculatedBaseFont(true).GetWidth(headers[i]);
                total += w;
                widths[i] = w;
            }
            var result = new float[columns];
            for (var i = 0; i < columns; ++i)
            {
                result[i] = (float)widths[i] / total * 100;
            }
            return result;
        }

        private static void SetColumnAlignment(DataTable dataTable, PdfPCell cell, int i)
        {
            if (dataTable.Columns[i].DataType == typeof(string))
            {
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            }
            if (dataTable.Columns[i].DataType == typeof(Boolean))
            {
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            }
            if (dataTable.Columns[i].DataType == typeof(DateTime))
            {
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            }
            if (dataTable.Columns[i].DataType == typeof(int))
            {
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            }
        }

        private static PdfPCell GetCell(string text)
        {
            return GetCell(text, 1, 1);
        }

        private static PdfPCell GetCell(string text, int colSpan, int rowSpan)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text));
            cell.HorizontalAlignment = 1;
            cell.Rowspan = rowSpan;
            cell.Colspan = colSpan;

            return cell;
        }

        #endregion
    }

}
