namespace LipstickDataAccess.DTOs
{
    public class CategoryDTO : BaseDTO
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameVN { get; set; }
        public string? Note { get; set; }
        public bool InNavbar { get; set; }
        public int Priority { get; set; }
        //public ICollection<MenuItemDTO> MenuItems { get; set; }
        //public MenuGroupDTO()
        //{
        //    MenuItems = new List<MenuItemDTO>();
        //}
    }
}
