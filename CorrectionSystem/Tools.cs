using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrectionSystem
{
    class Tools
    {


        /*************************************************************/
        /****************** Get The Number Of Rows *******************/
        public static int getNumberOfRowsInTable(string tbName)
        {
            DataTable deptTable = Database.returnTable("select * from " + tbName);
            int numOfRows = deptTable.Rows.Count;
            return numOfRows;
        }
        /***************** End Get The Number Of Rows *****************/
        /**************************************************************/

    }
}
