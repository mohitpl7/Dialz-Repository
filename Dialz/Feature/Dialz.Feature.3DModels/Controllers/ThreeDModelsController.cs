using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dialz.Feature._3DModels.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dialz.Feature._3DModels.Controllers
{
    public class ThreeDModelsController : Controller
    {
        private IThreeDModelsRepository _repo;

        public ThreeDModelsController (IThreeDModelsRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}