using Common;
using Common.ViewModels.LipstickClientViewModels;
using LipstickBusinessLogic.ILipstickClientHelpers;
using LipstickDataAccess;

namespace LipstickBusinessLogic.LipstickClientHelpers
{
    public class CategoryClientHelper : ICategoryClientHelper
    {
        private IUnitOfWork _unitOfWork;
        public CategoryClientHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CategoryClientViewModel> GetMenu(string language)
        {
            List<CategoryClientViewModel> categories = new List<CategoryClientViewModel>();
            var data = _unitOfWork.CategoryRepository.GetAll(s => s.IsActive && !s.IsDeleted, orderBy: p => p.OrderBy(s => s.Priority));
            data.ToList().ForEach(s =>
            {

                var subCategories = _unitOfWork.SubCategoryRepository.GetAll(x => x.CategoryId == s.Id && x.IsActive && !x.IsDeleted, orderBy: p => p.OrderBy(s => s.Priority));
                if (subCategories != null && subCategories.Count() > 0)
                {
                    CategoryClientViewModel category = new CategoryClientViewModel
                    {
                        Id = s.Id,
                        Name = string.Equals(language, ELanguages.EN.ToString()) ? s.NameEN : s.NameVN
                    };
                    subCategories.ToList().ForEach(x =>
                    {
                        SubCategoryClientViewModel subCategory = new SubCategoryClientViewModel
                        {
                            Id = x.Id,
                            Name = string.Equals(language, ELanguages.EN.ToString()) ? x.NameEN : x.NameVN
                        };
                        category.SubCategories.Add(subCategory);
                    });
                    categories.Add(category);
                }
            });
            return categories;
        }

        public IEnumerable<CategoryClientViewModel> GetNavigationBar(string language)
        {
            List<CategoryClientViewModel> categories = new List<CategoryClientViewModel>();
            var data = _unitOfWork.CategoryRepository.GetAll(s => s.InNavbar && s.IsActive && !s.IsDeleted, orderBy: p => p.OrderBy(s => s.Priority));
            data.ToList().ForEach(s =>
            {

                CategoryClientViewModel category = new CategoryClientViewModel
                {
                    Id = s.Id,
                    Name = string.Equals(language, ELanguages.EN.ToString()) ? s.NameEN : s.NameVN
                };
                var subCategories = _unitOfWork.SubCategoryRepository.GetAll(x => x.InNavbar && x.CategoryId == s.Id && x.IsActive && !x.IsDeleted, orderBy: p => p.OrderBy(s => s.Priority));
                subCategories.ToList().ForEach(x =>
                {
                    SubCategoryClientViewModel subCategory = new SubCategoryClientViewModel
                    {
                        Id = x.Id,
                        Name = string.Equals(language, ELanguages.EN.ToString()) ? x.NameEN : x.NameVN
                    };
                    category.SubCategories.Add(subCategory);
                });
                categories.Add(category);
            });
            return categories;
        }
    }
}
