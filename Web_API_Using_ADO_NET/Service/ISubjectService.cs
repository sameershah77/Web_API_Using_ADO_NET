using System.Collections.Generic;
using Web_API_Using_ADO_NET.Model;

namespace Web_API_Using_ADO_NET.Service
{
    public interface ISubjectService
    {
        List<Subject> GetAll();
        Subject GetById(int id);
        bool Add(Subject subject);
        bool Update(Subject subject);
        bool Delete(int id);
    }
}
