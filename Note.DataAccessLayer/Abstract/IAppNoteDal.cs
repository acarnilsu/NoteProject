using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note.EntityLayer.Concrete;

namespace Note.DataAccessLayer.Abstract
{
    public interface IAppNoteDal: IGenericDal<AppNote>
    {
    }
}
