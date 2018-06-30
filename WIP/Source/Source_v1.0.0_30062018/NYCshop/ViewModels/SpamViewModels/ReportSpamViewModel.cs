using NYCshop.Metadata.SpamMetadatas;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.SpamViewModels
{
    /// <summary>
    /// ViewModel: báo cáo vi phạm
    /// </summary>
    [MetadataType(typeof(ReportSpamViewModelMetadata))]
    public class ReportSpamViewModel
    {
        public int SpamID { get; set; }
        public string SpamContent { get; set; }
        public int ProductID { get; set; }
        private Product Product { get; set; }
    }
}