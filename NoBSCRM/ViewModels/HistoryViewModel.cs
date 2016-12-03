using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoBSCRM.Models;

namespace NoBSCRM.ViewModels
{
    public class HistoryViewModel : IEntityViewModel
    {
        public IEntity ActiveEntity { get; set; }
    }
}
