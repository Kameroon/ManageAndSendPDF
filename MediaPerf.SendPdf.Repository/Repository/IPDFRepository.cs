using MediaPerf.SendPdf.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPerf.SendPdf.Repository.Repository
{
    public interface IPDFRepository
    {
        IList<IAuthor> GetAuthors();

    }
}
