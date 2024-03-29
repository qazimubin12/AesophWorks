﻿using AesophWorks.Entities;
using AesophWorks.Services;
using AesophWorks.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AesophWorks.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        //bool isInRole = UserServices.Instance.CheckRoleForAdmin();
        public ActionResult Index(string SearchTerm)
        {
            ItemListingViewModel model = new ItemListingViewModel();
            model.Items = ItemServices.Instance.GetAllItems(SearchTerm);
            return View(model);
            
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            ItemActionViewModel model = new ItemActionViewModel();
            List<string> ItemTypes = new List<string>();
            ItemTypes.Add("Cutting Board");
            ItemTypes.Add("Chauterice Board");
            ItemTypes.Add("Serving Trays");
            ItemTypes.Add("Knives and Cutlery");
            ItemTypes.Add("CNC Engraving");
            ItemTypes.Add("Ornaments");
            ItemTypes.Add("Wedding Favor");
            ItemTypes.Add("Coaster");
            ItemTypes.Add("Adirondack Chairs");
            model.ItemTypes = ItemTypes;
            if (ID != 0)
            {
                var item = ItemServices.Instance.GetItem(ID);
                model.ID = item.ID;
                model.Name = item.Name;
                model.ItemType = item.ItemType;
              
                return PartialView("Action", model);

            }
            else
            {
                return View("Action", model);
            }
        }



        [HttpPost]
        public ActionResult Action(ItemActionViewModel model)
        {
            if (model.ID != 0) //update record
            {
                var item = ItemServices.Instance.GetItem(model.ID);

                item.ID = model.ID;
                item.Name = model.Name;
                item.ItemType = model.ItemType;
                
                ItemServices.Instance.UpdateItem(item);

            }
            else
            {
                var item = new Item();
                item.Name = model.Name;
                item.ItemType = model.ItemType;
               
                ItemServices.Instance.SaveItem(item);
            }
            return RedirectToAction("Index", "Item");
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            ItemActionViewModel model = new ItemActionViewModel();
            var item = ItemServices.Instance.GetItem(ID);
            model.ID = item.ID;
            model.Name = item.Name;

            return View("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ItemActionViewModel model)
        {

            if (model.ID != 0) //we are trying to delete a record
            {
                var item = ItemServices.Instance.GetItem(model.ID);
                ItemServices.Instance.DeleteItem(item.ID);

            }
            return RedirectToAction("Index", "Item");

        }







    }
}