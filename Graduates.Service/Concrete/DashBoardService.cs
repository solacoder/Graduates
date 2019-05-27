using System;
using System.Collections.Generic;
using System.Text;
using Graduates.Core;
using Graduates.Service.Abstract;
using Graduates.ViewModel.Dtos;

namespace Graduates.Service.Concrete
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IUnitOfWork _uow;

        public DashBoardService(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public DashBoardItem GetDashBoardItem(string resource)
        {
            DashBoardItem count = null;

            switch (resource)
            {
                case ("institutions"):
                    count.Name = "Institutions";
                    count.Description = "Number of Institutions";
                    count.Value = _uow.Institutions.Count().ToString();
                    break;
                case ("faculties"):
                    count.Name = "Faculties";
                    count.Description = "Number of Faculties";
                    count.Value = _uow.Faculties.Count().ToString();
                    break;
                case ("departments"):
                    count.Name = "Departments";
                    count.Description = "Number of Departments";
                    count.Value = _uow.Departments.Count().ToString();
                    break;
                case ("courses"):
                    count.Name = "Courses";
                    count.Description = "Number of Courses";
                    count.Value = _uow.Courses.Count().ToString();
                    break;
                case ("news"):
                    count.Name = "News";
                    count.Description = "Number of Descriptions";
                    count.Value = _uow.News.Count().ToString();
                    break;
                case ("articles"):
                    count.Name = "Articles";
                    count.Description = "Number of Articles";
                    count.Value = _uow.Articles.Count().ToString();
                    break;
                case ("programs"):
                    count.Name = "Programs";
                    count.Description = "Number of Programs";
                    count.Value = _uow.Programs.Count().ToString();
                    break;
            }
            return count;
        }

        public Dictionary<string, DashBoardItem> GetDashBoardItems()
        {
            Dictionary<string, DashBoardItem> items = new Dictionary<string, DashBoardItem>();

            items.Add("institutions",
                new DashBoardItem
                {
                    Name = "Institutions",
                    Description = "Number of Institutions",
                    Value = _uow.Institutions.Count().ToString()
                });
            items.Add("faculties",
                new DashBoardItem
                {
                    Name = "Facaulties",
                    Description = "Number of Faculties",
                    Value = _uow.Faculties.Count().ToString()
                });
            items.Add("departments",
                new DashBoardItem
                {
                    Name = "Departments",
                    Description = "Number of Departments",
                    Value = _uow.Departments.Count().ToString()
                });
            items.Add("courses",
                new DashBoardItem
                {
                    Name = "Courses",
                    Description = "Number of Courses",
                    Value = _uow.Courses.Count().ToString()
                });
            items.Add("news",
                new DashBoardItem
                {
                    Name = "News",
                    Description = "Number of News",
                    Value = _uow.News.Count().ToString()
                });
            items.Add("articles",
                new DashBoardItem
                {
                    Name = "Articles",
                    Description = "Number of Articles",
                    Value = _uow.Articles.Count().ToString()
                });
            items.Add("programs",
                new DashBoardItem
                {
                    Name = "Programs",
                    Description = "Number of Programs",
                    Value = _uow.Programs.Count().ToString()
                });
            items.Add("candidates",
                new DashBoardItem
                {
                    Name = "Candidates",
                    Description = "Number of Candidates",
                    Value = _uow.Candidates.Count().ToString()
                });

            return items;
        }
                   
    }
}
