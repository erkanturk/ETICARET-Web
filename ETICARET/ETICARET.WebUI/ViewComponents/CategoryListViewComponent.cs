using ETICARET.Business.Abstract;
using ETICARET.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.ViewComponents
{
    public class CategoryListViewComponent: ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            var model = new CategoryListViewModel()
            {
                SelectedCategory=RouteData.Values["category"]?.ToString(),
                Categories=_categoryService.GetAll()
            };
            return View(model);
        }
    }
}
