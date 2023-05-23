using System.Data;

namespace GHCore.Expression
{
    /// <summary>
    /// DataTable.Comput，动态计算数学表达式值
    /// </summary>
    public class DataTableEvaluator
    {
        public static object Eval(string expression)
        {
            //string expression = "1+2*3";
            DataTable eval = new DataTable();
            object result = eval.Compute(expression, "");
            return result;
        }
    }
}
