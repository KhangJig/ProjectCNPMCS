using NYCshop.CustomTypes;
using NYCshop.Models;
using NYCshop.Resources.DataAccessMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.DataAccess
{
    public class SpamDAO
    {
        private ExLoverShopDb db = new ExLoverShopDb();

        public SpamDAO() { }

        /// <summary>
        /// Thêm 1 báo cáo vi phạm mới
        /// </summary>
        /// <param name="spam">Thông tin báo cáo vi phạm</param>
        /// <returns></returns>
        public SuccessAndMsg AddNewSpam(Spam spam)
        {
            try
            {
                db.Spams.Add(spam);
                db.SaveChanges();

                return new SuccessAndMsg(true, SpamDAOMsg.AddNewSpamSuccessful);
            }
            catch(Exception e)
            {
                string s = e.ToString();
                return new SuccessAndMsg(false, SpamDAOMsg.AddNewSpamFailed);
            }
        }
    }
}