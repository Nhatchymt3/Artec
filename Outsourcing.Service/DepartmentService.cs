using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Service
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartmentById(int DepartmentId);
        void CreateDepartment(Department obj);
        void EditDepartment(Department obj);
        void DeleteDepartment(int id);
        void SaveChange();
    }
    public class DepartmentService : IDepartmentService
    {
        #region Field
        private readonly IDepartmentRepository DepartmentRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public DepartmentService(IDepartmentRepository _DepartmentRepository, IUnitOfWork unitOfWork)
        {
            this.DepartmentRepository = _DepartmentRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Department> GetDepartments()
        {
            var Departments = DepartmentRepository.GetMany(t => !t.Deleted);
            return Departments;
        }

        public Department GetDepartmentById(int departmentId)
        {
            var Department = DepartmentRepository.GetById(departmentId);
            return Department;
        }

        public void CreateDepartment(Department Department)
        {
            DepartmentRepository.Add(Department);
            SaveChange();
        }

        public void EditDepartment(Department DepartmentToEdit)
        {
            DepartmentRepository.Update(DepartmentToEdit);
            SaveChange();
        }

        public void DeleteDepartment(int DepartmentId)
        {
            //Get picture by id.
            var Department = DepartmentRepository.GetById(DepartmentId);
            if (Department != null)
            {
                Department.Deleted = true;
                DepartmentRepository.Update(Department);
                SaveChange();
            }
        }

        public void SaveChange()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}
