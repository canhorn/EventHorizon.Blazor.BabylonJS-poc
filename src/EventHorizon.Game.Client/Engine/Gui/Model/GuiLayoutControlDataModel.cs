namespace EventHorizon.Game.Client.Engine.Gui.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using EventHorizon.Game.Client.Engine.Gui.Api;

    public class GuiLayoutControlDataModel
        : IGuiLayoutControlData
    {
        public string Id { get; set; } = string.Empty;
        public int Sort { get; set; }
        public int? Layer { get; set; }
        public string TemplateId { get; set; } = string.Empty;
        public GuiControlOptionsModel? Options { get; set; }
        IGuiControlOptions? IGuiLayoutControlData.Options => Options;
        public GuiGridLocationModel? GridLocation { get; set; }
        IGuiGridLocation? IGuiLayoutControlData.GridLocation => GridLocation;
        public List<GuiLayoutControlDataModel>? ControlList { get; set; }
        IEnumerable<IGuiLayoutControlData>? IGuiLayoutControlData.ControlList => ControlList;
        public object? LinkWith { get; set; }

        public GuiLayoutControlDataModel() { }
        public GuiLayoutControlDataModel(
            IGuiLayoutControlData control
        )
        {
            Id = control.Id;
            Sort = control.Sort;
            Layer = control.Layer;
            TemplateId = control.TemplateId;
            Options = new GuiControlOptionsModel(
                control.Options
            );
            GridLocation = control.GridLocation != null
                ? new GuiGridLocationModel(
                    control.GridLocation
                ) : null;
            ControlList = control.ControlList?.Select(
                control => new GuiLayoutControlDataModel(
                    control
                )
            ).ToList();
            LinkWith = control.LinkWith;
        }
    }
}