using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Services.Activities
{
    public interface IActivityService
    {
        Task AddAsync(Guid id,Guid userId,string category,
            string name,string description,DateTime createdAt);
    }
}
