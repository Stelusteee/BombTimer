namespace BombTimer
{
    public class CustomContextMenuRenderer : ToolStripProfessionalRenderer
    {
        SolidBrush BgClr = new SolidBrush(Color.FromArgb(255, 76, 88, 68));
        SolidBrush YlClr = new SolidBrush(Color.FromArgb(255, 196, 181, 80));

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            e.Graphics.FillRectangle(BgClr, e.AffectedBounds);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            e.Graphics.FillRectangle(e.Item.Selected ? YlClr : BgClr, e.Graphics.ClipBounds);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            e.Graphics.FillRectangle(BgClr, e.AffectedBounds);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = Color.White;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = Color.White;
            base.OnRenderArrow(e);
        }
    }
}
