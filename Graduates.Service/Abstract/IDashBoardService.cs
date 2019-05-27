using Graduates.ViewModel.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Service.Abstract
{
    public interface IDashBoardService
    {
        DashBoardItem GetDashBoardItem(string resource);
        Dictionary<string, DashBoardItem> GetDashBoardItems();
    }

}
