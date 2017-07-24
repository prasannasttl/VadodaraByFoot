
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using VadodaraByFoot.iOS.Renderers;

[assembly: ExportRenderer (typeof(ViewCell), typeof(ListviewCellRendererIos))]
namespace VadodaraByFoot.iOS.Renderers
{
    public class ListviewCellRendererIos : ViewCellRenderer
    {
        public override UIKit.UITableViewCell GetCell (Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var cell =  base.GetCell (item, reusableCell, tv);
            if (cell != null)
                cell.SelectionStyle = UIKit.UITableViewCellSelectionStyle.None;
            return cell;
        }
    }
}