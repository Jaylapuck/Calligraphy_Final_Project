using System.Collections.Generic;
using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Calligraphy.Data.Repo.Contract
{
    public class ContractRepo : IContractRepo
    {
        private readonly CalligraphyContext _context;

        public ContractRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public IEnumerable<ContractEntity> GetAll()
        {
            using (_context)
            {
                return _context.Contracts.ToList();
            }
        }

        public ContractEntity GetById(int contractId)
        {
            using (_context)
            {
                return _context.Contracts.FirstOrDefault(x => x.ContractId == contractId);
            }
        }

        public IEnumerable<ContractEntity> GetByMonthOfYear(int Month, int Year, bool IsFinished)
        {
            using (_context)
            {
                return _context.Contracts.ToList().Where(x =>
                    x.DateCommissioned.Month == Month &&
                    x.DateCommissioned.Year == Year &&
                    x.IsFinished == IsFinished
                );
            }
        }

        public int CreateNewContract(ContractEntity newEntity)
        {
            _context.Contracts.Add(newEntity);
            return _context.SaveChanges();
        }

        public ContractEntity UpdateContract(ContractEntity entity)
        {
            using (_context)
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return entity;
            }
        }
    }
}