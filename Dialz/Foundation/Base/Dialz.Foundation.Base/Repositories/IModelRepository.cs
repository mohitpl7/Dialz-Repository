using Dialz.Foundation.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dialz.Foundation.Base.Repositories
{
    public interface IModelRepository
    {
        public IRenderingBase GetModel();
    }
}
