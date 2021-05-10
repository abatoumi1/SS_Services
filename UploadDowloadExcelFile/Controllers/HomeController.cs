using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadDowloadExcelFile.Models;
using UploadDowloadExcelFile.Shared;

namespace UploadDowloadExcelFile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment _hostingEnvironment;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Import()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "UploadExcel";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table table-bordered'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                        }
                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");
                }
            }
            return this.Content(sb.ToString());
        }

        public async Task<IActionResult> Download123()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"Employees.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("employee");
                IRow row = excelSheet.CreateRow(3);
                row.CreateCell(2).SetCellValue("List Employees");
                row = excelSheet.CreateRow(3);
                row.CreateCell(0).SetCellValue("EmployeeId");
                row.CreateCell(1).SetCellValue("EmployeeName");
                row.CreateCell(2).SetCellValue("Age");
                row.CreateCell(3).SetCellValue("Sex");
                row.CreateCell(4).SetCellValue("Designation");

                row = excelSheet.CreateRow(4);
                row.CreateCell(0).SetCellValue(1);
                row.CreateCell(1).SetCellValue("Jack Supreu");
                row.CreateCell(2).SetCellValue(45);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Solution Architect");

                row = excelSheet.CreateRow(5);
                row.CreateCell(0).SetCellValue(2);
                row.CreateCell(1).SetCellValue("Steve khan");
                row.CreateCell(2).SetCellValue(33);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Software Engineer");

                row = excelSheet.CreateRow(6);
                row.CreateCell(0).SetCellValue(3);
                row.CreateCell(1).SetCellValue("Romi gill");
                row.CreateCell(2).SetCellValue(25);
                row.CreateCell(3).SetCellValue("FeMale");
                row.CreateCell(4).SetCellValue("Junior Consultant");

                row = excelSheet.CreateRow(7);
                row.CreateCell(0).SetCellValue(4);
                row.CreateCell(1).SetCellValue("Hider Ali");
                row.CreateCell(2).SetCellValue(34);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Accountant");

                row = excelSheet.CreateRow(8);
                row.CreateCell(0).SetCellValue(5);
                row.CreateCell(1).SetCellValue("Mathew");
                row.CreateCell(2).SetCellValue(48);
                row.CreateCell(3).SetCellValue("Male");
                row.CreateCell(4).SetCellValue("Human Resource");

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }

        public ActionResult Download11()
        {
            string Files = "wwwroot/UploadExcel/CoreProgramm_ExcelImport.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(Files);
            System.IO.File.WriteAllBytes(Files, fileBytes);
            MemoryStream ms = new MemoryStream(fileBytes);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "employee.xlsx");
        }

     


        public async Task<IActionResult> Download14() //WriteExcelFilesAsync()
        {

            
            int rowNum = 0;
            string sWebRootFolder = _hostingEnvironment.WebRootPath;

            byte[] bytes = null;
            //Only include college logo if it could be accessed
            var logoPath = sWebRootFolder + "\\images\\djangui3.PNG";
            var collegeLogoStream = new System.IO.FileStream(logoPath, FileMode.Open);
            bytes = IOUtils.ToByteArray(collegeLogoStream);
            collegeLogoStream.Close();
            collegeLogoStream.Dispose();



           

                //Generate each Excel workbook
                string folderName = "UploadExcel";
                // string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(sWebRootFolder, folderName);
                // StringBuilder sb = new StringBuilder();
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                string sFileName = @"Employees.xlsx";
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                FileInfo file = new FileInfo(Path.Combine(newPath, sFileName));
                var memory = new MemoryStream();
                using (var fs = new FileStream(Path.Combine(newPath, sFileName), FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet;
                    IRow row;
                    ICell cell;
                    CellRangeAddress region;
                    IDrawing drawing;
                    IClientAnchor anchor;
                    IPicture picture;

                    //Only include college logo if it could be accessed
                    int collegeLogo = 0;

                    //Add college logo
                    collegeLogo = workbook.AddPicture(bytes, PictureType.PNG);


                    //For date formatting
                    ICreationHelper createHelper = workbook.GetCreationHelper();
                    //   IHyperlink link = createHelper.CreateHyperlink(HyperlinkType.Url);

                    //Cell formats
                    CellType ctString = CellType.String;
                    CellType ctNumber = CellType.Numeric;
                    CellType ctFormula = CellType.Formula;
                    CellType ctBlank = CellType.Blank;
                    CellType ctDate = CellType.Unknown;

                    //Cell Colours
                    XSSFColor cLightBlue = new XSSFColor(Color.LightSteelBlue);
                    XSSFColor cReturned = new XSSFColor(Color.Yellow);

                    //Cell Borders
                    BorderStyle bLight = BorderStyle.Thin;
                    BorderStyle bMedium = BorderStyle.Medium;

                    //Fonts
                    //Header
                    IFont fHeader = workbook.CreateFont();
                    fHeader.FontHeight = 20;
                    fHeader.FontName = "Arial";
                    fHeader.IsBold = true;

                    //Sub-header
                    IFont fSubHeader = workbook.CreateFont();
                    fSubHeader.FontHeight = 14;
                    fSubHeader.FontName = "Arial";
                    fSubHeader.IsBold = true;

                    //Table Header
                    IFont fBold = workbook.CreateFont();
                    fBold.IsBold = true;

                    //Cell formats
                    //Page Header
                    XSSFCellStyle sHeader = (XSSFCellStyle)workbook.CreateCellStyle();
                    sHeader.SetFont(fHeader);

                    //Page Subheader
                    XSSFCellStyle sSubHeader = (XSSFCellStyle)workbook.CreateCellStyle();
                    sHeader.SetFont(fSubHeader);
                    sHeader.SetFont(fBold);

                    //Table Header
                    XSSFCellStyle sTableHeader = (XSSFCellStyle)workbook.CreateCellStyle();
                    sTableHeader.SetFont(fBold);
                    sTableHeader.SetFillForegroundColor(cLightBlue);
                    sTableHeader.FillPattern = FillPattern.SolidForeground;
                    sTableHeader.BorderTop = bMedium;
                    sTableHeader.BorderBottom = bMedium;
                    sTableHeader.BorderLeft = bMedium;
                    sTableHeader.BorderRight = bMedium;
                    sTableHeader.WrapText = true;

                    XSSFCellStyle sReturned = (XSSFCellStyle)workbook.CreateCellStyle();
                    sReturned.SetFont(fBold);
                    sReturned.SetFillForegroundColor(cReturned);
                    sReturned.FillPattern = FillPattern.SolidForeground;
                    sReturned.BorderTop = bMedium;
                    sReturned.BorderBottom = bMedium;
                    sReturned.BorderLeft = bMedium;
                    sReturned.BorderRight = bMedium;

                    //Border only
                    XSSFCellStyle sBorderMedium = (XSSFCellStyle)workbook.CreateCellStyle();
                    sBorderMedium.BorderTop = bMedium;
                    sBorderMedium.BorderBottom = bMedium;
                    sBorderMedium.BorderLeft = bMedium;
                    sBorderMedium.BorderRight = bMedium;

                    XSSFCellStyle sBorderDate = (XSSFCellStyle)workbook.CreateCellStyle();
                    sBorderDate.BorderTop = bMedium;
                    sBorderDate.BorderBottom = bMedium;
                    sBorderDate.BorderLeft = bMedium;
                    sBorderDate.BorderRight = bMedium;
                    sBorderDate.SetDataFormat(createHelper.CreateDataFormat().GetFormat("dd/mm/yyyy"));

                    //Create Index Sheet
                    sheet = workbook.CreateSheet("Employee List");
                    row = sheet.CreateRow(0);
                    row.Height = 1000;

                    //Insert College Logo (to right) - second col/row must be greater otherwise nothing appears
                    drawing = sheet.CreateDrawingPatriarch();
                    anchor = createHelper.CreateClientAnchor();
                    anchor.Col1 = 3;
                    anchor.Row1 = 0;
                    anchor.Col2 = 5;
                    anchor.Row2 = 1;

                    picture = drawing.CreatePicture(anchor, collegeLogo);


                    cell = row.CreateCell(0);

                    cell.SetCellValue("Employee List of Programmes from ");
                    cell.CellStyle = sHeader;

                    //Merge header row
                    region = CellRangeAddress.ValueOf("A1:H1");
                    sheet.AddMergedRegion(region);

                    row = sheet.CreateRow(1);

                    row = sheet.CreateRow(2);

                    cell = row.CreateCell(0, ctString);
                    cell.SetCellValue("ID");
                    cell.CellStyle = sTableHeader;

                    cell = row.CreateCell(1, ctString);
                    cell.SetCellValue("First Name");
                    cell.CellStyle = sTableHeader;

                    cell = row.CreateCell(2, ctString);
                    cell.SetCellValue("Last Name");
                    cell.CellStyle = sTableHeader;

                    cell = row.CreateCell(3, ctString);
                    cell.SetCellValue("Email");
                    cell.CellStyle = sTableHeader;

                    cell = row.CreateCell(4, ctString);
                    cell.SetCellValue("Start Date");
                    cell.CellStyle = sTableHeader;

                    cell = row.CreateCell(5, ctString);
                    cell.SetCellValue("Phone Number");
                    cell.CellStyle = sTableHeader;

                    cell = row.CreateCell(6, ctString);
                    cell.SetCellValue("Status");
                    cell.CellStyle = sTableHeader;

                    cell = row.CreateCell(7, ctString);
                    cell.SetCellValue("Owner");
                    cell.CellStyle = sTableHeader;



                    //Column widths
                    sheet.SetColumnWidth(0, 4 * 256);
                    sheet.SetColumnWidth(1, 50 * 256);
                    sheet.SetColumnWidth(2, 50 * 256);
                    sheet.SetColumnWidth(3, 30 * 256);
                    sheet.SetColumnWidth(4, 40 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 10 * 256);
                    sheet.SetColumnWidth(7, 20 * 256);
                    

                    //The current row in the worksheet
                    rowNum = 2;
                    

                    foreach (var programme in Data.GetMember())
                    {
                        rowNum += 1;
                        row = sheet.CreateRow(rowNum);

                        cell = row.CreateCell(0, ctNumber);
                        cell.SetCellValue(programme.MemberID);
                        cell.CellStyle = sBorderMedium;

                        cell = row.CreateCell(1, ctString);
                        cell.SetCellValue(programme.FirstName);
                        cell.CellStyle = sBorderMedium;

                        cell = row.CreateCell(2, ctString);
                        cell.SetCellValue(programme.LastName);
                        cell.CellStyle = sBorderMedium;

                        cell = row.CreateCell(3, ctString);
                        cell.SetCellValue(programme.Email);
                        cell.CellStyle = sBorderMedium;

                        cell = row.CreateCell(4, ctString);
                        cell.SetCellValue(programme.StartDate.ToLongDateString());
                        cell.CellStyle = sBorderMedium;

                        cell = row.CreateCell(5, ctString);
                        cell.SetCellValue(programme.Phone);
                        cell.CellStyle = sBorderMedium;
                        // cell.Hyperlink = link;

                        cell = row.CreateCell(6, ctString);
                        cell.SetCellValue(programme.Role.ToString());
                        cell.CellStyle = sBorderMedium;
                        // cell.Hyperlink = link;

                        cell = row.CreateCell(7, ctString);
                        cell.SetCellValue(programme.OwnerID);
                        cell.CellStyle = sReturned;

                        
                    }

                    workbook.Write(fs);
                    using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
                
            }
        }
        public async Task<IActionResult> Download() //WriteExcelFilesAsync()
        {

            //string filePath = null;
            //string fileName = null;
            //string fileURL = null;
            int rowNum = 0;
          //  int startAtRowNum = 0;
           // int colNum = 0;
           // int numFilesSaved = 0;
            string sWebRootFolder = _hostingEnvironment.WebRootPath;

            byte[] bytes = null;
            //Only include college logo if it could be accessed
            var logoPath = sWebRootFolder + "\\images\\djangui3.PNG";
            var collegeLogoStream = new System.IO.FileStream(logoPath, FileMode.Open);
            bytes = IOUtils.ToByteArray(collegeLogoStream);
            collegeLogoStream.Close();
            collegeLogoStream.Dispose();



            //Create Master Excel Programme List


            //Generate each Excel workbook
            string folderName = "UploadExcel";
            // string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(sWebRootFolder, folderName);
            // StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            string sFileName = @"MemberContributions.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(newPath, sFileName));
            var memory = new MemoryStream();



            using (var fs = new FileStream(Path.Combine(newPath, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet;
                IRow row;
                ICell cell;
                CellRangeAddress region;
                IDrawing drawing;
                IClientAnchor anchor;
                IPicture picture;

                int collegeLogo = 0;

                collegeLogo = workbook.AddPicture(bytes, PictureType.PNG);


                //For date formatting
                ICreationHelper createHelper = workbook.GetCreationHelper();

                //Cell formats
                CellType ctString = CellType.String;
                CellType ctNumber = CellType.Numeric;
                CellType ctFormula = CellType.Formula;
                CellType ctBlank = CellType.Blank;

                //Cell Colours
                XSSFColor cLightBlue = new XSSFColor(Color.LightSteelBlue);
                XSSFColor cBlue = new XSSFColor(Color.RoyalBlue);
                XSSFColor cDeepSkyBlue = new XSSFColor(Color.SkyBlue);
                XSSFColor coloType = new XSSFColor(Color.Gray);
                XSSFColor coloAmount = new XSSFColor(Color.AliceBlue);

                //Cell Borders
                BorderStyle bLight = BorderStyle.Thin;
                BorderStyle bMedium = BorderStyle.Medium;

                //Fonts
                //Header
                IFont fHeader = workbook.CreateFont();
                fHeader.FontHeight = 20;
                fHeader.FontName = "Arial";
                fHeader.IsBold = true;

                //Sub-header
                IFont fSubHeader = workbook.CreateFont();
                fSubHeader.FontHeight = 14;
                fSubHeader.FontName = "Arial";
                fSubHeader.IsBold = true;

                //Table Header
                IFont fBold = workbook.CreateFont();
                fBold.IsBold = true;

                IFont fWhite = workbook.CreateFont();
                fWhite.Color = IndexedColors.White.Index;

                //Cell formats
                //Page Header
                XSSFCellStyle sHeader = (XSSFCellStyle)workbook.CreateCellStyle();
                sHeader.SetFont(fHeader);

                //Page Subheader
                XSSFCellStyle sSubHeader = (XSSFCellStyle)workbook.CreateCellStyle();
                sHeader.SetFont(fSubHeader);
                sHeader.SetFont(fBold);

                //Table Header
                XSSFCellStyle sTableHeader = (XSSFCellStyle)workbook.CreateCellStyle();
                sTableHeader.SetFont(fBold);
                sTableHeader.SetFillForegroundColor(cLightBlue);
                sTableHeader.FillPattern = FillPattern.SolidForeground;
                sTableHeader.BorderTop = bMedium;
                sTableHeader.BorderBottom = bMedium;
                sTableHeader.BorderLeft = bMedium;
                sTableHeader.BorderRight = bMedium;
                sTableHeader.WrapText = true;

                //XSSFCellStyle sTableHeaderCenter = (XSSFCellStyle)workbook.CreateCellStyle();
                //sTableHeaderCenter.SetFont(fBold);
                //sTableHeaderCenter.SetFillForegroundColor(cLightBlue);
                //sTableHeaderCenter.FillPattern = FillPattern.SolidForeground;
                //sTableHeaderCenter.Alignment = HorizontalAlignment.Center;
                //sTableHeaderCenter.BorderTop = bMedium;
                //sTableHeaderCenter.BorderBottom = bMedium;
                //sTableHeaderCenter.BorderLeft = bMedium;
                //sTableHeaderCenter.BorderRight = bMedium;
                //sTableHeaderCenter.WrapText = true;

                //Date
                XSSFCellStyle sDate = (XSSFCellStyle)workbook.CreateCellStyle();
                sDate.SetDataFormat(createHelper.CreateDataFormat().GetFormat("dd/mm/yyyy"));
                sDate.BorderTop = bMedium;
                sDate.BorderBottom = bMedium;
                sDate.BorderLeft = bMedium;
                sDate.BorderRight = bMedium;

                //Rotated
                XSSFCellStyle sRotated = (XSSFCellStyle)workbook.CreateCellStyle();
                sRotated.Rotation = 90;
                sRotated.BorderTop = bMedium;
                sRotated.BorderBottom = bMedium;
                sRotated.BorderLeft = bMedium;
                sRotated.BorderRight = bMedium;

                //Merged Centred
                //XSSFCellStyle sMergedCentred = (XSSFCellStyle)workbook.CreateCellStyle();
                //sMergedCentred.SetFont(fBold);
                //sMergedCentred.Alignment = HorizontalAlignment.Center;
                //sMergedCentred.VerticalAlignment = VerticalAlignment.Center;
                //sMergedCentred.BorderTop = bLight;
                //sMergedCentred.BorderBottom = bLight;
                //sMergedCentred.BorderLeft = bLight;
                //sMergedCentred.BorderRight = bLight;

                XSSFCellStyle stype = (XSSFCellStyle)workbook.CreateCellStyle();
                stype.SetFillForegroundColor(coloType);
                stype.FillPattern = FillPattern.SolidForeground;
                stype.BorderTop = bMedium;
                stype.BorderBottom = bMedium;
                stype.BorderLeft = bMedium;
                stype.BorderRight = bMedium;

                XSSFCellStyle sAmount = (XSSFCellStyle)workbook.CreateCellStyle();
                sAmount.SetFillForegroundColor(coloAmount);
                sAmount.FillPattern = FillPattern.SolidForeground;
                sAmount.BorderTop = bMedium;
                sAmount.BorderBottom = bMedium;
                sAmount.BorderLeft = bMedium;
                sAmount.BorderRight = bMedium;

                XSSFCellStyle sOther = (XSSFCellStyle)workbook.CreateCellStyle();
                sOther.SetFillForegroundColor(cDeepSkyBlue);
                sOther.FillPattern = FillPattern.SolidForeground;
                sOther.BorderTop = bMedium;
                sOther.BorderBottom = bMedium;
                sOther.BorderLeft = bMedium;
                sOther.BorderRight = bMedium;

                //Border Only
                //XSSFCellStyle sBorderLight = (XSSFCellStyle)workbook.CreateCellStyle();
                //sBorderLight.BorderTop = bLight;
                //sBorderLight.BorderBottom = bLight;
                //sBorderLight.BorderLeft = bLight;
                //sBorderLight.BorderRight = bLight;

                //XSSFCellStyle sBorderMedium = (XSSFCellStyle)workbook.CreateCellStyle();
                //sBorderMedium.BorderTop = bMedium;
                //sBorderMedium.BorderBottom = bMedium;
                //sBorderMedium.BorderLeft = bMedium;
                //sBorderMedium.BorderRight = bMedium;                             

                //Border Only
                //XSSFCellStyle sBorderDate = (XSSFCellStyle)workbook.CreateCellStyle();
                //sBorderDate.BorderTop = bMedium;
                //sBorderDate.BorderBottom = bMedium;
                //sBorderDate.BorderLeft = bMedium;
                //sBorderDate.BorderRight = bMedium;
                //sBorderDate.SetDataFormat(createHelper.CreateDataFormat().GetFormat("dd/mm/yyyy"));

                //Create Index Sheet
                sheet = workbook.CreateSheet("Index");
                row = sheet.CreateRow(0);
                row.Height = 1000;

                //Insert College Logo (to right) - second col/row must be greater otherwise nothing appears
                drawing = sheet.CreateDrawingPatriarch();
                anchor = createHelper.CreateClientAnchor();
                anchor.Col1 = 3;
                anchor.Row1 = 0;
                anchor.Col2 = 5;
                anchor.Row2 = 1;

                picture = drawing.CreatePicture(anchor, collegeLogo);


                cell = row.CreateCell(0);

                cell.SetCellValue("Contribution from " + DateTime.Now + " to " + DateTime.Now.AddSeconds(1));
                cell.CellStyle = sHeader;

                //Merge header row
                region = CellRangeAddress.ValueOf("A1:B1");
                sheet.AddMergedRegion(region);

                row = sheet.CreateRow(1);

                row = sheet.CreateRow(2);

                cell = row.CreateCell(0, ctString);
                cell.SetCellValue("Member ID");
                cell.CellStyle = sTableHeader;

                cell = row.CreateCell(1, ctString);
                cell.SetCellValue("FirstName");
                cell.CellStyle = sTableHeader;

                cell = row.CreateCell(2, ctString);
                cell.SetCellValue("LastName");
                cell.CellStyle = sTableHeader;

                region = CellRangeAddress.ValueOf("D3:E3");
                sheet.AddMergedRegion(region);

                cell = row.CreateCell(3, ctString);
                cell.SetCellValue("Contribution");
                cell.CellStyle = sTableHeader;
                cell = row.CreateCell(4, ctString);
                cell.SetCellValue("");
                cell.CellStyle = sTableHeader;



                //Column widths
                sheet.SetColumnWidth(0, 10 * 256);
                sheet.SetColumnWidth(1, 30 * 256);
                sheet.SetColumnWidth(2, 30 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                 sheet.SetColumnWidth(4, 10 * 256);
                /*sheet.SetColumnWidth(5, 8 * 256);
                sheet.SetColumnWidth(6, 10 * 256);
                sheet.SetColumnWidth(7, 10 * 256);*/
                //sheet.SetColumnWidth(8, 12 * 256);
                //sheet.SetColumnWidth(9, 12 * 256);
                //sheet.SetColumnWidth(10, 20 * 256);
                //sheet.SetColumnWidth(11, 20 * 256);

                //The current row in the worksheet
                rowNum = 2;

                var result = Data.GetContribution()
               .GroupBy(a => new Tuple<int, string, string>(a.MemberID, a.FirstName, a.LastName))  //+"-"+ a.FirstName +","+ a.LastName
               .ToDictionary(g => g.Key, g => g.Where(d => d.ContributionTypeID != 0)
                .GroupBy(a1 => new Tuple<int, string>(a1.ContributionTypeID, a1.ContributionTypeName))
                .ToDictionary(g1 => g1.Key, g1 => g1
                .Select(s => new ContributionByMember
                {
                    ContributionTypeName = s.ContributionTypeName,
                    Amount = s.Amount,
                    ContributionDate = s.ContributionDate,
                    ContributionID = s.ContributionID,
                    //IsActive = s.IsActive
                })));


               // var programme = Data.GetContribution();

                //Generate each Excel worksheet
                if (result != null && result.Count > 0)
                {
                    foreach (var key1 in result)
                    {
                        var crs = key1.Key;
                        rowNum += 1;
                        row = sheet.CreateRow(rowNum);
                        XSSFCellStyle cellStyle=sOther;
                       // XSSFCellStyle cellStyleDate;

                        cell = row.CreateCell(0, ctNumber);
                        cell.SetCellValue(crs.Item1);
                        cell.CellStyle = cellStyle;

                        cell = row.CreateCell(1, ctString);
                        cell.SetCellValue(crs.Item2);
                        cell.CellStyle = cellStyle;

                        cell = row.CreateCell(2, ctString);
                        cell.SetCellValue(crs.Item3);
                        cell.CellStyle = cellStyle;

                        cell = row.CreateCell(3, ctString);
                        cell.SetCellValue("");
                        cell.CellStyle = sRotated;

                        cell = row.CreateCell(4, ctString);
                        cell.SetCellValue("");
                        cell.CellStyle = sRotated;

                        region = CellRangeAddress.ValueOf("D" + (rowNum + 1) + ":E" + (rowNum + 1));
                        sheet.AddMergedRegion(region);

                        foreach (var key2 in key1.Value)
                        {
                            rowNum += 1;
                            row = sheet.CreateRow(rowNum);                           

                            cell = row.CreateCell(3, ctString);
                            cell.SetCellValue(key2.Key.Item2);
                            cell.CellStyle = stype;

                            cell = row.CreateCell(4, ctString);
                            cell.SetCellValue("");
                            cell.CellStyle = stype;

                             //region = CellRangeAddress.ValueOf("D" + (rowNum - 1) + ":E" + (rowNum - 1));
                             //sheet.AddMergedRegion(region);

                            foreach (var item in key2.Value)
                            {
                                rowNum += 1;
                                row = sheet.CreateRow(rowNum);
                                cell = row.CreateCell(3, ctString);
                                cell.CellStyle = sAmount;
                                cell.SetCellValue(item.ContributionDate.ToShortDateString());
                                cell = row.CreateCell(4, ctNumber);
                                cell.CellStyle = sAmount;
                                cell.SetCellValue((double)item.Amount);
                            }
                        }
                    }
                }
                else
                {
                    rowNum += 1;
                    row = sheet.CreateRow(rowNum);

                    cell = row.CreateCell(0, ctString);
                    cell.CellStyle = sHeader;
                    cell.SetCellValue("Error - No courses could be loaded. Please check CourseData Stored Procedure");
                }

                workbook.Write(fs);
                using (var stream = new FileStream(Path.Combine(newPath, sFileName), FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                //var th = memory;
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
            }
        }
    }
}


        
    

