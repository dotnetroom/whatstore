﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using WhatStore.Domain.Infrastructure.Models.Identity;
using System.Globalization;

namespace WhatStore.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private UserManager<ApplicationUser> _userManager;
        private IStoreRepository _storeRepository;
        public ProductController(IProductRepository productRepository, UserManager<ApplicationUser> userManager, IStoreRepository storeRepository)
        {
            _productRepository = productRepository;
            _userManager = userManager;
            _storeRepository = storeRepository;
        }


        [HttpGet("register")]
        public async Task<IActionResult> RegisterProduct()
        {
            try
            {
                var viewModel = new RegisterProductViewModel();
                var category = await _productRepository.GetCategories();
                viewModel.Categories = category;

                return View(viewModel);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterProduct(RegisterProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            List<string> fileNames = new List<string> { };
            string fileName;

            if (model.Picture != null)
            {
                foreach (var file in model.Picture)
                {
                    using (var ms = file.OpenReadStream())
                    {
                        byte[] pictureBytes = new byte[ms.Length];
                        ms.Read(pictureBytes, 0, (int)ms.Length);


                        if (pictureBytes != null && pictureBytes.Length > 0)
                        {
                            fileName = $"picture_{Guid.NewGuid()}_{DateTime.UtcNow.Millisecond.ToString()}.jpg";
                            System.IO.File.WriteAllBytes($"C:\\whatstore\\{fileName}", pictureBytes);

                            fileNames.Add(
                                fileName
                            );


                        }
                    }

                }
            }


            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (await _productRepository.InsertProduct(fileNames, user.StoreId, model.ProductName, model.Description, model.Price, model.Picture,
                                                       model.HasVariety, model.Colors, model.Sizes, model.IsFreeShip, model.Length,
                                                       model.Weigth, model.Widith, model.Tags, model.Id))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                ModelState.AddModelError("Id", "Código já existente");
                model.ReturnMessage = "Erro ao salvar alterações";
                return View("RegisterProduct", model);
            }

            return RedirectToAction("RegisterProduct", model);

        }


        [HttpGet("information")]
        public async Task<IActionResult> Product(RegisterProductViewModel model)
        {
            try
            {
                List<ProductViewModel> productsList = new List<ProductViewModel>() { };



                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var products = await _productRepository.GetProducts(user.StoreId);

                foreach (var product in products)
                {
                    var image = await _productRepository.GetImage(product.Id);
                    if (image != null && image.Length > 0)
                    {
                        image = Url.Action(image, "image");
                    }
                    var viewModel = new ProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Pictures = image
                    };

                    productsList.Add(viewModel);

                }

                return View(productsList);

            }
            catch (Exception ex)
            {
                return BadRequest();

            }
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditProduct(string id)
        {
            try
            {
                List<string> productImage = new List<string> { };
                List<PictureProductViewModel> listImage = new List<PictureProductViewModel> { };

                var dataProduct = await _productRepository.GetProduct(id);

                var dataTag = await _productRepository.GetTag(dataProduct.Id);

                var dataImage = await _productRepository.GetImages(dataProduct.Id);

                if (dataImage != null && dataImage.Count > 0)
                {
                    foreach (var image in dataImage)
                    {
                        var picture = new PictureProductViewModel()
                        {
                            Id = image.Id,
                            ImageName = Url.Action(image.ImageName, "image")
                        };
                        listImage.Add(picture);
                    }
                }

                string resultDataTag = string.Join(", ", dataTag);

                var viewModel = new RegisterProductViewModel()
                {
                    Id = dataProduct.Id,
                    Description = dataProduct.Description,
                    IsFreeShip = dataProduct.IsFreeShipping,
                    Length = (dataProduct.IsFreeShipping != false) ? double.Parse(dataProduct.Length) : 0,
                    ProductName = dataProduct.Name,
                    Price = dataProduct.Price,
                    Weigth = (dataProduct.IsFreeShipping != false) ? double.Parse(dataProduct.Weigth) : 0,
                    Widith = (dataProduct.IsFreeShipping != false) ? double.Parse(dataProduct.Widith) : 0,
                    Tags = resultDataTag,
                    ImageName = listImage
                };
                return View(viewModel);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditProductSave(RegisterProductViewModel model)
        {
            List<string> fileNames = new List<string> { };
            string fileName;

            if (model.Picture != null)
            {
                foreach (var file in model.Picture)
                {
                    using (var ms = file.OpenReadStream())
                    {
                        byte[] pictureBytes = new byte[ms.Length];
                        ms.Read(pictureBytes, 0, (int)ms.Length);


                        if (pictureBytes != null && pictureBytes.Length > 0)
                        {
                            fileName = $"picture_{Guid.NewGuid()}_{DateTime.UtcNow.Millisecond.ToString()}.jpg";
                            System.IO.File.WriteAllBytes($"C:\\whatstore\\{fileName}", pictureBytes);

                            fileNames.Add(
                                fileName
                            );


                        }
                    }

                }
            }





            if (await _productRepository.UpdateProduct(model.ProductName, model.Description,
                model.Price, model.HasVariety, model.Colors, model.Sizes, model.IsFreeShip, model.Length,
                model.Weigth, model.Widith, model.Tags, model.Id, fileNames))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return RedirectToAction("Product", model);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await _productRepository.DeleteProduct(id))
            {

            }

            return RedirectToAction("Product");
        }

        [HttpPost("delete/picture")]
        public async Task<IActionResult> DeletePicture(long id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await _productRepository.DeleteImage(id))
            {
                return Ok();
            }

            return BadRequest("Não foi possível deletar a imagem.");

        }

       
    }
}
