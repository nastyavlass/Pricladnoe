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
        private ICompanyRepository? _companyRepository;
        private IEmployeeRepository? _employeeRepository;
        private IApplianceRepository? _applianceRepository;
        private IFurnitureRepository? _furnitureRepository;

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

        public IApplianceRepository Appliance
        {
            get
            {
                if (_applianceRepository == null)
                    _applianceRepository = new ApplianceRepository(_repositoryContext);
                return _applianceRepository;
            }
        }

        public IFurnitureRepository Furniture
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
