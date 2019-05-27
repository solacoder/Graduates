using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Graduates.ViewModel.ViewModels
{
    public class ArticleVM
    {
        public long Id { set; get; }
        public string Title { set; get; }

        [Display(Name="Sub Title")]
        public string SubTitle { set; get; }

        [Display(Name ="Big Image")]
        public string BigImagePath { set; get; }
        public string Content { set; get; }

        [Display(Name ="Thumbnail")]
        public string ThumbNailPath { set; get; }

        public string ThumbNailImg { set; get; }
        public string BigImageImg { set; get; }
        public string BigImageFileType { set; get; }
        public string ThumbNailFileType { set; get; }
        public DateTime DatePublished { set; get; }
        public string CreatedBy { set; get; }
        public string ApprovedBy { set; get; }
        public string DateApproved { set; get; }
        public bool IsApproved { set; get; }
        public bool IsActive { set; get; }
        public bool DeleteFlag { set; get; }
        [Display(Name ="Article Category")]
        public int ArticleCategoryId { set; get; }
        public string ArticleCategoryName { set; get; }
    }
}
