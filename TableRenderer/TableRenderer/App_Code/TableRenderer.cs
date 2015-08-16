using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace TableRenderer.Engine
{
    /// <summary>
    /// Render a HTML table
    /// </summary>
    public class TableRenderer
    {
        /// <summary>
        /// Table Open
        /// </summary>
        protected const string TableOpen = "<table>";

        /// <summary>
        /// Table Close
        /// </summary>
        protected const string TableClose = "</table>";

        /// <summary>
        /// Table Head Open
        /// </summary>
        protected const string TableHeadOpen = "<thead>";

        /// <summary>
        /// Table Head Close
        /// </summary>
        protected const string TableHeadClose = "</thead>";

        /// <summary>
        /// Table Body Open
        /// </summary>
        protected const string TableBodyOpen = "<tbody>";

        /// <summary>
        /// Table Body Close
        /// </summary>
        protected const string TableBodyClose = "</tbody>";

        /// <summary>
        /// Table Foot Open
        /// </summary>
        protected const string TableFootOpen = "<tfoot>";

        /// <summary>
        /// Table Foot Close
        /// </summary>
        protected const string TableFootClose = "</tfoot>";

        /// <summary>
        /// Table Row Open
        /// </summary>
        protected const string TableRowOpen = "<tr>";

        /// <summary>
        /// Table Row Close
        /// </summary>
        protected const string TableRowClose = "</tr>";

        /// <summary>
        /// Table Cell Open
        /// </summary>
        protected const string TableCellOpen = "<td>";

        /// <summary>
        /// Table Cell Close
        /// </summary>
        protected const string TableCellClose = "</td>";

        /// <summary>
        /// Table Header Open
        /// </summary>
        protected const string TableHeadCellOpen = "<th>";

        /// <summary>
        /// Table Header Open
        /// </summary>
        protected const string TableHeadCellClose = "</th>";

        /// <summary>
        /// Columns of the table
        /// </summary>
        protected List<string> Columns;

        /// <summary>
        /// Values of the table
        /// </summary>
        protected List<Dictionary<string, object>> Rows;
        
        /// <summary>
        /// Table Renderer
        /// </summary>
        public TableRenderer()
        {
            Columns = new List<string>();
            Rows = new List<Dictionary<string, object>>();
        }

        /// <summary>
        /// Add a DataTable to the Renderer
        /// </summary>
        /// <param name="table"></param>
        public void Set(DataTable table)
        {
            // Add Columns
            foreach (DataColumn col in table.Columns)
            {
                Columns.Add(col.ColumnName);
            }

            // Add Values
            foreach (DataRow dataRow in table.Rows)
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                foreach (string column in Columns)
                {
                    row[column] = dataRow[column];
                }
                Rows.Add(row);
            }
        }

        /// <summary>
        /// Add a Column Name
        /// </summary>
        /// <param name="columnName"></param>
        public void AddColumn(string columnName)
        {
            Columns.Add(columnName);
        }

        /// <summary>
        /// Add a Row
        /// </summary>
        /// <param name="row"></param>
        public void AddRow(Dictionary<string, object> row)
        {
            Rows.Add(row);
        }

        /// <summary>
        /// Add a row
        /// </summary>
        /// <param name="row"></param>
        public void AddRow(string[] dataRow)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            int i = 0;
            foreach (string column in Columns)
            {
                if (i > dataRow.Length) break;
                row[column] = dataRow[i];
                i++;
            }
            Rows.Add(row);
        }

        /// <summary>
        /// Gets an HTML Table
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetTable()
        {
            return GetTable(new StringBuilder());
        }

        /// <summary>
        /// Render this table
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            return GetTable(new StringBuilder()).ToString();
        }

        /// <summary>
        /// Render this table
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public string Render(StringBuilder builder)
        {
            return GetTable(builder).ToString();
        }

        /// <summary>
        /// Get an HTML Table
        /// </summary>
        public StringBuilder GetTable(StringBuilder builder)
        {
            // Get the Table Open
            builder.AppendLine(TableOpen);

            // Get the Table Header
            builder = GetTableHead(builder);

            // Get the Table Body
            builder = GetTableBody(builder);

            // Get the Table Footer
            builder = GetTableFoot(builder);
            
            // Get the Table Close
            builder.AppendLine(TableClose);

            return builder;
        }

        /// <summary>
        /// Get Table Head
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected StringBuilder GetTableHead(StringBuilder builder)
        {
            builder.AppendLine("\t" + TableHeadOpen);
            builder = GetTableHeadRow(builder);
            builder.AppendLine("\t" + TableHeadClose);
            return builder;
        }

        /// <summary>
        /// Get the body of the Table Head
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected StringBuilder GetTableHeadRow(StringBuilder builder)
        {
            builder.AppendLine("\t\t" + TableRowOpen);
            foreach (string column in Columns)
            {
                builder.Append("\t\t\t" + TableHeadCellOpen);
                if (column != null)
                    builder.Append(column);
                builder.AppendLine(TableHeadCellClose);
            }
            builder.AppendLine("\t\t" + TableRowClose);
            return builder;
        }

        /// <summary>
        /// Get Table Body
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected StringBuilder GetTableBody(StringBuilder builder)
        {
            builder.AppendLine("\t" + TableBodyOpen);
            foreach (Dictionary<string, object> row in Rows)
            {
                builder = GetTableRow(builder, row);
            }
            builder.AppendLine("\t" + TableBodyClose);
            return builder;
        }

        /// <summary>
        /// Get a Table Row
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        protected StringBuilder GetTableRow(StringBuilder builder, Dictionary<string, object> row)
        {
            builder.AppendLine("\t\t" + TableRowOpen);
            foreach (string column in Columns)
            {
                if (row[column] != null)
                    builder.AppendLine("\t\t\t" + TableCellOpen + row[column].ToString() + TableCellClose);
                else
                    builder.AppendLine(TableCellOpen + TableCellClose);
            }
            builder.AppendLine("\t\t" + TableRowClose);
            return builder;
        }

        /// <summary>
        /// Get Table Foot
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected StringBuilder GetTableFoot(StringBuilder builder)
        {
            builder.AppendLine("\t\t" + TableRowOpen);
            builder = GetTableHeadRow(builder);
            builder.AppendLine("\t\t" + TableRowClose);
            return builder;
        }
    }
}