using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppCode.Services
{
    interface ISpreadSheetLoader
    {
        void LoadAndFill(int planId);
    }
}
