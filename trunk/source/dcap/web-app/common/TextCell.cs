using System;
using DocumentFormat.OpenXml.Spreadsheet;

namespace web_app.common
{
    public class TextCell : Cell
    {
        public TextCell(string header, string text, int index)
        {
            this.DataType = CellValues.InlineString;
            this.CellReference = header + index;
            //Add text to the text cell.
            this.InlineString = new InlineString { Text = new Text { Text = text } };
        }
    }
    public class NumberCell : Cell
    {
        public NumberCell(string header, string text, int index)
        {
            this.DataType = CellValues.Number;
            this.CellReference = header + index;
            this.CellValue = new CellValue(text);
        }
    }
    public class FormatedNumberCell : NumberCell
    {
        public FormatedNumberCell(string header, string text, int index)
            : base(header, text, index)
        {
            this.StyleIndex = 2;
        }
    }
    public class DateCell : Cell
    {
        public DateCell(string header, DateTime dateTime, int index)
        {
            this.DataType = CellValues.Date;
            this.CellReference = header + index;
            this.StyleIndex = 1;
            this.CellValue = new CellValue { Text = dateTime.ToOADate().ToString() }; ;
        }
    }
    public class FomulaCell : Cell
    {
        public FomulaCell(string header, string text, int index)
        {
            this.CellFormula = new CellFormula { CalculateCell = true, Text = text };
            this.DataType = CellValues.Number;
            this.CellReference = header + index;
            this.StyleIndex = 2;
        }
    }
    public class HeaderCell : TextCell
    {
        public HeaderCell(string header, string text, int index, Stylesheet styles, 
            System.Drawing.Color fillColour, double? fontSize, bool isBold)
            : base(header, text, index)
        {
            UInt32Value fontId = CreateFont(styles, "Arial", fontSize, isBold, System.Drawing.Color.Black);
            UInt32Value fillId = CreateFill(styles, fillColour);
            UInt32Value formatId = CreateCellFormat(styles, fontId, fillId, 0);
            this.StyleIndex = formatId;
        }

        private static UInt32Value CreateCellFormat(
            Stylesheet styleSheet,
            UInt32Value fontIndex,
            UInt32Value fillIndex,
            UInt32Value numberFormatId)
        {
            CellFormat cellFormat = new CellFormat();

            if (fontIndex != null)
                cellFormat.FontId = fontIndex;

            if (fillIndex != null)
                cellFormat.FillId = fillIndex;

            if (numberFormatId != null)
            {
                cellFormat.NumberFormatId = numberFormatId;
                cellFormat.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            }

            styleSheet.CellFormats.Append(cellFormat);

            UInt32Value result = styleSheet.CellFormats.Count;
            styleSheet.CellFormats.Count++;
            return result;
        }

        private static UInt32Value CreateFill(
            Stylesheet styleSheet,
            System.Drawing.Color fillColor)
        {

            HexBinaryValue hexBinaryValue = new HexBinaryValue();
            hexBinaryValue.Value = System.Drawing.ColorTranslator.ToHtml(
                System.Drawing.Color.FromArgb(
                    fillColor.A,
                    fillColor.R,
                    fillColor.G,
                    fillColor.B)).Replace("#", "");
            ForegroundColor foregroundColor = new ForegroundColor();
            foregroundColor.Rgb = hexBinaryValue;
            PatternFill patternFill = 
                new PatternFill(foregroundColor
                    );

//            patternFill.PatternType = fillColor ==
//                        System.Drawing.Color.White ? PatternValues.None : PatternValues.LightDown;
            patternFill.PatternType = fillColor ==
                        System.Drawing.Color.White ? PatternValues.None : PatternValues.Solid;

            Fill fill = new Fill(patternFill);

            styleSheet.Fills.Append(fill);

            UInt32Value result = styleSheet.Fills.Count;
            styleSheet.Fills.Count++;
            return result;
        }

        private static UInt32Value CreateFont(
            Stylesheet styleSheet,
            string fontName,
            double? fontSize,
            bool isBold,
            System.Drawing.Color foreColor)
        {

            Font font = new Font();

            if (!string.IsNullOrEmpty(fontName))
            {
                FontName name = new FontName();
                name.Val = fontName;
                font.Append(name);
            }

            if (fontSize.HasValue)
            {
                FontSize size = new FontSize();
                size.Val = fontSize.Value;
                font.Append(size);
            }

            if (isBold == true)
            {
                Bold bold = new Bold();
                font.Append(bold);
            }

            HexBinaryValue hexBinaryValue = new HexBinaryValue();
            hexBinaryValue.Value = System.Drawing.ColorTranslator.ToHtml(
                System.Drawing.Color.FromArgb(
                    foreColor.A,
                    foreColor.R,
                    foreColor.G,
                    foreColor.B)).Replace("#", "");
            Color color = new Color();
            color.Rgb = hexBinaryValue;
            font.Append(color);

            styleSheet.Fonts.Append(font);
            UInt32Value result = styleSheet.Fonts.Count;
            styleSheet.Fonts.Count++;
            return result;
        }
    }
}