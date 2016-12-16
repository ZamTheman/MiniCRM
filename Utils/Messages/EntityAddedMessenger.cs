using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace Utils.Messages
{
    public class EntityAddedMessenger
    {
        public IEntity Entity { get; set; }
    }
}
