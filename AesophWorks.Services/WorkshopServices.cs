﻿using AesophWorks.Database;
using AesophWorks.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesophWorks.Services
{
    public class WorkshopServices
    {
        #region Singleton
        public static WorkshopServices Instance
        {
            get
            {
                if (instance == null) instance = new WorkshopServices();
                return instance;
            }
        }
        private static WorkshopServices instance { get; set; }
        private WorkshopServices()
        {
        }
        #endregion


        #region ListingIems
        public List<Workshop> GetAllWorkshops(string SearchTerm)
        {
            using (var context = new AWContext())
            {
                if (SearchTerm != null)
                {
                    return context.Workshops.Where(x => x.Name.Contains(SearchTerm)).ToList();
                }
                else
                {
                    return context.Workshops.ToList();
                }
            }
        }





        #endregion

        #region GetWorkshop
        public Workshop GetWorkshop(int ID)
        {

            using (var context = new AWContext())
            {

                return context.Workshops.Find(ID);
            }

        }


        public Workshop GetWorkshop(string Name)
        {

            using (var context = new AWContext())
            {

                return context.Workshops.Where(x => x.Name == Name).FirstOrDefault();
            }

        }
        #endregion

        #region UpdateWorkshop
        public void UpdateWorkshop(Workshop Workshop)
        {
            using (var context = new AWContext())
            {
                context.Entry(Workshop).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        #endregion

        #region SaveWorkshops
        public void SaveWorkshop(Workshop Workshop)
        {
            using (var context = new AWContext())
            {
                context.Workshops.Add(Workshop);
                context.SaveChanges();
            }
        }
        #endregion

        #region DeleteWorkshop
        public void DeleteWorkshop(int ID)
        {
            using (var context = new AWContext())
            {

                var cb = context.Workshops.Find(ID);
                context.Workshops.Remove(cb);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
