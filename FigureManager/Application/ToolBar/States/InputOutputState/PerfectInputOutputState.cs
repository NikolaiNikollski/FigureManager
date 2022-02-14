using FigureManager.Application.ImportExportDto;
using FigureManager.ToolBar;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FigureManager.Application.ToolBar.States
{
    class PerfectInputOutputState : InputOutputState
    {
        public PerfectInputOutputState(Canvas canvas, Toolbar toolbar) : base(toolbar, canvas)
        {
            ActiveInputOutputButton = ButtonType.PerfectInputOutput;
        }

        public override void Export(Canvas canvas)
        {
            StreamWriter sw = new StreamWriter(OutputPath);
            sw.Write(JsonConvert.SerializeObject(CanvasDto.CanvasExport(canvas)));
            sw.Dispose();
        }

        public override Canvas Import()
        {
            StreamReader sr = new StreamReader(InputPath, Encoding.Default);
            CanvasDto canvas = JsonConvert.DeserializeObject<CanvasDto>(sr.ReadToEnd());
            sr.Close();
            return CanvasDto.CanvasImport(canvas);
        }
    }
}
