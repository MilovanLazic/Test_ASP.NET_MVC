using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Test.BusinessLogic.Interfaces;

namespace Test.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeChartController : ControllerBase
    {
        private readonly IEmployeeLogic _employeeLogic;
        private readonly Random _random = new Random();

        public EmployeeChartController(IEmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetChart()
        {
            var employees = await _employeeLogic.GetAll();

            var bitmap = new Bitmap(1000, 750);
            var graphics = Graphics.FromImage(bitmap);

            var totalWorkTime = employees.Sum(e => e.TotalTimeWorked?.TotalHours ?? 0);
            var startAngle = 0f;

            var employeeData = new List<EmployeeChartData>();

            foreach (var employee in employees)
            {
                var workTime = employee.TotalTimeWorked?.TotalHours ?? 0;
                var sweepAngle = (float)(workTime / totalWorkTime) * 360;
                var color = GetUniqueColor(employeeData.Select(e => e.Color).ToList());

                graphics.FillPie(new SolidBrush(color), 50, 50, 650, 650, startAngle, sweepAngle);

                employeeData.Add(new EmployeeChartData
                {
                    Name = employee.Name,
                    Color = color,
                    PercentageOfWork = (workTime / totalWorkTime) * 100
                });

                startAngle += sweepAngle;
            }

            DrawLabels(graphics, employeeData);

            var stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(stream, "image/png");

        }
        private Color GetUniqueColor(List<Color> existingColors)
        {
            const int maxAttempts = 100;
            for (int i = 0; i < maxAttempts; i++)
            {
                var color = Color.FromArgb(
                    _random.Next(0, 256),
                    _random.Next(0, 256),
                    _random.Next(0, 256)
                );

                if (!existingColors.Any(c => ColorDistance(c, color) < 100))
                {
                    return color;
                }
            }

            return Color.Empty;
        }

        private double ColorDistance(Color c1, Color c2)
        {
            var rDiff = c1.R - c2.R;
            var gDiff = c1.G - c2.G;
            var bDiff = c1.B - c2.B;
            return Math.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);
        }

        private void DrawLabels(Graphics graphics, List<EmployeeChartData> employeeData)
        {
            var labelFont = new Font(FontFamily.GenericSansSerif, 14);
            var labelBrush = Brushes.Black;
            var labelX = 730;

            var labelY = 50;

            foreach (var data in employeeData)
            {
                graphics.FillRectangle(new SolidBrush(data.Color), labelX, labelY, 21, 21);

                graphics.DrawString($"{data.Name}, {data.PercentageOfWork:F2}%", labelFont, labelBrush, labelX + 20, labelY);

                labelY += 30;
            }
        }

        private class EmployeeChartData
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public double PercentageOfWork { get; set; }
        }
    }
}