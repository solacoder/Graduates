using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("News")]
    public class News
    {
        public long Id { set; get; }
        public string Title { set; get; }
        public string SubTitle { set; get; }
        public string BigImagePath { set; get; }
        public string ThumbnailPath { set; get; }
        public string Content { set; get; }
        public string BigImageFileType { set; get; }
        public string ThumbNailFileType { set; get; }
        public DateTime DatePublished { set; get; }
        public string CreatedBy { set; get; }
        public string ApprovedBy { set; get; }
        public string DateApproved { set; get; }
        public bool IsApproved { set; get; }
        public bool IsActive {set; get;}
        public string Status { set; get; }
        public string ActionType { set; get; }
        public bool DeleteFlag { set; get; }
        public long NewsCategoryId { set; get; }
        public NewsCategory NewsCategory { set; get; }
    }
}
