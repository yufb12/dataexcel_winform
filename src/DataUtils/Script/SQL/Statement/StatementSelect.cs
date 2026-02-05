using Feng.Forms.Interface;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;

namespace Feng.Script.Sql
{

    //SELECT
    //[DISTINCT | DISTINCTROW(a.col_name, b.col_name)]
    //    select_expr[, select_expr]
    //[FROM table as A
    //    {LEFT|INNER} JOIN table as B on a.col_name=b.col_name] and b.maxrownum
    //[WHERE where_condition]
    //[GROUP BY {col_name} 
    //[HAVING where_condition]
    //[ORDER BY {col_name}
    //    [ASC | DESC]]
    //[LIMIT {row_count | position OFFSET row_count}] 


    //public class Field
    //{
    //    public string TableName { get; set; }
    //    public string FieldName { get; set; }
    //    public string AsName { get; set; }
    //}
    //public class OrderByField
    //{
    //    public string TableName { get; set; }
    //    public string FieldName { get; set; }
    //    public string AsName { get; set; }
    //    public bool ASC { get; set; }
    //}
    //public class FromTable
    //{
    //    public string TableName { get; set; }
    //    public string AsName { get; set; }
    //}

    //public class JOINTable
    //{
    //    public string JoinType { get; set; }
    //    public string TableName { get; set; }
    //    public string AsName { get; set; }
    //    public List<OnFiled> OnFileds { get; set; }
    //}
    //public class OnFiled
    //{
    //    public string SourceTable { get; set; }
    //    public string SourceValue { get; set; }
    //    public int EQType { get; set; }
    //    public string TargetTable { get; set; }
    //    public string TargetValue { get; set; }

    //}
    //public class Where_Condition
    //{ 
    //    public List<OnFiled> OnFileds { get; set; }
    //}
    //public class Group
    //{
    //    public List<Field> Fields { get; set; }
    //}
    //public class LIMIT
    //{
    //    public int Position { get; set; }
    //    public int Length { get; set; }
    //}
    //public class StatementSelect : StatementBase
    //{ 
    //    public override object Exec(IBreak parent, ICBEexpress excute)
    //    {
    //        System.Data.DataTable table = new System.Data.DataTable();

    //        return table;
    //    }

    //    public override string ToString()
    //    {
    //        return "IF " + base.ToString();
    //    }

    //}
 
 
  
}
