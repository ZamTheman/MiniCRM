using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Vpn;
using NoBSCRM.Models;

namespace NoBSCRM.Messages
{
    public class SelectedCompanyMessenger
    {
        public Company SelectedCompany { get; set; }
    }
}
