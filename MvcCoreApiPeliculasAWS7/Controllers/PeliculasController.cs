﻿using Microsoft.AspNetCore.Mvc;
using MvcCoreApiPeliculasAWS7.Models;
using MvcCoreApiPeliculasAWS7.Services;

namespace MvcCoreApiPeliculasAWS7.Controllers
{
    public class PeliculasController : Controller
    {
        private ServiceApiPeliculas service;
        private ServiceStorageAWS serviceBucket;

        public PeliculasController(ServiceApiPeliculas service,
            ServiceStorageAWS serviceBucket)
        {
            this.service = service;
            this.serviceBucket = serviceBucket;
        }

        public async Task<IActionResult> Index()
        {
            List<Pelicula> peliculas =
                await this.service.GetPeliculasAsync();
            return View(peliculas);
        }

        public IActionResult PeliculasActor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PeliculasActor(string actor)
        {
            List<Pelicula> peliculas =
                await this.service
                .GetPeliculasActoresAsync(actor);
            return View(peliculas);
        }

        public async Task<IActionResult> Details(int id)
        {
            Pelicula pelicula = await this.service.FindPelicula(id);
            return View(pelicula);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pelicula pelicula, IFormFile file)
        {
            pelicula.Foto = file.FileName;

            using (Stream stream = file.OpenReadStream())
            {

                await this.serviceBucket.UploadFileAsync(file.FileName, stream);
            }

            await this.service.CreatePeliculaAsync(pelicula);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Pelicula pelicula = await this.service.FindPelicula(id);
            return View(pelicula);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Pelicula pelicula)
        {
            await this.service.UpdatePeliculaAsync(pelicula);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePeliculaAsync(id);
            return RedirectToAction("Index");
        }
    }
}
