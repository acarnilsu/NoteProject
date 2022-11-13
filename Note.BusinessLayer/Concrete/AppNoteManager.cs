﻿using Note.BusinessLayer.Abstract;
using Note.DataAccessLayer.Abstract;
using Note.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.BusinessLayer.Concrete
{
    public class AppNoteManager : GenericManager<AppNote>, IAppNoteService
    {
        public AppNoteManager(IGenericDal<AppNote> genericDal, IUnitOfWork unitOfWork) : base(genericDal, unitOfWork)
        {
        }
    }
}
