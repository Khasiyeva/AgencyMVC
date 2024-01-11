using Agency.Business.ViewModels;
using Agency.DAL.DAL;
using Agency.MVC.Helpers;
using Agency.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agency.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PortfolioController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            List<Portfolio> portfolios = _context.Portfolios.ToList();
            return View(portfolios);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Portfolio portfolio)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if (!portfolio.ImageFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("ImageFile", "You can only insert image");
                return View();
            }

            portfolio.ImgUrl = portfolio.ImageFile.Upload(_environment.WebRootPath, @"\Upload\Portfolio\");

            if (!portfolio.ImageFile.CheckContent("image/"))
            {
                ModelState.AddModelError("ImageFile", "Please enter the correct format");
                return View();
            }

            if (!portfolio.ImageFile.CheckLength(2097152))
            {
                ModelState.AddModelError("ImageFile", "The image maximum can be 2mb");
                return View();
            }

            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            Portfolio portfolio = _context.Portfolios.Find(id);

            UpdateVM updatePortfolio = new() 
            {
                Title=portfolio.Title,
                Description =portfolio.Description,
                ImgUrl=portfolio.ImgUrl

            };


            return View(updatePortfolio);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateVM updateVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            updateVM.ImgUrl = updateVM.ImageFile.Upload(_environment.WebRootPath, @"\Upload\Portfolio\");

            Portfolio oldPortfolio = _context.Portfolios.Find(updateVM.Id);
            oldPortfolio.Title= updateVM.Title;
            oldPortfolio.Description= updateVM.Description;
            oldPortfolio.ImgUrl = updateVM.ImgUrl;

           await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Portfolio portfolio = _context.Portfolios.Find(id);
             _context.Portfolios.Remove(portfolio);
             _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
