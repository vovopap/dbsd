﻿using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    [Authorize]
    public class FlowerController : BaseController
    {
        // GET: Vacancy
        public ActionResult Index(string fTitle, string fDescription)
        {
            FlowerManager manager = new FlowerManager();
            FlowerCategoryManager categoryManager = new FlowerCategoryManager();
            IList<Flower> list = manager.FilterFlowers(fTitle, fDescription);
            IList<FlowerCategory> categoris = categoryManager.GetFlowerCategories();
            foreach(Flower item in list)
            {
                item.FlowerCategory = categoris.First(d => d.Id == item.FlowerCategoryId);
            }
            ViewBag.fTitle = fTitle;
            ViewBag.fDescription = fDescription;
            return View(list);
        }


        // GET: Flower/Create
        public ActionResult Create()
        {
            FlowerCategoryManager manager = new FlowerCategoryManager();
            IList<FlowerCategory> categories = manager.GetFlowerCategories();
            FlowerWrapper wrapper = new FlowerWrapper();
            wrapper.flower = new Flower();
            wrapper.categories = categories;
            return View(wrapper);
        }

        // POST: Flower/Create
        [HttpPost]
        public ActionResult Create(Flower flower)
        {
            try
            {
                FlowerManager manager = new FlowerManager();
                manager.CreateFlower(flower);

                return RedirectToAction("Index");
            }
            catch
            {
                FlowerCategoryManager manager = new FlowerCategoryManager();
                IList<FlowerCategory> categories = manager.GetFlowerCategories();
                FlowerWrapper wrapper = new FlowerWrapper();
                wrapper.flower = new Flower();
                wrapper.categories = categories;
                return View(wrapper);
            }
        }

        // GET: Vacancy/Edit/5
        public ActionResult Edit(int id)
        {
            FlowerManager flowerManager = new FlowerManager();
            FlowerCategoryManager manager = new FlowerCategoryManager();
            IList<FlowerCategory> categories = manager.GetFlowerCategories();
            FlowerWrapper wrapper = new FlowerWrapper();
            wrapper.flower = flowerManager.GetFlowerById(id);
            wrapper.categories = categories;
            return View(wrapper);
        }

        // POST: Vacancy/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Flower flower)
        {
            try
            {
                FlowerManager manager = new FlowerManager();
                manager.UpdateFlower(id, flower);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //[HttpGet]
        //public ActionResult Report(int? vacancyId)
        //{
        //    VacancyManager manager = new VacancyManager();
        //    Recruiter recruiter = this.getRecruiter();
        //    IList<Report> list = new List<Report>();
        //    ViewBag.vacancies = manager.GetVacancies(recruiter.RecruiterId);
        //    ViewBag.vacancyId = vacancyId;
        //    if(vacancyId.HasValue)
        //    {
        //        list = manager.GenerateReport(vacancyId.Value, recruiter.RecruiterId);
        //    }

        //    return View(list);
        //}

        // GET: Vacancy/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vacancy/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}