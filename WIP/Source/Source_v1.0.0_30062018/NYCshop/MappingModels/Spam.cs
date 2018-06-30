using NYCshop.ViewModels.SpamViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    public partial class Spam
    {
        public Spam()
        {

        }

        public Spam(ReportSpamViewModel model, string reporter)
        {
            this.Reporter = reporter;
            this.ReportDate = DateTime.Now;
            this.Resolved = false;
            this.ProductID = model.ProductID;

            if(model != null)
            {
                this.SpamContent = model.SpamContent;
                this.Resolver = null;
                this.ResolveDate = null;
                this.ProperReport = null;
            }
        }

        public int SpamID { get; set; }
        public string Reporter { get; set; }
        public virtual User UserReporter { get; set; }
        public string SpamContent { get; set; }
        public int ProductID { get; set; }
        private Product Product { get; set; }
        public DateTime ReportDate { get; set; }
        public bool Resolved { get; set; }
        public DateTime? ResolveDate { get; set; }
        public string Resolver { get; set; }
        public virtual User UserResolver { get; set; }
        public bool? ProperReport { get; set; }
    }
}