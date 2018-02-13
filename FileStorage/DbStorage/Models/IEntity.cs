using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbStorage.Models
{
    /// <summary>
    /// Сущность
    /// Хранится в БД
    /// </summary>


    public interface IEntity
    {
        long Id { get; set; }
    }
}
