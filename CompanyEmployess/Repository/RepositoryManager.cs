using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private IAppliancesRepository _catRepository;
        private IFurnitureRepository _furnitureRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }

        public IAppliancesRepository Cat
        {
            get
            {
                if (_catRepository == null)
                    _catRepository = new AppliancesRepository(_repositoryContext);
                return _catRepository;
            }
        }

        public IFurnitureRepository Dog
        {
            get
            {
                if (_furnitureRepository == null)
                    _furnitureRepository = new FurnitureRepository(_repositoryContext);
                return _furnitureRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
