using Feng.Excel.Interfaces;
using Feng.Script.CBEexpress;

namespace Feng.Excel.Script
{
    public class FunctionTools
    {
        public static ICell GetCell(ICBContext context, object arg)
        { 
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = arg as ICell;
            if (cell != null)
                return cell;
            string ct = Feng.Utils.ConvertHelper.ToString(arg);
            cell = proxy.Grid.GetCellByNameAndID(ct);
            if (cell != null)
            {
                if (cell.OwnMergeCell != null)
                {
                    return cell.OwnMergeCell;
                }
                return cell;
            }
            return null;
        }
    }
}
