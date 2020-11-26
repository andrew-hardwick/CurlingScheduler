using CurlingScheduler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurlingScheduler.Service
{
    internal class DrawBalancer : Balancer
    {

        internal Schedule Schedule(
            IEnumerable<string> teams,
            int sheetCount,
            int drawCount, 
            int weekCount, 
            DrawAlignment drawAlignment)
        {
            //todo implement

            return new Schedule();
        }
    }
}
