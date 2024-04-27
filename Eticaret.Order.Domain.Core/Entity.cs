using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Domain.Core
{
    public abstract class Entity
    {
        private int _Id;

        public virtual int Id
        {
            get => _Id;
            set => _Id = value;
        }
    }
}
